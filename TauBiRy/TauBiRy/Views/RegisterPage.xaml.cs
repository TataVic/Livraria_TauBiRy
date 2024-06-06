
using System.Collections.Specialized;
using System.Net.Mail;
using System.Text.RegularExpressions;
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
    //m�todo do bot�o de cadastrar usu�rio
    private async void Cadastrar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Nome.Text))
        {
            var usuarioExistente = conexao.Table<Usuarios>().FirstOrDefault(u => u.Nome == Nome.Text || u.Email == Email.Text); //valida o mesmo nome e email

            if (usuarioExistente != null)
                {
                    await DisplayAlert("Erro", "Este nome de usu�rio j� est� em uso. Por favor, escolha outro.", "OK");
                }
                else
                {
                    string GerarPin = GeradorPin(); //gera��o do pin

                    Usuarios usuarios = new Usuarios
                    {
                        Nome = Nome.Text,
                        Email = Email.Text,
                        Telefone = Telefone.Text,
                        Senha = Senha.Text,
                        Pin = GerarPin
                    };
                    conexao.Insert(usuarios);
                    await DisplayAlert("Sucesso", $"Usu�rio cadastrado com sucesso!  Seu PIN � {GerarPin}", "OK");
                    await Navigation.PopAsync();
                }
            }
        else
        {
            await DisplayAlert("Erro", "Por favor, preencha os dados obrigat�rios.", "OK");
        }
    }

    //m�todo para gerar o pin aleat�rio e com tamanho 4
    private string GeradorPin()
    {
        Random random = new Random();
        string pin = random.Next(1000, 9999).ToString();
        return pin;
    }

    //m�todo que chama a valida��o para o campo email
    private void Email_Unfocused(object sender, FocusEventArgs e)
    {
        string email = Email.Text; 

        if (!EmailValido(email))
        {
            //validaEmail.IsVisible = true; //habilitar caso queiram a mensagem
            DisplayAlert("Valida��o de E-mail", "E-mail inv�lido!", "OK");
           
        }
       
    }

    //m�todo de forma��o da m�scara padr�o de email
    private bool EmailValido(string email)
    {
        string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Express�o Regular 
        return Regex.IsMatch(email, padraoEmail);
    }

    //Ao confirmar no enter, executa o bot�o
    private void Senha_Completed(object sender, EventArgs e)
    {
        Cadastrar.Focus();
        Cadastrar_Clicked(sender,e);
    }

    // M�todo para chamar o campo de telefone
    private void Telefone_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        string FormataTelefone = FormatadorTelefone(entry.Text);

        if (FormataTelefone != entry.Text)
        {
            entry.Text = FormataTelefone;
        }
    }
   
    private string FormatadorTelefone(string input) // M�todo para aplicar a m�scara
    {
        // Remove caracteres n�o num�ricos
        var numbers = Regex.Replace(input, @"[^\d]", ""); //testar

        // Aplicar m�scara
        if (numbers.Length > 10)
        {
            numbers = numbers.Substring(0, 11); //Limita a quantidade de 11 d�gitos
            return Regex.Replace(numbers, @"(\d{2})(\d{5})(\d{4})", "($1) $2-$3");
        }
        else if (numbers.Length > 6)
        {
            return Regex.Replace(numbers, @"(\d{2})(\d{4})(\d+)", "($1) $2-$3");
        }
        else if (numbers.Length > 2)
        {
            return Regex.Replace(numbers, @"(\d{2})(\d+)", "($1) $2");
        }
        else
        {
            return numbers;
        }
    }

}