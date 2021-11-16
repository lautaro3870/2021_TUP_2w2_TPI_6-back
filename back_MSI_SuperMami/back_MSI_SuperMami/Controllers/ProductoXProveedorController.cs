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
        [Route("producto/proveedor")]
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


                    if (proxProv.Estado == true)
                    {
                        var dto = new DTOProductoProveedor
                        {
                            id = proxProv.Idproductoproveedor,

                            proveedor = prov.Nombre,
                            producto = prod.Nombre,
                            precio = proxProv.Precio

                        };
                        lista.Add(dto);
                    }
                }

                respuestas.Respuesta = lista.OrderBy(f => f.proveedor); 
                return respuestas;
            }

            return respuestas;
        }


        [HttpGet]
        [Route("producto/proveedor/{id}")]
        public ActionResult<RespuestaAPI> Get(int id)
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var proXpro = bd.Productosxproveedores.Where(f => f.Idproveedor == id).ToList();
          
            var lista = new List<DTOProductosPrecioProveedor>();

            if (proXpro != null)
            {
                foreach (var i in proXpro)
                {
                    var proxProv = bd.Productosxproveedores.FirstOrDefault(f => f.Idproductoproveedor == i.Idproductoproveedor);
                    var prod = bd.Productos.FirstOrDefault(f => f.Idproducto == i.Idproducto);
                    var uni = bd.UnidadDeMedida.FirstOrDefault(f => f.Idunidadmedida == prod.Idunidadmedida);
                    var cat = bd.Categorias.FirstOrDefault(f => f.Idcategoria == prod.Idcategoria);

                    if (proxProv.Estado == true)
                    {
                        var dto = new DTOProductosPrecioProveedor
                        {
                            idproducto = prod.Idproducto,
                            nombre = prod.Nombre,
                            descripcion = prod.Descripcion,
                            unidadMedida = uni.Nombre,
                            categoria = cat.Nombre,
                            marca = prod.Marca,
                            imagen = prod.Imagen,
                            precio = proxProv.Precio
                        };

                        lista.Add(dto);
                    }
                }

                respuestas.Respuesta = lista.OrderBy(f => f.nombre); ;
                return respuestas;
            }

            respuestas.Respuesta = bd.Productosxproveedores.Where(x => x.Estado == true).ToList();
            return respuestas;
        }

        [HttpPost]
        [Route("producto/proveedor")]
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

        //dar de baja
        [HttpDelete]
        [Route("producto/proveedor/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una producto/proveedor a dar de baja";
                return res;
            }
            else
            {
                var pago = bd.Productosxproveedores.Find(id);
                try
                {
                    if (pago != null && pago.Estado == true)
                    {
                        pago.Estado = false;
                        res.Ok = true;
                        bd.Productosxproveedores.Update(pago);
                        bd.SaveChanges();
                        res.Respuesta = "Producto/proveedor dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay producto/proveedor habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra producto/proveedor solicitada";
                    return res;
                }

            }
        }
        //Dar de alta
        [HttpPut]
        [Route("producto/proveedor/alta/{id}")]
        public ActionResult<RespuestaAPI> ActualizarEstado(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese un producto/proveedor a dar de alta";
                return res;
            }
            else
            {
                var pago = bd.Productosxproveedores.Find(id);
                try
                {
                    if (pago != null && pago.Estado == false)
                    {
                        pago.Estado = true;
                        res.Ok = true;
                        bd.Productosxproveedores.Update(pago);
                        bd.SaveChanges();
                        res.Respuesta = "producto/proveedor dada de alta";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No existe esa producto/proveedor deshabilitada";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra producto/proveedor solicitada";
                    return res;
                }

            }
        }


    }
}
