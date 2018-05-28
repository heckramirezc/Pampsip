using System;
using System.Collections.Generic;
using System.Linq;
using ImageCircle.Forms.Plugin.Abstractions;
using Pampsip.Controls;
using Pampsip.Data;
using Pampsip.Models.SQLite;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class ServicioSasociadoDetalle : ContentPage
    {
		ExtendedListView Facturas;
		CircleImage continuar;
		List<facturas> facturas;
		public ServicioSasociadoDetalle(servicios Servicio)
		{
			NavigationPage.SetBackButtonTitle(this,Servicio.alias.ToUpper());

			MessagingCenter.Subscribe<FacturasDTModeloVista>(this, "PDF", async (sender) =>
            {
				await Navigation.PushAsync(new FacturaVista());
            });

			Label Bienvenida = new Label
            {
                BackgroundColor = Color.Transparent,
                Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333), 0, 0, 0),
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                TextColor = Color.White,
                FontSize = (App.DisplayScreenWidth / 15.04)
            };
			Bienvenida.Text = Servicio.alias;

			continuar = new CircleImage
            {
                BackgroundColor = Color.Transparent,
                BorderColor = Color.FromHex("f6f6f6"),
                BorderThickness = Convert.ToInt32(App.DisplayScreenWidth / 107.428571428571429),
                Aspect = Aspect.AspectFill,
                Source = "iContinuarA"
            };
			TapGestureRecognizer continuarTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			continuarTAP.Tapped+= ContinuarTAP_Tapped;
			continuar.GestureRecognizers.Add(continuarTAP);

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

			CC.Children.Add(continuar,
                            Constraint.Constant((App.DisplayScreenWidth / 1.540983606557377)),
			                Constraint.Constant(App.DisplayScreenHeight / 4.69364161849711),
			                Constraint.Constant(App.DisplayScreenHeight / 12.492307692307692),
                            Constraint.Constant(App.DisplayScreenHeight / 12.492307692307692)
                           );
			

			facturas = new List<facturas>
            {
				new facturas{
                    alias = "FACTURA MARZO 2018",
                    proveedor="EEGSA",
					categoria=string.Empty,
					saldo=!Servicio.alias.Equals("CASA PROPIA")?"TOTAL: Q.2,150.00":"TOTAL: Q.852.00",
					aviso=string.Empty,
                    vencimiento = "Ven: 01 - 03 - 2018",
					background = "iFacturaBackground",
					iconEstado = "iNoSeleccionado",
					backgroundColor = Color.FromHex("BFBFBF")
                },

            };
			if(!Servicio.alias.Equals("CASA PROPIA"))
			{
				facturas.Add(new facturas
				{
					alias = "FACTURA ABRIL 2018",
					proveedor = "EEGSA",
					categoria = string.Empty,
					saldo = "TOTAL: Q.2,150.00",
					aviso = string.Empty,
					vencimiento = "Ven: 13 - 07 - 1991",
					background = "iFacturaBackground",
					iconEstado = "iNoSeleccionado",
					backgroundColor = Color.FromHex("BFBFBF")
				});
				facturas.Add(new facturas
                {
                    alias = "FACTURA MAYO 2018",
                    proveedor = "EEGSA",
                    categoria = string.Empty,
                    saldo = "TOTAL: Q.2,150.00",
                    aviso = string.Empty,
                    vencimiento = "Ven: 13 - 07 - 1991",
                    background = "iFacturaBackground",
                    iconEstado = "iNoSeleccionado",
                    backgroundColor = Color.FromHex("BFBFBF")
				});

			}

            Facturas = new ExtendedListView
            {
                ItemTemplate = new DataTemplate(typeof(FacturasDTModeloVista)),
                Margin = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = facturas,
                RowHeight = Convert.ToInt32((App.DisplayScreenWidth / 1.978947368421053)),
                IsScrollEnable = true,
                IsPullToRefreshEnabled = false,
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.White,
                BackgroundColor = Color.Transparent,
                HasUnevenRows = false
            };
			Facturas.ItemSelected += Facturas_ItemSelected;


            Grid Contenido = new Grid
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
            Contenido.Children.Add(Facturas, 0, 1);

            Padding = 0;
            Content = Contenido;
		}

		void ContinuarTAP_Tapped(object sender, EventArgs e)
		{
			if (facturas.Count(n => n.isSelected == true) > 0)
				Navigation.PushAsync(new ResumenPago(facturas));
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
				if(factura==ContextoActual && index==Constantes.isEnableSelected && !factura.isSelected)
				{
					factura.isSelected = true;
					ContextoActual.backgroundColor = Color.FromHex("4D4D4D");
					ContextoActual.iconEstado = "iSeleccionado";
                    ContextoActual.background = "iFacturaSeleccionadaBackground";
					Constantes.isEnableUnSelected = Constantes.isEnableSelected;
					if(facturas.Count()!=Constantes.isEnableSelected)
						Constantes.isEnableSelected = Constantes.isEnableSelected + 1;
				}
				else if (factura == ContextoActual && index == Constantes.isEnableUnSelected && factura.isSelected)
                {                    
					factura.isSelected = false;
                    ContextoActual.background = "iFacturaBackground";
					ContextoActual.iconEstado = "iNoSeleccionado";
					ContextoActual.backgroundColor = Color.FromHex("BFBFBF");
					if(Constantes.isEnableSelected!=1)
						Constantes.isEnableSelected = Constantes.isEnableUnSelected;
					Constantes.isEnableUnSelected = Constantes.isEnableUnSelected-1;
                }
			}

			if(facturas.Count(n => n.isSelected == true)>0)
				continuar.Source = "iContinuarB";
			else
				continuar.Source = "iContinuarA";
			
			Facturas.SelectedItem = null;
		}

	}
}