using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Proveedore
    {
        public Proveedore()
        {
            OrdenesDeCompras = new HashSet<OrdenesDeCompra>();
            Productosxproveedores = new HashSet<Productosxproveedore>();
            Proveedoresxformadeenvios = new HashSet<Proveedoresxformadeenvio>();
            Proveedoresxformasdepagos = new HashSet<Proveedoresxformasdepago>();
        }

        public int Idproveedor { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Cuit { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public bool Estado { get; set; }
        public int Idarea { get; set; }

        public virtual Area IdareaNavigation { get; set; }
        public virtual ICollection<OrdenesDeCompra> OrdenesDeCompras { get; set; }
        public virtual ICollection<Productosxproveedore> Productosxproveedores { get; set; }
        public virtual ICollection<Proveedoresxformadeenvio> Proveedoresxformadeenvios { get; set; }
        public virtual ICollection<Proveedoresxformasdepago> Proveedoresxformasdepagos { get; set; }
    }
}
