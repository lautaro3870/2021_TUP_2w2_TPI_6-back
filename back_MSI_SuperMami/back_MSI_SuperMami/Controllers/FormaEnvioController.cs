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

        //Registrar Nueva Forma de Envío
        [HttpPost]
        [Route("[controller]/formasEnvio")]
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
