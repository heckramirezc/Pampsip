using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Plugin.Toasts;
using Card.IO;
using Pampsip.Droid.Services;

namespace Pampsip.Droid
{
    [Activity(Label = "Pampsip.Droid", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
		    //AnimationViewRenderer.Init();
            base.OnCreate(bundle);
			App.DisplayScreenWidth = (double)Resources.DisplayMetrics.WidthPixels / (double)Resources.DisplayMetrics.Density;
            App.DisplayScreenHeight = (double)Resources.DisplayMetrics.HeightPixels / (double)Resources.DisplayMetrics.Density;
            System.Diagnostics.Debug.WriteLine("Ancho: " + App.DisplayScreenWidth);
            System.Diagnostics.Debug.WriteLine("Alto: " + App.DisplayScreenHeight);


            global::Xamarin.Forms.Forms.Init(this, bundle);
			DependencyService.Register<ToastNotificatorImplementation>();
            ToastNotificatorImplementation.Init(this);          
            LoadApplication(new App());
        }

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (data != null)
            {
                // Be sure to JavaCast to a CreditCard (normal cast won‘t work)      
                InfoShareHelper.Instance.CardInfo = data.GetParcelableExtra(CardIOActivity.ExtraScanResult).JavaCast<CreditCard>();
            }
            else
            {
				Console.WriteLine("Scanning Canceled!");
            }
        }
    }
}
