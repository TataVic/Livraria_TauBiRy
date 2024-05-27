
using SQLite;
using TauBiRy.Models;

namespace TauBiRy.Views;

public partial class LoginPage : ContentPage
{
    string caminhoBD;  //caminho do banco
    SQLiteConnection conexao;

    public LoginPage()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "usuarios.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Usuarios>();
    }

    private async void OnRegisterTapped(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }

    private void OnForgotPasswordTapped(object sender, TappedEventArgs e)
    {

    }

    private void Logar_Clicked(object sender, EventArgs e)
    {

    }
}