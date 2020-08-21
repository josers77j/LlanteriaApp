using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;
using SwipeViewDemos.View;

namespace SwipeViewDemos.ViewModel
{
    public class ProductAddViewModel :BaseClass
    {
		#region propiedades full/observable collection
		private ObservableCollection<CategoriaModel> _oCategory;

		public ObservableCollection<CategoriaModel> oCategory
		{
			get { return _oCategory; }
			set { _oCategory = value; }
		}


		private CategoriaModel _Category;

		public CategoriaModel Category
		{
			get { return _Category; }
			set
			{
				_Category = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<ProductoModel> _oProduct;

		public ObservableCollection<ProductoModel> oProductt
		{
			get { return _oProduct; }
			set
			{
				_oProduct = value;
				OnPropertyChanged();
			}
		}

		private ProductoModel _Product;

		public ProductoModel Product
		{
			get { return _Product; }
			set { _Product = value; }
		}
		#endregion

		#region propiedades/command
		public Command EditCommand { get; set; }

		public Command DeleteCommand { get; set; }
		#endregion

		#region constructor/es
		public ProductAddViewModel()
		{
			CargarCategory();
			Cargarproduct();
			DeleteCommand = new Command<ProductoModel>(async (Product) => await delete(Product));
			EditCommand = new Command<ProductoModel>(async (Product) => await edit(Product));

		}
		#endregion

		#region metodos
		private async Task edit(ProductoModel product)
		{
			var pagina = new ProductAddEditView();
			pagina.BindingContext = new ProductAddEditViewModel(product);
			await App.Current.MainPage.Navigation.PushAsync(pagina);
		}

		private async Task delete(ProductoModel Product)
		{
			var answer = await App.Current.MainPage.DisplayAlert("Mensaje", "Al eliminar un producto, se borrara de la lista de ventas, esta seguro?", "si", "no");
			if (answer == true)
			{
				await App.pDatabase.DeleteItemAsync(Product);
				Cargarproduct();
			}
			else
			{
				return;
			}

		}


		private async void Cargarproduct()
		{
			var listatemporal = await App.pDatabase.GetItemsAsync();
			oProductt = new ObservableCollection<ProductoModel>(listatemporal.OrderBy(product => product.Nombre_Producto));
		}

		private async void CargarCategory()
		{
			var listatemporal = await App.Database.GetItemsAsync();
			oCategory = new ObservableCollection<CategoriaModel>(listatemporal.OrderBy(category => category.Nombre_Categoria));
		}

		#endregion









	}
}