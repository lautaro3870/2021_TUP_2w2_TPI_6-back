using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOListaProductos
    {
        public string producto { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public string marca { get; set; }
        public string categoria { get; set; }
        public string proveedor { get; set; }
        public string unidadMedida { get; set; }

        public List<ProductosxProveedores> prodxproveedores { get; set; }

    }

    public class ProductosxProveedores
    {
        public int idProveedor { get; set; }
    }
}
