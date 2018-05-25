using System;
using System.Collections.Generic;
using Pampsip.Models.SQLite;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class MetodoPago : ContentPage
	{
		List<facturas> facturas;
		int isEnableSelected = 1;
		int isEnableUnSelected = 0;
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
			Contenido.Children.Add(new Label
			{
				Margin = new Thickness(0,(App.NavigationBarHeight/18.454545454545455),0,0),
				BackgroundColor = Color.Transparent,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 26.857142857142857),
				Text = "¿Qué método de pago \r\nprefieres utilizar para realizar el pago ?"
			}, 0, 1);
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

		void Continuar_Clicked(object sender, EventArgs e)
		{

		}
	}
}