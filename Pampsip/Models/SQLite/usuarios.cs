using System;
using SQLite;

namespace Pampsip.Models.SQLite
{
    public class usuarios
    {        
        public usuarios(){}
        [PrimaryKey, Column("id")]
        public int id { get; set; }
		public int userId { get; set; }
        public string customer_id { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string emailAlternativo { get; set; }
        public string phone { get; set; }
        public string avatar { get; set; }
        public string firstname { get; set; }
        public string membershipNumber { get; set; }
        public double currentBalance { get; set; }        
        public string lastname { get; set; }                
        public string gender { get; set; }
        public string fechaUltimoInicio { get; set; }
        public string FechaNacimiento { get; set; }        
        public string TipoIdentificacion { get; set; }
        public string NumeroIdentificacion { get; set; }
    }
}
