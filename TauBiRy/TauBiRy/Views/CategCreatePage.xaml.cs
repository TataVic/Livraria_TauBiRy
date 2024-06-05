using SQLite;
using TauBiRy.Models;

namespace TauBiRy.Views;

    public partial class CategCreatePage : ContentPage
    {
        string caminhoBD;
        SQLiteConnection conexao;
        int acao;
        Categoria categoriaAtual;

        public CategCreatePage(int acao, Categoria categoria = null)
        {
            InitializeComponent();
            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Categoria>();
            this.acao = acao;
            this.categoriaAtual = categoria;

            if (acao == 2 && categoria != null)
            {
                TxtNomeCateg.Text = categoria.Categ;
                TxtDescricao.Text = categoria.Descricao;
            }
        }

        private async void BtnsalvarCateg_Clicked(object sender, EventArgs e)
        {
            if (acao == 1)
            {
                if (!string.IsNullOrEmpty(TxtNomeCateg.Text) && !string.IsNullOrEmpty(TxtDescricao.Text))
                {
                    Categoria categoria = new Categoria
                    {
                        Categ = TxtNomeCateg.Text,
                        Descricao = TxtDescricao.Text,
                    };

                    conexao.Insert(categoria);
                    DisplayAlert("Sucesso", "Categoria salva com sucesso!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                }
            }

            if (acao == 2)
            {
                if (!string.IsNullOrEmpty(TxtNomeCateg.Text) && !string.IsNullOrEmpty(TxtDescricao.Text))
                {
                    categoriaAtual.Categ = TxtNomeCateg.Text;
                    categoriaAtual.Descricao = TxtDescricao.Text;

                    conexao.Update(categoriaAtual);
                    DisplayAlert("Sucesso", "Categoria atualizada com sucesso!", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    DisplayAlert("Erro", "Por favor, preencha todos os campos.", "OK");
                }
            }
        }
    }
