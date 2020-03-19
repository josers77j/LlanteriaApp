using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class ProductosViewModel:BaseClass
    {
		public Command DeleteCommand { get; set; }


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
			cargarProductos();
			IsRefreshing = false;
		}

		#endregion

		private ProductoModel _Productos;

		public ProductoModel Productos
		{
			get { return _Productos; }
			set { _Productos = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<ProductoModel> _oProductos;

		public ObservableCollection<ProductoModel> oProductos
		{
			get { return _oProductos; }
			set { _oProductos = value;
				OnPropertyChanged();
			}
		}

		public Command NuevoCommand { get; set; }

		public ProductosViewModel()
		{
			NuevoCommand = new Command(nuevo);
			DeleteCommand = new Command<ProductoModel>(async (Product) => await delete(Product));
			cargarProductos();
		}

		private async Task delete(ProductoModel Product)
		{
			var answer = await App.Current.MainPage.DisplayAlert("Mensaje", "Al eliminar un producto, se borrara de la lista de ventas, esta seguro?", "si", "no");
			if (answer == true)
			{
				await App.pDatabase.DeleteItemAsync(Product);
				cargarProductos();
			}
			else
			{
				return;
			}
		}

		private async void nuevo()
		{
			var pagina = new ProductoEditView();
			pagina.BindingContext = new ProductoEditViewModel();
			await App.Current.MainPage.Navigation.PushAsync(pagina);
		}

		private async void cargarProductos()
		{
			var listatemporal = await App.pDatabase.GetItemsAsync();
			oProductos = new ObservableCollection<ProductoModel>(listatemporal);
		}
	}
}
