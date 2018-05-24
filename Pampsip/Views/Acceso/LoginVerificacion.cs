using System;
using System.Threading.Tasks;
using Lottie.Forms;
using Pampsip.Controls;
using Plugin.Toasts;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Pampsip.Views.Acceso
{
	public class LoginVerificacion : PopupPage
    {
		ExtendedEntry Token;
		private string TokenLogin;
		public LoginVerificacion(string TokenLogin)
        {
			this.TokenLogin = TokenLogin;			
			Label Mensaje = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Ingresa el token\r\nque has recibido por SMS\r\ny/o en tu bandeja de entrada",
                FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
                TextColor = Color.FromHex("4D4D4D"),
                FontSize = (App.DisplayScreenWidth / 26.857142857142857)
            };

			Token = new ExtendedEntry()
            {
                Margin = 0,
                Keyboard = Keyboard.Numeric,
                Placeholder = "Token",
                PlaceholderColor = Color.FromHex("4D4D4D"),
                FontFamily = Device.OnPlatform("Montserrat-Light", "Montserrat-Light", null),
                TextColor = Color.FromHex("4D4D4D"),
                BackgroundColor = Color.Transparent,
                Text = string.Empty,
                HasBorder = false,
                FontSize = (App.DisplayScreenWidth / 25.066666666666667)
            };
			Token.TextChanged += Token_TextChanged;

            
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
				WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                HeightRequest = (App.DisplayScreenHeight / 20.3),
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


			Content = new StackLayout
            {				
				WidthRequest = App.DisplayScreenHeight / 2.498461538461538,
				MinimumWidthRequest = App.DisplayScreenHeight / 2.498461538461538,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
				BackgroundColor = Color.White,
				Spacing = App.DisplayScreenWidth / 12.533333333333333,
				Padding = App.DisplayScreenWidth / 15.04,//new Thickness((App.DisplayScreenWidth / 15.04) , (App.DisplayScreenWidth / 18.8)),
                Children =
                {
                    Mensaje,                    
					new StackLayout
					{
						Spacing = 0,
						//Padding = App.DisplayScreenWidth/
						Children = 
						{
							Token,
                            new BoxView{BackgroundColor = Color.FromHex("BFBFBF"), HeightRequest =(App.DisplayScreenWidth /341.818181818181818), HorizontalOptions = LayoutOptions.FillAndExpand},
						}
					},
                    Continuar
                }
            };
        }

		private async void ShowToast(ToastNotificationType type, string titulo, string descripcion, int tiempo)
        {
            var notificator = DependencyService.Get<IToastNotificator>();
            bool tapped = await notificator.Notify(type, titulo, descripcion, TimeSpan.FromSeconds(tiempo));
        }

		async void Continuar_Clicked(object sender, System.EventArgs e)
		{
			if (String.IsNullOrEmpty(Token.Text))
            {
                await DisplayAlert("", "Por favor, indique el token proporcionado", "Aceptar");
				Token.Focus();
                return;
            }
			//ShowToast(ToastNotificationType.Error, "Inicio de sesión", "Token de inicio de sesión incorrecto", 6);
			MessagingCenter.Send<LoginVerificacion>(this, "Login");
			ShowToast(ToastNotificationType.Success, "Bienvenido", "Inicio de sesión exitoso", 4);
		}


		protected override void OnAppearing()
        {
            base.OnAppearing();
        }

		void Token_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!Token.Text.Equals(TokenLogin))
            {
				Token.TextColor = Color.FromHex("E9242A");
            }
            else
            {
				Token.TextColor = Color.FromHex("53A946");
            }
		}


		protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        protected virtual Task OnAppearingAnimationEnd()
        {
            return Content.FadeTo(0.5);
        }

        protected virtual Task OnDisappearingAnimationBegin()
        {
            return Content.FadeTo(1); ;
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override bool OnBackgroundClicked()
        {
            return false;
        }
    }
}
