using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

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
                if 
            ((string.IsNullOrEmpty(Producto.Nombre_Producto)) ||
            (string.IsNullOrEmpty(Producto.Precio.ToString())) ||
            (string.IsNullOrEmpty(Producto.Cantidad.ToString())) ||
            (string.IsNullOrEmpty(Producto.IdCategoria.ToString())))
                {
                    await App.Current.MainPage.DisplayAlert("Mensaje", "Todos los campos deben estar llenos", "ok");
                }
                else
                {
                    Producto.ID = 0;
                    Producto.IdCategoria = Category.ID;
                    Producto.Precio = Convert.ToDouble(Precio);
                    Producto.Cantidad = Convert.ToInt32(Cantidad);
                    
                    await App.pDatabase.SaveItemAsync(Producto);
                    await App.Current.MainPage.Navigation.PopAsync();

                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Message","Ocurrio un error","ok");
                throw;
            }
        

            #endregion

        }
    }
}
