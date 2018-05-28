using System;
using System.Collections.Generic;
using ImageCircle.Forms.Plugin.Abstractions;
using Lottie.Forms;
using Pampsip.Controls;
using Pampsip.Data;
using Pampsip.Models.SQLite;
using Pampsip.Pages.Menu;
using Xamarin.Forms;

namespace Pampsip.Views.Generales
{
	public class GeneralesVista : ContentPage
    {
		List<servicios> servicios;
		ExtendedListView ServiciosAsociados;
		public GeneralesVista()
		{			
			NavigationPage.SetBackButtonTitle(this, "GENERALES");
			Label Bienvenida = new Label
            {
				BackgroundColor = Color.Transparent,
				Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333),0,0,0),
				HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "PAMPSIP",
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                TextColor = Color.White,
				FontSize = (App.DisplayScreenWidth / 15.04)
            };

			CircleImage avatar = new CircleImage
            {
				BackgroundColor = Color.Transparent,
                BorderColor = Color.FromHex("f6f6f6"),
				BorderThickness = Convert.ToInt32(App.DisplayScreenWidth / 107.428571428571429),
                Aspect = Aspect.AspectFill,
				Source = "avatar"
            };

			AnimationView avatarDefault = new AnimationView
            {
                AutoPlay = true,
				Animation = "outline_user.json",
                Loop = true,
				WidthRequest = App.DisplayScreenHeight / 10.15,
				HeightRequest = App.DisplayScreenHeight / 10.15,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            };

			Grid Avatar = new Grid
            {
                Children =
                {
                    avatar,
                    avatarDefault
                }
            };

			CircleImage config = new CircleImage
            {
				BackgroundColor = Color.Transparent,
                BorderColor = Color.FromHex("f6f6f6"),
				BorderThickness = Convert.ToInt32(App.DisplayScreenWidth / 107.428571428571429),
                Aspect = Aspect.AspectFill,
                Source = "iConfig"
            };

			TapGestureRecognizer configTAP = new TapGestureRecognizer { NumberOfTapsRequired = 1 };
			configTAP.Tapped+= (sender, e) => 
			{
				MessagingCenter.Send<RootPagina>((RootPagina)Application.Current.MainPage, "Categorias");   
			};
			config.GestureRecognizers.Add(configTAP);

			RelativeLayout CC = new RelativeLayout()
            {				
				Padding = 0,
                WidthRequest = App.DisplayScreenWidth,
                HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.Start,
				BackgroundColor = Color.Transparent
			};
            
            CC.Children.Add(new Image
            {
				BackgroundColor = Color.Transparent,
                Aspect = Aspect.Fill,
                Source = "header",
                HeightRequest = App.DisplayScreenHeight / 2.889679715302491
            },
                            Constraint.Constant(0),
			                Constraint.Constant(-20),
                            Constraint.Constant(App.DisplayScreenWidth),
                            Constraint.Constant(App.DisplayScreenHeight / 2.889679715302491)
            );

            CC.Children.Add(Bienvenida,
                            Constraint.Constant(0),
			                Constraint.Constant(App.DisplayScreenHeight / 8.12),
                            Constraint.Constant(App.DisplayScreenWidth)
                           );

			CC.Children.Add(Avatar,
			                Constraint.Constant((App.DisplayScreenWidth / 9.4)),
			                Constraint.Constant(App.DisplayScreenHeight / 5.205128205128205),
			                Constraint.Constant(App.DisplayScreenHeight / 8.12),
			                Constraint.Constant(App.DisplayScreenHeight / 8.12)
                           );

			CC.Children.Add(config,
			                Constraint.Constant((App.DisplayScreenWidth / 1.540983606557377)),
			                Constraint.Constant(App.DisplayScreenHeight / 3.625),
			                Constraint.Constant(App.DisplayScreenHeight / 12.492307692307692),
			                Constraint.Constant(App.DisplayScreenHeight / 12.492307692307692)
                           );

			servicios = new List<servicios>
			{
				new servicios{
					alias = "CONTADOR MARÍA", 
					proveedor="EEGSA", 
					categoria="Categoría: Hogar 1", 
					saldo="SALDO: Q.2,150.00", 
					aviso="3 Facturas pendiente de pago", 
					vencimiento = "Ven: 13 - 07 - 1991"
				},
				new servicios{
					alias = "CONTADOR PEDRO",
                    proveedor="EEGSA",
                    categoria="Categoría: Hogar 1",
					saldo="SALDO: Q.100.00",
					aviso="3 Facturas pendiente de pago",
                    vencimiento = "Ven: 13 - 07 - 1991"
                },
				new servicios{
					alias = "CASA PROPIA",
                    proveedor="EEGSA",
                    categoria="Categoría: Hogar 2",
					saldo="SALDO: Q.852.00",
					aviso="1 Facturas pendiente de pago",
                    vencimiento = "Ven: 01 - 03 - 2018"
                }
			};

			ServiciosAsociados = new ExtendedListView
			{
				ItemTemplate = new DataTemplate(typeof(ServiciosAsociadosDTModeloVista)),
				Margin = 0,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
				ItemsSource = servicios,
				RowHeight = Convert.ToInt32((App.DisplayScreenWidth / 1.978947368421053)),
				IsScrollEnable = true,
                IsPullToRefreshEnabled = false,
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.White,
				BackgroundColor = Color.Transparent,
				HasUnevenRows = false
            };
			ServiciosAsociados.ItemSelected+= ServiciosAsociados_ItemSelected;

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
			Contenido.Children.Add(ServiciosAsociados, 0, 1);

			Padding = 0;
			Content = Contenido;
		}
		protected override void OnAppearing()
		{
			base.OnAppearing();
			Constantes.isEnableSelected = 1;
			Constantes.isEnableUnSelected = 0;
		}

		async void ServiciosAsociados_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			if (e.SelectedItem == null)
				return;
			await Navigation.PushAsync(new ServicioSasociadoDetalle((servicios)e.SelectedItem));
			ServiciosAsociados.SelectedItem = null;
		}

	}
}