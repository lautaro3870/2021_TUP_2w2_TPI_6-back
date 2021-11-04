﻿using back_MSI_SuperMami.Comandos;
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
        [Route("ordenes-compra-linq/{id}")]
        public ActionResult<RespuestaAPI> GetLinq(int id)
        {
            var respuesta = new RespuestaAPI();
            respuesta.Respuesta = bd.DetalleOrdens.
                Where(x => x.Idordendecompra == id).Sum(x => x.Precio * x.Cantidad);
            
            //var a = from p in bd.DetalleOrdens where p.Idordendecompra == id select p;
            return respuesta;
        }

        [HttpGet]
        [Route("ordenes-compra-dto/{id}")]
        public ActionResult<RespuestaAPI> GetConsulta(int id)
        {
            var respuesta = new RespuestaAPI();
            respuesta.Ok = true;
            var orden = bd.OrdenesDeCompras.FirstOrDefault(x => x.Idordendecompra == id);
            var prov = bd.Proveedores.FirstOrDefault(x => x.Idproveedor == orden.Idproveedor);
            var envio = bd.FormaDeEnvios.FirstOrDefault(x => x.Idformadeenvio == orden.Idformadeenvio);
            var pago = bd.FormaDePagos.FirstOrDefault(x => x.Idformapago == orden.Idformapago);
            var estado = bd.EstadoOrdendecompras.FirstOrDefault(x => x.Idestado == orden.Idestado);

            
            var DTO = new DTOOrdenDeCompra
            {
                Proveedor = prov.Nombre,
                FormaDeEnvio = envio.Descripcion,
                FormaDePago = pago.Descripcion,
                Estado = estado.Estado
            };

            respuesta.Respuesta = DTO;

            return respuesta;
        }

        [HttpGet]
        [Route("ordenes-compra-dto")]
        public ActionResult<RespuestaAPI> GetConsulta2()
        {
            var respuesta = new RespuestaAPI();
            respuesta.Ok = true;
            var orden = bd.OrdenesDeCompras.ToList();
            //var prov = bd.Proveedores.FirstOrDefault(x => x.Idproveedor == orden.Idproveedor);
            //var envio = bd.FormaDeEnvios.FirstOrDefault(x => x.Idformadeenvio == orden.Idformadeenvio);
            //var pago = bd.FormaDePagos.FirstOrDefault(x => x.Idformapago == orden.Idformapago);
            //var estado = bd.EstadoOrdendecompras.FirstOrDefault(x => x.Idestado == orden.Idestado);


            var lista = new List<DTOOrdenDeCompra>();

            if(orden != null)
            {
                foreach(var x in orden)
                {
                    var formadeenvio = bd.FormaDeEnvios.FirstOrDefault(f => f.Idformadeenvio == x.Idformadeenvio);
                    var formadepago = bd.FormaDePagos.FirstOrDefault(f => f.Idformapago == x.Idformapago);
                    var proveedor = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == x.Idproveedor);
                    var estado = bd.EstadoOrdendecompras.FirstOrDefault(f => f.Idestado == x.Idestado);

                    var DTO = new DTOOrdenDeCompra
                    {
                        Proveedor = proveedor.Nombre,
                        FormaDeEnvio = formadeenvio.Descripcion,
                        FormaDePago = formadepago.Descripcion,
                        Estado = estado.Estado
                    };

                    lista.Add(DTO);
                }
            }
            
            respuesta.Respuesta = lista;

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

        //[HttpPut]
        //[Route("ordenes-compra/{id}")]
        //public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarOrdenDeCompra comando)
        //{
        //    var res = new RespuestaAPI();
        //    if (id == 0)
        //    {
        //        res.Ok = false;
        //        res.Respuesta = "Ingrese una orden de compra a dar de baja";
        //        return res;
        //    }
        //    else
        //    {
        //        try
        //        {
                    
        //            if (comando.proveedor == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso el proveedor";
        //                return res;
        //            }
        //            if (comando.formadeenvio.Equals(""))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la forma de envio";
        //                return res;
        //            }

        //            if (comando.formadepago == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la forma de pago";
        //                return res;
        //            }

                   

        //            var p = bd.OrdenesDeCompras.Where(x => x.Idordendecompra == id).FirstOrDefault();

        //            if (p != null)
        //            {
        //                p.Idproveedor = comando.proveedor;
        //                p.Idformadeenvio = comando.formadeenvio;
        //                p.Idformapago = comando.formadepago;
                        

        //                bd.OrdenesDeCompras.Update(p);
        //                bd.SaveChanges();

        //                res.Ok = true;
        //                res.Respuesta = "Producto modificado";
        //                return res;
        //            }
        //            else
        //            {
        //                res.Ok = false;
        //                res.Respuesta = "Producto no encontrado";
        //                return res;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            res.Ok = false;
        //            res.Respuesta = "Producto no encontrado";
        //            return res;
        //        }
        //    }
        //}

        [HttpPost]
        [Route("ordenes-compra")]
        public RespuestaAPI PostOrden([FromBody] ComandoRegistrarOrdenDeCompra comando)
        {
            var res = new RespuestaAPI();
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

            if (comando.fechaRegistro.Equals(""))
            {
                res.Ok = false;
                res.Error = "Ingrese una fecha de registro";
                return res;
            }

            OrdenesDeCompra orden = new OrdenesDeCompra();

            orden.Idproveedor = comando.proveedor;
            orden.Idformadeenvio = comando.formadeenvio;
            orden.Idformapago = comando.formadepago;
            orden.fecha_registro = comando.fechaRegistro;
            //orden.fecha = DateTime.Now;
            orden.Idestado = 1;

            int id = orden.Idordendecompra;

            bd.OrdenesDeCompras.Add(orden);
            bd.SaveChanges();

            foreach(var detalle in comando.Detalle)
            {
                DetalleOrden d = new DetalleOrden();
                d.Cantidad = detalle.cantidad;
                d.Precio = detalle.precio;
                d.Idproducto = detalle.producto;
                d.Idordendecompra = orden.Idordendecompra;

                bd.DetalleOrdens.Add(d);
                bd.SaveChanges();

            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Orden de compra insertada correctamente";
            return res;
        }

        [HttpPut]
        [Route("ordenes-compra/{id}")]
        public RespuestaAPI PutOrdendetalle(int id, [FromBody] ComandoRegistrarOrdenDeCompra comando)
        {
            var res = new RespuestaAPI();
            OrdenesDeCompra orden = new OrdenesDeCompra();

            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una orden de compra a modificar";
                return res;
            }
            else
            {
                try
                {
                    orden = bd.OrdenesDeCompras.FirstOrDefault(x => x.Idordendecompra == id);

                    if(orden != null)
                    {
                        orden.Idproveedor = comando.proveedor;
                        orden.Idformadeenvio = comando.formadeenvio;
                        orden.Idformapago = comando.formadepago;
                        //orden.Idestado = 1;
                        id = orden.Idordendecompra;

                        bd.OrdenesDeCompras.Update(orden);
                        bd.SaveChanges();

                        foreach (var detalle in comando.Detalle)
                        {
                            DetalleOrden d = new DetalleOrden();
                            d.Cantidad = detalle.cantidad;
                            d.Precio = detalle.precio;
                            d.Idproducto = detalle.producto;
                            d.Idordendecompra = id;

                            bd.DetalleOrdens.Update(d);
                            bd.SaveChanges();

                        }
                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Orden de compra no encontrada";
                        return res;
                    }

                }
                catch
                {
                    res.Ok = false;
                    res.Respuesta = "Producto no encontrado";
                    return res;
                }
            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Orden de compra insertada correctamente";
            return res;
        }

        //[HttpPost]
        //[Route("ordenes-compra")]
        //public RespuestaAPI PostOrden([FromBody] ComandoRegistrarOrdenDeCompra comando)
        //{
        //    RespuestaAPI res = new RespuestaAPI();

        //    if (comando.proveedor == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el proveedor";
        //        return res;
        //    }
        //    if (comando.formadeenvio == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la forma de envio";
        //        return res;
        //    }

        //    if (comando.formadepago == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la forma de pago";
        //        return res;
        //    }

        //    OrdenesDeCompra orden = new OrdenesDeCompra();
        //    orden.Idproveedor = comando.proveedor;
        //    orden.Idformadeenvio = comando.formadeenvio;
        //    orden.Idformapago = comando.formadepago;
        //    orden.Idestado = 1;

        //    bd.OrdenesDeCompras.Add(orden);
        //    bd.SaveChanges();

        //    res.Ok = true;
        //    res.InfoAdicional = "Orden de compra insertada correctamente";
        //    return res;

        //}

    }



}

