using Xamarin.Forms;
using SwipeViewDemos.Data;
using SwipeViewDemos.Services;
using SwipeViewDemos.View;
namespace SwipeViewDemos
{
    public partial class App : Application
    {
        private static VentasDataBase vdatabase;

        public static VentasDataBase vDatabase
        {
            get
            {
                if (vdatabase == null)
                {
                    vdatabase =
                         new VentasDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Vent.db3"));
                }
                return vdatabase;
            }
        }

        private static LlanteriaDataBase database;

        public static LlanteriaDataBase Database
        {
            get
            {
                if (database == null)
                {
                    database =
                         new LlanteriaDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Cat1.db3"));
                }
                return database;
            }
        }

        private static ProductosDataBase pdatabase;

        public static ProductosDataBase pDatabase
        {
            get
            {
                if (pdatabase == null)
                {
                    pdatabase =
                         new ProductosDataBase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Products.db3"));
                }
                return pdatabase;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MenuPageView());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
