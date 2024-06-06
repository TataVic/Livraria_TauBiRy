
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

    private async void Cadastrar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Nome.Text))
        {
            var usuarioExistente = conexao.Table<Usuarios>().FirstOrDefault(u => u.Nome == Nome.Text || u.Email == Email.Text);

                if (usuarioExistente != null)
                {
                    // Se encontrar um usu�rio com o mesmo nome, exibe um alerta
                    await DisplayAlert("Erro", "Este nome de usu�rio j� est� em uso. Por favor, escolha outro.", "OK");
                }
                else
                {
                    Usuarios usuarios = new Usuarios
                    {
                        Nome = Nome.Text,
                        Email = Email.Text,
                        Telefone = Telefone.Text,
                        Senha = Senha.Text,
                    };


                    conexao.Insert(usuarios);
                    await DisplayAlert("Sucesso", "Usu�rio cadastrado com sucesso!", "OK");
                    await Navigation.PopAsync();
                }
            }
        else
        {
            await DisplayAlert("Erro", "Por favor, preencha os dados obrigat�rios.", "OK");
        }
    }

    private void Email_Unfocused(object sender, FocusEventArgs e)
    {
        string email = Email.Text; 

        if (!EmailValido(email))
        {
            DisplayAlert("Valida��o de E-mail", "E-mail inv�lido!", "OK");
            validationLabel.IsVisible = true;
        }
       
    }
    //valida a m�scara padr�o de email
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