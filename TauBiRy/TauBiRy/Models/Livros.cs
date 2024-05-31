using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using SQLite;

namespace TauBiRy
{
    [Table("Livros")]
    public class Livro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Isbn { get; set; }

        // Use o ID da categoria como chave estrangeira
        [Indexed]
        public int CategoriaId { get; set; }
        public string Categoria { get; set; }

        public string Idioma { get; set; }
        public string Editora { get; set; }
        public DateTime Anolancamento { get; set; } = DateTime.MinValue;
        public string? avalipessoal { get; set; }
        public string? Status { get; set; }

        // Construtor padrão necessário para SQLite
        public Livro() { }
    }
}
