using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Pampsip.Controls;
using Pampsip.Helpers;
using Pampsip.Interfaces;
using Pampsip.Models.SQLite;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{	
	public class DatosPago : ContentPage
	{
		List<facturas> facturas;
		bool escaneando;
		Grid Contenido;
		Label Bienvenida;
		ExtendedEntry Nombre, Numero, Vencimiento, CVV;
		BoxView CVVBV, VencimientoBV, NumeroBV, NombreBV;
		int TOTALICIMO;
		public DatosPago(List<facturas> facturas, int TOTALICIMO)
		{
			this.TOTALICIMO = TOTALICIMO;
			NavigationPage.SetBackButtonTitle(this, "DATOS DE PAGO");
			this.facturas = facturas;
			Bienvenida = new Label
			{
				BackgroundColor = Color.Transparent,
				Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333), 0, 0, 0),
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.White,
				FontSize = (App.DisplayScreenWidth / 18.8),
				Text = "DATOS DE PAGO"
			};

			RelativeLayout CC = new RelativeLayout()
			{
				Padding = 0,
				WidthRequest = App.DisplayScreenWidth,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.Transparent,
			};

			CC.Children.Add(new Image
			{
				Aspect = Aspect.Fill,
				Source = "header",
				HeightRequest = App.DisplayScreenHeight / 2.889679715302491
			},
							Constraint.Constant(0),
							Constraint.Constant(-60),
							Constraint.Constant(App.DisplayScreenWidth),
							Constraint.Constant(App.DisplayScreenHeight / 2.889679715302491)
			);

			CC.Children.Add(Bienvenida,
							Constraint.Constant(0),
							Constraint.Constant(App.DisplayScreenHeight / 10.15),
							Constraint.Constant(App.DisplayScreenWidth)
						   );
                                                  

			Contenido = new Grid
			{
				Padding = 0,
				BackgroundColor = Color.White,
				RowSpacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
				}
			};

			Contenido.Children.Add(CC, 0, 0);
			if(Settings.session_MetodoPago.Equals("tarjeta"))			   
			{
				Contenido.Children.Add(ContenidoTarjeta(), 0, 1);
				Bienvenida.Text = "DATOS DE PAGO";
			}
			else
			{
				Contenido.Children.Add(ContenidoTransferencia(), 0, 1);
				Bienvenida.Text = "SELECCIONA TU CUENTA";
			}
				

			Padding = 0;
			Content = Contenido;
		}

		void CambiarMetodoTAP_Tapped(object sender, EventArgs e)
		{
			if (Settings.session_MetodoPago.Equals("tarjeta"))
            {
				Contenido.Children.Remove(ContenidoTransferencia());
                Contenido.Children.Add(ContenidoTarjeta(), 0, 1);
                Bienvenida.Text = "DATOS DE PAGO";
            }
            else
            {
				Contenido.Children.Remove(ContenidoTarjeta());
                Contenido.Children.Add(ContenidoTransferencia(), 0, 1);
                Bienvenida.Text = "SELECCIONA TU CUENTA";
            }

		}


		void CVV_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (((ExtendedEntry)sender).Text.Length == 4 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);
            if (!string.IsNullOrEmpty(CVV.Text))
            {
                CVVBV.BackgroundColor = Color.FromHex("4D4D4D");
                CVV.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
                if (CVV.Text.Length < 3)
                    CVV.TextColor = Color.FromHex("E9242A");
                else
                    CVV.TextColor = Color.FromHex("4D4D4D");
            }
            else
            {
                CVVBV.BackgroundColor = Color.FromHex("BFBFBF");
                CVV.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
            }
        }


		void Vencimiento_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (((ExtendedEntry)sender).Text.Length == 1 && (e.NewTextValue.Length >= e.OldTextValue.Length) && e.NewTextValue.Length == 1)
            {
                if (Convert.ToInt32(e.NewTextValue) >= 2)
                {
                    Vencimiento.Text = "0" + e.NewTextValue;
                }
            }

            if (((ExtendedEntry)sender).Text.Length == 2 && (e.NewTextValue.Length >= e.OldTextValue.Length) && e.NewTextValue.Length == 2 && !((ExtendedEntry)sender).Text.Equals("12"))
            {
                if (Convert.ToInt32(e.NewTextValue) > 12)
                {
                    ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1) + "2";
                    return;
                }
            }

            if (((ExtendedEntry)sender).Text.Length == 2 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " / ";

            if (((ExtendedEntry)sender).Text.Length == 5 && (e.OldTextValue.Length >= e.NewTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);
            if (((ExtendedEntry)sender).Text.Length == 4 && (e.OldTextValue.Length == 5))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 3);

            if (((ExtendedEntry)sender).Text.Length == 8 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);

            if (!string.IsNullOrEmpty(Vencimiento.Text))
            {
                VencimientoBV.BackgroundColor = Color.FromHex("4D4D4D");
                Vencimiento.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
                if (!IsValidExpiration(Regex.Replace(Vencimiento.Text.Trim(), @"\s+", "")))
                    Vencimiento.TextColor = Color.FromHex("E9242A");
                else
                    Vencimiento.TextColor = Color.FromHex("4D4D4D");
            }
            else
            {
                VencimientoBV.BackgroundColor = Color.FromHex("BFBFBF");
                Vencimiento.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
            }
        }


		void Numero_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (((ExtendedEntry)sender).Text.Length == 4 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " ";
            if (((ExtendedEntry)sender).Text.Length == 5 && (e.OldTextValue.Length >= e.NewTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);
            else if (((ExtendedEntry)sender).Text.Length == 4 && (e.OldTextValue.Length == 5))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);

            if (((ExtendedEntry)sender).Text.Length == 9 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " ";
            if (((ExtendedEntry)sender).Text.Length == 10 && (e.OldTextValue.Length >= e.NewTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);
            else if (((ExtendedEntry)sender).Text.Length == 9 && (e.OldTextValue.Length == 10))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);

            if (((ExtendedEntry)sender).Text.Length == 14 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text + " ";
            if (((ExtendedEntry)sender).Text.Length == 15 && (e.OldTextValue.Length >= e.NewTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);
            else if (((ExtendedEntry)sender).Text.Length == 14 && (e.OldTextValue.Length == 15))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);


            if (((ExtendedEntry)sender).Text.Length == 20 && (e.NewTextValue.Length >= e.OldTextValue.Length))
                ((ExtendedEntry)sender).Text = ((ExtendedEntry)sender).Text.Substring(0, ((ExtendedEntry)sender).Text.Length - 1);

            if (!string.IsNullOrEmpty(Numero.Text))
            {
                NumeroBV.BackgroundColor = Color.FromHex("4D4D4D");
                Numero.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
                if (!IsValidNumber(Regex.Replace(Numero.Text.Trim(), @"\s+", "")))
                    Numero.TextColor = Color.FromHex("E9242A");
                else
                    Numero.TextColor = Color.FromHex("4D4D4D");

            }
            else
            {
                NumeroBV.BackgroundColor = Color.FromHex("BFBFBF");
                Numero.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
            }
        }


		void Nombre_TextChanged(object sender, TextChangedEventArgs e)
		{
            if (!string.IsNullOrEmpty(Nombre.Text))
            {
                NombreBV.BackgroundColor = Color.FromHex("4D4D4D");
                Nombre.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
            }
            else
            {
                NombreBV.BackgroundColor = Color.FromHex("BFBFBF");
                Nombre.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
            }
        }


		private View ContenidoTransferencia()
		{
			Label CambiarMetodo = new Label
            {
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("BFBFBF"),
                FontSize = (App.DisplayScreenWidth / 26.857142857142857),
                Text = "Cambiar método de pago"
            };

            TapGestureRecognizer CambiarMetodoTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            CambiarMetodoTAP.Tapped += CambiarMetodoTAP_Tapped;
            CambiarMetodo.GestureRecognizers.Add(CambiarMetodoTAP);

			Button continuar = new Button
            {
                Margin = 0,
                Text = "CONTINUAR",
                TextColor = Color.White,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                FontSize = (App.DisplayScreenWidth / 25.066666666666667),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                HeightRequest = (App.DisplayScreenHeight / 20.3),
            };
            continuar.Clicked += Continuar_Clicked;

			Image continuarImage = new Image
            {
                Source = "loginButton",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                HeightRequest = (App.DisplayScreenHeight / 20.3),
            };
                                   
			Grid Continuar = new Grid
            {
                Padding = 0,
                Children =
                {
                    continuarImage,
                    continuar
                }
            };

			ExtendedPicker Cuentas = new ExtendedPicker
			{
				XAlign = TextAlignment.Center,
				TextColor = Color.FromHex("4D4D4D"),
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				HasBorder = false,
                Title = "¿A qué área pertenece tu consulta?",
			};

			Cuentas.Items.Add("BI - 2345 - 0 - 5315");
			Cuentas.Items.Add("BANRURAL - 93872183921 - 0 - 12");
			Cuentas.Items.Add("G&T - 63635 - 05- 4");



			return new ScrollView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
                    Padding = new Thickness((App.DisplayScreenWidth / 9.060240963855422), 0),
                    Children =
                    {
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 8.355555555555556,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        new Label
                        {
                            BackgroundColor = Color.Transparent,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                            TextColor = Color.FromHex("4D4D4D"),
                            FontSize = (App.DisplayScreenWidth / 26.857142857142857),
							Text = "Selecciona la cuenta a debitar"
                        },
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 17.090909090909091,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },                        
                        CambiarMetodo,
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 9.894736842105263,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
						new StackLayout
						{
							Spacing = App.DisplayScreenWidth/37.6,
							Children = 
							{
								new Grid
                                {
                                    Children =
                                    {
                                        Cuentas,
										new Image
										{
											Aspect = Aspect.Fill,
											HorizontalOptions = LayoutOptions.End,
											VerticalOptions = LayoutOptions.Center,
											WidthRequest = App.DisplayScreenWidth/36.049856184084372,
											HeightRequest = App.DisplayScreenWidth/25.066666666666667
										}
                                    }
                                },
								new BoxView { BackgroundColor = Color.FromHex("4D4D4D"), HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand },
							}
						},
						new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 9.894736842105263,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        new Grid
                        {
                            BackgroundColor = Color.Transparent,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Padding = new Thickness(0, (App.DisplayScreenHeight / 20.3)),
                            Children =
                            {
                                Continuar
                            }
                        }
                    }
                }
            };
		}

		private View ContenidoTarjeta()
		{
			Label CambiarMetodo = new Label
			{
				BackgroundColor = Color.Transparent,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("BFBFBF"),
				FontSize = (App.DisplayScreenWidth / 26.857142857142857),
				Text = "Cambiar método de pago"
			};

			TapGestureRecognizer CambiarMetodoTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			CambiarMetodoTAP.Tapped+= CambiarMetodoTAP_Tapped;
			CambiarMetodo.GestureRecognizers.Add(CambiarMetodoTAP);


			Nombre = new ExtendedEntry()
            {
                Margin = 0,
				Keyboard = Keyboard.Create(KeyboardFlags.All),
				XAlign = TextAlignment.Center,
				//HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Nombre de la tarjeta",
				PlaceholderColor = Color.FromHex("BFBFBF"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
				FontSize = (App.DisplayScreenWidth / 34.181818181818182)
            };
			NombreBV = new BoxView { BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand };
			Nombre.TextChanged+= Nombre_TextChanged;
			Numero = new ExtendedEntry()
            {
                Margin = 0,
                Keyboard = Keyboard.Numeric,
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Número de la tarjeta",
				PlaceholderColor = Color.FromHex("BFBFBF"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
				FontSize = (App.DisplayScreenWidth / 34.181818181818182)
            };
			NumeroBV = new BoxView { BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand };
			Numero.TextChanged+= Numero_TextChanged;
            
			Vencimiento = new ExtendedEntry()
            {
                Margin = 0,
                Keyboard = Keyboard.Numeric,
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Vencimiento",
				PlaceholderColor = Color.FromHex("BFBFBF"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
				FontSize = (App.DisplayScreenWidth / 34.181818181818182)
            };
			VencimientoBV = new BoxView { BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand };
			Vencimiento.TextChanged+= Vencimiento_TextChanged;

			CVV = new ExtendedEntry()
            {
                Margin = 0,
                Keyboard = Keyboard.Numeric,
				XAlign = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "CVV",
				PlaceholderColor = Color.FromHex("BFBFBF"),
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
				FontSize = (App.DisplayScreenWidth / 34.181818181818182)
            };
			CVVBV = new BoxView { BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest = (App.DisplayScreenWidth / 341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand };

			CVV.TextChanged+= CVV_TextChanged;


			Label Total = new Label
            {
                BackgroundColor = Color.Transparent,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 18.8),
				Text = "TOTAL: Q."+TOTALICIMO
            };

			Grid FooterTarjeta = new Grid
            {
                Padding = 0,
                BackgroundColor = Color.White,
                RowSpacing = 0,
				ColumnSpacing = App.DisplayScreenWidth/75.2,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                }
            };
			FooterTarjeta.Children.Add(Vencimiento, 0, 0);
			FooterTarjeta.Children.Add(VencimientoBV, 0, 1);
			FooterTarjeta.Children.Add(CVV, 1, 0);
			FooterTarjeta.Children.Add(CVVBV, 1, 1);

			Button continuar = new Button
            {
                Margin = 0,
                Text = "CONTINUAR",
                TextColor = Color.White,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                FontSize = (App.DisplayScreenWidth / 25.066666666666667),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                HeightRequest = (App.DisplayScreenHeight / 20.3),
            };
            continuar.Clicked += Continuar_Clicked;

			Image continuarImage = new Image
			{
				Source = "loginButton",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
				HeightRequest = (App.DisplayScreenHeight / 20.3),
			};

            Grid Continuar = new Grid
            {
                Padding = 0,
                Children =
                {
                    continuarImage,
                    continuar
                }
            };


			Grid TarjetasCredito = new Grid
			{				
				HorizontalOptions = LayoutOptions.Center,
				ColumnSpacing = App.DisplayScreenWidth/6.714285714285714,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) }
                },
                ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto)},
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto)}
                }
			};

			TarjetasCredito.Children.Add(
				new Image
				{
					VerticalOptions = LayoutOptions.Center,
					Source = "iMetodosPago",
					Aspect = Aspect.Fill,
					WidthRequest = App.DisplayScreenWidth / 2.506666666666667,
					HeightRequest = App.DisplayScreenWidth / 7.754176118787379
				}, 0, 0);
			Image escanearTarjeta = new Image
			{
				VerticalOptions = LayoutOptions.Center,
				Source = "iScanTarjeta",
				Aspect = Aspect.Fill,
				WidthRequest = App.DisplayScreenWidth / 8.355555555555556,
				HeightRequest = App.DisplayScreenWidth / 8.355555555555556
			};
			TarjetasCredito.Children.Add(escanearTarjeta, 1, 0);

			TapGestureRecognizer escanearTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			escanearTAP.Tapped+= (sender, e) => 
			{
				escaneando = true;
				DependencyService.Get<ICardService>().StartCapture();
			};

			escanearTarjeta.GestureRecognizers.Add(escanearTAP);

			return new ScrollView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				Content = new StackLayout
                {
					HorizontalOptions = LayoutOptions.FillAndExpand,
                    Spacing = 0,
					Padding = new Thickness((App.DisplayScreenWidth/9.060240963855422),0),
                    Children =
                    {
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 8.355555555555556,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        new Label
                        {
                            BackgroundColor = Color.Transparent,
                            HorizontalTextAlignment = TextAlignment.Center,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                            TextColor = Color.FromHex("4D4D4D"),
                            FontSize = (App.DisplayScreenWidth / 26.857142857142857),
                            Text = "Ahora necesitamos un poco \r\nde información\r\npara terminar el proceso de pago.\r\n:) "
                        },
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 17.090909090909091,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        TarjetasCredito,
                        new BoxView
                        {
                            HeightRequest = App.DisplayScreenWidth / 15.04,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        CambiarMetodo,
                        new BoxView
                        {
							HeightRequest = App.DisplayScreenWidth / 9.894736842105263,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        Nombre,
						NombreBV,
						new BoxView
                        {
							HeightRequest = App.DisplayScreenWidth / 25.066666666666667,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
						Numero,
						NumeroBV,
						new BoxView
                        {
							HeightRequest = App.DisplayScreenWidth / 25.066666666666667,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
                        FooterTarjeta,
						new BoxView
                        {
							HeightRequest = App.DisplayScreenWidth / 9.4,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
						Total,
						new BoxView
                        {
							HeightRequest = App.DisplayScreenWidth / 18.8,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            BackgroundColor = Color.Transparent
                        },
						new Grid
                        {
                            BackgroundColor = Color.Transparent,
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            Padding = new Thickness(0, (App.DisplayScreenHeight / 20.3)),
                            Children =
                            {
                                Continuar
                            }
                        }
                    }
                }
			};
		}


		private static bool IsValidNumber(string number)
        {
            int[] DELTAS = new int[] { 0, 1, 2, 3, 4, -4, -3, -2, -1, 0 };
            int checksum = 0;
            char[] chars = number.ToCharArray();
            for (int i = chars.Length - 1; i > -1; i--)
            {
                int j = ((int)chars[i]) - 48;
                checksum += j;
                if (((i - chars.Length) % 2) == 0)
                    checksum += DELTAS[j];
            }

            return ((checksum % 10) == 0);
        }

		public bool IsValidExpiration(string txtExpiration)
        {
			if (txtExpiration.Length < 5)
				return false;
            string[] date = Regex.Split(txtExpiration, "/");
            string[] currentDate = Regex.Split(DateTime.Now.ToString("MM/yy"), "/");
			if (Convert.ToInt32(date[1]) > (Convert.ToInt32(currentDate[1]) + 13))
				return false;
            int compareYears = string.Compare(date[1], currentDate[1]);
            int compareMonths = string.Compare(date[0], currentDate[0]);

            //if expiration date is in MM/YYYY format
			if (Regex.Match(txtExpiration, @"^\d{2}/\d{2}$").Success)
            {
                //if month is "01-12" and year starts with "20"
                if (Regex.Match(date[0], @"^[0][1-9]|[1][0-2]$").Success)
                {
                    //if expiration date is after current date
                    if ((compareYears == 1) || (compareYears == 0 && (compareMonths == 1)))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

		async void Continuar_Clicked(object sender, EventArgs e)
		{
			if(Settings.session_MetodoPago.Equals("tarjeta"))
			{
				if (String.IsNullOrEmpty(Nombre.Text))
                {
                    await DisplayAlert("", "Por favor, indique el nombre que aparece en su tarjeta", "Aceptar");
					Nombre.Focus();
                    return;
                }

				if (String.IsNullOrEmpty(Numero.Text))
                {
                    await DisplayAlert("", "Por favor, indique el número de su tarjeta", "Aceptar");
					Numero.Focus();
                    return;
                }
				else if (!IsValidNumber(Regex.Replace(Numero.Text.Trim(), @"\s+", "")))
				{
					await DisplayAlert("Número inválido", "Verifique el número de su tarjeta", "Aceptar");
                    Numero.Focus();
                    return;
				}                
				if (String.IsNullOrEmpty(Vencimiento.Text))
                {
                    await DisplayAlert("", "Por favor, indique la fecha de expiración de su tarjeta", "Aceptar");
					Vencimiento.Focus();
                    return;
                }
				else if (!IsValidExpiration(Regex.Replace(Vencimiento.Text.Trim(), @"\s+", "")))
				{
					await DisplayAlert("Fecha inválida", "Verifique la fecha de expiración de su tarjeta", "Aceptar");
                    Vencimiento.Focus();
                    return;
				}
				if (String.IsNullOrEmpty(CVV.Text))
                {
                    await DisplayAlert("", "Por favor, indique el código CVV de su tarjeta", "Aceptar");
					CVV.Focus();
                    return;
                }
				else if(CVV.Text.Trim().Length<3)
				{
					await DisplayAlert("Código CVV inválido", "Verifique el código CVV de su tarjeta", "Aceptar");
					CVV.Focus();
                    return;
				}
			}
			else
			{
				
			}
			PopupPage pagar = new Pagar();
            //pagar.bac
			await Navigation.PushPopupAsync(pagar);
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			if(escaneando)
			{
				try
				{								
        			Nombre.TextChanged -= Nombre_TextChanged;
        			Numero.TextChanged -= Numero_TextChanged;
        			Vencimiento.TextChanged -= Vencimiento_TextChanged;
        			CVV.TextChanged -= CVV_TextChanged;
        			string numero = DependencyService.Get<ICardService>().GetCardNumber();

        			numero = numero.Substring(0, 4) + " " + numero.Substring(4, 4) + " " + numero.Substring(8, 4) + " " + numero.Substring(12, 4);
        			            
        			Numero.Text = numero;
        			Nombre.Text = DependencyService.Get<ICardService>().GetCardholderName();
        			string mes = DependencyService.Get<ICardService>().GetExpiryMonth();
        			string anio = DependencyService.Get<ICardService>().GetExpiryYear();
        			if (mes.Length == 1)
        				mes = "0" + mes;
        			anio = anio.Substring(2);
        			Vencimiento.Text = mes +" / "+anio;
        			CVV.Text = DependencyService.Get<ICardService>().GetCVV();


        			Nombre.TextChanged += Nombre_TextChanged;
                    Numero.TextChanged += Numero_TextChanged;
        			Vencimiento.TextChanged += Vencimiento_TextChanged;
        			CVV.TextChanged += CVV_TextChanged;				
				}
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
				escaneando = false;
			}
		}
	}
}