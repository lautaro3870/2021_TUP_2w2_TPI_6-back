using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Productosxproveedore
    {
        public int Idproveedor { get; set; }
        public int Idproducto { get; set; }
        public double Precio { get; set; }
        public int Idproductoproveedor { get; set; }
        public bool? Estado { get; set; }

        public virtual Producto IdproductoNavigation { get; set; }
        public virtual Proveedore IdproveedorNavigation { get; set; }
    }
}
