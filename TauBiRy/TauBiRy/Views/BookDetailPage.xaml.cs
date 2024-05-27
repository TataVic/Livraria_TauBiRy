using SQLite;

namespace TauBiRy.Views;

public partial class BookDetailPage : ContentPage
{
    int livroId;
    string caminhoBD;
    SQLiteConnection conexao;
    Livro livroAtual;

    public BookDetailPage(int livroId)
    {
        InitializeComponent();
        this.livroId = livroId;
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Livro>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CarregarDetalhesLivro();
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
            avali.Text = livroAtual.avalipessoal;

            //Categoria.Text = livroAtual.Categoria;   // precisa ter o FK ainda nao implementado
        }
        else
        {
            DisplayAlert("Erro", "Livro n�o encontrado", "OK");
        }
    }

    ///  dando erro ao trazer as infroma��es , utilizando a pagina de cadastro   

    /// precisa passar como parametro o livro atual e a a��o 2 do bookeditpage (ERROOO)
    private async void BtnEditar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new BookEditPage(livroId));
    }

    /// deu certo 
    private async void BtnExcluir_Clicked(object sender, EventArgs e)
    {
        bool confirmarExclusao = await DisplayAlert("Excluir Livro", "Tem certeza que deseja excluir este livro?", "Sim", "N�o");

        if (confirmarExclusao)
        {
            if (livroAtual != null)
            {
                conexao.Delete(livroAtual);
                await DisplayAlert("Sucesso", "Livro exclu�do com sucesso!", "OK");
            }
            else
            {
                await DisplayAlert("Erro", "O livro a ser exclu�do n�o foi encontrado.", "OK");
            }

            // Navega de volta para a p�gina principal (MainPage)
            await Navigation.PopToRootAsync();
        }
    }

    /// deu certo 
    private async void BtnSalvarAvi_Clicked(object sender, EventArgs e)
    {



        var selectedStatus = StatusPicker.SelectedItem as string;
        if (selectedStatus != null)
        {


            {
                livroAtual.Status = selectedStatus;
                conexao.Update(livroAtual);
            }
        }


        if (avali != null && livroAtual != null)
        {
            livroAtual.avalipessoal = avali.Text; // Atualiza o valor da avalia��o pessoal do livro
            conexao.Update(livroAtual);
            await DisplayAlert("Sucesso", "Livro atualizado com sucesso!", "OK");
            await Navigation.PopAsync();
        }
    }

}