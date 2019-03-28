using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class DAConexion
    {
        public static string ConexionBD()
        {
            var sCadena = ConfigurationManager.ConnectionStrings["BDPedidos"].ConnectionString.ToString();
            return sCadena;
        }

    }
}