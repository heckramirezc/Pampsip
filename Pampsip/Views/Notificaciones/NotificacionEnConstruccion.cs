using System.Threading.Tasks;
using Lottie.Forms;
using Pampsip.Pages.Menu;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Pampsip.Views.Notificaciones
{
	public class NotificacionEnConstruccion : PopupPage
    {
		public NotificacionEnConstruccion()
        {

			Button volver = new Button
            {
                Margin = 0,
                Text = "VOLVER",
                TextColor = Color.White,
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                FontSize = (App.DisplayScreenWidth / 25.066666666666667),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BackgroundColor = Color.Transparent,
                WidthRequest = (App.DisplayScreenHeight / 3.608888888888889),
                HeightRequest = (App.DisplayScreenHeight / 20.3),
            };
			volver.Clicked+= Volver_Clicked;

			FormattedString Mensaje = new FormattedString();
			Mensaje.Spans.Add(new Span
			{
				Text = ":)\r\nHola",
				FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
			});

			Mensaje.Spans.Add(new Span
			{
				Text = " Aún estamos trabajando\r\nen esta sección y pronto estará lista,\r\nya verás que será genial.",
				FontFamily = Device.OnPlatform("Montserrat-Regular", "Montserrat-Regular", null),
				FontSize = (App.DisplayScreenWidth / 25.066666666666667)
			});

			Content = new StackLayout
			{
				WidthRequest = App.DisplayScreenWidth / 1.175,
				HeightRequest = App.DisplayScreenWidth / 1.175,
				MinimumWidthRequest = App.DisplayScreenWidth / 1.175,
				MinimumHeightRequest = App.DisplayScreenWidth / 1.175,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				Padding = 0,
				Spacing = 0,
				Children =
				{
					new Grid
					{
						Padding = 0,
						Children =
						{
							new Image
							{
								WidthRequest = App.DisplayScreenWidth/1.175,
								HeightRequest = App.DisplayScreenWidth/1.175,
								Source = "backgroundModal",
								Aspect = Aspect.Fill
							},
							new StackLayout
							{
								Padding = new Thickness(0,(App.DisplayScreenWidth/7.52),0,0),
								Spacing = App.DisplayScreenWidth/3.957894736842105,
								Children =
								{
									new Label
									{
										HorizontalTextAlignment = TextAlignment.Center,
										HorizontalOptions = LayoutOptions.Center,
										FormattedText = Mensaje,
                                        TextColor = Color.FromHex("4D4D4D"),
										FontSize = (App.DisplayScreenWidth / 25.066666666666667)
                                    },
									new Grid
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
											volver
                                        }
                                    }
								}
							}
                        }
                    }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

		void Volver_Clicked(object sender, System.EventArgs e)
		{			
			MessagingCenter.Send<RootPagina>((RootPagina)Application.Current.MainPage, "Generales");   
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
