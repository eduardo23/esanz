using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class BEcliente
    {
        public int id_cliente { get; set; }
        public string pNombre { get; set; }
        public string sNombre { get; set; }
        public string pApellido { get; set; }
        public string sApellido { get; set; }
        public string direccion { get; set; }
        public DateTime fechaNacimiento { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string ciudad { get; set; }
        public string Nombres { get; set; }
    }
}