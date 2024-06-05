using SQLite;
using System.Collections.ObjectModel;
using TauBiRy.Models;

namespace TauBiRy.Views
{
    public partial class CategListPage : ContentPage
    {
        string caminhoBD;
        SQLiteConnection conexao;
        ObservableCollection<Categoria> categorias;

        public CategListPage()
        {
            InitializeComponent();

            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Categoria>();

            categorias = new ObservableCollection<Categoria>(conexao.Table<Categoria>().ToList());
            CollectionViewControll.ItemsSource = categorias;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            AtualizarListaCategorias();
        }

        private void AtualizarListaCategorias()
        {
            categorias.Clear();
            var categoriasDoBanco = conexao.Table<Categoria>().ToList();
            foreach (var categoria in categoriasDoBanco)
            {
                categorias.Add(categoria);
            }
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
        private async void BtnInserir_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategCreatePage(1));
        }

        private async void Editar_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var categoria = button.CommandParameter as Categoria;
            await Navigation.PushAsync(new CategCreatePage(2, categoria));

        }

        private async void Excluir_Clicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var categoria = button?.CommandParameter as Categoria;
            conexao.Delete(categoria);
            AtualizarListaCategorias();
            await DisplayAlert("Sucesso", "Categoria excluída com sucesso!", "OK");

        }
    }
}
