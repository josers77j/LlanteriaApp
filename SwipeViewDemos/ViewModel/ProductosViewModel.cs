using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class ProductosViewModel:BaseClass
    {
		readonly List<OrderByModel> items;

        private ObservableCollection<OrderByModel> _OList;

        public ObservableCollection<OrderByModel> OList
        {
            get { return _OList; }
            set { _OList = value; }
        }


        private OrderByModel _List;

        public OrderByModel List
        {
            get { return _List; }
            set { _List = value;
				OnPropertyChanged();

				Filtrar(List.Lista);
			}
        }

        private async void Filtrar(string lista)
        {
			var listafiltrada = await App.pDatabase.GetItemsAsync();
			switch (lista)
            {
				case string A when A == "Ordenar de A a Z":
			
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderBy(n => n.Nombre_Producto));
					break;

				case string Z when Z == "Ordenar de Z a A":
				
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderByDescending(z => z.Nombre_Producto));
					break;

				case string MY when MY == "Menor Precio":
				
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderBy(z => z.Precio));
					break;

				case string MN when MN == "Mayor Precio":
					
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderByDescending(z => z.Precio));
					break;

				case string MC when MC == "Menor Cantidad":
					
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderBy(z => z.Cantidad));
					break;

				case string MC when MC == "Mayor Cantidad":
					
					oProductos = new ObservableCollection<ProductoModel>(listafiltrada.OrderByDescending(z => z.Cantidad));
					break;
			}
        }


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
            if (List != null)
            {
				Filtrar(List.Lista);
            }
			else
            {
				cargarProductos();
            }
			IsRefreshing = false;
		}

		#endregion

		#region propiedades full/observable collection
		private ProductoModel _Productos;

		public ProductoModel Productos
		{
			get { return _Productos; }
			set
			{
				_Productos = value;
				OnPropertyChanged();
			}
		}

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

		#endregion

		#region propiedades/command
		public Command DeleteCommand { get; set; }
		public Command NuevoCommand { get; set; }
        public Command VentaCommand { get; set; }
        public Command EditCommand { get; set; }
        #endregion

        #region constructor/es
        public ProductosViewModel()
		{
			NuevoCommand = new Command(nuevo);
			DeleteCommand = new Command<ProductoModel>(async (Product) => await delete(Product));
			VentaCommand = new Command<ProductoModel>(async (Product) => await venta(Product));
			EditCommand = new Command<ProductoModel>(async (Product) => await edit(Product));

			cargarProductos();

			items = new List<OrderByModel>()
			{
				new OrderByModel { Lista = "Ordenar de A a Z"},
				new OrderByModel { Lista = "Ordenar de Z a A"},
				new OrderByModel { Lista = "Mayor Precio"},
				new OrderByModel { Lista = "Menor Precio"},
				new OrderByModel { Lista = "Mayor Cantidad"},
				new OrderByModel { Lista = "Menor Cantidad"},
			};
			llenarList();
		}

        private Task venta(ProductoModel product)
        {
            throw new NotImplementedException();
        }



        #endregion

        #region metodos

        private async Task edit(ProductoModel product)
		{
			var pagina = new ProductoEditView();
			pagina.BindingContext = new ProductoEditViewModel(product);
			await App.Current.MainPage.Navigation.PushAsync(pagina);
		}

		private void llenarList()
		{
			var listatemporal = items;
			OList = new ObservableCollection<OrderByModel>(listatemporal);
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
		#endregion


	

    }
}
