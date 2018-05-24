using System;
using Pampsip.Common;
using Xamarin.Forms;

namespace Pampsip.Models.Menu
{
    public enum MenuTipo
    {                
        Generales,
        Categorias,
        Historial,
        Ajustes,
		Contactanos,
        Ubicaciones
    }
    public class Menus
    {
        public Menus()
        {
			MenuTipo = MenuTipo.Generales;
        }

        public string Title { get; set; }
        public MenuTipo MenuTipo { get; set; }
        //public MenuTipo MenuTipoSiguiente { get; set; }
        public bool isSelected { get; set; }
        //public bool isSelectedSiguiente { get; set; }
        public Color TextColor { 
            get
            {                
				return isSelected ? Color.FromHex("21538B") : ColorResources.MenuTitle;
            } 
        }


        public string FontFamily
        {
            get
            {                
				return isSelected?FontResources.LabelFont:FontResources.Label2Font;
            }
        }

		public bool SeparatorVisibility { get; set; }
    }
}
