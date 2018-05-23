using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Pampsip.Interfaces;
using Pampsip.Models.Login;
using Newtonsoft.Json;
using SQLite;
using Pampsip.Helpers;
using Pampsip.Models.FaceRecognition;
using System.IO;
using System.Net.Http.Headers;

namespace Pampsip.Data
{
    public class ServicioWeb : IServicioWeb
    {
        HttpClient pampsipAPI;
        HttpClient faceAPI;
        private LoginResponse Usuario;
		private List<FaceResponse> Face;
		private List<IdentifyResponse> Candidatos;
		private PersonResponse Persona;
            
        public ServicioWeb()
        {
            pampsipAPI = new HttpClient();
            //cliente.MaxResponseContentBufferSize = 256000;
            //cliente.Timeout = TimeSpan.FromSeconds(60000);

            //cliente.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "bhfu9d8ycwtm3a0sfnucminyfhi287ey");
            pampsipAPI.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));


            faceAPI = new HttpClient();
            faceAPI.DefaultRequestHeaders.Add(Constantes.SubscriptionKeyName, Settings.session_SubscriptionKey);
        }

        public async Task<LoginResponse> LoginAsync(Login peticion)
        {
            Usuario = new LoginResponse();
            var uri = new Uri(Constantes.URL_Login);
            try
            {                
                /*IEnumerable <KeyValuePair <string, string>> parametros = new[]
                {
                    new KeyValuePair<string,string>("username", peticion.username),
                    new KeyValuePair<string,string>("password",peticion.password)
                };

                StringContent Peticion = new StringContent(parametros.ToString(), Encoding.UTF8, "application/json");
                */
                System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri + peticion.parametros);

				var solicitud = await pampsipAPI.PostAsync(uri+ peticion.parametros, null);
                solicitud.EnsureSuccessStatusCode();
                string respuesta = await solicitud.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
                Usuario = JsonConvert.DeserializeObject<LoginResponse>(respuesta);
            }
            catch (HttpRequestException e) 
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
            }
            catch (Exception ex) 
            {                
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }

            return Usuario;
        }  

        
		public async Task<List<FaceResponse>> DetectAsync(Detect peticion)
        {        
			Face = new List<FaceResponse>();
			var uri = new Uri(Constantes.URL_Detect);
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Constantes.URL_Detect+peticion.parametros);
			try
			{
				System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri + peticion.parametros);
				request.Content = new StreamContent(peticion.Stream as Stream);
				request.Content.Headers.ContentType = new MediaTypeHeaderValue(Constantes.StreamContentTypeHeader);
				var solicitud = await faceAPI.SendAsync(request);
                solicitud.EnsureSuccessStatusCode();
                string respuesta = await solicitud.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
				Face = JsonConvert.DeserializeObject<List<FaceResponse>>(respuesta);

			}
			catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }         
            return Face;
        }

		public async Task<List<IdentifyResponse>> IdentifyAsync(Identify peticion)
        {
			Candidatos = new List<IdentifyResponse>();
			var uri = new Uri(Constantes.URL_Identify);
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Constantes.URL_Identify);
            try
            {
                System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri + peticion.parametros);

				var json = await Task.Run(() => JsonConvert.SerializeObject(peticion.peticion));
                System.Diagnostics.Debug.WriteLine(json);
                StringContent Peticion = new StringContent(json, Encoding.UTF8, "application/json");                

                request.Content = Peticion;
                
                var solicitud = await faceAPI.SendAsync(request);
                solicitud.EnsureSuccessStatusCode();
                string respuesta = await solicitud.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
				Candidatos = JsonConvert.DeserializeObject<List<IdentifyResponse>>(respuesta);

            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
			return Candidatos;
        }

		public async Task<PersonResponse> GetPersonAsync(Person peticion)
        {
			Persona = new PersonResponse();
			var uri = new Uri(Constantes.URL_Person + peticion.parametros);
            try
            {
                System.Diagnostics.Debug.WriteLine("PARAMETROS: " + uri);

				var solicitud = await faceAPI.GetAsync(uri);
                solicitud.EnsureSuccessStatusCode();
                string respuesta = await solicitud.Content.ReadAsStringAsync();

                System.Diagnostics.Debug.WriteLine("RESPUESTA: " + respuesta);
				Persona = JsonConvert.DeserializeObject<PersonResponse>(respuesta);

            }
            catch (HttpRequestException e)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + e.Message);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ERROR: " + ex.Message);
            }
			return Persona;
        }
    }
}