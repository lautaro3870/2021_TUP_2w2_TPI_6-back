using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Proveedoresxformasdepago
    {
        public int Idproveedor { get; set; }
        public int Idformapago { get; set; }

        public virtual FormaDePago IdformapagoNavigation { get; set; }
        public virtual Proveedore IdproveedorNavigation { get; set; }
    }
}
