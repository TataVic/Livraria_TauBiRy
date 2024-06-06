
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
    //método do botão de cadastrar usuário
    private async void Cadastrar_Clicked(object sender, EventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(Nome.Text))
        {
            var usuarioExistente = conexao.Table<Usuarios>().FirstOrDefault(u => u.Nome == Nome.Text || u.Email == Email.Text); //valida o mesmo nome e email

            if (usuarioExistente != null)
                {
                    await DisplayAlert("Erro", "Este nome de usuário já está em uso. Por favor, escolha outro.", "OK");
                }
                else
                {
                    string GerarPin = GeradorPin(); //geração do pin

                    Usuarios usuarios = new Usuarios
                    {
                        Nome = Nome.Text,
                        Email = Email.Text,
                        Telefone = Telefone.Text,
                        Senha = Senha.Text,
                        Pin = GerarPin
                    };
                    conexao.Insert(usuarios);
                    await DisplayAlert("Sucesso", $"Usuário cadastrado com sucesso!  Seu PIN é {GerarPin}", "OK");
                    await Navigation.PopAsync();
                }
            }
        else
        {
            await DisplayAlert("Erro", "Por favor, preencha os dados obrigatórios.", "OK");
        }
    }

    //método para gerar o pin aleatório e com tamanho 4
    private string GeradorPin()
    {
        Random random = new Random();
        string pin = random.Next(1000, 9999).ToString();
        return pin;
    }

    //método que chama a validação para o campo email
    private void Email_Unfocused(object sender, FocusEventArgs e)
    {
        string email = Email.Text; 

        if (!EmailValido(email))
        {
            //validaEmail.IsVisible = true; //habilitar caso queiram a mensagem
            DisplayAlert("Validação de E-mail", "E-mail inválido!", "OK");
           
        }
       
    }

    //método de formação da máscara padrão de email
    private bool EmailValido(string email)
    {
        string padraoEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Expressão Regular 
        return Regex.IsMatch(email, padraoEmail);
    }

    //Ao confirmar no enter, executa o botão
    private void Senha_Completed(object sender, EventArgs e)
    {
        Cadastrar.Focus();
        Cadastrar_Clicked(sender,e);
    }

    // Método para chamar o campo de telefone
    private void Telefone_TextChanged(object sender, TextChangedEventArgs e)
    {
        var entry = sender as Entry;
        string FormataTelefone = FormatadorTelefone(entry.Text);

        if (FormataTelefone != entry.Text)
        {
            entry.Text = FormataTelefone;
        }
    }
   
    private string FormatadorTelefone(string input) // Método para aplicar a máscara
    {
        // Remove caracteres não numéricos
        var numbers = Regex.Replace(input, @"[^\d]", ""); //testar

        // Aplicar máscara
        if (numbers.Length > 10)
        {
            numbers = numbers.Substring(0, 11); //Limita a quantidade de 11 dígitos
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