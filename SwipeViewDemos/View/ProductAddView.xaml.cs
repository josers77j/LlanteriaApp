using SwipeViewDemos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SwipeViewDemos.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductAddView : ContentPage
    {
        public ProductAddView()
        {
            InitializeComponent();
        }

        private ProductoModel _Product;

        public ProductoModel Product
        {
            get { return _Product; }
            set { _Product = value;
                OnPropertyChanged();
            }
        }

        

        
    }
}