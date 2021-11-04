using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class OrdenesDeCompra
    {
        public OrdenesDeCompra()
        {
            DetalleOrdens = new HashSet<DetalleOrden>();
        }

        public int Idordendecompra { get; set; }
        public int Idproveedor { get; set; }
        public int Idformadeenvio { get; set; }
        public int Idformapago { get; set; }
        public int Idestado { get; set; }
        public DateTime fecha_registro { get; set; }


        public virtual EstadoOrdendecompra IdestadoNavigation { get; set; }
        public virtual FormaDeEnvio IdformadeenvioNavigation { get; set; }
        public virtual FormaDePago IdformapagoNavigation { get; set; }
        public virtual Proveedore IdproveedorNavigation { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; }
    }
}
