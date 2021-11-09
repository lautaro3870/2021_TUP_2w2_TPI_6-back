using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Estado { get; set; }
        public int Idrol { get; set; }
        public string NombreUsuario { get; set; }

        public virtual Role IdrolNavigation { get; set; }
    }
}
