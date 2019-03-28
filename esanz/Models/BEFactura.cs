using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class BEFactura
    {
        public int num_factura { get; set; }
        public int id_cliente { get; set; }

        [DataType(DataType.Date)]
        public Nullable<DateTime> fecha { get; set; }
        public int num_pago { get; set; }
        public BEcliente cliente { get; set; }  
        public BEmodo_pago modo_pago { get; set; }
        public List<BEdetalle> detalle { get; set; }
        public Nullable<decimal> total { get; set; }
        public BEFactura()
        {
            cliente = new BEcliente();
            modo_pago = new BEmodo_pago();
            detalle = new List<BEdetalle>();
        }        

    }
}