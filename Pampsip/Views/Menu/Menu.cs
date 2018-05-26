using System;
using System.ComponentModel;
using Xamarin.Forms;
using Pampsip.Common;
using Pampsip.ViewModels.Menu;
using Pampsip.Controls;
using Pampsip.Helpers;
using Pampsip.Models.Menu;
using Pampsip.Data;
using System.Threading.Tasks;
using Plugin.Toasts;

namespace Pampsip.Pages.Menu
{
    public class Menu : ContentPage, INotifyPropertyChanged
    {
        
        public ExtendedListView Menus { get; private set; }
        public MenuVistaModelo modeloVista;
		ActivityIndicator facebookIndicador, twitterIndicador, messengerIndicador;        
		MenuTipo menuAnterior;        
        Grid facebook, twitter, messenger;
        public Menu()
        {
            Icon = ImageResources.MenuIcon;
            Title = StringResources.MenuTitle;
            BindingContext = modeloVista = new MenuVistaModelo();
            BackgroundColor = ColorResources.MenuBackground;                             

            Menus = new ExtendedListView
            {
				IsScrollEnable = false,
                Margin = 0,
                ItemsSource = modeloVista.Menus,
				RowHeight = Convert.ToInt32((App.DisplayScreenHeight / 13.533333333333333)),
                IsPullToRefreshEnabled = false,
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.White,
                HasUnevenRows = false
            };

			menuAnterior = MenuTipo.Contactanos;
            Menus.ItemSelected += (sender, e) =>
             {
                 try
                 {
                     var elemento = e.SelectedItem as Menus;
					 if (menuAnterior == elemento.MenuTipo)                     
                         return;                     
					 else
						menuAnterior = elemento.MenuTipo;

                     foreach (var _menu in modeloVista.Menus)
                     {
                         if (_menu.MenuTipo == elemento.MenuTipo)
                             _menu.isSelected = true;
                         else
                         {
                             _menu.isSelected = false;
                         }
                     }                     
                     Menus.ItemsSource = null;
                     Menus.ItemsSource = modeloVista.Menus;
                 }
                 catch (Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message);
                 }
             };

			Label header = new Label
            {
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "MENÚ",
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				TextColor = Color.FromHex("4D4D4D"),
				FontSize = (App.DisplayScreenWidth / 15.04)
            };

               


			facebook = new Grid
			{
				Children = 
				{
					new Image
                    {
                        HorizontalOptions = LayoutOptions.Start,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
                        //WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                        Aspect = Aspect.AspectFit,
                        Source = "iFacebook",
                    }
				}
			};

			twitter = new Grid
			{
				Children =
				{
					new Image
					{
						HorizontalOptions = LayoutOptions.Center,
						VerticalOptions = LayoutOptions.Center,
						HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
                        //WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                        Aspect = Aspect.AspectFit,
						Source = "iTwitter",

					}
				}
			};

			messenger = new Grid
			{
				Children = 
				{
					new Image
                    {
                        HorizontalOptions = LayoutOptions.End,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
                        //WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                        Aspect = Aspect.AspectFit,
                        Source = "iMessenger",

                    }
				}
			};

            facebookIndicador = new ActivityIndicator
            {
				HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
				WidthRequest = (App.DisplayScreenHeight / 36.909090909090909),
                IsRunning = false,
                IsVisible = false
            };

			twitterIndicador = new ActivityIndicator
            {
				HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
                WidthRequest = (App.DisplayScreenHeight / 36.909090909090909),
                IsRunning = false,
                IsVisible = false

            };

			messengerIndicador = new ActivityIndicator
            {
				HeightRequest = (App.DisplayScreenHeight / 36.909090909090909),
                WidthRequest = (App.DisplayScreenHeight / 36.909090909090909),
                IsRunning = false,
                IsVisible = false

            };



