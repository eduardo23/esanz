using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class BEdetalle
    {
        public int num_detalle { get; set; }
        public int num_factura { get; set; }
        public Nullable<int> id_producto { get; set; }

        [DisplayFormat(DataFormatString = "{0:N0}")]
        public Nullable<int> cantidad { get; set; }

        [DisplayFormat(DataFormatString = "{0:###.00}")]
        public Nullable<decimal> precio { get; set; }
        public virtual BEproducto producto { get; set; }
        public Nullable<decimal> subtotal{ get; set;}

        public BEdetalle()
        {
            producto = new BEproducto();
        }
    }
}