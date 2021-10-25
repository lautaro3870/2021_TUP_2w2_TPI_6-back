using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Models
{
    public class loginResponse
    {
        public loginResponse(string token, string mail, string role)
        {
            this.token = token;
            this.mail = mail;
            this.role = role;
        }
        public string token  { get; set; }
        public string mail{ get; set; }
        public string role { get; set; }
    }
}
