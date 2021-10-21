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
    public class CategoriaController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<CategoriaController> _logger;

        public CategoriaController(ILogger<CategoriaController> logger)
        {
            _logger = logger;
        }

        //Registrar Nueva Categoría
        [HttpPost]
        [Route("[controller]/categorias")]
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
