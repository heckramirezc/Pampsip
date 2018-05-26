using System;
using Xamarin.Forms;
using Pampsip.Models.Menu;
using Pampsip.Common;
using Pampsip.Helpers;
using Pampsip.Data;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Pampsip.Interfaces;
using Pampsip.Views.Generales;
using Pampsip.Views.Categorias;
using Pampsip.Views.Historial;
using Pampsip.Views.Contactanos;
using Pampsip.Views.Ajustes;

namespace Pampsip.Pages.Menu
{
    public class RootPagina : MasterDetailPage
    {
        MenuTipo menuAnterior;
        Menu menu = new Menu();
        public RootPagina()
        {
			/*if(Constantes.NavigationBarHeight.Equals(0f))
			{
				Constantes.NavigationBarHeight = App.NavigationBarHeight;
			}
			*/

			MessagingCenter.Subscribe<RootPagina>(this, "Generales", async (sender) =>
			{
				await Navigation.PopAllPopupAsync();
				menu.Menus.SelectedItem = menu.modeloVista.Menus[0];
			});

			MessagingCenter.Subscribe<RootPagina>(this, "Categorias", async (sender) =>
            {                
                menu.Menus.SelectedItem = menu.modeloVista.Menus[1];
            });

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
			menuAnterior = MenuTipo.Contactanos;
			NavegarA(MenuTipo.Generales);           
        }

        private void Menus_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {                           
            try
            {                                
                var elemento = e.SelectedItem as Menus;
                if (elemento == null)
                    return;                                                
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
            Detail = mostrarPagina;
            IsPresented = false;

        }
		NavigationPage generales, categorias, historial, ajustes, contactanos/*, ubicaciones*/;

        NavigationPage PaginaPorOpcion(MenuTipo opcion)
        {
            switch (opcion)
            {                
				case MenuTipo.Generales:
                    {
						if (generales != null)
							return generales;                        
						generales = new NavigationPage(new GeneralesVista());
						return generales;
                    }                
				case MenuTipo.Categorias:
                    {
						if (categorias != null)
							return categorias;						
						categorias = new NavigationPage(new CategoriasVista());
						return categorias;
                    } 
				case MenuTipo.Historial:
                    {
                        if (historial != null)
							return historial;                        
						historial = new NavigationPage(new HistorialVista());
						return historial;
                    } 
				case MenuTipo.Ajustes:
                    {
						if (ajustes != null)
							return ajustes;                        
						ajustes = new NavigationPage(new AjustesVista());
						return ajustes;
                    }
				case MenuTipo.Contactanos:
                    {
						if (contactanos != null)
							return contactanos;                                                
						contactanos = new NavigationPage(new ContactanosVista());
						return contactanos;
                    } 
                /*case MenuTipo.Ubicaciones:
                    {
                        if (ubicaciones != null)
                            return ubicaciones;

                        /*Task.Run(async () => 
                        { 
                            await App.ManejadorDatos.GetLocationsAsync();
                        });
                        var modeloVista = new UbicacionesVistaModelo() { Navigation = Navigation };
                        ubicaciones = new NavigationPage(new UbicacionesVista(modeloVista));*/
                        /*return ubicaciones;
                    } */
            }
			return generales;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
			menu.Menus.SelectedItem = menu.modeloVista.Menus[0];;
        }
    }
}
