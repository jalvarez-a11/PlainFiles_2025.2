using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plainFiles.core.Models
{
    public class Persona
    {
        public int ID { get; set; }
        public string Nombres { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Ciudad { get; set; } = "";
        public string Telefono { get; set; } = "";
        public decimal Saldo { get; set; }

        public string ToFileString()
        {
            return $"{ID},{Nombres},{Apellidos},{Ciudad},{Telefono},{Saldo}";
        }

        public static Persona FromFileString(string line)
        {
            var parts = line.Split(',');
            return new Persona
            {
                ID = int.Parse(parts[0]),
                Nombres = parts[1],
                Apellidos = parts[2],
                Ciudad = parts[3],
                Telefono = parts[4],
                Saldo = decimal.Parse(parts[5])
            };
        }

        public override string ToString()
        {
            return $"{ID} - {Nombres} {Apellidos} ({Ciudad}) Tel: {Telefono} Saldo: {Saldo:N2}";
        }
    }
}
