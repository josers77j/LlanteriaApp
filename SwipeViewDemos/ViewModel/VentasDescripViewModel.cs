using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using SwipeViewDemos.Models;
using SwipeViewDemos.View;

namespace SwipeViewDemos.ViewModel
{
    class VentasDescripViewModel:BaseClass
    {
        #region propiedades full /Observable collection
        private ObservableCollection<VentaProductModel> _oVentas;

		public ObservableCollection<VentaProductModel> oVentas
		{
			get { return _oVentas; }
			set { _oVentas = value; OnPropertyChanged(); }
		}

		private VentaProductModel _Ventas;

		public VentaProductModel Ventas
		{
			get { return _Ventas; }
			set { _Ventas = value; OnPropertyChanged(); }
		}
		#endregion

		#region Constructor/es
		public VentasDescripViewModel()
		{
			
		}
		public VentasDescripViewModel(VentaProductModel Venta)
		{
			
			_oVentas = new ObservableCollection<VentaProductModel>();
			_oVentas.Add(Venta);
			Ventas = new VentaProductModel();
			Ventas = Venta;
		}
        #endregion
    }
}
