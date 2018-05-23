using System;
using System.Collections.ObjectModel;
using Pampsip.Common;
using Pampsip.Models.Menu;

namespace Pampsip.ViewModels.Menu
{
    public class MenuVistaModelo : BaseVistaModelo
    {
        public ObservableCollection<Menus> Menus { get; set; }
        public MenuVistaModelo()
        {
            Title = StringResources.MenuTitle;
            Menus = new ObservableCollection<Menus>();
            Menus.Add(new Menus
            {
                Title = StringResources.Menu1,
                MenuTipo = MenuTipo.Fidelizacion,
                MenuTipoSiguiente = MenuTipo.Suplemento
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu4,
                MenuTipo = MenuTipo.Suplemento,
                MenuTipoSiguiente = MenuTipo.Listas
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu2,
                MenuTipo = MenuTipo.Listas,
                MenuTipoSiguiente = MenuTipo.Blog
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu3,
                MenuTipo = MenuTipo.Blog,
                MenuTipoSiguiente = MenuTipo.Ubicaciones
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu5,
                MenuTipo = MenuTipo.Ubicaciones,
                MenuTipoSiguiente = MenuTipo.Perfil
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu6,
                MenuTipo = MenuTipo.Perfil,
                MenuTipoSiguiente = MenuTipo.Contactanos
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu7,
                MenuTipo = MenuTipo.Contactanos
            });
        }
    }
}
