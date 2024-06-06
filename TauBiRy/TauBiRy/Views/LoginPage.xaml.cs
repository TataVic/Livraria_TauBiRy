
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

    //método para validar o usuário conforme nome ou email, senha ou PIN
    private bool ValidaUser(string identifier, string Senha_Pin)
    {
        var user = conexao.Table<Usuarios>().FirstOrDefault(u => (u.Nome == identifier || u.Email == identifier));
        if (user != null)
        {
            // Verifica se a senha fornecida corresponde à senha armazenada ou ao PIN armazenado
            if (user.Senha == Senha_Pin || user.Pin == Senha_Pin)
            {
                return true; 
            }
        }
        return false;
    }

    private async void Logar_Clicked(object sender, EventArgs e)
    {
        Usuarios usuarios = new Usuarios();
        string identifier = User.Text;
        string password = Senha_Pin.Text; 

        bool autentificar = ValidaUser(identifier, password); //valida o usuário e seu password

        if (autentificar)
        {
            await Navigation.PushAsync(new BookListPage());
        }
        else
        {
            //Caso não tenha usuário, chama a tela de registro
            bool RegistrarUsuário = await DisplayAlert("Erro", "Usuário não encontrado. Deseja se cadastrar?", "Sim", "Não");

            if (RegistrarUsuário)
            {
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }

    //Ao confirmar no enter, executa o botão a sua ação
    private void Senha_Completed(object sender, EventArgs e)
    {
        Logar.Focus();
        Logar_Clicked(sender, e);
    }
}