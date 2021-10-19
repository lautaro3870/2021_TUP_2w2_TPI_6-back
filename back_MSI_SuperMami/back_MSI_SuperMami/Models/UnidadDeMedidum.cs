using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class UnidadDeMedidum
    {
        public UnidadDeMedidum()
        {
            Productos = new HashSet<Producto>();
        }

        public int Idunidadmedida { get; set; }
        public string Nombre { get; set; }
        public string Descipcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Producto> Productos { get; set; }
    }
}
