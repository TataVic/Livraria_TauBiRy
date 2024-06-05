using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using TauBiRy.Models;
using TauBiRy.Services;
using Microsoft.Maui.Controls;

namespace TauBiRy.Views
{
    public partial class BookListPage : ContentPage
    {
        string caminhoBD;
        SQLiteConnection conexao;
        ObservableCollection<Livro> livros;
        ObservableCollection<Livro> livrosFiltrados;
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
            livrosFiltrados = new ObservableCollection<Livro>();
            CollectionViewControl.ItemsSource = livrosFiltrados;
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
                Console.WriteLine($"Livro: {livro.Titulo}, Status: {livro.Status}");
            }

            // Aplicar a filtragem com base no status selecionado
            FiltrarLivros(StatusPicker.SelectedItem?.ToString() ?? "Todos");
        }

        private void FiltrarLivros(string status)
        {
            Console.WriteLine($"Filtrando livros com status: {status}");
            livrosFiltrados.Clear();

            switch (status)
            {
                case "Todos":
                    foreach (var livro in livros)
                    {
                        livrosFiltrados.Add(livro);
                    }
                    break;
                case "Lido":
                    foreach (var livro in livros.Where(l => l.Status == "Lido"))
                    {
                        livrosFiltrados.Add(livro);
                    }
                    break;
                case "Lendo":
                    foreach (var livro in livros.Where(l => l.Status == "Lendo"))
                    {
                        livrosFiltrados.Add(livro);
                    }
                    break;
                case "Não leu":
                    foreach (var livro in livros.Where(l => l.Status == "Não leu"))
                    {
                        livrosFiltrados.Add(livro);
                    }
                    break;
            
            }

            foreach (var livro in livrosFiltrados)
            {
                Console.WriteLine($"Livro Filtrado: {livro.Titulo}, Status: {livro.Status}");
            }
        }

        private void OnStatusPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedStatus = StatusPicker.SelectedItem?.ToString() ?? "Todos";
            FiltrarLivros(selectedStatus);
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
