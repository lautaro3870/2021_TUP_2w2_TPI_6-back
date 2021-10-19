using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class DetalleOrden
    {
        public int Iddetalle { get; set; }
        public decimal Precio { get; set; }
        public int Idproducto { get; set; }
        public int Idordendecompra { get; set; }
        public int Cantidad { get; set; }

        public virtual OrdenesDeCompra IdordendecompraNavigation { get; set; }
        public virtual Producto IdproductoNavigation { get; set; }
    }
}
