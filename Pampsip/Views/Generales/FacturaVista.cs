using System;
using System.Threading.Tasks;
using Lottie.Forms;
using Pampsip.Controls;
using Plugin.Toasts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class FacturaVista : ContentPage
    {
		public FacturaVista()
        {
            RelativeLayout CC = new RelativeLayout()
            {
                Padding = 0,                
                WidthRequest = App.DisplayScreenWidth,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.White,
            };

            CC.Children.Add(new Image
                {
                    Aspect = Aspect.Fill,
                    Source = "header",
                    HeightRequest = App.DisplayScreenHeight / 2.889679715302491
                },
                            Constraint.Constant(0),
                            Constraint.Constant(-20),
                            Constraint.Constant(App.DisplayScreenWidth),
                            Constraint.Constant(App.DisplayScreenHeight / 2.889679715302491)
            );

			CC.Children.Add(new ExtendedPdfView
                {
    				BackgroundColor = Color.Transparent,
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    VerticalOptions = LayoutOptions.FillAndExpand,
                    Uri = "eegsaPDF.pdf"
                },
                Constraint.Constant(0),
                Constraint.Constant(App.NavigationBarHeight+20),
                Constraint.Constant(App.DisplayScreenWidth),
                Constraint.Constant(App.DisplayScreenHeight-App.NavigationBarHeight)
            );

            Padding = 0;
			Content = CC;
        }      
    }
}
