
using SQLite;
using TauBiRy.Models;

namespace TauBiRy.Views;

public partial class RegisterPage : ContentPage
{
    string caminhoBD;  //caminho do banco
    SQLiteConnection conexao;
    Usuarios usuarios;

    public RegisterPage()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "usuarios.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Usuarios>();
    }

    private async void Cadastrar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Nome.Text))
        {
            Usuarios usuarios = new Usuarios
            {
                Nome = Nome.Text,
                Email = Email.Text,
                Telefone = Telefone.Text,
                Senha = Senha.Text,
            };


            conexao.Insert(usuarios);
            await DisplayAlert("Sucesso", "Usuário cadastrado com sucesso!", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Erro", "Por favor, preencha os dados obrigatórios.", "OK");
        }
    }
}