            TapGestureRecognizer facebookTAP = new TapGestureRecognizer();
            TapGestureRecognizer twitterTAP = new TapGestureRecognizer();
			TapGestureRecognizer messengerTAP = new TapGestureRecognizer();
            facebookTAP.NumberOfTapsRequired = 1;
			twitterTAP.NumberOfTapsRequired = 1;
			messengerTAP.NumberOfTapsRequired = 1;
            facebookTAP.Tapped += async (object sender, EventArgs e) =>
             {
                 facebookIndicador.IsVisible = true;
                 facebookIndicador.IsRunning = true;
                 facebook.IsVisible = false;
				 Device.OpenUri(new Uri("fb://profile/604146393288718"));
                 await Task.Delay(2000);
				if (Constantes.RedSocialPresentada)
                 {
					Constantes.RedSocialPresentada = false;
                 }
                 else
                 {
					Device.OpenUri(new Uri("https://www.facebook.com/Pampsip/"));
                 }
                 facebookIndicador.IsVisible = false;
                 facebookIndicador.IsRunning = false;
                 facebook.IsVisible = true;
             };

			twitterTAP.Tapped += async (object sender, EventArgs e) =>
            {
				twitterIndicador.IsVisible = true;
				twitterIndicador.IsRunning = true;
				twitter.IsVisible = false;
				Device.OpenUri(new Uri("twitter://user?user_id=999476282705502208"));
				//Device.OpenUri(new Uri("twitter://userName?user_id=198829810"));
                await Task.Delay(2000);
				if (Constantes.RedSocialPresentada)
                {
					Constantes.RedSocialPresentada = false;
                }
                else
                {
					Device.OpenUri(new Uri("https://twitter.com/pamsip1"));
                }
				twitterIndicador.IsVisible = false;
				twitterIndicador.IsRunning = false;
				twitter.IsVisible = true;
            };

			messengerTAP.Tapped += async (object sender, EventArgs e) =>
            {
				messengerIndicador.IsVisible = true;
				messengerIndicador.IsRunning = true;
				messenger.IsVisible = false;
				Device.OpenUri(new Uri("https://www.messenger.com/t/604146393288718"));
				messengerIndicador.IsVisible = false;
				messengerIndicador.IsRunning = false;
				messenger.IsVisible = true;
            };
            facebook.GestureRecognizers.Add(facebookTAP);
			twitter.GestureRecognizers.Add(twitterTAP);
			messenger.GestureRecognizers.Add(messengerTAP);

            Grid redesSociales = new Grid
            {
				WidthRequest = App.DisplayScreenHeight/5.010180786080089,
				HeightRequest = App.DisplayScreenHeight/36.909090909090909,
                Padding = 0,
                ColumnSpacing = 0,
				HorizontalOptions = LayoutOptions.Center,
                RowDefinitions = {
					new RowDefinition { Height = new GridLength ((App.DisplayScreenHeight / 36.909090909090909), GridUnitType.Absolute) }},
                ColumnDefinitions = {
					new ColumnDefinition { Width = new GridLength ((App.DisplayScreenHeight / 15.030542358240267), GridUnitType.Absolute) },
					new ColumnDefinition { Width = new GridLength ((App.DisplayScreenHeight / 15.030542358240267), GridUnitType.Absolute) },
					new ColumnDefinition { Width = new GridLength ((App.DisplayScreenWidth / 15.030542358240267), GridUnitType.Star) }
                }
            };

            redesSociales.Children.Add(new Grid { Children = { facebookIndicador, facebook } }, 0, 0);
			redesSociales.Children.Add(new Grid { Children = { twitterIndicador, twitter } }, 1, 0);
			redesSociales.Children.Add(new Grid { Children = { messengerIndicador, messenger } }, 2, 0);

			Grid RedesSociales = new Grid
			{				
				Padding = 0,
				HeightRequest = App.DisplayScreenHeight / 36.909090909090909,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Children = 
				{
					redesSociales
				}
			};



            Button cerrarSesion = new Button
            {
                Margin = 0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "cerrar sesión",
				TextColor = Color.FromHex("BFBFBF"),
                FontFamily = FontResources.ButtonFont,
				FontSize = ((App.DisplayScreenHeight / 54.133333333333333)),
				WidthRequest = App.DisplayScreenHeight / 5.678321678321678,
				HeightRequest = App.DisplayScreenHeight/40.6
            };

			Grid CerrarSesion = new Grid
			{				
				Children =
				{
					new StackLayout
					{
						HeightRequest = App.DisplayScreenHeight / 32.48,
                        WidthRequest = App.DisplayScreenHeight / 5.678321678321678,
						Spacing = App.DisplayScreenHeight/162.4,
						HorizontalOptions = LayoutOptions.Center,
						Children =
						{
							cerrarSesion,
							new BoxView
							{
								HorizontalOptions = LayoutOptions.FillAndExpand,
								BackgroundColor = Color.FromHex("BFBFBF"),
								HeightRequest = (App.DisplayScreenWidth / 341.818181818181818),
								Opacity = 0.25
							}
						}
					}
				}
			};

