using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeViewDemos.Models
{
    public class VentaModel
    {
        private int _Cantidad;
        private bool _Factura;
        private int _IdProducto;

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        
        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; }
        }

        
        public bool Factura
        {
            get { return _Factura; }
            set { _Factura = value; }
        }
        
        public int IdProducto
        {
            get { return _IdProducto; }
            set { _IdProducto = value; }
        }


    }
}
