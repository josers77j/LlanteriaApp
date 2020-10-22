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

        private double _Total;

        public double Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        private DateTime _Time;

        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }

    }
}
