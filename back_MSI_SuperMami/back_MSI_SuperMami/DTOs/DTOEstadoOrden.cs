using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOEstadoOrden
    {
        public List<DTOOrdenDeCompraListado> listaAceptadas { get; set; }
        public List<DTOOrdenDeCompraListado> listaRechazadas { get; set; }
        public List<DTOOrdenDeCompraListado> listaPendientes { get; set; }


    }
}
