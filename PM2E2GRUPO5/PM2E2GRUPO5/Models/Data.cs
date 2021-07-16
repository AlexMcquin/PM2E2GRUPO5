using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SQLite;

namespace PM2E2GRUPO5.Models
{
    public class Data
    {

        [PrimaryKey, AutoIncrement]

        public int id { get; set; }

        [MaxLength(250)]
        public double latitud { get; set; }
        [MaxLength(250)]
        public double longitud { get; set; }

        public String descripcion { get; set; }

        [MaxLength(100), Unique]
        public String descripcionCorta { get; set; }


    }
}
