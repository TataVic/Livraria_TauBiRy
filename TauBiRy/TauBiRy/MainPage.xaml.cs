using SQLite;

namespace TauBiRy
{
    public partial class MainPage : ContentPage
    {
        string caminhoBD; 
        SQLiteConnection conexao;
        public MainPage()
        {
            InitializeComponent();
            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "pessoas.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Livro>();
            
        }



    }

}
