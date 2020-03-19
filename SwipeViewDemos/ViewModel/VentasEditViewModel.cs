using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class VentasEditViewModel:BaseClass
    {
        public Command CancelarCommand { get; set; }
        public VentaModel Venta { get; set; }
        public Command GuardarCommand { get; set; }

        private ObservableCollection<ProductoModel> _oProductos;

        public ObservableCollection<ProductoModel> oProductos
        {
            get { return _oProductos; }
            set { _oProductos = value;
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
            }
        }

        private ObservableCollection<CategoriaModel> _oCategoria;

        public ObservableCollection<CategoriaModel> oCategoria
        {
            get { return _oCategoria; }
            set { _oCategoria = value; 
                OnPropertyChanged(); 
            }
        }

        private CategoriaModel _Categoria;

        public CategoriaModel Categoria
        {
            get { return _Categoria; }
            set { _Categoria = value;
                OnPropertyChanged();
                LlenarProducto(_Categoria.ID);
            }
        }

        private async void LlenarProducto(int IdCategoria)
        {
            var ListaTemporal = await App.pDatabase.GetItemsAsync();
            var ListaFiltrada = (from c in ListaTemporal
                                 where c.IdCategoria == IdCategoria
                                 select c).ToList();
            oProductos = null;
            oProductos = new ObservableCollection<ProductoModel>(ListaFiltrada);
        }

        private int _CantidadP;

        public int CantidadP
        {
            get { return _CantidadP; }
            set { _CantidadP = value;
                OnPropertyChanged();
                
            }
        }

        private int _CantidadTotal;

        public int CantidadTotal
        {
            get { return _CantidadTotal; }
            set { _CantidadTotal = value;
                OnPropertyChanged();
        
            }
        }

        private bool _Factura;

        public bool Factura
        {
            get { return _Factura; }
            set { _Factura = value;
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

        private int _Cantidad;

        public int Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value;
                OnPropertyChanged();
                try
                {
                    if (_Cantidad >= 0 && Productos != null)
                    {
                        CantidadP = Productos.Cantidad;
                        CantidadTotal = CantidadP - Cantidad;

                        if (CantidadTotal < 0)
                        {
                            calcular();
                        }
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                
                

            }
        }
        private int _CantidadAnterior;

        public int CantidadAnterior
        {
            get { return _CantidadAnterior; }
            set { _CantidadAnterior = value; OnPropertyChanged(); }
        }


        private async void calcular()
        {
            
                         
                Cantidad = CantidadAnterior;
                await
                     App.Current.MainPage.DisplayAlert("Message","no puede superar el limite de productos en stock","ok");
                   
            
            
            
        }
        

        public VentasEditViewModel()
        {
            Venta = new VentaModel();
            GuardarCommand = new Command(guardar);
            cargarproductos();
            cargarcategorias();
            CancelarCommand = new Command(cancelar);
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
            try
            {
                
                if (Cantidad <= 0)
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje","no selecciono una cantidad / no quedan productos / no selecciono un producto","ok");
                }

                else 
                {
                    Venta.ID = 0;
                    Venta.Cantidad = Cantidad;
                    Venta.IdProducto = Productos.ID;
                    Venta.Factura = Factura;
                    await App.vDatabase.SaveItemAsync(Venta);
                    Productos.ID = Productos.ID;
                    Productos.Cantidad = CantidadTotal;
                    await App.pDatabase.SaveItemAsync(Productos);
                    await App.Current.MainPage.DisplayAlert("Mensaje", "Exito al guardar", "ok");
                    await App.Current.MainPage.Navigation.PopAsync();

                }

            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Mensaje","Error al guardar","ok");                
            }
            

        }
        private bool _lblsi;

        public bool lblsi
        {
            get { return _lblsi; }
            set { _lblsi = value;
                OnPropertyChanged();
            }
        }

        private bool _lblno;

        public bool lblno
        {
            get { return _lblno; }
            set { _lblno = value;
                OnPropertyChanged();
            }
        }

    }
}
