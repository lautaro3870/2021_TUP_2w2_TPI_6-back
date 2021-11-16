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
    public class CategoriaController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ILogger<CategoriaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("categorias")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.Categorias.Where(x => x.Estado == true).OrderBy(x => x.Nombre).ToList();
            return respusta;
        }
        [HttpGet]
        [Route("categorias/{id}")]
        public ActionResult<RespuestaAPI> Get(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese una categoria a dar de baja";
                return respusta;
            }
            else
            {
                var categoria = bd.Categorias.Find(id);
                try
                {
                    if (categoria != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = categoria;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra la categoria solicitada";
                    return respusta;
                }

            }

        }
        //Modificar categoría
        [HttpPut]
        [Route("categorias/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarCategoria comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una categoria a dar de baja";
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
                   


                    var p = bd.Categorias.Where(x => x.Idcategoria == id).FirstOrDefault();

                    if (p != null)
                    {
                        p.Nombre = comando.nombre;
                        p.Descripcion = comando.descripcion;


                        bd.Categorias.Update(p);
                        bd.SaveChanges();

                        res.Ok = true;
                        res.Respuesta = "Categoría modificada";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Categoría no encontrada";
                        return res;
                    }
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "Categoría no encontrada";
                    return res;
                }
            }
            

        }

        //dar de baja
        [HttpDelete]
        [Route("categorias/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una categoria a dar de baja";
                return res;
            }
            else
            {
                var categoria = bd.Categorias.Find(id);
                try
                {
                    if (categoria != null && categoria.Estado == true)
                    {
                        categoria.Estado = false;
                        res.Ok = true;
                        bd.Categorias.Update(categoria);
                        bd.SaveChanges();
                        res.Respuesta = "Categoria dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay categorias habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la categoria solicitada";
                    return res;
                }

            }
        }

        //Dar de alta
        [HttpPut]
        [Route("categorias/alta/{id}")]
        public ActionResult<RespuestaAPI> ActualizarEstado(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una categoría a dar de alta";
                return res;
            }
            else
            {
                var cat = bd.Categorias.Find(id);
                try
                {
                    if (cat != null && cat.Estado == false)
                    {
                        cat.Estado = true;
                        res.Ok = true;
                        bd.Categorias.Update(cat);
                        bd.SaveChanges();
                        res.Respuesta = "Categoría dada de alta";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No existe esa categoría deshabilitada";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la categoría solicitada";
                    return res;
                }

            }
        }

        //Registrar Nueva Categoría
        [HttpPost]
        [Route("categorias")]
        public RespuestaAPI registrarCategoria([FromBody] ComandoRegistrarCategoria categoria)
        {
            RespuestaAPI res = new RespuestaAPI();

            if (string.IsNullOrEmpty(categoria.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(categoria.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            Categoria c = new Categoria()
            {
                Nombre = categoria.nombre,
                Descripcion = categoria.descripcion,
                Estado = true
            };

            bd.Categorias.Add(c);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La categoría se cargo correctamente";
            return res;
        }
    }
}
