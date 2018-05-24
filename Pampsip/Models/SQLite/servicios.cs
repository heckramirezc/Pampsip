using System;
using SQLite;

namespace Pampsip.Models.SQLite
{
    public class servicios
    {
        public servicios(){}
        [PrimaryKey, Column("id")]
        public string alias { get; set; }
		public string proveedor { get; set; }
		public string categoria { get; set; }
		public string saldo { get; set; }
		public string aviso { get; set; }
		public string vencimiento { get; set; }
    }
}
