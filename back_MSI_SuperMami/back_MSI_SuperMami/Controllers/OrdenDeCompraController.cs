using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.DTOs;
using back_MSI_SuperMami.Helpers;
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

        
        //[HttpGet]
        //[Route("ordenes-compra")]
        //public ActionResult<RespuestaAPI> Get()
        //{
        //    var respuesta = new RespuestaAPI();

        //    var orden = bd.OrdenesDeCompras.ToList();


        //    var listaAceptadas = new List<DTOOrdenDeCompraListado>();
        //    var listaRechadas = new List<DTOOrdenDeCompraListado>();
        //    var listaPendientes = new List<DTOOrdenDeCompraListado>();
        //    if (orden != null)
        //    {
        //        foreach (var i in orden)
        //        {
        //            var o = bd.OrdenesDeCompras.FirstOrDefault(f => f.Idordendecompra == i.Idordendecompra);
        //            var prov = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == i.Idproveedor);
        //            var envio = bd.FormaDeEnvios.FirstOrDefault(f => f.Idformadeenvio == i.Idformadeenvio);
        //            var pago = bd.FormaDePagos.FirstOrDefault(f => f.Idformapago == i.Idformapago);
        //            var detalle = bd.DetalleOrdens.Where(f => f.Idordendecompra == i.Idordendecompra).ToList();

        //            var detalle2 = bd.DetalleOrdens.FirstOrDefault(f => f.Idordendecompra == i.Idordendecompra);


        //            Producto producto = null;
        //            List<Producto> listaProductos = new List<Producto>();

        //            if (detalle.Count != 0)
        //            {
        //                foreach (var x in detalle)
        //                {
        //                    producto = bd.Productos.FirstOrDefault(f => f.Idproducto == x.Idproducto);
        //                    listaProductos.Add(producto);
        //                }

        //                foreach (var y in listaProductos)
        //                {
        //                    if (o.Idestado == 1)
        //                    {
        //                        var aceptadas = new DTOOrdenDeCompraListado
        //                        {
        //                            proveedor = prov.Nombre,
        //                            formaPago = pago.Nombre,
        //                            formaEnvio = envio.Nombre
        //                        };
        //                        listaAceptadas.Add(aceptadas);
        //                    }
        //                    else
        //                    if (o.Idestado == 2)
        //                    {
        //                        var Rechazadas = new DTOOrdenDeCompraListado
        //                        {
        //                            proveedor = prov.Nombre,
        //                            formaPago = pago.Nombre,
        //                            formaEnvio = envio.Nombre
        //                        };
        //                        listaRechadas.Add(Rechazadas);
        //                    }
        //                    else
        //                    if(o.Idestado == 3)
        //                    {
        //                        var Pendientes = new DTOOrdenDeCompraListado
        //                        {
        //                            proveedor = prov.Nombre,
        //                            formaPago = pago.Nombre,
        //                            formaEnvio = envio.Nombre
        //                        };
        //                        listaPendientes.Add(Pendientes);
        //                    }
        //                    else
        //                    {
        //                        respuesta.Error = "No existe la orden de compra";
        //                    }
        //                }
        //            }

        //        }
        //        var listaOrdenesPorEstado = new DTOEstadoOrden
        //        {
        //            listaAceptadas = listaAceptadas,
        //            listaPendientes = listaPendientes,
        //            listaRechazadas = listaRechadas
        //        };

        //        respuesta.Respuesta = listaOrdenesPorEstado;

        //        return respuesta;
        //    }

        //    respuesta.Ok = true;

        //    return respuesta;
        //}

        [HttpGet]
        [Route("ordenes-compra")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuesta = new RespuestaAPI();
            Metodos m = new Metodos();
            var orden = bd.OrdenesDeCompras.ToList();


            var lista = new List<DTONuevaListaOrdenes>();
            
            if (orden != null)
            {
                foreach (var i in orden)
                {

                    var o = bd.OrdenesDeCompras.FirstOrDefault(f => f.Idordendecompra == i.Idordendecompra);
                    var prov = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == i.Idproveedor);
                    var pagos = bd.FormaDePagos.FirstOrDefault(f => f.Idformapago == i.Idformapago);
                    var entregas = bd.FormaDeEnvios.FirstOrDefault(f => f.Idformadeenvio == i.Idformadeenvio);
                    var detalles = bd.DetalleOrdens.Where(y => y.Idordendecompra == i.Idordendecompra).ToList();

                    
                    var listaDetalle = m.obtenerDetallesOrdenes(o.Idordendecompra);
                    var dto = new DTONuevaListaOrdenes
                    {
                        idordendecompra = o.Idordendecompra,
                        idproveedor = o.Idproveedor,
                        idformasPago = o.Idformapago,
                        idformasEntrega = o.Idformadeenvio,
                        proveedor = prov.Nombre,
                        formasEntrega = entregas.Nombre,
                        formasPago = pagos.Nombre,
                        idestado = o.Idestado,
                        fechaRegistro = o.FechaRegistro,
                        detalle = listaDetalle
                    };

                    lista.Add(dto);
                }

                respuesta.Respuesta = lista.OrderByDescending(x=>x.idestado).OrderBy(x => x.idordendecompra);
                return respuesta;
            }
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
        [Route("ordenes-compra-reporte")]
        public ActionResult<RespuestaAPI> GetReporte()
        {
            var respuesta = new RespuestaAPI();
            respuesta.Ok = true;
            var orden = bd.OrdenesDeCompras.ToList();

            var lista = new List<DTOReporteOrden>();

            if (orden != null)
            {
                foreach (var x in orden)
                {
                    var detalle = bd.DetalleOrdens.FirstOrDefault(f => f.Idordendecompra == x.Idordendecompra);
                    var prov = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == x.Idproveedor);
                    var estado = bd.EstadoOrdendecompras.FirstOrDefault(f => f.Idestado == x.Idestado);
                    var sum = bd.DetalleOrdens.Sum(f => f.Cantidad * f.Precio);

                    var dto = new DTOReporteOrden
                    {
                        proveedor = prov.Nombre,
                        suma = sum
                    };
                    lista.Add(dto);

                }

            }
            respuesta.Respuesta = lista;
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

            if (orden != null)
            {
                foreach (var x in orden)
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

            OrdenesDeCompra orden = new OrdenesDeCompra();

            orden.Idproveedor = comando.proveedor;
            orden.Idformadeenvio = comando.formadeenvio;
            orden.Idformapago = comando.formadepago;
            orden.FechaRegistro = DateTime.Now;
            orden.Idestado = 3;

            //int id = orden.Idordendecompra;

            bd.OrdenesDeCompras.Add(orden);
            bd.SaveChanges();

            foreach (var detalle in comando.Detalle)
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

                    if (orden != null)
                    {
                        orden.Idproveedor = comando.proveedor;
                        orden.Idformadeenvio = comando.formadeenvio;
                        orden.Idformapago = comando.formadepago;
                        //orden.Idestado = 1;
                        id = orden.Idordendecompra;

                        bd.OrdenesDeCompras.Update(orden);
                        bd.SaveChanges();

                        if (comando.Detalle != null)
                        {
                            var detalles = bd.DetalleOrdens.Where(f => f.Idordendecompra == id).ToList();
                            foreach (var i in detalles)
                            {
                                bd.DetalleOrdens.Remove(i);
                            }

                            foreach (var detalle in comando.Detalle)
                            {
                                DetalleOrden d = new DetalleOrden();
                                d.Cantidad = detalle.cantidad;
                                d.Precio = detalle.precio;
                                d.Idproducto = detalle.producto;
                                d.Idordendecompra = id;

                                bd.DetalleOrdens.Add(d);
                                bd.SaveChanges();

                            }
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
                    res.Respuesta = "Orden de compra no encontrada";
                    return res;
                }
            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Orden de compra modificada correctamente";
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

        //Modificar Estado a Aceptado
        //[HttpPut]
        //[Route("orden-compra/aceptar/{id}")]
        //public ActionResult<RespuestaAPI> OrdenAceptada(int id)
        //{
        //    var res = new RespuestaAPI();
        //    if (id == 0)
        //    {
        //        res.Ok = false;
        //        res.Respuesta = "Ingrese una orden de compra";
        //        return res;
        //    }
        //    else
        //    {
        //        var orden = bd.OrdenesDeCompras.Find(id);
        //        try
        //        {
        //            if (orden != null && orden.Idestado == 3)
        //            {
        //                orden.Idestado = 1;
        //                res.Ok = true;
        //                bd.OrdenesDeCompras.Update(orden);
        //                bd.SaveChanges();
        //                res.Respuesta = "Orden de compra estado modificado a aceptado";
        //                return res;
        //            }
        //            else
        //            {
        //                res.Ok = false;
        //                res.Error = "No es una orden de compra pendiente";
        //            }

        //            return res;
        //        }
        //        catch (Exception e)
        //        {
        //            res.Ok = false;
        //            res.Respuesta = "No se encuentra la orden de compra enviada";
        //            return res;
        //        }

        //    }

        //}

        //Modificar Estado a Cancelado
        //[HttpPut]
        //[Route("orden-compra/cancelar/{id}")]
        //public ActionResult<RespuestaAPI> OrdenCancelada(int id)
        //{
        //    var res = new RespuestaAPI();
        //    if (id == 0)
        //    {
        //        res.Ok = false;
        //        res.Respuesta = "Ingrese una orden de compra";
        //        return res;
        //    }
        //    else
        //    {
        //        var orden = bd.OrdenesDeCompras.Find(id);
        //        try
        //        {
        //            if (orden != null && orden.Idestado == 3)
        //            {
        //                orden.Idestado = 2;
        //                res.Ok = true;
        //                bd.OrdenesDeCompras.Update(orden);
        //                bd.SaveChanges();
        //                res.Respuesta = "Orden de compra estado modificado a cancelado";
        //                return res;
        //            }
        //            else
        //            {
        //                res.Ok = false;
        //                res.Error = "No es una orden de compra pendiente";
        //            }

        //            return res;
        //        }
        //        catch (Exception e)
        //        {
        //            res.Ok = false;
        //            res.Respuesta = "No se encuentra la orden de compra enviada";
        //            return res;
        //        }

        //    }
        //}


        //Modificar Estado a Aceptado
        [HttpPut]
        [Route("orden-compra/estado/{id}")]
        public ActionResult<RespuestaAPI> modificarEstado(int id, [FromBody] int idestado)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "No enviaste el estado";
                return res;
            }
            else
            {
                var orden = bd.OrdenesDeCompras.Find(id);
                try
                {
                    if (orden != null && orden.Idestado == 3)
                    {
                        if(idestado==1)
                        {
                            orden.Idestado = 1;
                            res.Ok = true;
                            bd.OrdenesDeCompras.Update(orden);
                            bd.SaveChanges();
                            res.Respuesta = "Se aceptó la orden de compra";
                        }else if(idestado==2)
                        {
                            orden.Idestado = 2;
                            res.Ok = true;
                            bd.OrdenesDeCompras.Update(orden);
                            bd.SaveChanges();
                            res.Respuesta = "Se rechazó la orden de compra";
                        }
                        
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No es una orden de compra pendiente";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la orden de compra enviada";
                    return res;
                }

            }

        }

    }





}

