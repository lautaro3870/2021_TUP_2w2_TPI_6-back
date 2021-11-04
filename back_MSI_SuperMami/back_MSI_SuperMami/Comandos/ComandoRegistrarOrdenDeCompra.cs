using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos
{
    public class ComandoRegistrarOrdenDeCompra
    {
        public int proveedor { get; set; }
        public int formadeenvio { get; set; }
        public int formadepago { get; set; }
        public DateTime fechaRegistro { get; set; }

        public List<Detalle> Detalle { get; set; }
    }

    public class Detalle
    {
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public int producto { get; set; }
    }



    
}
