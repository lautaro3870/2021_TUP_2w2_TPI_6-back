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
    public class FormaPagoController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<FormaPagoController> _logger;

        public FormaPagoController(ILogger<FormaPagoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("formas-pago")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.FormaDePagos.Where(x => x.Estado == true).ToList();
            return respusta;
        }

        [HttpGet]
        [Route("formas-pago/{id}")]
        public ActionResult<RespuestaAPI> GetFormasPago(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese una forma de pago a dar de baja";
                return respusta;
            }
            else
            {
                var formaPago = bd.FormaDePagos.Find(id);
                try
                {
                    if (formaPago != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = formaPago;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra la forma de pago solicitada";
                    return respusta;
                }

            }

        }

        //Modificar
        [HttpPut]
        [Route("formas-pago/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarFormaPago comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de pago a dar de baja";
                return res;
            }
            else
            {
                try
                {
                    if (string.IsNullOrEmpty(comando.nombre))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso el nombre";
                        return res;
                    }
                    if (string.IsNullOrEmpty(comando.descripcion))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la descripción";
                        return res;
                    }


                    var p = bd.FormaDePagos.Where(x => x.Idformapago == id).FirstOrDefault();

                    if (p != null)
                    {
                        p.Nombre = comando.nombre;
                        p.Descripcion = comando.descripcion;


                        bd.FormaDePagos.Update(p);
                        bd.SaveChanges();

                        res.Ok = true;
                        res.Respuesta = "Forma de pago modificada";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Forma de pago no encontrada";
                        return res;
                    }
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "Forma de pago no encontrada";
                    return res;
                }
            }
            

        }

        //dar de baja
        [HttpDelete]
        [Route("formas-pago/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de pago a dar de baja";
                return res;
            }
            else
            {
                var pago = bd.FormaDePagos.Find(id);
                try
                {
                    if (pago != null && pago.Estado == true)
                    {
                        pago.Estado = false;
                        res.Ok = true;
                        bd.FormaDePagos.Update(pago);
                        bd.SaveChanges();
                        res.Respuesta = "Forma de pago dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay formas de pago habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la forma de pago solicitada";
                    return res;
                }

            }
        }

        //Registrar Nueva Forma de Pago
        [HttpPost]
        [Route("formas-pago")]
        public RespuestaAPI registrarFormaPago([FromBody] ComandoRegistrarFormaPago formaPago)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(formaPago.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(formaPago.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            FormaDePago  f = new FormaDePago()
            {
                Nombre = formaPago.nombre,
                Descripcion = formaPago.descripcion,
                Estado = true
            };

            bd.FormaDePagos.Add(f);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La Forma de Pago se cargo correctamente";
            return res;
        }

        //Método para obtener las formas de pago que tiene un proveedor pasado como parámetro

        [HttpGet]
        [Route("formas-pago/proveedor/{id}")]
        public ActionResult<RespuestaAPI> GetFormasXProveedor(int id)
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var forXpro = bd.Proveedoresxformasdepagos.Where(f => f.Idproveedor == id).ToList();

            List<FormaDePago> lista = new List<FormaDePago>();

            if (forXpro.Count != 0)
            {

                foreach (var i in forXpro)
                {
                    var formas = bd.FormaDePagos.FirstOrDefault(f => f.Idformapago == i.Idformapago);
                    lista.Add(formas);
                }
                respuestas.Respuesta = lista;
            }
            return respuestas;
        }
    }
}
