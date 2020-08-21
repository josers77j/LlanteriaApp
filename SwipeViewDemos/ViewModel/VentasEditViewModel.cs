using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class VentasEditViewModel : BaseClass
    {
        //codigo de la ventana ventas edit view 


        #region propiedades full/observable collection
        private ObservableCollection<ProductoModel> _oProductos;

        public ObservableCollection<ProductoModel> oProductos
        {
            get { return _oProductos; }
            set
            {
                _oProductos = value;
                OnPropertyChanged();
            }
        }

        private ProductoModel _Productos;

        public ProductoModel Productos
        {
            get { return _Productos; }
            set
            {
                _Productos = value;
                OnPropertyChanged();
                CantidadP = Productos != null ? Productos.Cantidad : 0;
                
                var z = Productos != null ? Productos.Cantidad : 0; ;

                CantidadTotal = z;

                if (CantidadP == 0)
                {
                    
                }
                else
                {
                    CantidadTotal = z - 1;
                }
                
                Precio = Productos != null ? Productos.Precio : 0;
                PrecioTotal = Productos != null ? Productos.Precio : 0;
                CantidadLbL = 1;
                enableButtonD();
                enableButtonU();
                
            }
        }

        private ObservableCollection<CategoriaModel> _oCategoria;

        public ObservableCollection<CategoriaModel> oCategoria
        {
            get { return _oCategoria; }
            set
            {
                _oCategoria = value;
                OnPropertyChanged();
            }
        }

        private CategoriaModel _Categoria;

        public CategoriaModel Categoria
        {
            get { return _Categoria; }
            set
            {
                _Categoria = value;
                OnPropertyChanged();
                LlenarProducto(_Categoria.ID);
            }
        }
        #endregion

        #region propiedades/command
        public Command CancelarCommand { get; set; }
        public VentaModel Venta { get; set; }
        public Command GuardarCommand { get; set; }
        public Command UpCommand { get; set; }
        public Command DownCommand { get; set; }
 

        #endregion

        #region propiedades de muestra
        private int _CantidadP;

        public int CantidadP
        {
            get { return _CantidadP; }
            set
            {
                _CantidadP = value;
                OnPropertyChanged();

            }
        }

        private int _CantidadTotal;

        public int CantidadTotal
        {
            get { return _CantidadTotal; }
            set
            {
                _CantidadTotal = value;
                OnPropertyChanged();

            }
        }

        private bool _Factura;

        public bool Factura
        {
            get { return _Factura; }
            set
            {
                _Factura = value;
                OnPropertyChanged();
                Venta.Factura = Factura;
                lblno = true;
                if (Factura == true)
                {
                    lblsi = true;
                    lblno = false;
                }
                else
                {
                    lblsi = false;
                    lblno = true;
                }
            }
        }

        private bool _lblsi;
        public bool lblsi
        {
            get { return _lblsi; }
            set
            {
                _lblsi = value;
                OnPropertyChanged();
            }
        }

        private bool _lblno;

        public bool lblno
        {
            get { return _lblno; }
            set
            {
                _lblno = value;
                OnPropertyChanged();
            }
        }


        private int Cantidadl;

        public int CantidadLbL
        {
            get { return Cantidadl; }
            set { Cantidadl = value; OnPropertyChanged(); }
        }

        private double _Precio;

        public double Precio
        {
            get { return _Precio; }
            set { _Precio = value; OnPropertyChanged();
            }
        }

        private double _PrecioTotal;

        public double PrecioTotal
        {
            get { return _PrecioTotal; }
            set { _PrecioTotal = value; OnPropertyChanged(); }
        }

        

        private bool _UpEnable;

        public bool UpEnable
        {
            get { return _UpEnable; }
            set { _UpEnable = value; OnPropertyChanged(); }
        }


        private bool _DownEnable;

        public bool DownEnable
        {
            get { return _DownEnable; }
            set { _DownEnable = value; OnPropertyChanged(); }
        }

        #endregion

        #region constructor/es
        public VentasEditViewModel()
        {
            Venta = new VentaModel();
            GuardarCommand = new Command(guardar);
            cargarproductos();
            cargarcategorias();
            CancelarCommand = new Command(cancelar);
            UpCommand = new Command(Up);
            DownCommand = new Command(Down);
            CantidadLbL = 1;
            lblno = true;           
            enableButtonD();
            enableButtonU();
        }


        #endregion

        #region metodos

        private async void LlenarProducto(int IdCategoria)
        {
            var ListaTemporal = await App.pDatabase.GetItemsAsync();
            var ListaFiltrada = (from c in ListaTemporal
                                 where c.IdCategoria == IdCategoria
                                 select c).ToList();
            oProductos = null;
            oProductos = new ObservableCollection<ProductoModel>(ListaFiltrada);
        }


        private async void cancelar()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void cargarcategorias()
        {
            var listatemporal = await App.Database.GetItemsAsync();
            oCategoria = new ObservableCollection<CategoriaModel>(listatemporal);
        }

        private async void cargarproductos()
        {
            var listatemporal = await App.pDatabase.GetItemsAsync();
            oProductos = new ObservableCollection<ProductoModel>(listatemporal);
        }

        private async void guardar()
        {
            var action = await App.Current.MainPage.DisplayAlert("Realizar venta","Esta seguro?","Proceder","Cancelar");
            if (action == true)
            {
                try
                {

                    if (CantidadLbL <= 0 || CantidadP <= 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "No selecciono una cantidad /No quedan productos /No selecciono un producto", "ok");
                    }

                    else
                    {
                        Venta.ID = 0;
                        Venta.Cantidad = CantidadLbL;
                        Venta.IdProducto = Productos.ID;
                        Venta.Factura = Factura;
                        await App.vDatabase.SaveItemAsync(Venta);
                        Productos.ID = Productos.ID;
                        Productos.Cantidad = CantidadTotal;
                        await App.pDatabase.SaveItemAsync(Productos);
                        var continuar = await App.Current.MainPage.DisplayAlert("Mensaje", "Venta exitosa", "Finalizar", "Nueva venta");
                        if(continuar == true)
                        { 
                        await App.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                           
                        }
                    }

                }
                catch (Exception)
                {
                    await App.Current.MainPage.DisplayAlert("Alerta", "Error en la venta", "Atras");
                }
            }
            else
            {
                //nada xd
            }
            
        }

        private void Down()
        {
            int x, y;
            double z;
            x = CantidadLbL;
            y = CantidadTotal;
            z = PrecioTotal;
            if (x <= 1)
            {

            }
            else
            {
                x--;
                y++;
                z = PrecioTotal - Precio;
            }
            CantidadLbL = x;
            CantidadTotal = y;
            PrecioTotal = z;

            enableButtonD();
            enableButtonU();
        }

        private async void Up()
        {
            int x, y;
            double z;
            x = CantidadLbL;
            y = CantidadTotal;
            z = PrecioTotal;

           

            if (x >= _CantidadP)
            {
                await App.Current.MainPage.DisplayAlert("mensaje", "seleccione un producto/ limite de producto alcanzado", "ok");
            }
            else
            {
                x++;
                y--;
                z = Precio * x;
            }
            CantidadLbL = x;
            CantidadTotal = y;
            PrecioTotal = z;

            enableButtonU();
            enableButtonD();
        }
        private void enableButtonD()
        {
            if (CantidadLbL <= 1)
            {
                DownEnable = false;
            } 
            else
            {
                DownEnable = true;
            }

        }
        private void enableButtonU()
        {
            if (CantidadLbL >= _CantidadP)
            {
                UpEnable = false;
            }
            else
            {
                UpEnable = true;
            }
        }

        #endregion

    }
}
