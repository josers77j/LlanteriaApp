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
    public partial class pruebaPage : ContentPage
    {
        public pruebaPage()
        {
            InitializeComponent();
            //Title = "PageTitle";


             NavigationPage.SetHasBackButton(this, true);
           

            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.OrangeRed;
            
        }
    }
}