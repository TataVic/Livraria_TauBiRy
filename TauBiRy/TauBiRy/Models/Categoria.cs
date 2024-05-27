using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TauBiRy.Models
{

    [Table("Categorias")]
    public class Categoria
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Categ { get; set; }
    }
}
