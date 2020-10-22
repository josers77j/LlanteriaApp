using System;
using System.Collections.Generic;
using System.Text;

namespace SwipeViewDemos.Models
{
    public class VentaProductModel
    {
        private string _Nombre_Producto;
        private int _Cantidad;
        private double _Precio;
        private int _IdCategoria;
        private string _Medida;
        private double _Total;

        private bool _Factura;
        private int _IdProducto;
        private int _Cantidadv;
        private DateTime _Time;
        private bool _BoolMedida;

        public int ID_Product { get; set; }


        public string Nombre_Producto
        {
            get { return _Nombre_Producto; }
            set { _Nombre_Producto = value; }
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

        public int ID_Sell { get; set; }

       
        public int Cantidadv
        {
            get { return _Cantidadv; }
            set { _Cantidadv = value; }
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


       
        public DateTime Time
        {
            get { return _Time; }
            set { _Time = value; }
        }


        public double Total
        {
            get { return _Total; }
            set { _Total = value; }
        }


        public string Medida
        {
            get { return _Medida; }
            set { _Medida = value; }
        }



        public bool BoolMedida
        {
            get { return _BoolMedida; }
            set { _BoolMedida = value; }
        }


    }
}
