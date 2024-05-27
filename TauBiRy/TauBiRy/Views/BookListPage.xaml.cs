using SQLite;
using System.Collections.ObjectModel;

namespace TauBiRy.Views;

public partial class BookListPage : ContentPage
{
    string caminhoBD;
    SQLiteConnection conexao;
    ObservableCollection<Livro> livros;
    public BookListPage()
    {
        InitializeComponent();

        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Livro>();

        livros = new ObservableCollection<Livro>(conexao.Table<Livro>().ToList());
        CollectionViewControl.ItemsSource = livros;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        AtualizarListaLivros();
    }

    private void AtualizarListaLivros()
    {
        livros.Clear();
        var livrosDoBanco = conexao.Table<Livro>().ToList();
        foreach (var livro in livrosDoBanco)
        {
            livros.Add(livro);
        }
    }
    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        /*
        var label = (Label)sender;
        if ((label != null) && (label.BindingContext is Pessoa p))
        {
            TxtId.Text = p.Id.ToString();
            TxtTexto.Text = p.Nome;
        }*/
    }


    private async void Cadastrar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookCreatePage(1));
    }

    private async void Btnvisualizar(object sender, EventArgs e)
    {
        var button = sender as Button;
        var livroId = (int)button.CommandParameter;
        var livro = livros.FirstOrDefault(l => l.Id == livroId);

        if (livro != null)
        {
            await Navigation.PushAsync(new BookDetailPage(livroId));
        }
    }

}