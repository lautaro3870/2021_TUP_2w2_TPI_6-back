﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Comandos
{
    public class ComandoRegistrarFormaPago
    {
        public string nombre { get; set; }
        public string descripcion { get; set; }

        public int idformadepago { get; set; }

        public float porcentaje { get; set; }

    }
}
