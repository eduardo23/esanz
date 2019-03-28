using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace esanz.Models
{
    public class BEproducto
    {
        public int id_producto { get; set; }
        public string nombre { get; set; }

        [DisplayFormat(DataFormatString = "{0:###.00}")]
        public Nullable<decimal> precio { get; set; }
        public Nullable<int> stock { get; set; }
        public Nullable<int> id_categoria { get; set; }
        public virtual BEcategoria categoria { get; set; }

        public BEproducto()
        {
            categoria = new BEcategoria();
        }
    }
}