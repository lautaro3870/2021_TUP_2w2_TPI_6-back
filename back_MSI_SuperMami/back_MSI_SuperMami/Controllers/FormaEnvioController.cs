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
            respusta.Respuesta = bd.FormaDeEnvios.Where(x => x.Estado == true).ToList();
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
                respusta.Respuesta = "Ingrese una forma de envio a dar de baja";
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

        //dar de baja
        [HttpPut]
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

        //Registrar Nueva Forma de Envío
        [HttpPost]
        [Route("formas-envio")]
        public RespuestaAPI registrarFormaEnvio([FromBody] ComandoRegistrarFormaEnvio formaEnvio)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(formaEnvio.Nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(formaEnvio.Descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            FormaDeEnvio f = new FormaDeEnvio()
            {
                Nombre = formaEnvio.Nombre,
                Descripcion = formaEnvio.Descripcion,
                Estado = true
            };

            bd.FormaDeEnvios.Add(f);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La forma de envío se cargo correctamente";
            return res;
        }
    }
}
