using System;
using System.Collections.Generic;
using SQLite;
using TauBiRy.Models;
using TauBiRy.Services;
using Microsoft.Maui.Controls;

namespace TauBiRy.Views
{
    public partial class BookCreatePage : ContentPage
    {
        int acao;
        string caminhoBD;
        SQLiteConnection conexao;
        Livro livroAtual;
        CategoriaService categoriaService;

        public BookCreatePage(int acao)
        {
            InitializeComponent();
            this.acao = acao;
            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "livro.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Livro>();
            conexao.CreateTable<Categoria>(); // Certifique-se de que a tabela Categoria seja criada aqui também
            categoriaService = new CategoriaService(conexao);
            LoadCategorias();
        }

        public BookCreatePage(int acao, Livro livro) : this(acao)
        {
            this.livroAtual = livro;
            PreencherDadosLivro();
        }

        private void LoadCategorias()
        {
            List<Categoria> categorias = categoriaService.GetCategorias();
            foreach (var categoria in categorias)
            {
                Categoria.Items.Add(categoria.Categ);
            }
        }

        private void PreencherDadosLivro()
        {
            if (livroAtual != null)
            {
                Titulo.Text = livroAtual.Titulo;
                Autor.Text = livroAtual.Autor;
                Isbn.Text = livroAtual.Isbn;
                Idioma.Text = livroAtual.Idioma;
                Editora.Text = livroAtual.Editora;
                Anolancamento.Date = livroAtual.Anolancamento;

                Categoria categoria = categoriaService.GetCategorias().FirstOrDefault(c => c.Id == livroAtual.CategoriaId);
                if (categoria != null)
                {
                    Categoria.SelectedItem = categoria.Categ;
                }
            }
        }

        private async void BtnCadastrar_Clicked(object sender, EventArgs e)
        {
            if (acao == 1)
            {
                if (!string.IsNullOrWhiteSpace(Titulo.Text))
                {
                    if (Categoria.SelectedItem != null)
                    {
                        string categoriaSelecionada = Categoria.SelectedItem.ToString();
                        Categoria categoria = categoriaService.GetCategorias().FirstOrDefault(c => c.Categ == categoriaSelecionada);

                        if (categoria != null)
                        {
                            Livro livro = new Livro
                            {
                                Autor = Autor.Text,
                                Titulo = Titulo.Text,
                                CategoriaId = categoria.Id,
                                Isbn = Isbn.Text,
                                Idioma = Idioma.Text,
                                Editora = Editora.Text,
                                Anolancamento = Anolancamento.Date,
                            };

                            conexao.Insert(livro);
                            await DisplayAlert("Sucesso", "Livro cadastrado com sucesso!", "OK");
                            await Navigation.PopAsync();
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erro", "Por favor, selecione uma categoria.", "OK");
                    }
                }
            }
        }
    }
}
