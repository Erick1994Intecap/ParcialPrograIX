using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class Cliente
    {
        public int? id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public int? carne { get; set; }
        public int? DPI { get; set; }
        public string correo { get; set; }
        public string genero { get; set; }
        public string direccion { get; set; }
    }
}
