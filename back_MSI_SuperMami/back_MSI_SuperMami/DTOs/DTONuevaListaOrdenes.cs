using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTONuevaListaOrdenes
    {
        public int Idordendecompra { get; set; }
        public int Idproveedor { get; set; }
        public int Idformadeenvio { get; set; }
        public int Idformapago { get; set; }
        public int Idestado { get; set; }
        public DateTime? FechaRegistro { get; set; }
        public List<DTODetalle> Detalle { get; set; }

    }

    

}
