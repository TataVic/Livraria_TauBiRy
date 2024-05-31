using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TauBiRy.Models;
using SQLite;

namespace TauBiRy
{
    public class CategoriaService
    {
        private readonly SQLiteConnection _database;

        public CategoriaService(SQLiteConnection database)
        {
            _database = database;
            _database.CreateTable<Categoria>();
        }

        public List<Categoria> GetCategorias()
        {
            return _database.Table<Categoria>().ToList();
        }
    }
}
