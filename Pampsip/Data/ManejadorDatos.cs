using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pampsip.Interfaces;
using Pampsip.Models.FaceRecognition;
using Pampsip.Models.Login;

namespace Pampsip.Data
{
    public class ManejadorDatos
    {
        IServicioWeb ServicioWeb;

        public ManejadorDatos(IServicioWeb servicio)
        {
            ServicioWeb = servicio;
        }

        public Task<LoginResponse> LoginAsync(Login peticion)
        {
            return ServicioWeb.LoginAsync(peticion);
        }

		public Task<List<FaceResponse>> DetectAsync(Detect peticion)
        {
			return ServicioWeb.DetectAsync(peticion);        
        }

		public Task<List<IdentifyResponse>> IdentifyAsync(Identify peticion)
        {
			return ServicioWeb.IdentifyAsync(peticion);
        }
        
		public Task<PersonResponse> GetPersonAsync(Person peticion)
        {
			return ServicioWeb.GetPersonAsync(peticion);
        }
    }
}
