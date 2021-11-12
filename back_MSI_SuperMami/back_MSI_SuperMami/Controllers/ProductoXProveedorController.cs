using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.DTOs;
using back_MSI_SuperMami.Models;
using back_MSI_SuperMami.Respuestas;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Controllers
{
    [ApiController]
    [EnableCors("MSI2021")]
    public class ProductoXProveedorController : ControllerBase
    {
        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<ProductoXProveedorController> _logger;

        public ProductoXProveedorController(ILogger<ProductoXProveedorController> logger)
        {
            _logger = logger;
        }
        //[HttpGet]
        //[Route("producto-proveedor")]
        //public ActionResult<RespuestaAPI> Get()
        //{
        //    var respuestas = new RespuestaAPI();
        //    respuestas.Ok = true;
        //    respuestas.Respuesta = bd.Productosxproveedores.Where(x => x.Estado == true).ToList();
        //    return respuestas;
        //}
        [HttpGet]
        [Route("producto-proveedor")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var pro = bd.Productosxproveedores.ToList();

            var lista = new List<DTOProductoProveedor>();

            if (pro != null)
            {
                foreach (var i in pro)
                {
                    var proxProv = bd.Productosxproveedores.FirstOrDefault(f => f.Idproductoproveedor == i.Idproductoproveedor);
                    var prov = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == i.Idproveedor);
                    var prod = bd.Productos.FirstOrDefault(f => f.Idproducto == i.Idproducto);



                    var dto = new DTOProductoProveedor
                    {
                        id = proxProv.Idproductoproveedor,
                       
                        proveedor = prov.Nombre,
                        producto = prod.Nombre,
                        precio = proxProv.Precio

                    };
                    lista.Add(dto);
                }

                respuestas.Respuesta = lista;
                return respuestas;
            }

            respuestas.Respuesta = bd.Productosxproveedores.Where(x => x.Estado == true).ToList();
            return respuestas;
        }


        [HttpPost]
        [Route("producto-proveedor")]
        public RespuestaAPI PostOrden([FromBody] ComandoProductoxProveedor comando)
        {
            var res = new RespuestaAPI();
            if (comando.proveedor == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el proveedor";
                return res;
            }
            if (comando.producto == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el producto";
                return res;
            }

            if (comando.precio == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el precio";
                return res;
            }



            Productosxproveedore orden = new Productosxproveedore();

            orden.Idproveedor = comando.proveedor;
            orden.Idproducto = comando.producto;
            orden.Precio = comando.precio;
            //orden.fecha = DateTime.Now;
            orden.Estado = true;

            int id = orden.Idproductoproveedor;

            bd.Productosxproveedores.Add(orden);
            bd.SaveChanges();



            res.Ok = true;
            res.InfoAdicional = "Asignación entro producto y proveedor insertada correctamente";
            return res;
        }
    }
}
