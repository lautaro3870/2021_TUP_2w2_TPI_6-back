using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOProductosPrecioProveedor
    {
        public int idproducto { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public String unidadMedida { get; set; }
        public String categoria { get; set; }
        public string marca { get; set; }

        public string imagen { get; set; }

        public double precio { get; set; }

    }
}
