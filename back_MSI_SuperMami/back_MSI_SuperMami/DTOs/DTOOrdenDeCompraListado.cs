using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOOrdenDeCompraListado
    {
        public string proveedor { get; set; }
        public string formaPago { get; set; }
        public string formaEnvio { get; set; }
        public string producto { get; set; }
        public int cantidad { get; set; }
    }
}
