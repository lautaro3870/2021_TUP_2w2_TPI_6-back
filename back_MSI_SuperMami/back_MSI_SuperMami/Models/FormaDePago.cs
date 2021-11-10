using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class FormaDePago
    {
        public FormaDePago()
        {
            OrdenesDeCompras = new HashSet<OrdenesDeCompra>();
            Proveedoresxformasdepagos = new HashSet<Proveedoresxformasdepago>();
        }

        public int Idformapago { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }
        public float? Porcentaje { get; set; }

        public virtual ICollection<OrdenesDeCompra> OrdenesDeCompras { get; set; }
        public virtual ICollection<Proveedoresxformasdepago> Proveedoresxformasdepagos { get; set; }
    }
}
