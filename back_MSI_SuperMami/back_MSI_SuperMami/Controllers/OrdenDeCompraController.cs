using back_MSI_SuperMami.Comandos;
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
    public class OrdenDeCompraController
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<OrdenDeCompraController> _logger;

        public OrdenDeCompraController(ILogger<OrdenDeCompraController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("ordenes-compra")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuesta = new RespuestaAPI();
            respuesta.Ok = true;
            respuesta.Respuesta = bd.OrdenesDeCompras.Where(x => x.Idestado == 3).ToList();
            return respuesta;
        }

        [HttpGet]
        [Route("ordenes-compra/{id}")]
        public ActionResult<RespuestaAPI> GetPorId(int id)
        {
            var respuesta = new RespuestaAPI();
            if (id.Equals(0))
            {
                respuesta.Ok = false;
                respuesta.Respuesta = "Ingrese una orden de compra";
                return respuesta;
            }
            else
            {
                var orden = bd.OrdenesDeCompras.Find(id);
                try
                {
                    if (orden != null)
                    {
                        respuesta.Ok = true;
                        respuesta.Respuesta = orden;
                        return respuesta;
                    }
                    return respuesta;
                }
                catch
                {
                    respuesta.Ok = false;
                    respuesta.Respuesta = "No se encuentra la orden de compra solicitada";
                    return respuesta;
                }
            }
        }

        [HttpPut]
        [Route("ordenes-compra/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarOrdenDeCompra comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una orden de compra a dar de baja";
                return res;
            }
            else
            {
                try
                {
                    
                    if (comando.proveedor == 0)
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso el proveedor";
                        return res;
                    }
                    if (comando.formadeenvio.Equals(""))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la forma de envio";
                        return res;
                    }

                    if (comando.formadepago == 0)
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la forma de pago";
                        return res;
                    }

                   

                    var p = bd.OrdenesDeCompras.Where(x => x.Idordendecompra == id).FirstOrDefault();

                    if (p != null)
                    {
                        p.Idproveedor = comando.proveedor;
                        p.Idformadeenvio = comando.formadeenvio;
                        p.Idformapago = comando.formadepago;
                        

                        bd.OrdenesDeCompras.Update(p);
                        bd.SaveChanges();

                        res.Ok = true;
                        res.Respuesta = "Producto modificado";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Producto no encontrado";
                        return res;
                    }
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "Producto no encontrado";
                    return res;
                }
            }


        }


        [HttpPost]
        [Route("ordenes-compra")]
        public RespuestaAPI PostOrden([FromBody] ComandoRegistrarOrdenDeCompra comando)
        {
            RespuestaAPI res = new RespuestaAPI();

            if (comando.proveedor == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el proveedor";
                return res;
            }
            if (comando.formadeenvio == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso la forma de envio";
                return res;
            }

            if (comando.formadepago == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso la forma de pago";
                return res;
            }

            OrdenesDeCompra orden = new OrdenesDeCompra();
            orden.Idproveedor = comando.proveedor;
            orden.Idformadeenvio = comando.formadeenvio;
            orden.Idformapago = comando.formadepago;
            orden.Idestado = 1;

            bd.OrdenesDeCompras.Add(orden);
            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Orden de compra insertada correctamente";
            return res;

        }

    }



}

