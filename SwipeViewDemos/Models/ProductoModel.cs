using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeViewDemos.Models
{
    public class ProductoModel
    {
        private string _Nombre_Producto;
        private int _Cantidad;
        private double _Precio;
        private int _IdCategoria;
        private string _Medida;
        private bool _BoolMedida;


        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }


        public string Nombre_Producto
        {
            get { return _Nombre_Producto; }
            set { _Nombre_Producto = value; }
        }

        
        public string Medida
        {
            get { return _Medida; }
            set { _Medida = value; }
        }


        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        
        public double Precio
        {
            get { return _Precio; }
            set { _Precio = value; }
        }


        public int IdCategoria
        {
            get { return _IdCategoria; }
            set { _IdCategoria = value; }
        }

     
        public bool BoolMedida
        {
            get { return _BoolMedida; }
            set { _BoolMedida = value; }
        }


    }
}
