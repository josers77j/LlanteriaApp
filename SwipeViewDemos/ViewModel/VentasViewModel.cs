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
    public class VentasViewModel:BaseClass
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
			cargarventas();
			IsRefreshing = false;
		}

		#endregion

		#region propiedades full/observable collection
		private VentaProductModel _ProductVenta;
		public VentaProductModel ProductVenta
		{
			get { return _ProductVenta; }
			set
			{
				_ProductVenta = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<VentaProductModel> _oProductVenta;
		public ObservableCollection<VentaProductModel> oProductVenta
		{
			get { return _oProductVenta; }
			set
			{
				_oProductVenta = value;
				OnPropertyChanged();
			}
		}


		

		private ProductoModel _Product;

		public ProductoModel Product
		{
			get { return _Product; }
			set
			{
				_Product = value;
				OnPropertyChanged();

			}
		}


		private VentaProductModel _Ventas;

		public VentaProductModel Ventas
		{
			get { return _Ventas; }
			set
			{
				_Ventas = value;
				OnPropertyChanged();
				
			}
		}

		private ObservableCollection<VentaProductModel> _oVentas;

		public ObservableCollection<VentaProductModel> oVentas
		{
			get { return _oVentas; }
			set
			{
				_oVentas = value;
				OnPropertyChanged();
			}
		}

		private string _Precio;

		public string NewPrice
		{
			get { return _Precio; }
			set { _Precio = value; OnPropertyChanged();
				NewPrice = Convert.ToString(5);
			}
		}

		private string _NPrice;

		public string NPrice
		{
			get { return _NPrice; }
			set { _NPrice = value; OnPropertyChanged(); }
		}

		#endregion

		#region propiedades/command
		public Command NuevoCommand { get; set; }
		public Command DetalleCommand { get; set; }

		#endregion

		#region constructor/es
		public VentasViewModel()
		{
			NuevoCommand = new Command(nuevo);
			DetalleCommand = new Command<VentaProductModel>(async (Product) => await VentasD(Product));
			cargarventas();
			NPrice = "hola";
		}

		

		#endregion

		#region metodos

		private async Task VentasD(VentaProductModel Data)
		{
			var pagina = new VentasDescripView();
			pagina.BindingContext = new VentasDescripViewModel(Data);
			await App.Current.MainPage.Navigation.PushAsync(pagina);
		}

		private async void nuevo()
		{
			var pagina = new VentasEditView();
			pagina.BindingContext = new VentasEditViewModel();
			await App.Current.MainPage.Navigation.PushAsync(pagina);
		}

		private async void cargarventas()
		{

			var producttemporal = await App.pDatabase.GetItemsAsync();
			var ventatemporal = await App.vDatabase.GetItemsAsync();

			IEnumerable<VentaProductModel> ListaInnerJoin = (from vt in ventatemporal
															 join pt in producttemporal
															 on vt.IdProducto equals pt.ID
															 select new VentaProductModel
															 {
																 ID_Sell = vt.ID,
																 IdProducto = vt.IdProducto,
																 Cantidadv = vt.Cantidad,
																 Factura = vt.Factura,

																 ID_Product = pt.ID,
																 Nombre_Producto = pt.Nombre_Producto,
																 Cantidad = pt.Cantidad,
																 Precio = pt.Precio,
																 IdCategoria = pt.IdCategoria,

															 }).ToList();

			oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin);
		}
		#endregion






	}
}
