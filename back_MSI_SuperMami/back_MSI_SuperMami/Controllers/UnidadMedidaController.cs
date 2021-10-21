//using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.Models;
//using back_MSI_SuperMami.Respuestas;
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

        //Registrar Nueva Unidad de Medida
        /*[HttpPost]
        [Route("[controller]/unidadesMedida")]
        public RespuestaAPI registrarUnidadMedida([FromBody] ComandoRegistrarUnidadMedida unidadMedida)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(unidadMedida.Nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(unidadMedida.Descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            UnidadDeMedidum  u = new UnidadDeMedidum()
            {
                Nombre = unidadMedida.Nombre,
                Descipcion = unidadMedida.Descripcion,
                Estado = true
            };

            bd.UnidadDeMedida.Add(u);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La Unidad de Medida se cargo correctamente";
            return res;
        }*/
    }
}
