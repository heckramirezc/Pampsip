using System;
using System.ComponentModel;
using Xamarin.Forms;
using Pampsip.Common;
using Pampsip.ViewModels.Menu;
using ImageCircle.Forms.Plugin.Abstractions;
using Pampsip.Controls;
//using FFImageLoading.Svg.Forms;
using System.Xml.Linq;
/*using FFImageLoading.Forms;
using FFImageLoading.Work;
using FFImageLoading.Transformations;*/
using System.Collections.Generic;
using Pampsip.Helpers;
using Pampsip.Models.Menu;
using Pampsip.Data;
using Pampsip.Models.SQLite;
using System.Threading.Tasks;
using Pampsip.Interfaces;


namespace Pampsip.Pages.Menu
{
    public class Menu : ContentPage, INotifyPropertyChanged
    {
        CircleImage avatar;
        public ExtendedListView Menus { get; private set; }
        public MenuVistaModelo modeloVista;
        ActivityIndicator instagramIndicador, facebookIndicador;
        BoxView Separator;
		MenuTipo menuAnterior;
        Button cerrarSesion;
        CircleImage instagram, facebook;
        public Menu()
        {
            Icon = ImageResources.MenuIcon;
            Title = StringResources.MenuTitle;
            BindingContext = modeloVista = new MenuVistaModelo();
            BackgroundColor = ColorResources.MenuBackground;

            MessagingCenter.Subscribe<RootPagina>(this, "Login", (sender) =>
            {
                if (!string.IsNullOrEmpty(Settings.session_idUsuario))
                {
                    usuarios usuario = App.Database.GetEmailUser(Convert.ToInt32(Settings.session_idUsuario));
                    if (usuario != null)
                    {
                        cerrarSesion.IsVisible = true;
                        if (!string.IsNullOrEmpty(usuario.avatar))
                        {
                            if (usuario.avatar.Contains("http"))
                                avatar.Source = Xamarin.Forms.ImageSource.FromUri(new Uri(usuario.avatar));
                            else
                                avatar.Source = Xamarin.Forms.ImageSource.FromFile(usuario.avatar);
                        }
                    }



                }
            });

            MessagingCenter.Subscribe<RootPagina>(this, "CreateUser", (sender) =>
            {
                if (!string.IsNullOrEmpty(Settings.session_idUsuario))
                {
                    usuarios usuario = App.Database.GetEmailUser(Convert.ToInt32(Settings.session_idUsuario));
                    if (usuario != null && !string.IsNullOrEmpty(usuario.avatar))
                    {
                        if (usuario.avatar.Contains("http"))
                            avatar.Source = Xamarin.Forms.ImageSource.FromUri(new Uri(usuario.avatar));
                        else
                            avatar.Source = Xamarin.Forms.ImageSource.FromFile(usuario.avatar);
                    }
                }
            });

			MessagingCenter.Subscribe<RootPagina>(this, "ActualizacionPerfil", (sender) =>
            {
                if (!string.IsNullOrEmpty(Settings.session_idUsuario))
                {
                    usuarios usuario = App.Database.GetEmailUser(Convert.ToInt32(Settings.session_idUsuario));
                    if (usuario != null && !string.IsNullOrEmpty(usuario.avatar))
                    {
                        if (usuario.avatar.Contains("http"))
                            avatar.Source = Xamarin.Forms.ImageSource.FromUri(new Uri(usuario.avatar));
                        else
                            avatar.Source = Xamarin.Forms.ImageSource.FromFile(usuario.avatar);
                    }
                }
            });
            

            Separator = new BoxView { Margin = new Thickness(((App.DisplayScreenWidth / 9.14285714)), 0), HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("48016B"), HeightRequest = (App.DisplayScreenHeight / 284), Opacity = 0.25 };
            Menus = new ExtendedListView
            {
                Header = new Grid { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { Separator } },
                Margin = 0,
                ItemsSource = modeloVista.Menus,
                RowHeight = Convert.ToInt32((App.DisplayScreenHeight / 14.2)),
                IsPullToRefreshEnabled = false,
                SeparatorVisibility = SeparatorVisibility.None,
                SeparatorColor = Color.White,
                HasUnevenRows = false
            };



            MessagingCenter.Subscribe<RootPagina>(this, "Inicio", (sender) =>
            {
                ActualizarListView();
            });

            MessagingCenter.Subscribe<RootPagina>(this, "Notificaciones", (sender) =>
            {
                try
                {
                    foreach (var _menu in modeloVista.Menus)
                    {
                        _menu.isSelected = false;
                        _menu.isSelectedSiguiente = false;
                    }
                    Separator.IsVisible = true;
                    Menus.ItemsSource = null;
                    Menus.ItemsSource = modeloVista.Menus;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            });

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
                             if (_menu.MenuTipoSiguiente == elemento.MenuTipo)
                                 _menu.isSelectedSiguiente = true;
                             else
                                 _menu.isSelectedSiguiente = false;
                         }
                     }
                     if (elemento.MenuTipo == MenuTipo.Fidelizacion)
                         Separator.IsVisible = false;
                     else
                         Separator.IsVisible = true;
                     Menus.ItemsSource = null;
                     Menus.ItemsSource = modeloVista.Menus;
                 }
                 catch (Exception ex)
                 {
                     System.Diagnostics.Debug.WriteLine(ex.Message);
                 }
             };

            avatar = new CircleImage
            {
                BorderColor = Color.FromHex("48016B"),
                BorderThickness = (float)(App.DisplayScreenHeight / 227.2),
                HeightRequest = (App.DisplayScreenHeight / 4.421052631578947),
                WidthRequest = (App.DisplayScreenHeight / 4.421052631578947),
                Aspect = Aspect.AspectFill,
                Source = "avatar"
            };
            if (!string.IsNullOrEmpty(Settings.session_idUsuario))
            {
                usuarios usuario = App.Database.GetEmailUser(Convert.ToInt32(Settings.session_idUsuario));
                if (usuario != null && !string.IsNullOrEmpty(usuario.avatar))
                {
                    if (usuario.avatar.Contains("http"))
                        avatar.Source = Xamarin.Forms.ImageSource.FromUri(new Uri(usuario.avatar));
                    else
                        avatar.Source = Xamarin.Forms.ImageSource.FromFile(usuario.avatar);
                }
            }


            CircleImage editar = new CircleImage
            {
                BorderColor = Color.FromHex("E17200"),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Aspect = Aspect.AspectFill,
                BorderThickness = (float)(App.DisplayScreenHeight / 227.2),
                HeightRequest = (App.DisplayScreenHeight / 9.333333333333333),
                WidthRequest = (App.DisplayScreenHeight / 9.333333333333333),
                Source = "iEditar",

            };

            RelativeLayout header = new RelativeLayout()
            {
                HeightRequest = App.DisplayScreenHeight / 4.518695306284805,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start,

            };

            header.Children.Add(avatar,
                                Constraint.Constant(0),
                                Constraint.Constant(0),
                                Constraint.Constant(App.DisplayScreenHeight / 4.982456140350877),
                                Constraint.Constant(App.DisplayScreenHeight / 4.982456140350877)
            );

            header.Children.Add(
                editar,
                Constraint.RelativeToView(avatar, (arg1, arg2) => { return arg2.Width - (arg2.Width / 4.8); }),
                Constraint.RelativeToView(avatar, (arg1, arg2) => { return arg2.Height - (arg2.Height / 2.5); }),
                Constraint.Constant(App.DisplayScreenHeight / 10.518518518518519),
                Constraint.Constant(App.DisplayScreenHeight / 10.518518518518519)
            );

            TapGestureRecognizer headerTAP = new TapGestureRecognizer();
            headerTAP.NumberOfTapsRequired = 1;
            headerTAP.Tapped += async (sender, e) =>
             {
                 MessagingCenter.Send<RootPagina>((RootPagina)Application.Current.MainPage, "Perfil");
             };
            header.GestureRecognizers.Add(headerTAP);


            facebook = new CircleImage
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Color.FromHex("999999"),
                BorderThickness = (float)(App.DisplayScreenHeight / 516.363636363636364),
                HeightRequest = (App.DisplayScreenHeight / 13.853658536585366),
                WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                Aspect = Aspect.AspectFill,
                Source = "iFacebook",

            };

            CircleImage twiiter = new CircleImage
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Color.FromHex("999999"),
                BorderThickness = (float)(App.DisplayScreenHeight / 516.363636363636364),
                HeightRequest = (App.DisplayScreenHeight / 13.853658536585366),
                WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                Aspect = Aspect.AspectFill,
                Source = "iTwitter",

            };

            instagram = new CircleImage
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                BorderColor = Color.FromHex("999999"),
                BorderThickness = (float)(App.DisplayScreenHeight / 516.363636363636364),
                HeightRequest = (App.DisplayScreenHeight / 13.853658536585366),
                WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                Aspect = Aspect.AspectFill,
                Source = "iInstagram",

            };

            facebookIndicador = new ActivityIndicator
            {
                HeightRequest = (App.DisplayScreenHeight / 13.853658536585366),
                WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                IsRunning = false,
                IsVisible = false
            };

            instagramIndicador = new ActivityIndicator
            {
                HeightRequest = (App.DisplayScreenHeight / 13.853658536585366),
                WidthRequest = (App.DisplayScreenHeight / 13.853658536585366),
                IsRunning = false,
                IsVisible = false

            };



            TapGestureRecognizer facebookTAP = new TapGestureRecognizer();
            TapGestureRecognizer instagramTAP = new TapGestureRecognizer();
            facebookTAP.NumberOfTapsRequired = 1;
            instagramTAP.NumberOfTapsRequired = 1;
            /*facebookTAP.Tapped += async (object sender, EventArgs e) =>
             {
                 facebookIndicador.IsVisible = true;
                 facebookIndicador.IsRunning = true;
                 facebook.IsVisible = false;
                 Device.OpenUri(new Uri("fb://profile/200742925975"));
                 await Task.Delay(1000);
                 if (Constantes.FacebookPresentado)
                 {
                     Constantes.FacebookPresentado = false;
                 }
                 else
                 {
                     Device.OpenUri(new Uri("https://www.facebook.com/supermercadoslatorre"));
                 }
                 facebookIndicador.IsVisible = false;
                 facebookIndicador.IsRunning = false;
                 facebook.IsVisible = true;
             };
            instagramTAP.Tapped += async (object sender, EventArgs e) =>
            {
                instagramIndicador.IsVisible = true;
                instagramIndicador.IsRunning = true;
                instagram.IsVisible = false;
                Device.OpenUri(new Uri("instagram://user?username=supermercadoslatorre"));
                await Task.Delay(1000);
                if (Constantes.FacebookPresentado)
                {
                    Constantes.FacebookPresentado = false;
                }
                else
                {
                    Device.OpenUri(new Uri("https://www.instagram.com/supermercadoslatorre"));
                }
                instagramIndicador.IsVisible = false;
                instagramIndicador.IsRunning = false;
                instagram.IsVisible = true;
            };
            facebook.GestureRecognizers.Add(facebookTAP);
            instagram.GestureRecognizers.Add(instagramTAP);
*/
            Grid redesSociales = new Grid
            {
                Padding = 0,
                ColumnSpacing = App.DisplayScreenHeight / 28.4,
                VerticalOptions = LayoutOptions.EndAndExpand,
                HorizontalOptions = LayoutOptions.Center,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength ((App.DisplayScreenHeight / 13.853658536585366), GridUnitType.Absolute) }},
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength ((App.DisplayScreenHeight / 13.853658536585366), GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength ((App.DisplayScreenHeight / 13.853658536585366), GridUnitType.Absolute) },
                    //new ColumnDefinition { Width = new GridLength ((App.DisplayScreenWidth / 7.80487805), GridUnitType.Absolute) }
                }
            };

            redesSociales.Children.Add(new Grid { Children = { facebookIndicador, facebook } }, 0, 0);
            //redesSociales.Children.Add(twiiter, 1, 0);
            redesSociales.Children.Add(new Grid { Children = { instagramIndicador, instagram } }, 1, 0);

            /*

            CachedImage i3 = new CachedImage()
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Aspect = Aspect.AspectFit,
                Source = SvgImageSource.FromFile("iEditar.svg")
            };

            i3.Transformations = new System.Collections.Generic.List<ITransformation>() {
                new CircleTransformation(){BorderHexColor = "E17200", BorderSize=40},
            };


            header.Children.Add(
            ,0,0);
*/
            /*
            SvgCachedImage i2 = new SvgCachedImage()
            {
                WidthRequest = 40,
                HeightRequest = 40,
                Source = "iEditar.svg"
            };

            */


            cerrarSesion = new Button
            {
                BackgroundColor = Color.Transparent,
                Margin = 0,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Text = "cerrar sesión",
                TextColor = Color.FromHex("999999"),
                FontFamily = FontResources.ButtonFont,
                FontSize = ((App.DisplayScreenHeight / 47.333333333333333))
            };
            cerrarSesion.Clicked += CerrarSesion_Clicked;

            if (string.IsNullOrEmpty(Settings.session_Session_Token) && string.IsNullOrEmpty(Settings.session_idUsuario))
            {
                cerrarSesion.IsVisible = false;
            }

            Menus.ItemTemplate = new DataTemplate(typeof(MenuDTModeloVista));


            RelativeLayout Contenido = new RelativeLayout()
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            Contenido.Children.Add(header,
                              Constraint.Constant(App.DisplayScreenHeight / 10.716981132075472),
                              Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 11.591836734693878) : (App.DisplayScreenHeight / 19.586206896551724)),
                              Constraint.Constant(App.DisplayScreenHeight / 3.952954276567611),
                              Constraint.Constant(App.DisplayScreenHeight / 4.518695306284805)
                             );

            Contenido.Children.Add(Menus,
                              Constraint.Constant(0),
                              Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 2.927835051546392) : (App.DisplayScreenHeight / 3.264367816091954)),
                              Constraint.RelativeToParent((arg) => { return arg.Width; }),
                              Constraint.Constant(App.DisplayScreenHeight / 2.007067137809187)
                             );

            Contenido.Children.Add(redesSociales,
                              Constraint.Constant(App.DisplayScreenHeight / 7.675675675675676),
                                   Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 1.149797570850202) : (App.DisplayScreenHeight / 1.19831223628692)),
                              Constraint.Constant(App.DisplayScreenHeight / 5.568627450980392),
                              Constraint.Constant(App.DisplayScreenHeight / 13.853658536585366)
                             );


            Contenido.Children.Add(cerrarSesion,
                              Constraint.Constant(0),
                              Constraint.Constant(Device.RuntimePlatform == Device.iOS ? (App.DisplayScreenHeight / 1.047970479704797) : (App.DisplayScreenHeight / 1.088122605363985)),
                              Constraint.RelativeToParent((arg) => { return arg.Width; }),
                              Constraint.Constant(App.DisplayScreenHeight / 35.5)
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
                    _menu.isSelectedSiguiente = false;
                }
                Separator.IsVisible = true;
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
            instagramIndicador.IsVisible = false;
            facebookIndicador.IsVisible = false;
            facebookIndicador.IsRunning = false;
            instagramIndicador.IsRunning = false;
            instagram.IsVisible = true;
            facebook.IsVisible = true;
        }

		protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            System.Diagnostics.Debug.WriteLine("OnBindingContextChanged");
        }

        void CerrarSesion_Clicked(object sender, EventArgs e)
        {

            Settings.session_idUsuario = string.Empty;
            Settings.session_Session_Token = string.Empty;            
            MessagingCenter.Send<Menu>(this, "logout");
            avatar.Source = "avatar";
            cerrarSesion.IsVisible = false;
            try
            {
                foreach (var _menu in modeloVista.Menus)
                {
                    _menu.isSelected = false;
                    _menu.isSelectedSiguiente = false;
                }
                Separator.IsVisible = true;
                Menus.ItemsSource = null;
                Menus.ItemsSource = modeloVista.Menus;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }
    }
}

