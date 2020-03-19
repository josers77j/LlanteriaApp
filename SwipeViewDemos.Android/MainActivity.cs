using Android.App;
using Android.Content.PM;
using Android.InputMethodServices;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Keycode = Android.Views.Keycode;

namespace SwipeViewDemos.Droid
{
    [Activity(Label = "Llanteria App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && e.RepeatCount == 0)
            {
                var mainPage = App.Current.MainPage;
                if (mainPage.Navigation.ModalStack.Count == 0 && mainPage.Navigation.NavigationStack.Count == 1)
                {
                    AlertDialog.Builder dialog = new AlertDialog.Builder(this);
                    AlertDialog alert = dialog.Create();
                    alert.SetTitle("Mensaje");
                    alert.SetMessage("Quieres salir de la aplicacion?");
                    alert.SetButton("NO", (c, ev) =>
                    {

                    });
                    alert.SetButton2("SI", (c, ev) =>
                    {
                        Finish();
                    });
                    alert.Show();
                    return true;
                }
            }
            return base.OnKeyDown(keyCode, e);

        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            Xamarin.Forms.Forms.SetFlags("SwipeView_Experimental");

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
    }
}