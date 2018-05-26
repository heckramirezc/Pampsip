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
                MenuTipo = MenuTipo.Generales,
				SeparatorVisibility = true
                //MenuTipoSiguiente = MenuTipo.Categorias
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu2,
                MenuTipo = MenuTipo.Categorias,
				SeparatorVisibility = true
                //MenuTipoSiguiente = MenuTipo.Historial
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu3,
                MenuTipo = MenuTipo.Historial,
				SeparatorVisibility = true
                //MenuTipoSiguiente = MenuTipo.Ajustes
            });
            Menus.Add(new Menus
            {
                Title = StringResources.Menu4,
				SeparatorVisibility = true,
                MenuTipo = MenuTipo.Ajustes,
                //MenuTipoSiguiente = MenuTipo.Contactanos
            });/*
            Menus.Add(new Menus
            {
                Title = StringResources.Menu5,
				SeparatorVisibility = false,
                MenuTipo = MenuTipo.Contactanos
            });*/
        }
    }
}
