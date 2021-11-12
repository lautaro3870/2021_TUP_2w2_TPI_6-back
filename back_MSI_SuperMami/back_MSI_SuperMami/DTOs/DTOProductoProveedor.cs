using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOProductoProveedor
    {
        public int id { get; set; }
        public string  producto { get; set; }
        public string proveedor { get; set; }

        public double precio { get; set; }
    }
}
