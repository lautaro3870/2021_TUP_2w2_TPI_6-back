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
    public class FormaPagoController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<FormaPagoController> _logger;

        public FormaPagoController(ILogger<FormaPagoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("[controller]/Get")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respusta = new RespuestaAPI();
            respusta.Ok = true;
            respusta.Respuesta = bd.FormaDePagos.Where(x => x.Estado == true).ToList();
            return respusta;
        }

        //dar de baja
        [HttpPut]
        [Route("[controller]/darDeBaja")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese una forma de pago a dar de baja";
                return res;
            }
            else
            {
                var pago = bd.FormaDePagos.Find(id);
                try
                {
                    if (pago != null && pago.Estado == true)
                    {
                        pago.Estado = false;
                        res.Ok = true;
                        bd.FormaDePagos.Update(pago);
                        bd.SaveChanges();
                        res.Respuesta = "Forma de pago dada de baja";
                        return res;
                    }
                    else
                    {
                        res.Ok = false;
                        res.Error = "No hay formas de pago habilitadas";
                    }

                    return res;
                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Respuesta = "No se encuentra la forma de pago solicitada";
                    return res;
                }

            }
        }

        //Registrar Nueva Forma de Pago
        [HttpPost]
        [Route("[controller]/formasPago")]
        public RespuestaAPI registrarFormaPago([FromBody] ComandoRegistrarFormaPago formaPago)
        {
            RespuestaAPI res = new RespuestaAPI();
            
            if (string.IsNullOrEmpty(formaPago.Nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(formaPago.Descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }


            FormaDePago  f = new FormaDePago()
            {
                Nombre = formaPago.Nombre,
                Descripcion = formaPago.Descripcion,
                Estado = true
            };

            bd.FormaDePagos.Add(f);
            bd.SaveChanges();
            res.Ok = true;

            res.InfoAdicional = "La Forma de Pago se cargo correctamente";
            return res;
        }
    }
}
