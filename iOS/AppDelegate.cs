using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Lottie.Forms.iOS.Renderers;
using Pampsip.Interfaces;
using Pampsip.iOS.Services;
using Plugin.Toasts;
using UIKit;
using Xamarin.Forms;

namespace Pampsip.iOS
{
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            // Code for starting up the Xamarin Test Cloud Agent
#if DEBUG
			Xamarin.Calabash.Start();
#endif
			InitializeServices();
			ImageCircleRenderer.Init();
			AnimationViewRenderer.Init();
			DependencyService.Register<ToastNotificatorImplementation>();
			ToastNotificatorImplementation.Init();
			App.DisplayScreenWidth = (double)UIScreen.MainScreen.Bounds.Width;
            App.DisplayScreenHeight = (double)UIScreen.MainScreen.Bounds.Height;
            App.DisplayScaleFactor = (double)UIScreen.MainScreen.Scale;
            LoadApplication(new App());
			//imprimirFuentes();
            return base.FinishedLaunching(app, options);
        }

		void imprimirFuentes()
        {
            var fontList = new StringBuilder();
            var familyNames = UIFont.FamilyNames;
            foreach (var familyName in familyNames)
            {
                fontList.Append(String.Format("Family: {0}\n", familyName));
                Console.WriteLine("Family: {0}\n", familyName);
                var fontNames = UIFont.FontNamesForFamilyName(familyName);
                foreach (var fontName in fontNames)
                {
                    Console.WriteLine("\tFont: {0}\n", fontName);
                    fontList.Append(String.Format("\tFont: {0}\n", fontName));
                }
            };
        }

		private void InitializeServices()
        {
            DependencyService.Register<INavigationService, NavigationService>();
        }
    }    
}
