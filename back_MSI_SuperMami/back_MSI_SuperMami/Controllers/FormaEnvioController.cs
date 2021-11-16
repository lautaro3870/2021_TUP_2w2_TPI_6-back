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
    public class FormaEnvioController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<FormaEnvioController> _logger;

        public FormaEnvioController(ILogger<FormaEnvioController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("formas-envio")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.FormaDeEnvios.Where(x => x.Estado == true).OrderBy(x => x.Nombre).ToList();
            return respusta;
        }

        [HttpGet]
        [Route("formas-envio-baja")]
        public ActionResult<RespuestaAPI> GetFormsBaja()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.FormaDeEnvios.Where(x => x.Estado == false).OrderBy(x => x.Nombre).ToList();
            return respusta;
        }

        [HttpGet]
        [Route("formas-envio/{id}")]
        public ActionResult<RespuestaAPI> GetFormasEnvio(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese una forma de envio";
                return respusta;
            }
            else
            {
                var formaEnvio = bd.FormaDeEnvios.Find(id);
                try
                {
                    if (formaEnvio != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = formaEnvio;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra la forma de envio solicitada";
                    return respusta;
                }

            }

        }

        //Modificar
        [HttpPut]
        [Route("formas-envio/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarFormaEnvio comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de envio a modificar";
                return res;
            }
            try
            {
                if (string.IsNullOrEmpty(comando.nombre))
                {
                    res.Ok = false;
                    res.Error = "No se ingreso el nombre";
                    return res;
                }
                


                var p = bd.FormaDeEnvios.Where(x => x.Idformadeenvio == id).FirstOrDefault();

                if (p != null)
                {
                    p.Nombre = comando.nombre;
                    p.Descripcion = comando.descripcion;


                    bd.FormaDeEnvios.Update(p);
                    bd.SaveChanges();

                    res.Ok = true;
                    res.Respuesta = "Forma de envio modificada";
                    return res;
                }
                else
                {
                    res.Ok = false;
                    res.Respuesta = "Forma de envio no encontrada";
                    return res;
                }
            }
            catch (Exception e)
            {
                res.Ok = false;
                res.Respuesta = "Forma de envio no encontrada";
                return res;
            }

        }

        //dar de baja
        [HttpDelete]
        [Route("formas-envio/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de envio a dar de baja";
                return res;
            }
            else
            {
                var envio = bd.FormaDeEnvios.Find(id);
                try
                {
                    if (envio != null && envio.Estado == true)
                    {
                        envio.Estado = false;
                        res.Ok = true;
                        bd.FormaDeEnvios.Update(envio);
                        bd.SaveChanges();
                        res.Respuesta = "Forma de envio dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay formas de envio habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la forma de envio solicitada";
                    return res;
                }

            }
        }

        //Dar de alta
        [HttpPut]
        [Route("formas-envio/alta/{id}")]
        public ActionResult<RespuestaAPI> ActualizarEstado(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de envío a dar de alta";
                return res;
            }
            else
            {
                var pago = bd.FormaDeEnvios.Find(id);
                try
                {
                    if (pago != null && pago.Estado == false)
                    {
                        pago.Estado = true;
                        res.Ok = true;
                        bd.FormaDeEnvios.Update(pago);
                        bd.SaveChanges();
                        res.Respuesta = "Forma de envío dada de alta";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No existe esa forma de envío deshabilitada";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la forma de envío solicitada";
                    return res;
                }

            }
        }

        //Registrar Nueva Forma de Envío
        [HttpPost]
        [Route("formas-envio")]
        public RespuestaAPI registrarFormaEnvio([FromBody] ComandoRegistrarFormaEnvio formaEnvio)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(formaEnvio.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(formaEnvio.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            FormaDeEnvio f = new FormaDeEnvio()
            {
                Nombre = formaEnvio.nombre,
                Descripcion = formaEnvio.descripcion,
                Estado = true
            };

            bd.FormaDeEnvios.Add(f);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La forma de envío se cargo correctamente";
            return res;
        }

        //Método para obtener las formas de pago que tiene un proveedor pasado como parámetro

        [HttpGet]
        [Route("formas-envio/proveedor/{id}")]
        public ActionResult<RespuestaAPI> GetFormasXProveedor(int id)
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var forXpro = bd.Proveedoresxformadeenvios.Where(f => f.Idproveedor == id).ToList();

            var lista = new List<ComandoRegistrarFormaEnvio>();

            if (forXpro != null)
            {

                foreach (var i in forXpro)
                {
                    var f = bd.FormaDeEnvios.FirstOrDefault(f => f.Idformadeenvio == i.Idformadeenvio);

                    if (f.Estado == true)
                    {
                        var forma = new ComandoRegistrarFormaEnvio
                        {
                            nombre = f.Nombre,
                            descripcion = f.Descripcion,
                            idformadeenvio = f.Idformadeenvio
                        };

                        lista.Add(forma);
                    }
                }
                respuestas.Respuesta = lista.OrderBy(x => x.nombre);
                return respuestas;
            }

            return respuestas;
        }
    }
}
