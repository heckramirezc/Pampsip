using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Pampsip.Models.FaceRecognition;
using Pampsip.Models.Login;

namespace Pampsip.Interfaces
{
    public interface IServicioWeb
    {        
        Task<LoginResponse> LoginAsync(Login peticion);        
		Task<List<FaceResponse>> DetectAsync(Detect peticion);        
		Task<List<IdentifyResponse>> IdentifyAsync(Identify peticion);        
		Task<PersonResponse> GetPersonAsync(Person peticion);        
    }
}
