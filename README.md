# Livraria TauBiRy

## 📚 Cátalogo de Livro
Projeto em NET MAUI e a linguagem C# em conjunto com o banco de dados SQLite, que visa atender a proposta de criar um app que ofereça uma maneira organizada de catalogar coleções de livros, fornecendo informações detalhadas sobre cada obra.

## ✏️ Requisitos:
- Cadastro de Usuários e Proteção de acesso ao App através de um PIN gerado no cadastro.
- Criar, Listar, Editar e Excluir Livros.
- Cadastro de Categorias (exemplo: Ficção, romance, mistério, etc.).
- Cada cadastro de livro deve possuir os seguintes campos: Título, Autor, ISBN, Categoria (FK), Idioma, Editora,
Ano de Lançamento, Lido (sim, não ou lendo), Avaliação Pessoal.
- Filtro que exiba os livros que já foram lidos ou não, ou que estejam sendo lidos.
- Usar fontes e cores personalizadas.

### 🔨 Implantação 

### 🎮 Desenvolvimento em MVC (Model-View-Controller)
```
/TauBiRy
    /Models
        User.cs
        Book.cs
        AuthenticationService.cs (ainda não)
        BookService.cs (ainda não)
    /Views
        LoginPage.xaml
        LoginPage.xaml.cs
        BookListPage.xaml
        BookListPage.xaml.cs
        BookDetailPage.xaml
        BookDetailPage.xaml.cs
        BookEditPage.xaml
        BookEditPage.xaml.cs
    /Controllers
        LoginController.cs
        BookController.cs
    /Resources
        /Styles
        /Images
    /Services
        ApiService.cs  (ainda não)
    /Helpers
        Utility.cs
    App.xaml
    App.xaml.cs
    MainPage.xaml
    MainPage.xaml.cs
    Program.cs
```

### ✒️ Equipe:
- [Bianca](https://github.com/BiaCNtt)
- [Ryhan](https://github.com/ryhanclayver)
- [Tauani](https://github.com/TataVic) 


