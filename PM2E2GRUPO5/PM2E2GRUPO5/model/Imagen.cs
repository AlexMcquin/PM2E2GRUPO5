using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace PM2E2GRUPO5
{
    class Imagen
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("image")]
        public byte[] image { get; set; }
    }
}