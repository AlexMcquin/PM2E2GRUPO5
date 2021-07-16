using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using PM2E2GRUPO5.model;
using PM2E2GRUPO5.modelview;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2E2GRUPO5.Models;

namespace PM2E2GRUPO5
{
    public partial class MainPage : ContentPage
    {

        List<Imagen> service;
        RestService restService;
        byte[] imageArray = null;

        public MainPage()
        {
            InitializeComponent();
            restService = new RestService();
            enviarInfo.Clicked += EnviarInfo_Clicked;
        }

        private async void EnviarInfo_Clicked(object sender, EventArgs e)
        {
            var camera = new StoreCameraMediaOptions();
            camera.PhotoSize = PhotoSize.Full;
            camera.Name = "img";
            camera.Directory = "MiApp";


            var foto = await CrossMedia.Current.TakePhotoAsync(camera);


            if (foto != null)
            {

                image.Source = ImageSource.FromStream(() => {

                    return foto.GetStream();



                });
                using (MemoryStream memory = new MemoryStream())
                {

                    Stream stream = foto.GetStream();
                    stream.CopyTo(memory);
                    imageArray = memory.ToArray();
                }
            }
            var compartirfoto = foto.Path;


            Guardar.IsEnabled = true;

        }

        private async void guardar_Clicked(object sender, EventArgs e)
        {


            if (string.IsNullOrEmpty(imageArray.ToString()))
            {
                await DisplayAlert("Alerta", "No ha tomado fotografias", "ok");
                return;

            }
            else
            {
                var data = new Imagen
                {
                    id = 0,
                    image = imageArray
                };

                var client = new HttpClient();
                var json = JsonConvert.SerializeObject(data);
                var contentJSON = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(DataConstants.urlPost, contentJSON);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    await DisplayAlert("Datos", "Se guardo correctamente la información", "OK");

                }



            }


        }

        private void btnSalvar_Clicked(object sender, EventArgs e)
        {

            Int32 resultado = 0;
            var data = new Data()
            {
                latitud = Convert.ToDouble(this.txtLatitud.Text),
                longitud = Convert.ToDouble(this.txtLongitud.Text),
                descripcionCorta = this.txtShortDescripcion.Text

            };


            if (String.IsNullOrWhiteSpace(txtShortDescripcion.Text))
            {
                DisplayAlert("Advertencia", "Asegurese de llenar los campos obligatorios", "OK");

            }
            else if (String.IsNullOrWhiteSpace(txtLatitud.Text))
            {
                DisplayAlert("Advertencia", "Asegurese de llenar los campos obligatorios", "OK");

            }
            else if (String.IsNullOrWhiteSpace(txtLongitud.Text))
            {
                DisplayAlert("Advertencia", "Asegurese de llenar los campos obligatorios", "OK");

            }


            else
            {

                using (SQLiteConnection conexion = new SQLiteConnection(App.ubicacionDB))
                {
                    conexion.CreateTable<Data>();

                    resultado = conexion.Insert(data);

                    if (resultado > 0)
                        DisplayAlert("Aviso", "Adicionado", "Ok");
                    else
                        DisplayAlert("Aviso", "Error", "Ok");
                }

            }
        }

        private async void toolbarmenu_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Locaciones());
        }
    }


}
