using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using SwipeViewDemos.Models;

namespace SwipeViewDemos.ViewModel
{
    public class MenuPageViewModel
    {
       
        #region propiedades/command
        public Command QuestionCommand { get; set; }
        public Command CategoriaCommand { get; set; }
        public Command ProductoCommand { get; set; }
        public Command VentaCommand { get; set; }
        public Command ListVenta { get; set; }
        public Command MakeSaleCommand { get; set; }
        public CategoriaModel Category { get; set; }

        #endregion

        #region constructor/es
        public MenuPageViewModel()
        {
            QuestionCommand = new Command(question);
            MakeSaleCommand = new Command(MSale);
            VentaCommand = new Command(venta);
            CategoriaCommand = new Command(category);
            ProductoCommand = new Command(product);
            ListVenta = new Command(list);
        }

        #endregion

        #region metodos
        private async void question()
        {
            var pagina = new QuestionView();
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        private async void MSale()
        {
            var pagina = new VentasEditView();
            pagina.BindingContext = new VentasEditViewModel();
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        private async void list()
        {
            var pagina = new VentasView();
            pagina.BindingContext = new VentasViewModel();
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        private async void venta()
        {
            var pagina = new VentasEditView();
            pagina.BindingContext = new VentasEditViewModel();
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        private async void product()
        {
            var pagina = new ProductosView();
            pagina.BindingContext = new ProductosViewModel();
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }

        private async void category()
        {
            var pagina = new CategoriaView();
            pagina.BindingContext = new CategoriaViewModel(Category);
            await App.Current.MainPage.Navigation.PushAsync(pagina);
        }
        #endregion

    }
}