            cerrarSesion.Clicked += CerrarSesion_Clicked;



            Menus.ItemTemplate = new DataTemplate(typeof(MenuDTModeloVista));


            RelativeLayout Contenido = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            


			Contenido.Children.Add(header,
			                   Constraint.Constant(0),
			                   Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 8.923076923076923) : (App.DisplayScreenHeight / 10.278481012658228)),
			                   Constraint.RelativeToParent((arg) => { return arg.Width; })
                             );

            Contenido.Children.Add(Menus,
                              Constraint.Constant(0),
			                  Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 3.029850746268657) : (App.DisplayScreenHeight / 3.171875)),
                              Constraint.RelativeToParent((arg) => { return arg.Width; }),
			                  Constraint.Constant(App.DisplayScreenHeight / 2.942028985507246)
                             );

			Contenido.Children.Add(new Label
    			{
    				HorizontalTextAlignment = TextAlignment.Center,
    				HorizontalOptions = LayoutOptions.FillAndExpand,
    				Margin = 0,
				    TextColor = Color.FromHex("BFBFBF"),
    				FontSize = (App.DisplayScreenWidth / 25.066666666666667),
    				VerticalTextAlignment = TextAlignment.Center,
    				Text = "Contáctanos",
				    FontFamily =  Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                    
    			},
                  Constraint.Constant(0),
			      Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 1.407279029462738) : (App.DisplayScreenHeight / 1.43716814159292)),
                  Constraint.RelativeToParent((arg) => { return arg.Width; })
              );


            Contenido.Children.Add(RedesSociales,
		                       Constraint.Constant(0),
		                       Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 1.280757097791798) : (App.DisplayScreenHeight / 1.305466237942122)),
			                   Constraint.RelativeToParent((arg) => { return arg.Width; }),
			                   Constraint.Constant(App.DisplayScreenHeight / 36.909090909090909)
                             );


            Contenido.Children.Add(CerrarSesion,
			                  Constraint.Constant(0),
			                  Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 1.087014725568942) : (App.DisplayScreenHeight / 1.104761904761905)),
			                  Constraint.RelativeToParent((arg) => { return arg.Width; }),
			                  Constraint.Constant(App.DisplayScreenHeight / 32.48)
                             );

			Contenido.Children.Add(new BoxView {BackgroundColor = Color.FromHex("BFBFBF") , Opacity = 0.08},
			                       Constraint.RelativeToParent((arg) => { return arg.Width-(App.DisplayScreenHeight/203); }),
			                       Constraint.Constant(20),
			                       Constraint.Constant(App.DisplayScreenWidth/203),
			                       Constraint.Constant(App.DisplayScreenHeight)
			                      );

            
            Content = Contenido;
        }

        void ActualizarListView()
        {
            try
            {
                foreach (var _menu in modeloVista.Menus)
                {
                    _menu.isSelected = false;
                }
                Menus.ItemsSource = null;
                Menus.ItemsSource = modeloVista.Menus;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            System.Diagnostics.Debug.WriteLine("Menu");

            facebookIndicador.IsVisible = false;
			twitterIndicador.IsVisible = false;
			messengerIndicador.IsVisible = false;
            facebookIndicador.IsRunning = false;
			twitterIndicador.IsRunning = false;
			messengerIndicador.IsRunning = false;            
            facebook.IsVisible = true;
			twitter.IsVisible = true;
			messenger.IsVisible = true;
        }

		protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            System.Diagnostics.Debug.WriteLine("OnBindingContextChanged");
        }

        void CerrarSesion_Clicked(object sender, EventArgs e)
        {
			ShowToast(ToastNotificationType.Success, "Cierre de sesión", "Sesión finalizada con éxito", 4);
            Settings.session_idUsuario = string.Empty;
            Settings.session_Session_Token = string.Empty;            
			MessagingCenter.Send<Pampsip.Pages.Menu.Menu>(this, "logout");
        }

		private async void ShowToast(ToastNotificationType type, string titulo, string descripcion, int tiempo)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            bool tapped = await notificator.Notify(type, titulo, descripcion, TimeSpan.FromSeconds(tiempo));
        }
    }
}

