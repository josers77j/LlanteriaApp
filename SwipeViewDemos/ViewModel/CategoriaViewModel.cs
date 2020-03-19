using Rg.Plugins.Popup.Services;
using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class CategoriaViewModel:BaseClass
    {
		#region refresh view function
		const int RefreshDuration = 2;

		bool isRefreshing;

		public bool IsRefreshing
		{
			get { return isRefreshing; }
			set
			{
				isRefreshing = value;
				OnPropertyChanged();
			}
		}

		public ICommand RefreshCommand => new Command(async () => await RefreshItemsAsync());

		private async Task RefreshItemsAsync()
		{
			IsRefreshing = true;
			await Task.Delay(TimeSpan.FromSeconds(RefreshDuration));
			CargarCategory();
			IsRefreshing = false;
		}

		#endregion
		private ObservableCollection<CategoriaModel> _oCategory;
		private INavigation navigation;

		public ObservableCollection<CategoriaModel> oCategory
		{
			get { return _oCategory; }
			set { _oCategory = value;
				OnPropertyChanged(); }
		}

		public Command DeleteCommand { get; set; }
		public Command CategoryCommand { get; set; }
		public CategoriaModel Category { get; set; }

		public Command DetalleCommand { get; set; }

		public CategoriaViewModel()
		{
			DeleteCommand = new Command<CategoriaModel>(async (Category) => await delete(Category)); ;
		}
		public CategoriaViewModel(CategoriaModel Categoria)
		{
			DeleteCommand = new Command<CategoriaModel>(async (Category) => await delete(Category)); ;
			Categoria = new CategoriaModel();
			Category = new CategoriaModel();
			CategoryCommand = new Command(Cat);
			this.Category = Categoria;
			CargarCategory();
		}

		private async Task delete(CategoriaModel category)
		{
			await App.Database.DeleteItemAsync(category);
			CargarCategory();
		}

		private async void Cat()
		{
			var pagina = new CategoryViewPopUp();
			pagina.BindingContext = new CategoriaEditViewModel();
			await PopupNavigation.Instance.PushAsync(pagina);

		}

		private async void CargarCategory()
		{
			var listatemporal = await App.Database.GetItemsAsync();
			oCategory = new ObservableCollection<CategoriaModel>(listatemporal.OrderBy(cat => cat.Nombre_Categoria));
		}
	}
}
