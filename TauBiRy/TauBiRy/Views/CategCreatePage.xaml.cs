using SQLite;
using TauBiRy.Models;

namespace TauBiRy.Views;

public partial class CategCreatePage : ContentPage
{
    string caminhoBD;  // caminho do banco
    SQLiteConnection conexao;

    public CategCreatePage()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Categoria>();
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        // Código comentado para referência futura
        /*
        var label = (Label)sender;
        if ((label != null) && (label.BindingContext is Pessoa p))
        {
            TxtId.Text = p.Id.ToString();
            TxtTexto.Text = p.Nome;
        }
        */
    }

    private async void BtnsalvarCateg_Clicked(object sender, EventArgs e)
    {


        if (!string.IsNullOrEmpty(TxtNomeCateg.Text) && !string.IsNullOrEmpty(TxtDescricao.Text))
        {
            Categoria categoria = new Categoria
            {
                Categ = TxtNomeCateg.Text,
                Descricao = TxtDescricao.Text,

            };

            conexao.Insert(categoria);
            DisplayAlert("Sucesso", "Categoria salva com sucesso!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
        }


    }
}