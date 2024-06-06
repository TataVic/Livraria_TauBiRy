
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

    private async void RegistrarUsuario(object sender, TappedEventArgs e)
    {
        await Navigation.PushAsync(new RegisterPage());
    }
    private bool ValidaUser(string identifier, string senha)
    {
        var user = conexao.Table<Usuarios>().FirstOrDefault(u => (u.Nome == identifier || u.Email == identifier));
        return user != null && user.Senha == senha;
    }

    private async void Logar_Clicked(object sender, EventArgs e)
    {
        Usuarios usuarios = new Usuarios();
        string identifier = User.Text;
        string password = Senha.Text; 

        bool autentificar = ValidaUser(identifier, password);

        if (autentificar)
        {
            await Navigation.PushAsync(new BookListPage());
        }
        else
        {
            bool wantsToRegister = await DisplayAlert("Erro", "Usuário não encontrado. Deseja se cadastrar?", "Sim", "Não");

            if (wantsToRegister)
            {
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }

    //Ao confirmar no enter, executa o botão
    private void Senha_Completed(object sender, EventArgs e)
    {
        Logar.Focus();
        Logar_Clicked(sender, e);
    }
}