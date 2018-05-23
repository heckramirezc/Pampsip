using System;
using Pampsip.Controls;
using Pampsip.ViewModels;
using Xamarin.Forms;
using Pampsip.Common;
using Pampsip.Data;

namespace Pampsip.Pages.Menu
{
    public class MenuDTModeloVista : ExtendedViewCell
    {
        public MenuDTModeloVista()
        {

            Label tituloMenu = new Label
            {
                Margin = new Thickness((App.DisplayScreenWidth / 40), 0),
                FontSize = (App.DisplayScreenWidth / 26.66666667),
                VerticalTextAlignment = TextAlignment.Center,
            };
            tituloMenu.SetBinding(Label.TextProperty, "Title");
            tituloMenu.SetBinding(Label.TextColorProperty, "TextColor");
            tituloMenu.SetBinding(Label.FontFamilyProperty, "FontFamily");


			BoxView Separator = new BoxView { VerticalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("48016B"), HeightRequest = (App.DisplayScreenWidth / 160), Opacity = 0.25 };
            Separator.SetBinding(VisualElement.IsVisibleProperty, "SeparatorVisibility");

            Grid Menu = new Grid
            {               
                Padding = new Thickness((App.DisplayScreenWidth / 9.14285714), 0),
                RowSpacing = 0,
                HeightRequest = Convert.ToInt32((App.DisplayScreenHeight / 14.2)),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength ((App.DisplayScreenHeight / 284), GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) }
                }
            };
            Menu.Children.Add(tituloMenu, 0, 0);
            Menu.Children.Add(Separator, 0, 1);
            View = Menu;
            SelectedBackgroundColor = Color.FromHex("8FBD1E");
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
        }
    }
}
