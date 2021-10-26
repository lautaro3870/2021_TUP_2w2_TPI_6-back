using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos.Modificar
{
    public class ComandoModificarProducto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public decimal precio { get; set; }
        public int marca { get; set; }
        public DateTime vencimiento { get; set; }
        public int unidadMedida { get; set; }
        public int categoria { get; set; }
        

    }
}
