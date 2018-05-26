using Pampsip.Controls;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class FacturasCarretillaDTModeloVista : ExtendedViewCell
    {
        Grid Header;
        Image Background;
		public FacturasCarretillaDTModeloVista()
        {
            Label Factura = new Label
            {                
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("BFBFBF"),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };
            Factura.SetBinding(Label.TextProperty, "alias");
			Factura.SetBinding(Label.TextColorProperty, "backgroundColor");

            Label Proveedor = new Label
            {
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("BFBFBF"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
            Proveedor.SetBinding(Label.TextProperty, "proveedor");
			Proveedor.SetBinding(Label.TextColorProperty, "backgroundColor");

            Label Saldo = new Label
            {                
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("BFBFBF"),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };
            Saldo.SetBinding(Label.TextProperty, "saldo");
			Saldo.SetBinding(Label.TextColorProperty, "backgroundColor");

            Label Vencimiento = new Label
            {                               
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("BFBFBF"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
            Vencimiento.SetBinding(Label.TextProperty, "vencimiento");
			Vencimiento.SetBinding(Label.TextColorProperty, "backgroundColor");


            Header = new Grid
            {
                Padding = 0,
				RowSpacing = App.DisplayScreenWidth/188,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                },

                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }}
            };

            Header.Children.Add(Factura, 0, 0);
            Header.Children.Add(Proveedor, 0, 1);
			Header.Children.Add(Saldo, 0, 2);
			Header.Children.Add(Vencimiento, 0, 3);

			Grid Body = new Grid
            {
                Padding = 0,
                RowSpacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star)}
                },

                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) }}
            };

			Image estado = new Image
			{
				VerticalOptions = LayoutOptions.Start,
				HorizontalOptions = LayoutOptions.Center,
				Aspect = Aspect.Fill,
				WidthRequest = App.DisplayScreenWidth / 9.4,
				HeightRequest = App.DisplayScreenWidth / 9.4
			};

			Grid Estado = new Grid
			{
				VerticalOptions = LayoutOptions.Start,
				Children = 
				{
					estado
				}
			};
			estado.SetBinding(Image.SourceProperty, "iconEstado");

			Body.Children.Add(Header, 0, 0);
			Body.Children.Add(Estado, 1,0);

            Grid Contenido = new Grid
            {				
				BackgroundColor = Color.Transparent,
				Padding = new Thickness((App.DisplayScreenWidth/9.894736842105263), (App.DisplayScreenWidth/15.04), (App.DisplayScreenWidth / 9.894736842105263), 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
				RowSpacing = App.DisplayScreenWidth/25.066666666666667,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
					new RowDefinition { Height = new GridLength((App.DisplayScreenWidth / 341.818181818181818), GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                }
            };
                        
			Contenido.Children.Add(Body, 0, 0);
			Contenido.Children.Add(
				new BoxView 
        		{ 
        			VerticalOptions = LayoutOptions.FillAndExpand, 
        			BackgroundColor = Color.FromHex("BFBFBF"), 
        			HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), 
        			Opacity = 0.75 
        		}, 0, 1);                  

			View = Contenido;
        }               
    }
}