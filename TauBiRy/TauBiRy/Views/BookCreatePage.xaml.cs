using SQLite;

namespace TauBiRy.Views;

public partial class BookCreatePage : ContentPage
{

    int acao;
    string caminhoBD;  // caminho do banco
    SQLiteConnection conexao;
    Livro livroAtual;

    public BookCreatePage(int acao)
    {
        InitializeComponent();
        this.acao = acao;
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Livro>();
    }

    public BookCreatePage(int acao, Livro livro) : this(acao)
    {
        this.livroAtual = livro;
        PreencherDadosLivro();
    }

    private void PreencherDadosLivro()
    {
        if (livroAtual != null)
        {
            Titulo.Text = livroAtual.Titulo;
            Autor.Text = livroAtual.Autor;
            Isbn.Text = livroAtual.Isbn;
            Idioma.Text = livroAtual.Idioma;
            Editora.Text = livroAtual.Editora;
            Anolancamento.Date = livroAtual.Anolancamento;

            // Definir a seleção do Picker com base no valor do livro
            //Categoria.SelectedItem = livroAtual.Categoria;
        }
    }

    private async void BtnCadastrar_Clicked(object sender, EventArgs e)
    {
        if (acao == 1)
        {
            if (!string.IsNullOrWhiteSpace(Titulo.Text))
            {
                Livro livro = new Livro
                {
                    Autor = Autor.Text,
                    Titulo = Titulo.Text,
                    //Categoria = Categoria.SelectedItem.ToString(),
                    Isbn = Isbn.Text,
                    Idioma = Idioma.Text,
                    Editora = Editora.Text,
                    Anolancamento = Anolancamento.Date,
                };

                conexao.Insert(livro);
                await DisplayAlert("Sucesso", "Livro cadastrado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Erro", "Por favor, preencha os dados obrigatórios.", "OK");
            }
        }

    }
}