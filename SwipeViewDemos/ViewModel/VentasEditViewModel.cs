using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using System.Text;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class VentasEditViewModel : BaseClass
    {
        //codigo de la ventana ventas edit view 
        //agregar medida 

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
                ComprobarProducto();
                OnPropertyChanged();
            }
        }

       // readonly List<CategoriaModel> Lista;

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
                if (Categoria != null)
                {
                    LlenarProducto(_Categoria.ID);
                    
                }
                else
                {
                    cargarcategorias();
                }
               
               
               
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

        //procesar texto es parte del boton de procesar
        private string _ProcesarTexto;

        public string ProcesarTexto
        {
            get { return _ProcesarTexto; }
            set { _ProcesarTexto = value;
                OnPropertyChanged();

            }
        }

        // implementar al stepper
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
        // implementar al stepper
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
               
            }
        }

       
        //implementar al stepper
        private int Cantidadl;

        public int CantidadLbL
        {
            get { return Cantidadl; }
            set { Cantidadl = value; OnPropertyChanged(); }
        }


        //implementar al stepper
        private double _Precio;

        public double Precio
        {
            get { return _Precio; }
            set { _Precio = value; OnPropertyChanged();
               
            }
        }
        //implementar al stepper
        private double _PrecioTotal;

        public double PrecioTotal
        {
            get { return _PrecioTotal; }
            set { _PrecioTotal = value; OnPropertyChanged();              
                ProcesarTexto = $"Productos agregados: {StepperValue}  - $ {Math.Round(PrecioTotal, 2)}";

            }
        }

        
        //cambiar por stepper
        private bool _UpEnable;

        public bool UpEnable
        {
            get { return _UpEnable; }
            set { _UpEnable = value; OnPropertyChanged(); }
        }

        //cambiar por stepper
        private bool _DownEnable;

        public bool DownEnable
        {
            get { return _DownEnable; }
            set { _DownEnable = value; OnPropertyChanged(); }
        }

        private DateTime _Mytime;

        public DateTime Mytime
        {
            get { return _Mytime; }
            set { _Mytime = value;                          
            }
        }

        private int _StepperValue;

        public int StepperValue
        {
            get { return _StepperValue; }
            set { _StepperValue = value;

                ProcesarTexto = $"Productos agregados: {StepperValue}  - $ {Math.Round(PrecioTotal, 2)}";                
                ComprobarStepper();
                OnPropertyChanged();
            }
        }

        private int _StepperValueMax;

        public int StepperValueMax
        {
            get { return _StepperValueMax; }
            set { _StepperValueMax = value;
                OnPropertyChanged();
                
            }
        }

        private bool _StepperEnable;

        public bool StepperEnable
        {
            get { return _StepperEnable; }
            set { _StepperEnable = value; 
                OnPropertyChanged(); }
        }




        #endregion

        #region constructor/es
        public VentasEditViewModel()
        {
            Venta = new VentaModel();
            GuardarCommand = new Command(guardar);            
            CancelarCommand = new Command(cancelar);
          
            
            cargarproductos();
            cargarcategorias();
         
            var precio = PrecioTotal;
            
            ProcesarTexto = $"Productos agregados: {StepperValue}  - $ {Math.Round(PrecioTotal, 2)}";
            StepperValueMax = 1;

            //Lista = new List<CategoriaModel>()
            //{ 
            //    new CategoriaModel { ID = 1, Nombre_Categoria = "Reparacion" }, 
            //};
        }


        #endregion

        #region metodos

        private async void ComprobarProducto()
        {
            try
            {
                StepperValue = 0;
                PrecioTotal = 0;
                CantidadTotal = 0;

                CantidadP = Productos != null ? Productos.Cantidad : 0;                
                Precio = Productos != null ? Productos.Precio : 0;
                
                CantidadTotal = CantidadP;

                if (CantidadP != 0)
                {
                    StepperEnable = true;
                    StepperValueMax = CantidadP;
                    
                }                               
                else
                {
                    StepperEnable = false;
                    StepperValueMax = 1;
                }
                ProcesarTexto = $"Productos agregados: {StepperValue}  - $ {Math.Round(PrecioTotal, 2)}";
                
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Mesnsaje", "Ocurrio un error", "ok");                                     
            }                                 
        }

        private async void LlenarProducto(int IdCategoria)
        {            
            try
            {
                var ListaTemporal = await App.pDatabase.GetItemsAsync();
                var ListaFiltrada = (from c in ListaTemporal
                                     where c.IdCategoria == IdCategoria
                                     select c).ToList();
                oProductos = null;
                oProductos = new ObservableCollection<ProductoModel>(ListaFiltrada);

            }
            catch (Exception)
            {
                throw;
            }
            
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
            var filtrarlista = (from x in listatemporal
                                where x.Nombre_Producto == "Neumatico"
                                select x).ToList();
            oProductos = new ObservableCollection<ProductoModel>(filtrarlista);
        }

        private async void guardar()
        {
            try
            {

                var action = await App.Current.MainPage.DisplayAlert("Realizar venta", "Esta seguro?", "Proceder", "Cancelar");
                if (action == true)
                {

                    if (StepperValue <= 0 || CantidadP <= 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alerta", "No selecciono una cantidad /No quedan productos /No selecciono un producto", "ok");
                    }
                    else
                    {
                        Venta.ID = 0;
                        Venta.Cantidad = StepperValue;
                        Venta.IdProducto = Productos.ID;
                        Venta.Factura = Factura;
                        Venta.Total = PrecioTotal;
                        Venta.Time = DateTime.Now;
                        await App.vDatabase.SaveItemAsync(Venta);
                        Productos.ID = Productos.ID;
                        Productos.Cantidad = CantidadP - StepperValue;
                        await App.pDatabase.SaveItemAsync(Productos);
                        var continuar = await App.Current.MainPage.DisplayAlert("Mensaje", "Venta exitosa", "Finalizar", "Nueva venta");
                        if (continuar == true)
                        {
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                        else
                        {
                            metodo();
                        }
                    }

                }
            }            
              catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alerta", "Error en la venta", "Atras");
            }
        }

        private async void metodo()
        {
            var listatemporal = await App.Database.GetItemsAsync();
          
            var listaproducto = await App.pDatabase.GetItemsAsync();
            oProductos = null;
            oCategoria = null;
            cargarproductos();
            cargarcategorias();
        }


        private void ComprobarStepper()
        {           
            PrecioTotal = StepperValue * Precio;
             int c = Productos != null ? Productos.Cantidad : 0;
            if (StepperValue != 0)
            {
                int ct = c;
                CantidadTotal = ct - StepperValue;
            }
            else
            {
                CantidadTotal = c;
            }
            
        }

        

        #endregion

    }
}
