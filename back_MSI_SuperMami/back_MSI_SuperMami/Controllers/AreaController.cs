using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.Comandos.Modificar;
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
    public class AreaController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<AreaController> _logger;

        public AreaController(ILogger<AreaController> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        [Route("areas")]
        public ActionResult<RespuestaAPI> GetAreas()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.Areas.Where(x => x.Estado == true).ToList();
            return respusta;

        }
        [HttpGet]
        [Route("areas/{id}")]
        public ActionResult<RespuestaAPI> GetAreas(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese un área a dar de baja";
                return respusta;
            }
            else
            {
                var area = bd.Areas.Find(id);
                try
                {
                    if (area != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = area;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra el área solicitada";
                    return respusta;
                }

            }

        }

        //Registrar Nueva Área
        [HttpPost]
        [Route("areas")]
        public RespuestaAPI registrarArea([FromBody] ComandoRegistrarArea area)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(area.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(area.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            Area a = new Area()
            {
                Nombre = area.nombre,
                Descripcion = area.descripcion,
                Estado = true
            };

            bd.Areas.Add(a);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "El área se cargo correctamente";
            return res;
        }

        [HttpDelete]
        [Route("areas/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Error = "No se ingresó el área";
                return res;
            }
            else
            {
                try
                {
                    var area = bd.Areas.Where(x => x.Idarea == id).FirstOrDefault();
                    if (area != null && area.Estado == true)
                    {
                        area.Estado = false;
                        res.Ok = true;
                        res.Respuesta = area;
                        res.Error = "Área dada de baja";
                        bd.Areas.Update(area);
                        bd.SaveChanges();

                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay Áreas habilitadas";
                    }

                    return res;


                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Error = "No hay Áreas habilitadas";
                    return res;
                }
            }


        }
        //Modificar area
        [HttpPut]
        [Route("areas")]
        public ActionResult<RespuestaAPI> Put([FromBody] ComandoModificarArea comando)
        {
            var res = new RespuestaAPI();

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

                
                var p = bd.Areas.Where(x => x.Idarea == comando.id).FirstOrDefault();

                if (p != null)
                {
                    p.Nombre = comando.nombre;
                    p.Descripcion = comando.descripcion;
                   

                    bd.Areas.Update(p);
                    bd.SaveChanges();

                    res.Ok = true;
                    res.Respuesta = "Área modificada";
                    return res;
                }
                else
                {
                    res.Ok = false;
                    res.Respuesta = "Área no encontrada";
                    return res;
                }
            }
            catch (Exception e)
            {
                res.Ok = false;
                res.Respuesta = "Área no encontrada";
                return res;
            }

        }
    }
}
