using SQLite;
using TauBiRy.Models;
using TauBiRy.Services;

namespace TauBiRy.Views;

public partial class BookEditPage : ContentPage
{
    int livroId;
    string caminhoBD;
    SQLiteConnection conexao;
    Livro livroAtual;
    CategoriaService categoriaService;

    public BookEditPage(int livroId)
    {
        InitializeComponent();
        this.livroId = livroId;
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Livro>();
        conexao.CreateTable<Categoria>();
        categoriaService = new CategoriaService(conexao);
        LoadCategorias();

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarDetalhesLivro();
    }
    private void LoadCategorias()
    {
        List<Categoria> categorias = categoriaService.GetCategorias();
        foreach (var categoria in categorias)
        {
            Categoria.Items.Add(categoria.Categ);
        }
    } 
    private void CarregarDetalhesLivro()
    {
        livroAtual = conexao.Find<Livro>(livroId);
        if (livroAtual != null)
        {
            Titulo.Text = livroAtual.Titulo;
            Autor.Text = livroAtual.Autor;
            Isbn.Text = livroAtual.Isbn;
            Idioma.Text = livroAtual.Idioma;
            Editora.Text = livroAtual.Editora;
            Anolancamento.Date = livroAtual.Anolancamento;

            Categoria categoria = categoriaService.GetCategorias().FirstOrDefault(c => c.Id == livroAtual.CategoriaId);
            if (categoria != null)
            {
                Categoria.SelectedItem = categoria.Categ;
            }

        }
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        Livro livroAtual = new Livro();
        livroAtual.Autor = Autor.Text;
        livroAtual.Titulo = Titulo.Text;
        livroAtual.Categoria = Categoria.SelectedItem.ToString();
        livroAtual.Isbn = Isbn.Text;
        livroAtual.Idioma = Idioma.Text;
        livroAtual.Editora = Editora.Text;
        livroAtual.Anolancamento = Anolancamento.Date;
        conexao.Update(livroAtual);
        await DisplayAlert("Sucesso", "Livro atualizado com sucesso!", "OK");
        await Navigation.PopAsync();


    }
}









