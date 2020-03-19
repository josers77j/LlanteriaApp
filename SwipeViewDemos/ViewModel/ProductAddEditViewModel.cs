using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace SwipeViewDemos.ViewModel
{
    public class ProductAddEditViewModel:BaseClass
    {
		private ProductoModel _Product;

		public ProductoModel Product
		{
			get { return _Product; }
			set { _Product = value;
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


		public Command SaveCommand { get; set; }
		public Command CancelarCommand { get; set; }
		public ProductAddEditViewModel()
		{

		}

		public ProductAddEditViewModel(ProductoModel Producto)
		{
			CancelarCommand = new Command(cancelar);
			oProduct = new ObservableCollection<ProductoModel>();
			oProduct.Add(Producto);
			Product = new ProductoModel();
			Product = Producto;
			CantidadProduct = Product.Cantidad;
			SaveCommand = new Command(save);
		}

		private async void cancelar()
		{
			await App.Current.MainPage.Navigation.PopAsync();
		}

		private async void save()
		{
			if(CantidadEntry >= 1)
			{
				Product.ID = Product.ID;

				Product.Cantidad = CantidadEntry + Product.Cantidad;
				await App.pDatabase.SaveItemAsync(Product);
				await App.Current.MainPage.DisplayAlert("Mensaje", "Guardado", "ok");
				await App.Current.MainPage.Navigation.PopAsync();
			}
			else
			{
				await App.Current.MainPage.DisplayAlert("Mensaje","no se agrego una cantidad de productos","ok");
			}
			
		}


		private int _CantidadTotal;

		public int CantidadTotal
		{
			get { return _CantidadTotal; }
			set
			{
				_CantidadTotal = value;
				OnPropertyChanged();

			}
		}


		private int _CantidadEntry;

		public int CantidadEntry
		{
			get { return _CantidadEntry; }
			set
			{
				_CantidadEntry = value;
				OnPropertyChanged();				
				CantidadTotal = CantidadEntry + CantidadProduct;
			}
		}

		private int _CantidadProduct;

		public int CantidadProduct
		{
			get { return _CantidadProduct; }
			set
			{
				_CantidadProduct = value;
				OnPropertyChanged();
				
			}
		}




	}

}
