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
    public class UnidadMedidaController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<UnidadMedidaController> _logger;

        public UnidadMedidaController(ILogger<UnidadMedidaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("unidad-medidas")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.UnidadDeMedida.Where(x => x.Estado == true).ToList();
            return respusta;
        }

        [HttpGet]
        [Route("unidad-medidas/{id}")]
        public ActionResult<RespuestaAPI> GetUnidades(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese una unidad de medida a dar de baja";
                return respusta;
            }
            else
            {
                var unidad = bd.UnidadDeMedida.Find(id);
                try
                {
                    if (unidad != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = unidad;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra la unidad de medida solicitada";
                    return respusta;
                }

            }

        }


        //Modificar
        [HttpPut]
        [Route("unidad-medidas/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarUnidadMedida comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una unidad de medida a dar de baja";
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
                    


                    var p = bd.UnidadDeMedida.Where(x => x.Idunidadmedida == id).FirstOrDefault();

                    if (p != null)
                    {
                        p.Nombre = comando.nombre;
                        p.Descipcion = comando.descripcion;


                        bd.UnidadDeMedida.Update(p);
                        bd.SaveChanges();

                        res.Ok = true;
                        res.Respuesta = "Unidad de medida modificada";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Unidad de medida no encontrada";
                        return res;
                    }
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "Unidad de medida no encontrada";
                    return res;
                }
            }
            

        }

        //dar de baja
        [HttpDelete]
        [Route("unidad-medidas/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una unidad de medida a dar de baja";
                return res;
            }
            else
            {
                var unidadMedida = bd.UnidadDeMedida.Find(id);
                try
                {
                    if (unidadMedida != null && unidadMedida.Estado == true)
                    {
                        unidadMedida.Estado = false;
                        res.Ok = true;
                        bd.UnidadDeMedida.Update(unidadMedida);
                        bd.SaveChanges();
                        res.Respuesta = "Unidad de medida dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay unidades de medida habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la unidad de medida solicitada";
                    return res;
                }

            }
        }

        //Registrar Nueva Unidad de Medida
        [HttpPost]
        [Route("unidad-medidas")]
        public RespuestaAPI registrarUnidadMedida([FromBody] ComandoRegistrarUnidadMedida unidadMedida)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(unidadMedida.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(unidadMedida.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            UnidadDeMedidum  u = new UnidadDeMedidum()
            {
                Nombre = unidadMedida.nombre,
                Descipcion = unidadMedida.descripcion,
                Estado = true
            };

            bd.UnidadDeMedida.Add(u);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La Unidad de Medida se cargo correctamente";
            return res;
        }
    }
}
