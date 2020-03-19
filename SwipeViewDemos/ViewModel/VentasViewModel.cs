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
		public Command NuevoCommand{ get; set; }
		private VentaProductModel _ProductVenta;

		public VentaProductModel ProductVenta
		{
			get { return _ProductVenta; }
			set { _ProductVenta = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<VentaProductModel> _oProductVenta;

		public ObservableCollection<VentaProductModel> oProductVenta
		{
			get { return _oProductVenta; }
			set { _oProductVenta = value;
				OnPropertyChanged();
			}
		}


		private ObservableCollection<ProductoModel> _oProduct;

		public ObservableCollection<ProductoModel> oProduct
		{
			get { return _oProduct; }
			set { _oProduct = value;
				OnPropertyChanged();
			}
		}

		private ProductoModel _Product;

		public ProductoModel Product
		{
			get { return _Product; }
			set { _Product = value;
				OnPropertyChanged();

			}
		}


		private VentaModel _Ventas;

		public VentaModel Ventas
		{
			get { return _Ventas; }
			set { _Ventas = value; 
				OnPropertyChanged(); }
		}

		private ObservableCollection<VentaModel> _oVentas;

		public ObservableCollection<VentaModel> oVentas
		{
			get { return _oVentas; }
			set { _oVentas = value; 
				OnPropertyChanged(); }
		}

		public VentasViewModel()
		{
			NuevoCommand = new Command(nuevo);
			cargarventas();	
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
																		   
																		  ID_Product  = pt.ID,
																		   Nombre_Producto = pt.Nombre_Producto,
																		   Cantidad = pt.Cantidad,
																		   Precio = pt.Precio,
																		   IdCategoria = pt.IdCategoria,
																		   
																	   }).ToList();

			oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin);
		}
	}
}
