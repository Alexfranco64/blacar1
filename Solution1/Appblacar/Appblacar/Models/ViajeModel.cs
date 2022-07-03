using System;
using System.Collections.Generic;
using System.Text;

namespace Appblacar.Models
{
    public class ViajeModel
    {
        //[PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Name { get; set; }

        public double Latitud { get; set; }

        public double Longitud { get; set; }

        public string Origen { get; set; }

        public string Destino { get; set; }

        public string ImageBase64 { get; set; }

        public DateTime DateTime { get; set; }

        public int NoPasajeros { get; set; }

        public double Tarifa { get; set; }

        public bool ViajeRedondo { get; set; }
    }
}
