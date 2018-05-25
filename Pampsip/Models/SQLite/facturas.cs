
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SQLite;
using Xamarin.Forms;

namespace Pampsip.Models.SQLite
{
	public class facturas : INotifyPropertyChanged
    {
		string Background, IconEstado;
		Color BackgroundColor;
		public event PropertyChangedEventHandler PropertyChanged;
		public facturas() { }
        [PrimaryKey, Column("id")]
        public string alias { get; set; }

		public string background
        {
			get { return Background; }
            set
            {
				Background = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
			                new PropertyChangedEventArgs("background"));
                }
            }
        }

		public Color backgroundColor
        {
            get { return BackgroundColor; }
            set
            {
				BackgroundColor = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
					                new PropertyChangedEventArgs("backgroundColor"));
                }
            }
        }

		public string iconEstado
        {
			get { return IconEstado; }
            set
            {
				IconEstado = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this,
					                new PropertyChangedEventArgs("iconEstado"));
                }
            }
        }

        public string proveedor { get; set; }
        public string categoria { get; set; }
        public string saldo { get; set; }
        public string aviso { get; set; }
        public string vencimiento { get; set; }
		public bool isSelected { get; set; }
    }
}
