using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos.Modificar
{
    public class ComandoModificarProveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string CUIT { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public int Area { get; set; }
    }
}
