using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Proveedoresxformadeenvio
    {
        public int Idproveedor { get; set; }
        public int Idformadeenvio { get; set; }

        public virtual FormaDeEnvio IdformadeenvioNavigation { get; set; }
        public virtual Proveedore IdproveedorNavigation { get; set; }
    }
}
