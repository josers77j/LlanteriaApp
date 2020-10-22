using SwipeViewDemos.Models;
using SwipeViewDemos.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
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

		readonly List<OrderByModel> Lista;

        private ObservableCollection<OrderByModel> _OLista;

        public ObservableCollection<OrderByModel> OLista
		{
            get { return _OLista; }
            set { _OLista = value;
				OnPropertyChanged();
			}
        }


        private OrderByModel _MiLista;

        public OrderByModel MiLista
		{
            get { return _MiLista; }
            set { _MiLista = value; 
				OnPropertyChanged();
				Filtrar(MiLista.Lista);
			}
        }

		private async void Filtrar(string lista)
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
																 Time = vt.Time,
																 Total = vt.Total,

																 ID_Product = pt.ID,
																 Nombre_Producto = pt.Nombre_Producto,
																 Cantidad = pt.Cantidad,
																 Precio = pt.Precio,
																 IdCategoria = pt.IdCategoria,
																 Medida = pt.Medida,
																 BoolMedida = pt.BoolMedida
																 

															 }).ToList();

			oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin);

			switch (lista)
			{
				case string A when A == "Ventas recientes":

					oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin.OrderBy(R => R.Time));
					break;

				case string Z when Z == "Ventas Antiguas":

					oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin.OrderByDescending(z => z.Time));
					break;

				case string MY when MY == "neumaticos vendidos":
					var listafiltrada = (from n in ListaInnerJoin
										 where n.BoolMedida == true
										 select n).ToList();
					oProductVenta = new ObservableCollection<VentaProductModel>(listafiltrada.OrderBy(z => z.Medida));
					break;

				case string MN when MN == "productos vendidos":

					oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin.OrderBy(z => z.Nombre_Producto));
					break;
			}

		}
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

			Lista = new List<OrderByModel>()
			{
				new OrderByModel { Lista = "Ventas recientes"},
				new OrderByModel { Lista = "Ventas Antiguas"},
				new OrderByModel { Lista = "neumaticos vendidos"},
				new OrderByModel { Lista = "productos vendidos"}
			};
			OLista = new ObservableCollection<OrderByModel>(Lista);
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
																 Time = vt.Time,
																 Total = vt.Total,
																 

																 ID_Product = pt.ID,
																 Nombre_Producto = pt.Nombre_Producto,
																 Cantidad = pt.Cantidad,
																 Precio = pt.Precio,
																 IdCategoria = pt.IdCategoria,
																 Medida = pt.Medida,
																 BoolMedida = pt.BoolMedida
																 
																 
															 }).ToList();

			oProductVenta = new ObservableCollection<VentaProductModel>(ListaInnerJoin);
		}
		#endregion






	}
}
