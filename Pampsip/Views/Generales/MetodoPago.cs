using System;
using System.Collections.Generic;
using Pampsip.Helpers;
using Pampsip.Models.SQLite;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class MetodoPago : ContentPage
	{		
		List<facturas> facturas;
		public MetodoPago(List<facturas> facturas)
		{
			NavigationPage.SetBackButtonTitle(this, "MÉTODO DE PAGO");
			this.facturas = facturas;
			Label Bienvenida = new Label
			{
				BackgroundColor = Color.Transparent,
				Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333), 0, 0, 0),
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.White,
				FontSize = (App.DisplayScreenWidth / 15.04),
				Text = "¿MÉTODO DE PAGO?"
			};


			Label tarjeta = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 25.066666666666667),
				Text = "Tarjeta VISA o \r\nMastercard"
            };

			Label transferencia = new Label
            {
                HorizontalTextAlignment = TextAlignment.Start,
                BackgroundColor = Color.Transparent,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("BFBFBF"),
                FontSize = (App.DisplayScreenWidth / 25.066666666666667),
				Text = "Transferencia \r\nbancaria"
            };

			Image estadoTarjeta = new Image
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.Fill,
                WidthRequest = App.DisplayScreenWidth / 9.4,
                HeightRequest = App.DisplayScreenWidth / 9.4,
				Source = "iSeleccionado"
            };

			Image estadoTransferencia = new Image
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Center,
                Aspect = Aspect.Fill,
                WidthRequest = App.DisplayScreenWidth / 9.4,
                HeightRequest = App.DisplayScreenWidth / 9.4,
				Source = "iNoSeleccionado"
            };

			Grid Tarjeta = new Grid
            {
                Padding = 0,
                BackgroundColor = Color.White,
                RowSpacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }				
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) }
                }
            };

			Grid Transferencia = new Grid
            {
                Padding = 0,
                BackgroundColor = Color.White,
                RowSpacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Auto) }
                }
            };


			Tarjeta.Children.Add(tarjeta, 0, 0);
			Tarjeta.Children.Add(estadoTarjeta, 1, 0);
			Transferencia.Children.Add(transferencia, 0, 0);
			Transferencia.Children.Add(estadoTransferencia, 1, 0);

			TapGestureRecognizer TarjetaTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
            TapGestureRecognizer TransferenciaTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };

			if (!string.IsNullOrEmpty(Settings.session_MetodoPago) && Settings.session_MetodoPago.Equals("transferencia"))
			{
				transferencia.TextColor = Color.FromHex("4D4D4D");
				estadoTransferencia.Source = "iSeleccionado";
				tarjeta.TextColor = Color.FromHex("BFBFBF");
				estadoTarjeta.Source = "iNoSeleccionado";
				transferencia.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
				tarjeta.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
			}
			

            TarjetaTAP.Tapped += (sender, e) =>
            {
				if (!Settings.session_MetodoPago.Equals("tarjeta"))               
				{
					Settings.session_MetodoPago = "tarjeta";
					transferencia.TextColor = Color.FromHex("BFBFBF");
					estadoTransferencia.Source = "iNoSeleccionado";
					tarjeta.TextColor = Color.FromHex("4D4D4D");
					estadoTarjeta.Source = "iSeleccionado";
					tarjeta.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
					transferencia.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
				}
            };
            TransferenciaTAP.Tapped += (sender, e) =>
            {
				if (!Settings.session_MetodoPago.Equals("transferencia"))
                {
					Settings.session_MetodoPago = "transferencia";
					transferencia.TextColor = Color.FromHex("4D4D4D");
                    estadoTransferencia.Source = "iSeleccionado";
					tarjeta.TextColor = Color.FromHex("BFBFBF");
                    estadoTarjeta.Source = "iNoSeleccionado";
					transferencia.FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null);
					tarjeta.FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null);
                }
            };

			Tarjeta.GestureRecognizers.Add(TarjetaTAP);
			Transferencia.GestureRecognizers.Add(TransferenciaTAP);

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

			Grid Continuar = new Grid
			{
				Padding = 0,
				Children =
				{
					new Image
					{
						Source = "loginButton",
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions= LayoutOptions.Center,
						WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
						HeightRequest = (App.DisplayScreenHeight / 20.3),
					},
					continuar
				}
			};

			Grid Contenido = new Grid
			{
				Padding = 0,
				BackgroundColor = Color.White,
				RowSpacing = 0,
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				RowDefinitions = {
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
					new RowDefinition { Height = new GridLength (1, GridUnitType.Auto) }
				},
				ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
				}
			};

			Contenido.Children.Add(CC, 0, 0);
			Contenido.Children.Add(
				new StackLayout
    			{
    				Children = 
    				{
    					new BoxView
    					{
						    HeightRequest = App.DisplayScreenWidth/18.044444444444444,
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
                            Text = "¿Qué método de pago \r\nprefieres utilizar para realizar el pago ?"
                        }
    				}
    			}, 0, 1);
			Contenido.Children.Add(new Grid
			{
				Padding = new Thickness((App.DisplayScreenWidth/10.026666666666667),0),
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children =
				{					
					new StackLayout
					{
						Spacing = App.DisplayScreenHeight/16.24,
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Children = 
						{
							Tarjeta,
							new BoxView
                            {
                                VerticalOptions = LayoutOptions.FillAndExpand,
								BackgroundColor = Color.FromHex("707070"),
                                HeightRequest = (App.DisplayScreenWidth / 341.818181818181818)                                
                            },
							Transferencia
						}
					}
				}
			},0,2);
			Contenido.Children.Add(new Grid
			{
				BackgroundColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, (App.DisplayScreenHeight / 20.3)),
				Children =
				{
					Continuar
				}
			}, 0, 3);

			Padding = 0;
			Content = Contenido;
		}

		async void Continuar_Clicked(object sender, EventArgs e)
		{
			int TOTAL=0;
			foreach(var fact in facturas)
			{
				string valor = fact.saldo.Substring(9);
				double valor2 = Convert.ToDouble(valor);
                int saldo = Convert.ToInt32(valor2);
				TOTAL = TOTAL + saldo;
			}

			await Navigation.PushAsync(new DatosPago(facturas, TOTAL));
		}
	}
}