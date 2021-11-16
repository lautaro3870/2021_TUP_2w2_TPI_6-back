using back_MSI_SuperMami.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProjectSuperMami.PruebasUnitarias
{
    
    
        [TestClass]
        public class UnitTests
        {
            [TestMethod]
            public void ValidarArea_NombreNotNull_RetornaTrue()
            {
                var area = new Area();
                area.Nombre = "";

                bool resultado = area.Validar();
                Assert.IsTrue(resultado);

            }

            [TestMethod]
            public void ValidarProveedor_NombreDireccionTelefonoNotNull_RetornaTrue()
            {
                var prov = new Proveedore();
                prov.Nombre = "";
                prov.Direccion = "";
                prov.Telefono = "";

                bool resultado = prov.Validar();
                Assert.IsTrue(resultado);

            }

            [TestMethod]
            public void ValidarProducto_NombreDescripcionMarcaNotNull_RetornaTrue()
            {
                var prod = new Producto();
                prod.Nombre = "";
                prod.Descripcion = "";
                prod.Marca = "";

                bool resultado = prod.Validar();
                Assert.IsTrue(resultado);

            }
        }
    
}
