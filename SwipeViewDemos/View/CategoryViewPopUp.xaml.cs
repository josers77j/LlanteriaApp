using SwipeViewDemos.ViewModel;
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
    public partial class CategoryViewPopUp 
    {
        public CategoryViewPopUp()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            this.BindingContext = new
            CategoriaEditViewModel(Navigation);
        }
    }
}