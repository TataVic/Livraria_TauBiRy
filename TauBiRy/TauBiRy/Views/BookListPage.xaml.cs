using SQLite;
using System.Collections.ObjectModel;
using TauBiRy.Models;
using TauBiRy.Services;

namespace TauBiRy.Views
{
    public partial class BookListPage : ContentPage
    {
        string caminhoBD;
        SQLiteConnection conexao;
        ObservableCollection<Livro> livros;
        CategoriaService categoriaService;

        public BookListPage()
        {
            InitializeComponent();

            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Livro>();
            conexao.CreateTable<Categoria>();

            categoriaService = new CategoriaService(conexao);

            livros = new ObservableCollection<Livro>();
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
            var categorias = categoriaService.GetCategorias();

            foreach (var livro in livrosDoBanco)
            {
                var categoria = categorias.FirstOrDefault(c => c.Id == livro.CategoriaId);
                if (categoria != null)
                {
                    livro.Categoria = categoria.Categ;
                }
                livros.Add(livro);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            // Lógica para o gesto de toque
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

        private async void Cadastrarcategoria_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategListPage());
        }
    }
}
