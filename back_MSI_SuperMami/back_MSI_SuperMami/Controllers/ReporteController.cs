using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.DTOs;
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
    public class ReporteController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<ReporteController> _logger;

        public ReporteController(ILogger<ReporteController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("precios/proveedor/{id}")]
        public ActionResult<RespuestaAPI> GetPreciosxProveedor(int id)
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;

            var provxpro = bd.Productosxproveedores.Where(f => f.Idproducto == id).ToList();

            var lista = new List<DTOReporte1>();

            if (provxpro != null)
            {
                foreach (var i in provxpro)
                {
                    var precios = bd.Productosxproveedores.FirstOrDefault(f => f.Precio == i.Precio);
                    var prov = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == i.Idproveedor);
                    if (prov.Estado == true && i.Estado == true)
                    {
                        var dto = new DTOReporte1

                        {
                            proveedor = prov.Nombre,
                            idproveedor = prov.Idproveedor,
                            precio = precios.Precio
                        };

                        lista.Add(dto);
                    }
                }
                respusta.Respuesta = lista.OrderBy(f => f.precio);
                return respusta;

            }
            else
            {
                respusta.InfoAdicional = "No se dispone de ningun proveedor para este producto";
            }

            respusta.Respuesta = lista.OrderBy(f => f.precio);
            return respusta;
        }


    }
}
