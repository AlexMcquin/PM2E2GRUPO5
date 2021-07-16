using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Text;

namespace PM2E2GRUPO5.model
{
    class RestService
    {
        HttpClient cliente;
      
        public RestService()
        {
            cliente = new HttpClient();

            if (Device.RuntimePlatform == Device.UWP)
            {
                cliente.DefaultRequestHeaders.Add("User-Agent", "");
                cliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            }
        }

        public async Task<List<Imagen>> GetRepositoriesAsync(string url)
        {
            List<Imagen> lista = null;
            try
            {
                HttpResponseMessage respuesta = await cliente.GetAsync(url);
                if (respuesta.IsSuccessStatusCode)
                {
                    string img = await respuesta.Content.ReadAsStringAsync();
                    lista = JsonConvert.DeserializeObject<List<Imagen>>(img);

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error", ex.Message);
            }

            return lista;
        }

        public async Task<T> executeRequestPost<T>(object image)
        {


            Uri urlBase = new Uri(DataConstants.urlBase);
            cliente.BaseAddress = urlBase;
          
  
            string jsonData = JsonConvert.SerializeObject(image);

            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await cliente.PostAsync(DataConstants.urlPost1, content).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }
            else
            {
                return default(T);
            }
        }



    }
}
