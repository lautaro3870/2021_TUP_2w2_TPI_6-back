using back_MSI_SuperMami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.DTOs
{
    public class DTOProveedoresPut
    {

        public string nombre { get; set; }
        public string direccion { get; set; }
        public string cuit { get; set; }
        public string telefono { get; set; }
        public string email { get; set; }
        public int idarea { get; set; }

        public List<Productos> Productos { get; set; }

        public List<int> formasPago { get; set; }

        public List<int> formasEntrega { get; set; }

    }

    public class Productos
    {
        public int idproducto { get; set; }

        public double precio { get; set; }

    }

    //public class Productos
    //{
    //    public double[,] arreglo { get; set; }
    //}

    //public class Pagos
    //{
    //    public int formasPago { get; set; }
    //}

    //public class Entregas
    //{
    //    public int formasEntrega { get; set; }
    //}

}
