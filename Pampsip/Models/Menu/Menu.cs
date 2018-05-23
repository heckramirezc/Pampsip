using System;
using Pampsip.Common;
using Xamarin.Forms;

namespace Pampsip.Models.Menu
{
    public enum MenuTipo
    {                
        Inicio,
        Fidelizacion,
        Listas,
        Blog,
        Suplemento,
        Ubicaciones,
        Perfil,
        Notificaciones,
        Contactanos
    }
    public class Menus
    {
        public Menus()
        {
            MenuTipo = MenuTipo.Inicio;
        }

        public string Title { get; set; }
        public MenuTipo MenuTipo { get; set; }
        public MenuTipo MenuTipoSiguiente { get; set; }
        public bool isSelected { get; set; }
        public bool isSelectedSiguiente { get; set; }
        public Color TextColor { 
            get
            {                
                return isSelected ? Color.White : ColorResources.MenuTitle;
            } 
        }


        public string FontFamily
        {
            get
            {                
                return isSelected?FontResources.Label2Font:FontResources.LabelFont;
            }
        }

        public bool SeparatorVisibility
        {
            get
            {
                return isSelected || isSelectedSiguiente? false: true ;
            }
        }
    }
}
