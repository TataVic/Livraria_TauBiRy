using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TauBiRy
{
    [SQLite.Table("Livros")]
    public class Livro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public required string Autor { get; set; }
        public required string Isbn { get; set; }
        public required string Categoria { get; set; } // chave estrangeria da tabela categoria
        public required string Idioma { get; set; }
        public required string Editora { get; set; }
        public required DateTime Anolancamento { get; set; }
        public string? avalipessoal { get; set; }
        public string? Lido { get; set; }
    }
}
