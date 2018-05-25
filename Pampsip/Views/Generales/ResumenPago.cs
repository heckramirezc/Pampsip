using System;
using System.Collections.Generic;
using System.Linq;
using Pampsip.Controls;
using Pampsip.Models.SQLite;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class ResumenPago : ContentPage
    {
		ExtendedListView Facturas;        
        List<facturas> facturas;
        int isEnableSelected = 1;
        int isEnableUnSelected = 0;
		public ResumenPago(List<facturas> facturas, int isEnableSelected, int isEnableUnSelected)
		{
			NavigationPage.SetBackButtonTitle(this, "RESUMEN");
			this.facturas = facturas;
			this.isEnableUnSelected = isEnableUnSelected;
			this.isEnableSelected = isEnableSelected;
			Label Bienvenida = new Label
			{
				BackgroundColor = Color.Transparent,
				Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333), 0, 0, 0),
				HorizontalTextAlignment = TextAlignment.Start,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.White,
				FontSize = (App.DisplayScreenWidth / 15.04),
				Text="¿ESTÁS SEGURO?"
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

			Facturas = new ExtendedListView
			{
				Header = new BoxView
				{
					HeightRequest = App.DisplayScreenWidth/9.894736842105263,
					HorizontalOptions = LayoutOptions.FillAndExpand,
					BackgroundColor = Color.Transparent,
				},
				ItemTemplate = new DataTemplate(typeof(FacturasCarretillaDTModeloVista)),
				Margin = 0,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = facturas,
				RowHeight = Convert.ToInt32((App.DisplayScreenWidth / 3.514018691588785)),
				IsScrollEnable = true,
				IsPullToRefreshEnabled = false,
				SeparatorVisibility = SeparatorVisibility.None,
				SeparatorColor = Color.White,
				BackgroundColor = Color.Transparent,
				HasUnevenRows = false
			};
			Facturas.ItemSelected += Facturas_ItemSelected;

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
				BackgroundColor = Color.Transparent,                
				HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 26.857142857142857),
				Text = "A continuación te mostramos \r\nun resumen de lo que nos has\r\nindicado que deseas pagar.\r\n\r\nAsegurate de revisar bien cada detalle :)"
            }, 0, 1);
			Contenido.Children.Add(Facturas, 0, 2);
			Contenido.Children.Add(new Grid
			{
				BackgroundColor = Color.Transparent,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness(0, (App.DisplayScreenHeight / 20.3)),
				Children=
				{
					Continuar
				}
			}, 0, 3);

			Padding = 0;
			Content = Contenido;
		}

		void Facturas_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;
            facturas ContextoActual = ((facturas)e.SelectedItem);
            int index = 0;
            foreach (var factura in facturas)
            {
                index++;
                if (factura == ContextoActual && index == isEnableSelected && !factura.isSelected)
                {
                    factura.isSelected = true;
                    ContextoActual.backgroundColor = Color.FromHex("4D4D4D");
                    ContextoActual.iconEstado = "iSeleccionado";
                    ContextoActual.background = "iFacturaSeleccionadaBackground";
                    isEnableUnSelected = isEnableSelected;
                    if (facturas.Count() != isEnableSelected)
                        isEnableSelected = isEnableSelected + 1;
                }
                else if (factura == ContextoActual && index == isEnableUnSelected && factura.isSelected)
                {
                    factura.isSelected = false;
                    ContextoActual.background = "iFacturaBackground";
                    ContextoActual.iconEstado = "iNoSeleccionado";
                    ContextoActual.backgroundColor = Color.FromHex("BFBFBF");
                    if (isEnableSelected != 1)
                        isEnableSelected = isEnableUnSelected;
                    isEnableUnSelected = isEnableUnSelected - 1;
                }
            }         
            Facturas.SelectedItem = null;
        }



		async void Continuar_Clicked(object sender, EventArgs e)
		{
			if (facturas.Count(n => n.isSelected == true) > 0)
			    await Navigation.PushAsync(new MetodoPago(facturas));
		}

	}
}