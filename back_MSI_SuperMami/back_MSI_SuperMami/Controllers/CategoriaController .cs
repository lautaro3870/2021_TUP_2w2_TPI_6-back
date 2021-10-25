﻿using back_MSI_SuperMami.Comandos;
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
            respusta.Respuesta = bd.Categorias.Where(x => x.Estado == true).ToList();
            return respusta;
        }


        //dar de baja
        [HttpPut]
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

        //Registrar Nueva Categoría
        [HttpPost]
        [Route("categorias")]
        public RespuestaAPI registrarCategoria([FromBody] ComandoRegistrarCategoria categoria)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(categoria.Nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(categoria.Descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            Categoria  c = new Categoria()
            {
                Nombre = categoria.Nombre,
                Descripcion = categoria.Descripcion,
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
