using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace SwipeViewDemos.Models
{
    public class CategoriaModel:BaseClass
    {
        private string _Nombre_Categoria;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }        
        public string Nombre_Categoria
        {
            get { return _Nombre_Categoria; }
            set { _Nombre_Categoria = value; OnPropertyChanged(); }
        }

    }
}
