using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOListaProveedores
    {
        public int idproveedor { get; set; }

        public string nombre { get; set; }
        public string direccion { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public string area { get; set; }

        public List<int> formaPago { get; set; }
        public List<int> formaEnvio { get; set; }

        public List<DTOProductosPrecioProveedor> producto { get; set; }



        //public int idproducto { get; set; }

        //public string producto { get; set; }

        //public double precio { get; set; }





    }

    
}
