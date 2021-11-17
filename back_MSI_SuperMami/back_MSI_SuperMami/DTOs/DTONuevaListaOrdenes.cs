using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTONuevaListaOrdenes
    {
        public int idordendecompra { get; set; }
        public int idproveedor { get; set; }
        public int idformasEntrega { get; set; }
        public int idformasPago { get; set; }
        public int idestado { get; set; }

        public String proveedor { get; set; }

        public String formasEntrega { get; set; }

        public String formasPago { get; set; }

        public DateTime? fechaRegistro { get; set; }
        public List<DTODetalle> detalle { get; set; }

    }

    

}
