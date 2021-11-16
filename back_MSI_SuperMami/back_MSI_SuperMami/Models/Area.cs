using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Area
    {
        public Area()
        {
            Proveedores = new HashSet<Proveedore>();
        }

        public int Idarea { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        public virtual ICollection<Proveedore> Proveedores { get; set; }

        public bool Validar()
        {
            if (Nombre != null)
            {
                return true;
            }
            return false;
        }
    }
}
