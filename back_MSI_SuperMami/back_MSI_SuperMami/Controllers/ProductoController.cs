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
    public class ProductoController : ControllerBase
    {
        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/Get")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            respuestas.Respuesta = bd.Productos.Where(x => x.Estado == true).ToList();           
            return respuestas;
        }

        



    }
}
