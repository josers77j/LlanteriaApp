using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SwipeViewDemos.Models;
using System.Threading.Tasks;
using SwipeViewDemos.View;
using Rg.Plugins.Popup.Services;

namespace SwipeViewDemos.ViewModel
{
    public class CategoriaEditViewModel
    {
        #region propiedades/command
        
        public INavigation Navigation;
        public CategoriaModel Category { get; set; }
        public Command GuardarCommand { get; set; }
        public Command ModificarCommand { get; set; }
        public Command CancelarCommand { get; set; }
        public Command EliminarCommand { get; set; }

        public bool GuardarVisible { get; set; }
        public bool CancelarVisible { get; set; }
        #endregion

        #region constructor/es

        public CategoriaEditViewModel()
        {

        }
        public CategoriaEditViewModel(INavigation navigation)
        {

            Navigation = navigation;
            CancelarCommand = new Command(cancelar);
            Category = new CategoriaModel();
            GuardarCommand = new Command(async () => await Guardar());


        }

        #endregion

        #region metodos
        private async void cancelar()
        {
            await PopupNavigation.Instance.PopAsync();
        }

        public CategoriaEditViewModel(CategoriaModel Categoria)
        {
            Category = Categoria;
            GuardarCommand = new Command(async () => await Guardar());

        }

        public async Task Guardar()
        {
            if (string.IsNullOrEmpty(Category.Nombre_Categoria))
            {
                await App.Current.MainPage.DisplayAlert("Mensaje", "Sin datos a guardar", "ok");
                await PopupNavigation.Instance.PopAsync();
            }
            else
            {
                Category.ID = 0;
                await App.Database.SaveItemAsync(Category);
                await PopupNavigation.Instance.PopAsync();
            }

        }
        #endregion


    }

}
