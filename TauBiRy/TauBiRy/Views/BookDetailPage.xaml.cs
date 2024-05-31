using SQLite;
using TauBiRy.Models;
using Microsoft.Maui.Controls;
using System.Linq;

namespace TauBiRy.Views
{
    public partial class BookDetailPage : ContentPage
    {
        int livroId;
        string caminhoBD;
        SQLiteConnection conexao;
        Livro livroAtual;
        CategoriaService categoriaService;

        public BookDetailPage(int livroId)
        {
            InitializeComponent();
            this.livroId = livroId;
            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Livro>();

            // Initialize CategoriaService
            categoriaService = new CategoriaService(conexao);
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

                // Carregar a categoria do livro
                Categoria categoria = categoriaService.GetCategorias().FirstOrDefault(c => c.Id == livroAtual.CategoriaId);
                if (categoria != null)
                {
                    Categoria.Text = categoria.Categ;
                }
                else
                {
                    Categoria.Text = "Categoria não encontrada";
                }
            }
            else
            {
                DisplayAlert("Erro", "Livro não encontrado", "OK");
            }
        }

        private async void BtnEditar_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BookEditPage(livroId));
        }

        private async void BtnExcluir_Clicked(object sender, EventArgs e)
        {
            bool confirmarExclusao = await DisplayAlert("Excluir Livro", "Tem certeza que deseja excluir este livro?", "Sim", "Não");

            if (confirmarExclusao)
            {
                if (livroAtual != null)
                {
                    conexao.Delete(livroAtual);
                    await DisplayAlert("Sucesso", "Livro excluído com sucesso!", "OK");
                }
                else
                {
                    await DisplayAlert("Erro", "O livro a ser excluído não foi encontrado.", "OK");
                }

                // Navega de volta para a página principal (MainPage)
                await Navigation.PopToRootAsync();
            }
        }

        private async void BtnSalvarAvi_Clicked(object sender, EventArgs e)
        {
            var selectedStatus = StatusPicker.SelectedItem as string;
            if (selectedStatus != null)
            {
                livroAtual.Status = selectedStatus;
                conexao.Update(livroAtual);
            }

            if (avali != null && livroAtual != null)
            {
                livroAtual.avalipessoal = avali.Text; // Atualiza o valor da avaliação pessoal do livro
                conexao.Update(livroAtual);
                await DisplayAlert("Sucesso", "Livro atualizado com sucesso!", "OK");
                await Navigation.PopAsync();
            }
        }
    }
}
