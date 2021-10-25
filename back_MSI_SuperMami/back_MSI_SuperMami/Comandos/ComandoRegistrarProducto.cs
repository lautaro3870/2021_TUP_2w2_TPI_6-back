using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos
{
    public class ComandoRegistrarProducto
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime Vencimiento { get; set; }
        public int unidadMedida { get; set; }
        public int Categoria { get; set; }
        public int Marca { get; set; }



    }
}
