using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos
{
    public class ComandoRegistrarProveedor
    {
        public string nombre { get; set; }
        public string direccion { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int area { get; set; }

        public List<FormaDeEnvios> FormasDeEnvio { get; set; }
        public List<FormaDePagos> FormasDePago { get; set; }

    }

    public class FormaDeEnvios
    {
        public int envio { get; set; }
    }

    public class FormaDePagos
    {
        public int pago { get; set; }
    }
}
