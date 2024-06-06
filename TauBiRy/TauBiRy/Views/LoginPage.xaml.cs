
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

    //m�todo para validar o usu�rio conforme nome ou email, senha ou PIN
    private bool ValidaUser(string identifier, string Senha_Pin)
    {
        var user = conexao.Table<Usuarios>().FirstOrDefault(u => (u.Nome == identifier || u.Email == identifier));
        if (user != null)
        {
            // Verifica se a senha fornecida corresponde � senha armazenada ou ao PIN armazenado
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

        bool autentificar = ValidaUser(identifier, password); //valida o usu�rio e seu password

        if (autentificar)
        {
            await Navigation.PushAsync(new BookListPage());
        }
        else
        {
            //Caso n�o tenha usu�rio, chama a tela de registro
            bool RegistrarUsu�rio = await DisplayAlert("Erro", "Usu�rio n�o encontrado. Deseja se cadastrar?", "Sim", "N�o");

            if (RegistrarUsu�rio)
            {
                await Navigation.PushAsync(new RegisterPage());
            }
        }
    }

    //Ao confirmar no enter, executa o bot�o a sua a��o
    private void Senha_Completed(object sender, EventArgs e)
    {
        Logar.Focus();
        Logar_Clicked(sender, e);
    }
}