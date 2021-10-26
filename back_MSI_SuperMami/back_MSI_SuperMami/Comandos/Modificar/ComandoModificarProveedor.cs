using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos.Modificar
{
    public class ComandoModificarProveedor
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int area { get; set; }
    }
}
