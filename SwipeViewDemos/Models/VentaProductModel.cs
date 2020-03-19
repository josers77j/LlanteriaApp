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

     
        private bool _Factura;
        private int _IdProducto;

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

        private int _Cantidadv;

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


    }
}
