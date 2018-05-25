using Pampsip.Controls;
using Pampsip.Models.SQLite;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class FacturasDTModeloVista : ExtendedViewCell
    {
        Grid Header;
		Image Background;
		public FacturasDTModeloVista()
        {        
			Label Factura = new Label
            {
				Margin = new Thickness((App.DisplayScreenWidth / 20.888888888888889),0,0,0),
				HorizontalTextAlignment = TextAlignment.Start,
				BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };

			Factura.SetBinding(Label.TextProperty, "alias");                                   

            Label Proveedor = new Label
            {
				
				Margin = new Thickness((App.DisplayScreenWidth / 20.888888888888889), 0, 0, 0),
				BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
            Proveedor.SetBinding(Label.TextProperty, "proveedor");   

            Label Saldo = new Label
            {
				VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Center,
				Margin = new Thickness((App.DisplayScreenWidth / 3.916666666666667), 0, 0, 0),
				HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 18.8)
            };
            Saldo.SetBinding(Label.TextProperty, "saldo");
                        

            Label Vencimiento = new Label
            {
				VerticalOptions = LayoutOptions.Center,
				VerticalTextAlignment = TextAlignment.Center,
				Margin = new Thickness((App.DisplayScreenWidth / 3.916666666666667), 0, 0, 0),
				HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
            Vencimiento.SetBinding(Label.TextProperty, "vencimiento");
            

            Header = new Grid
            {
				Padding = new Thickness(0,(App.DisplayScreenWidth / 16.347826086956522),0,0),                
                RowSpacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
                },

                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }}
            };

			Header.Children.Add(Factura, 0, 0);
            Header.Children.Add(Proveedor, 0, 1);            

			Grid Body = new Grid
            {
                Padding = new Thickness(0, (App.DisplayScreenWidth / 16.347826086956522), 0, 0),                
				RowSpacing = App.DisplayScreenWidth/75.2,
				VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },                    
                },

                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }}
            };

			Body.Children.Add(Saldo,0,0);
			Body.Children.Add(Vencimiento, 0, 1);

			Header.Children.Add(Body, 0, 2);            

            Grid Contenido = new Grid
            {								
				Padding = new Thickness(0, 0, (App.DisplayScreenWidth/9.4), 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 0,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star)},
					new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                }
            };

			Grid Footer = new Grid
			{
				Padding = new Thickness(0, 0, 0, (App.DisplayScreenWidth / 18.8)),
				Children=
				{
					new Label
                    {                        
                        //Margin = new Thickness((App.DisplayScreenWidth / 9.4), 0, 0, 0),
                        HorizontalTextAlignment = TextAlignment.Center,
                        FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                        TextColor = Color.FromHex("4D4D4D"),
                        Text = "Toca para ver el detalle da la factura",
                        FontSize = (App.DisplayScreenWidth / 37.6)
                    }
				}
			};
            Contenido.Children.Add(Header, 0, 0);
			Contenido.Children.Add(Footer, 0, 1);

			TapGestureRecognizer ContenidoTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			ContenidoTAP.Tapped+= ContenidoTAP_Tapped;
			Header.GestureRecognizers.Add(ContenidoTAP);
			Footer.GestureRecognizers.Add(ContenidoTAP);


			Background = new Image
			{				
				Aspect = Aspect.Fill
			};
			Background.SetBinding(Image.SourceProperty, "background");

            View = new Grid
            {
				Padding = new Thickness(0, (App.DisplayScreenWidth / 37.6), (App.DisplayScreenWidth / 37.6), (App.DisplayScreenWidth / 37.6)),
                Children =
                {
                    new Frame
                    {
                        Padding = 0,
                        HeightRequest = App.DisplayScreenWidth / 2.211764705882353,
                        BackgroundColor = Color.Transparent,
                        HasShadow = true,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        Content = new Grid
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            Padding = 0,
                            Children =
                            {
                                Background,
                                Contenido
                            }
                        }
                    }
                }
            };
        }

		void ContenidoTAP_Tapped(object sender, System.EventArgs e)
		{			
			MessagingCenter.Send<FacturasDTModeloVista>(this, "PDF");
		}


        void Titulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Titulo_TextChanged");
            ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.ToUpper();
        }                      
    }
}