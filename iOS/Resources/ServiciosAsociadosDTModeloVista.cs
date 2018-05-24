using Pampsip.Controls;
using Pampsip.Models.SQLite;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class ServiciosAsociadosDTModeloVista : ExtendedViewCell
    {
        ExtendedEntry Titulo;
		Grid Header, Footer;
		public ServiciosAsociadosDTModeloVista()
        {
            Titulo = new ExtendedEntry()
            {                               
                Keyboard = Keyboard.Text,
                //IsAutocapitalize = true,
                Placeholder = "Alias de servicio",
                PlaceholderColor = Color.FromHex("D9D9D9"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HasBorder = false,
				FontSize = App.DisplayScreenWidth / 25.066666666666667
            };

            //Titulo.SetBinding(ExtendedEntry.TextColorProperty, "Color");
			Titulo.SetBinding(ExtendedEntry.TextProperty, "alias");

            Titulo.Focused += (sender, e) =>
            {
                Titulo.TextChanged += Titulo_TextChanged;
            };
            Titulo.Unfocused += (sender, e) =>
            {
                Titulo.TextChanged -= Titulo_TextChanged;
				servicios ContextoActual = (servicios)this.BindingContext;
                /*if (ContextoActual != null && !string.IsNullOrEmpty(ContextoActual.name))
                    App.Database.InsertLista(ContextoActual);*/
            };

			Label Proveedor = new Label
			{
				BackgroundColor = Color.Transparent,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 37.6)
			};
			Proveedor.SetBinding(Label.TextProperty, "proveedor");

			Label Categoria = new Label
			{
				BackgroundColor = Color.Transparent,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 37.6)
			};
			Categoria.SetBinding(Label.TextProperty, "categoria");

			Label Saldo = new Label
            {
				HorizontalTextAlignment = TextAlignment.End,
                BackgroundColor = Color.Transparent,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 31.333333333333333)
            };
			Saldo.SetBinding(Label.TextProperty, "saldo");

            Label Aviso = new Label
            {
				HorizontalTextAlignment = TextAlignment.End,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
			Aviso.SetBinding(Label.TextProperty, "aviso");

			Label Vencimiento = new Label
            {
				HorizontalTextAlignment = TextAlignment.End,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 37.6)
            };
			Vencimiento.SetBinding(Label.TextProperty, "vencimiento");
                     
			Header = new Grid
            {
				Padding = new Thickness((App.DisplayScreenWidth / 10.742857142857143), (App.DisplayScreenWidth / 16.347826086956522), (App.DisplayScreenWidth / 10.742857142857143), 0),
                ColumnSpacing = App.DisplayScreenWidth / 32,
                RowSpacing = 0,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                },

                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) }}
            };

			Header.Children.Add(Titulo, 0, 0);
			Header.Children.Add(Proveedor, 0, 1);
			Header.Children.Add(Categoria, 0, 2);            


			Footer = new Grid
            {
                Padding = new Thickness((App.DisplayScreenWidth / 10.742857142857143), (App.DisplayScreenWidth / 16.347826086956522), (App.DisplayScreenWidth / 10.742857142857143), 0),                
                RowSpacing = 0,
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                },

                ColumnDefinitions = {					
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }}
            };


			Footer.Children.Add(Saldo, 0, 0);
			Footer.Children.Add(Aviso, 0, 1);
			Footer.Children.Add(Vencimiento, 0, 2);            
   
            Grid Contenido = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding = 0,
                //Padding = App.DisplayScreenWidth/21.333333333333333,
                RowSpacing = App.DisplayScreenWidth / 26.666666666666667,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto)},
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                }
            };

            Contenido.Children.Add(Header, 0, 0);
			Contenido.Children.Add(Footer, 0, 0);


   

            View = new Grid
            {
				Padding = new Thickness((App.DisplayScreenWidth / 12.533333333333333), (App.DisplayScreenWidth / 37.6), 0, (App.DisplayScreenWidth / 37.6)),
                Children =
                {
                    new Frame
                    {
                        Padding = 0,
						HeightRequest = App.DisplayScreenWidth/2.506666666666667,
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
								new Image
                                {
                                    Source = "iServicioBackground",
                                    Aspect = Aspect.Fill
                                },
                                Contenido
                            }
                        }
                    }
                }
            };
        }

        void Titulo_TextChanged(object sender, TextChangedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Titulo_TextChanged");
            ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.ToUpper();
        }  
    }
}