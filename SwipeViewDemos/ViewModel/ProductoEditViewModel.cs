using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeViewDemos.ViewModel
{
   
    public class ProductoEditViewModel : BaseClass
    {

        

        #region propiedades full/observable collection
        private ObservableCollection<CategoriaModel> _oCategory;

        public ObservableCollection<CategoriaModel> oCategory
        {
            get { return _oCategory; }
            set
            {
                _oCategory = value;
                OnPropertyChanged();
            }
        }

        private CategoriaModel _Category;

        public CategoriaModel Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region
        private string _Precio;

        public string Precio
        {
            get { return _Precio; }
            set { _Precio = value; 
                OnPropertyChanged(); }
        }

        private string _Cantidad;

        public string Cantidad
        {
            get { return _Cantidad; }
            set { _Cantidad = value; 
                OnPropertyChanged(); }
        }

        private string _Medida;

        public string Medida
        {
            get { return _Medida; }
            set { _Medida = value;
                OnPropertyChanged();
            }
        }

        private string _Product;

        public string _Producto
        {
            get { return _Product; }
            set { _Product = value; 
                OnPropertyChanged(); }
        }

        private bool _BoolMedida;

        public bool BoolMedida
        {
            get { return _BoolMedida; }
            set { _BoolMedida = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region propiedades/command
        public Command ProductCommand { get; set; }
        public ProductoModel Producto { get; set; }
        public Command GuardarCommand { get; set; }
        #endregion

        #region constructor/es
        public ProductoEditViewModel()
        {
          
            Producto = new ProductoModel();
            Category = new CategoriaModel();
            ProductCommand = new Command(product);
            GuardarCommand = new Command(save);
            CargarCategory();
        }
        #endregion

        #region metodos
        private async void product()
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

    
        private async void CargarCategory()
        {
            var listatemporal = await App.Database.GetItemsAsync();
            oCategory = new ObservableCollection<CategoriaModel>(listatemporal);
        }

        private async void save()
        {
            try
            {
                var action = await App.Current.MainPage.DisplayAlert("Se agregara un nuevo producto", "Esta seguro?", "Guardar", "Cancelar");
                if (action == true)
                {

                    if
                ((string.IsNullOrEmpty(Producto.Precio.ToString())) ||
                (string.IsNullOrEmpty(Producto.Cantidad.ToString())) ||
                (string.IsNullOrEmpty(Producto.IdCategoria.ToString())))
                    {
                    
                        await App.Current.MainPage.DisplayAlert("Mensaje", "Todos los campos deben estar llenos", "ok");
                    }
                  
                    else
                    {

                        Producto.ID = 0;
                        Producto.IdCategoria = Category.ID;
                        var n = _Producto != null ? _Producto : Producto.Nombre_Producto = "Neumatico";
                        var m = Medida != null ? Medida : Producto.Medida = "---";
                        var b = Medida != null ? BoolMedida = true : Producto.BoolMedida = false;
                        Producto.Medida = m;
                        Producto.Nombre_Producto = n;
                        Producto.Precio = Convert.ToDouble(Precio);
                        Producto.Cantidad = Convert.ToInt32(Cantidad);
                        Producto.BoolMedida = BoolMedida;

                        await App.pDatabase.SaveItemAsync(Producto);
                        var accion = await App.Current.MainPage.DisplayAlert("Mensaje", "Desea agregar un nuevo producto", "Nuevo", "Cerrar");
                        if (accion == true)
                        {
                            var listatemporal = await App.Database.GetItemsAsync();
                            oCategory = null;
                            oCategory = new ObservableCollection<CategoriaModel>(listatemporal);
                            _Producto = null;
                            Medida = null;
                            Precio = null;
                            BoolMedida = false;
                            Cantidad = null;
                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PopAsync();
                        }
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Message", "Ocurrio un error", "ok");
                await App.Current.MainPage.Navigation.PopAsync();
            }

            }


            #endregion

        }
    }


