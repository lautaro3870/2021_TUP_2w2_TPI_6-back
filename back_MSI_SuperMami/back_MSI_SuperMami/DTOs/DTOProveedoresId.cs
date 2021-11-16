using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOProveedoresId
    {

        public int idproveedor { get; set; }

        public string nombre { get; set; }
        public string direccion { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int idarea { get; set; }

        public List<int> formasPago { get; set; }
        public List<int> formasEntrega { get; set; }

        public List<DTOProductosPrecioProveedor> productos { get; set; }

    }
}
