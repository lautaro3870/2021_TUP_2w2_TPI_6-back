using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Role
    {
        //public Role()
        //{
        //    Usuarios = new HashSet<Usuario>();
        //}

        public int Idrol { get; set; }
        public string Rol { get; set; }
        public string Descripcion { get; set; }

        //public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
