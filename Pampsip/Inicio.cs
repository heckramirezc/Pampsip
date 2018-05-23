using Xamarin.Forms;

namespace Pampsip
{
	public class Inicio : ContentPage
    {
		public Inicio()
		{
			Content = new StackLayout
			{
				Children =
				{
					new Label
					{
						Text = "Hola mundo"
					}
				}
			};
		}
    }
}