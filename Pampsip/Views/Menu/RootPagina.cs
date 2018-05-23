using System;
using Xamarin.Forms;
using Pampsip.Models.Menu;
using Pampsip.Common;
/*using Pampsip.ViewModels.Perfil;
using Pampsip.ViewModels.Inicio;
using Pampsip.ViewModels.Notificaciones;
using Pampsip.ViewModels.Ubicaciones;
using Pampsip.ViewModels.Blog;
using Pampsip.Pages.Perfil;
using Pampsip.Pages.Inicio;
using Pampsip.Pages.Notificaciones;
using Pampsip.Pages.Ubicaciones;
using Pampsip.Pages.Blog;*/
using Pampsip.Helpers;
using Pampsip.Data;
/*using Pampsip.Pages.Fidelizacion;
using Pampsip.ViewModels.Fidelizacion;
using Pampsip.ViewModels.Listas;
using Pampsip.Pages.Listas;
using Pampsip.ViewModels.Suplemento;
using Pampsip.Pages.Suplemento;
using Pampsip.ViewModels.Acceso;
using Pampsip.Pages.Acceso;*/
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Pampsip.Interfaces;

namespace Pampsip.Pages.Menu
{
    public class RootPagina : MasterDetailPage
    {
        MenuTipo menuAnterior;
        MenuTipo PostInicioSesion = MenuTipo.Inicio;
        Menu menu = new Menu();
        public RootPagina()
        {
			try
			{
				DependencyService.Get<INavigationService>().ShowStatusBar();
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
			}            
            Title = StringResources.MenuTitle;
            BackgroundColor = ColorResources.MenuBackground;
            menu.Menus.ItemSelected += Menus_ItemSelected;
            Master = menu;
            menuAnterior = MenuTipo.Perfil;
            NavegarA(MenuTipo.Inicio);

            /*MessagingCenter.Subscribe<InicioSesionVistaModelo>(this, "loginFB", async (sender) =>
            {
                System.Diagnostics.Debug.WriteLine("loginFB");
                try
                {
                    if (string.IsNullOrEmpty(sender.FacebookUser.Email) && !string.IsNullOrEmpty(sender.FacebookUser.Message))
                    {
                        MessagingCenter.Send<RootPagina>(this, "loginFB");
                        if (!sender.FacebookUser.Message.Equals("Login Canceled."))
                            await DisplayAlert("Error de inicio de sesión", sender.FacebookUser.Message, "Aceptar");
                    }
                    else
                    {                        
                        await Navigation.PopModalAsync();
                        await Navigation.PushModalAsync(new Registro(sender.FacebookUser));
                    }

                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }

                this.IsBusy = false;
                //Facebook.IsVisible = true;
                //Registrar.IsEnabled = true;
                //Ingresar.IsEnabled = true;
            });

            MessagingCenter.Subscribe<EstablecerContrasenia>(this, "LoginFailed", async (sender) =>
            {
                InicioSesion InicioSesion = new InicioSesion(sender.Peticion.peticion.entity.email, sender.Peticion.peticion.entity.password_hash);
                await Navigation.PopModalAsync();
                await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(InicioSesion);
                await DisplayAlert("Registro exitoso", "Tu registro fue completado con éxito sin embargo tuvimos inconvenientes al iniciar sesión automaticamente, intenta ingresar sesión en un momento.", "Aceptar");
            });
*/
            MessagingCenter.Subscribe<RootPagina>(this, "CreateUser", async (sender) =>
            {
                try
                {
					try
                    {
                        DependencyService.Get<INavigationService>().ShowStatusBar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    await Navigation.PopModalAsync();
                    await Navigation.PopModalAsync();
                    if (PostInicioSesion == MenuTipo.Fidelizacion || PostInicioSesion == MenuTipo.Perfil)
                    {
                        int cnt = 0;
                        foreach (var menuTipo in menu.modeloVista.Menus)
                        {
                            if (menuTipo.MenuTipo == PostInicioSesion)
                            {
                                menu.Menus.SelectedItem = menu.modeloVista.Menus[cnt];
                                break;
                            }
                            cnt++;
                        }
                        PostInicioSesion = MenuTipo.Inicio;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            });

            MessagingCenter.Subscribe<RootPagina>(this, "Login", async (sender) =>
            {
                try
                {
					try
                    {
                        DependencyService.Get<INavigationService>().ShowStatusBar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    await Navigation.PopModalAsync();
                    if(PostInicioSesion==MenuTipo.Fidelizacion || PostInicioSesion == MenuTipo.Perfil)
                    {                        
                        int cnt = 0;
                        foreach (var menuTipo in menu.modeloVista.Menus)
                        {
                            if (menuTipo.MenuTipo == PostInicioSesion)
                            {
                                menu.Menus.SelectedItem = menu.modeloVista.Menus[cnt];
                                break;
                            }
                            cnt++;
                        }
                        PostInicioSesion = MenuTipo.Inicio;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            });

            MessagingCenter.Subscribe<RootPagina>(this, "Inicio", (sender) =>
            {
                NavegarA(MenuTipo.Inicio);
                menu.Menus.SelectedItem = null;
            });


            MessagingCenter.Subscribe<RootPagina>(this, "Waze", (sender) =>
            {
                DisplayAlert("", "WAZE", "Aceptar");
            });

            MessagingCenter.Subscribe<RootPagina>(this, "Notificaciones", (sender) =>
            {
                NavegarA(MenuTipo.Notificaciones);
                menu.Menus.SelectedItem = null;
            });

            MessagingCenter.Subscribe<RootPagina>(this, "Ubicaciones", (sender) =>
            {
                menu.Menus.SelectedItem = menu.modeloVista.Menus[4];
            });
            /*
            MessagingCenter.Subscribe<RootPagina>(this, "PerfilFidelizacion", async (sender) =>
            {
                if (!string.IsNullOrEmpty(Settings.session_Session_Token) && !string.IsNullOrEmpty(Settings.session_idUsuario))
                {							
					try
					{
						Constantes.PerfilPresentado = true;
                        var modeloVista = new PerfilVistaModelo() { Navigation = Navigation };
                        await Detail.Navigation.PushAsync(new PerfilVista(modeloVista));                        
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
                }
                else
                {					
                    IsPresented = false;
                    PostInicioSesion = MenuTipo.Perfil;
                    await Navigation.PushModalAsync(new InicioSesion(string.Empty, string.Empty));
					try
                    {
                        DependencyService.Get<INavigationService>().HideStatusBar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            });

			MessagingCenter.Subscribe<RootPagina>(this, "Perfil", async (sender) =>
            {
                if (!string.IsNullOrEmpty(Settings.session_Session_Token) && !string.IsNullOrEmpty(Settings.session_idUsuario))
                {
                        menu.Menus.SelectedItem = menu.modeloVista.Menus[5];
                        IsPresented = false;                    
                }
                else
                {
                    IsPresented = false;
                    PostInicioSesion = MenuTipo.Perfil;
                    await Navigation.PushModalAsync(new InicioSesion(string.Empty, string.Empty));
					try
                    {
                        DependencyService.Get<INavigationService>().HideStatusBar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                }
            });
            */
            MessagingCenter.Subscribe<Menu>(this, "logout", (sender) =>
            {                
                if(menuAnterior == MenuTipo.Inicio)
                    MessagingCenter.Send<RootPagina>(this, "logout");
                menuAnterior = MenuTipo.Perfil;
                NavegarA(MenuTipo.Inicio);
                menu.Menus.SelectedItem = null;
            });

            /*
            MessagingCenter.Subscribe<InicioVista>(this, "fidelizacion", (sender) =>
            {
                NavegarA(MenuTipo.Fidelizacion);
                //menu.Menus.SelectedItem = MenuTipo.Ubicaciones;
            });

            MessagingCenter.Subscribe<InicioVista>(this, "listas", (sender) =>
            {
                NavegarA(MenuTipo.Listas);
                //menu.Menus.SelectedItem = MenuTipo.Ubicaciones;
            });

            MessagingCenter.Subscribe<InicioVista>(this, "blog", (sender) =>
            {
                NavegarA(MenuTipo.Blog);
                //menu.Menus.SelectedItem = MenuTipo.Ubicaciones;
            });
            MessagingCenter.Subscribe<InicioVista>(this, "suplemento", (sender) =>
            {
                NavegarA(MenuTipo.Suplemento);
                //menu.Menus.SelectedItem = MenuTipo.Ubicaciones;
            });*/
        }

        private void Menus_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {                           
            try
            {                                
                var elemento = e.SelectedItem as Menus;
                if (elemento == null)
                    return;                                
                if ((elemento.MenuTipo == MenuTipo.Perfil || elemento.MenuTipo == MenuTipo.Fidelizacion) && string.IsNullOrEmpty(Settings.session_Session_Token) && string.IsNullOrEmpty(Settings.session_idUsuario))
                {
                    PostInicioSesion = elemento.MenuTipo;
                    //Navigation.PushModalAsync(new InicioSesion(string.Empty, string.Empty));
					try
                    {
                        DependencyService.Get<INavigationService>().HideStatusBar();
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex.Message);
                    }
                    if(Detail!= inicio)
                    {
                        int cnt = 0;
                        foreach (var menuTipo in menu.modeloVista.Menus)
                        {
                            if (menuTipo.MenuTipo == menuAnterior)
                            {
                                menu.Menus.SelectedItem = menu.modeloVista.Menus[cnt];
                                return;
                            }
                            cnt++;
                        }
                    }
                    else
                        MessagingCenter.Send<RootPagina>(this, "Inicio");
                    return;
                }
                NavegarA(elemento.MenuTipo);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

        void NavegarA(MenuTipo opcion)
        {                        
            if (menuAnterior == opcion)
            {
                IsPresented = false;
                return;
            }
            menuAnterior = opcion;
            var mostrarPagina = PaginaPorOpcion(opcion);
            mostrarPagina.BarTextColor = ColorResources.BarTextColor;
            mostrarPagina.BarBackgroundColor = ColorResources.BarBackgroundColor;
			/*if (menuAnterior == MenuTipo.Fidelizacion && Constantes.PerfilPresentado)
            {
                try
                {
                    Task.Run(async () => { await fidelizacion.Navigation.PopAsync(false); });
                    Constantes.PerfilPresentado = false;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }*/
            Detail = mostrarPagina;
            IsPresented = false;

        }
        NavigationPage inicio, fidelizacion, listas, blog, suplemento, ubicaciones, perfil, notificaciones, contactanos;

        NavigationPage PaginaPorOpcion(MenuTipo opcion)
        {
            switch (opcion)
            {                
                case MenuTipo.Inicio:
                    {
                        if (inicio != null)
                            return inicio;
                        //var modeloVista = new InicioVistaModelo() { Navigation = Navigation };
                        //inicio = new NavigationPage(new InicioVista(modeloVista));
                        return inicio;
                    }                
                case MenuTipo.Fidelizacion:
                    {
                        if (fidelizacion != null)
                            return fidelizacion;
						/*try
						{
							Detail.Navigation.PushPopupAsync(new NotificacionCargando());
						}
						catch(Exception ex)
						{
							System.Diagnostics.Debug.WriteLine(ex.Message);
						}
                        var modeloVista = new FidelizacionVistaModelo() { Navigation = Navigation };
                        fidelizacion = new NavigationPage(new FidelizacionVista(modeloVista));*/
                        return fidelizacion;
                    } 
                case MenuTipo.Listas:
                    {
                        if (listas != null)
                            return listas;
                        //var modeloVista = new ListasVistaModelo() { Navigation = Navigation };
                        //listas = new NavigationPage(new ListasVista(modeloVista));
                        return listas;
                    } 
                case MenuTipo.Blog:
                    {
                        if (blog != null)
                            return blog;
                        //var modeloVista = new BlogVistaModelo() { Navigation = Navigation };
                        //blog = new NavigationPage(new BlogVista(modeloVista));
                        return blog;
                    }
                case MenuTipo.Suplemento:
                    {
                        if (suplemento != null)
                            return suplemento;
                        //var modeloVista = new SuplementoVistaModelo() { Navigation = Navigation };
                        //suplemento = new NavigationPage(new SuplementoVista(modeloVista));
                        //suplemento = new NavigationPage(new SuplementoGeneralVista());
                        return suplemento;
                    } 
                case MenuTipo.Ubicaciones:
                    {
                        if (ubicaciones != null)
                            return ubicaciones;

                        /*Task.Run(async () => 
                        { 
                            await App.ManejadorDatos.GetLocationsAsync();
                        });
                        var modeloVista = new UbicacionesVistaModelo() { Navigation = Navigation };
                        ubicaciones = new NavigationPage(new UbicacionesVista(modeloVista));*/
                        return ubicaciones;
                    } 
                case MenuTipo.Perfil:
                    {                        
                        if (perfil != null)
                            return perfil;
                        //var modeloVista = new PerfilVistaModelo() { Navigation = Navigation };
						//perfil = new NavigationPage(new PerfilVista(modeloVista));
                        return perfil;
                    }
                case MenuTipo.Notificaciones:
                    {
                        if (notificaciones != null)
                            return notificaciones;
                        //var modeloVista = new NotificacionesVistaModelo() { Navigation = Navigation };
                        //notificaciones = new NavigationPage(new NotificacionesVista(modeloVista));
                        return notificaciones;
                    }
                case MenuTipo.Contactanos:
                    {
                        if (contactanos != contactanos)
                            return notificaciones;
                        //contactanos = new NavigationPage(new Contactenos());
                        return contactanos;
                    }
            }
            return inicio;
        }


        async protected override void OnAppearing()
        {
			/*
            if (!Constantes.ExisteConexionAInternet)
            {
                await Navigation.PushPopupAsync(new NotificacionConexion());
            }
            */
            if (string.IsNullOrEmpty((Settings.session_Session_Token)))
            {
                base.OnAppearing();
                /*
                if ((Device.OS == TargetPlatform.iOS) || (Device.OS == TargetPlatform.Android))
                {
                    MessagingCenter.Send<RootPagina>(this, Constantes.NoAutenticado);
                }*/
            }
        }
    }
}
