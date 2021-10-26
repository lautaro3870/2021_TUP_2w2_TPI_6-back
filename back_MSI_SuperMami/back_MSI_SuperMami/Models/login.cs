using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Models
{
    public class login
    {
        public login(string usuario, string mail, string token, string rol)
        {
            this.Usuario = usuario;
            this.Mail = mail;
            this.Token = token;
            this.Rol = rol;
        }

        private string usuario;
        private string mail;
        private string token;
        private string rol;

        public string Usuario { get => usuario; set => usuario = value; }

        public string Mail { get => mail; set => mail = value; }
        public string Token { get => token; set => token = value; }
        public string Rol { get => rol; set => rol = value; }
    }
}
