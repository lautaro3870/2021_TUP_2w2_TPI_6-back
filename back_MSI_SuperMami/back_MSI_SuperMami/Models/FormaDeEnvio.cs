using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class FormaDeEnvio
    {
        public FormaDeEnvio()
        {
            OrdenesDeCompras = new HashSet<OrdenesDeCompra>();
            Proveedoresxformadeenvios = new HashSet<Proveedoresxformadeenvio>();
        }

        public int Idformadeenvio { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<OrdenesDeCompra> OrdenesDeCompras { get; set; }
        public virtual ICollection<Proveedoresxformadeenvio> Proveedoresxformadeenvios { get; set; }
    }
}
