using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos
{
    public class ComandoRegistrarProducto
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }
        
        public int unidadMedida { get; set; }
        public int categoria { get; set; }
        public string marca { get; set; }

        public string imagen { get; set; }

        //public List<Proveedores> Proveedores { get; set; }

    }

    //public class Proveedores
    //{
    //    //public int producto { get; set; }
    //    public int proveedor { get; set; }
    //}
}
