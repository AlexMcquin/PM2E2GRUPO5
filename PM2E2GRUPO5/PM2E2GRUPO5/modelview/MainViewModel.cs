using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PM2E2GRUPO5.model;
using Xamarin.Forms;

namespace PM2E2GRUPO5.modelview
{
    public class MainViewModel : ViewModelBase
    {
        RestService webApi = new RestService();

        private ObservableCollection<Imagen> listaimg;

        

        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; RaisePropetyChanged(); }
        }
        private byte[] img;
        public byte[] Img
        {
            get { return img; }
            set { img = value; RaisePropetyChanged(); }
        }


        public ICommand ConsultaPostImage { get; set; }
        public MainViewModel()
        {
            ConsultaPostImage = new Command(async () => await RequestPost());
        }

        public async Task RequestPost()
        {
            var dataImg = new { id = Id,Image=Img };

            listaimg = await webApi.executeRequestPost<ObservableCollection<Imagen>>(dataImg);
        }



    }
}