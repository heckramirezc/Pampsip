using System.Threading.Tasks;
using Lottie.Forms;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace Pampsip.Views.Notificaciones
{
	public class NotificacionCargando : PopupPage
    {
        public NotificacionCargando()
        {

            Content = new AnimationView
            {
                AutoPlay = true,
                Animation = "loader.json",
                Loop = true,
                WidthRequest = App.DisplayScreenWidth,
				HeightRequest = App.DisplayScreenHeight,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
