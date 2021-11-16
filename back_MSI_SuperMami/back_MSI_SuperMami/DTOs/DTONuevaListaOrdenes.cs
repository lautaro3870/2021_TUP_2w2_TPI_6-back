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
        public List<ListadoDetalles> Detalle { get; set; }

    }

    public class ListadoDetalles
    {
        public int idproducto { get; set; }
        //public string nombre { get; set; }
        //public string descripcion { get; set; }
        //public string marca { get; set; }
        //public string categoria { get; set; }
        //public string unidadMedida { get; set; }
        //public string imagen { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
    }

}
