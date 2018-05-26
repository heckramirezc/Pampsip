using Pampsip.Views.Notificaciones;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Pampsip.Views.Categorias
{
	public class CategoriasVista : ContentPage
    {
		public CategoriasVista()
		{
			Label Bienvenida = new Label
            {
                BackgroundColor = Color.Transparent,
                Margin = new Thickness((App.DisplayScreenWidth / 12.533333333333333), 0, 0, 0),
                HorizontalTextAlignment = TextAlignment.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "PAMPSIP",
                FontFamily = Device.OnPlatform("Montserrat-Bold", "Montserrat-Bold", null),
                TextColor = Color.White,
                FontSize = (App.DisplayScreenWidth / 15.04)
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
                            Constraint.Constant(-20),
                            Constraint.Constant(App.DisplayScreenWidth),
                            Constraint.Constant(App.DisplayScreenHeight / 2.889679715302491)
            );

			CC.Children.Add(Bienvenida,
                            Constraint.Constant(0),
                            Constraint.Constant(App.DisplayScreenHeight / 8.12),
                            Constraint.Constant(App.DisplayScreenWidth)
                           );
			
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
            //Contenido.Children.Add(ServiciosAsociados, 0, 1);

            Padding = 0;
            Content = Contenido;
		}

		async protected override void OnAppearing()
        {
            base.OnAppearing();
            await Navigation.PushPopupAsync(new NotificacionEnConstruccion());
        }
    }
}