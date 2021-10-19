using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class EstadoOrdendecompra
    {
        public EstadoOrdendecompra()
        {
            OrdenesDeCompras = new HashSet<OrdenesDeCompra>();
        }

        public int Idestado { get; set; }
        public string Estado { get; set; }

        public virtual ICollection<OrdenesDeCompra> OrdenesDeCompras { get; set; }
    }
}
