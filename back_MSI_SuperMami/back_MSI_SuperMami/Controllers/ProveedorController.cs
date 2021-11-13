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
    public class ProveedorController : ControllerBase
    {

        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<ProveedorController> _logger;

        public ProveedorController(ILogger<ProveedorController> logger)
        {
            _logger = logger;
        }

        //Proveedores habilitados
        //[HttpGet]
        //[Route("proveedores")]
        //public ActionResult<RespuestaAPI> Get()
        //{
        //    var respuestas = new RespuestaAPI();
        //    respuestas.Ok = true;
        //    respuestas.Respuesta = bd.Proveedores.Where(x => x.Estado == true).ToList();
        //    return respuestas;
        //}

        [HttpGet]
        [Route("proveedores")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var pro = bd.Proveedores.ToList();

            var lista = new List<DTOListaProveedores>();

            if (pro != null)
            {
                foreach (var i in pro)
                {
                    var proveedore = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == i.Idproveedor);
                    var area = bd.Areas.FirstOrDefault(f => f.Idarea == i.Idarea);


                    var dto = new DTOListaProveedores
                    {
                        idproveedor = proveedore.Idproveedor,
                        nombre = proveedore.Nombre,
                        direccion = proveedore.Direccion,
                        cuit = proveedore.Cuit,
                        telefono = proveedore.Telefono,
                        email = proveedore.Email,
                        area = area.Descripcion
      
                    };
                    lista.Add(dto);
                }

                respuestas.Respuesta = lista;
                return respuestas;
            }
            
            respuestas.Respuesta = bd.Proveedores.Where(x => x.Estado == true).ToList();
            return respuestas;
        }

        [HttpGet]
        [Route("proveedores/{id}")]
        public ActionResult<RespuestaAPI> GetProveedores(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese el proveedor";
                return respusta;
            }
            else
            {
                var prov = bd.Proveedores.Find(id);
                try
                {
                    if (prov != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = prov;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra el proveedor solicitado";
                    return respusta;
                }

            }

        }

        [HttpDelete]
        [Route("proveedores/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var respuesta = new RespuestaAPI();
            if (id == 0)
            {
                respuesta.Respuesta = "Ingrese el proveedor a dar de baja";
                respuesta.Ok = false;
                return respuesta;
            }
            else
            {
                var prov = bd.Proveedores.Find(id);
                try
                {
                    if (prov != null && prov.Estado == true)
                    {
                        respuesta.Ok = true;
                        prov.Estado = false;
                        bd.Proveedores.Update(prov);
                        bd.SaveChanges();
                        respuesta.Respuesta = "Proveedor dado de baja";
                        return respuesta;

                    }
                    else
                    {
                        respuesta.Ok = false;
                        respuesta.Error = "No hay proveedores habilitados";
                    }
                    return respuesta;
                }
                catch(Exception e)
                {
                    respuesta.Respuesta = "Error " + e;
                    respuesta.Ok = false;
                    return respuesta;
                }
            }

        }

        //Put
        [HttpPut]
        [Route("proveedores/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarProveedor comando)
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
                try
                {
                    if (string.IsNullOrEmpty(comando.nombre))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso el nombre";
                        return res;
                    }
                    if (string.IsNullOrEmpty(comando.direccion))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la Direccion";
                        return res;
                    }
                    if (string.IsNullOrEmpty(comando.cuit))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la CUIT";
                        return res;
                    }
                    if (string.IsNullOrEmpty(comando.telefono))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso el telefono";
                        return res;
                    }

                    if (string.IsNullOrEmpty(comando.email))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la Email";
                        return res;
                    }
                    if (comando.area == 0)
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso el area";
                        return res;
                    }

                    var prov = bd.Proveedores.Where(x => x.Idproveedor == id).FirstOrDefault();
                    if (prov != null)
                    {

                        prov.Nombre = comando.nombre;
                        prov.Direccion = comando.direccion;
                        prov.Estado = true;
                        prov.Email = comando.email;
                        prov.Cuit = comando.cuit;
                        prov.Telefono = comando.telefono;
                        prov.Idarea = comando.area;

                        bd.Update(prov);
                        bd.SaveChanges();

                        res.Ok = true;
                        res.Respuesta = "Proveedor modificado";
                        return res;

                    }
                    else
                    {
                        res.Respuesta = "Proveedor no encontrado";
                        res.Ok = false;
                        return res;
                    }

                }
                catch (Exception e)
                {
                    res.Respuesta = "Error: " + e;
                    res.Ok = false;
                    return res;
                }
            }

        }

        //[HttpPut]
        //[Route("proveedores/{id}")]
        //public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarProveedor comando)
        //{
        //    var res = new RespuestaAPI();
        //    if (id == 0)
        //    {
        //        res.Ok = false;
        //        res.Respuesta = "Ingrese un proveedor a modificar";
        //        return res;
        //    }
        //    else
        //    {
        //        try
        //        {
        //            if (string.IsNullOrEmpty(comando.nombre))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso el nombre";
        //                return res;
        //            }
        //            if (string.IsNullOrEmpty(comando.direccion))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la Direccion";
        //                return res;
        //            }
        //            if (string.IsNullOrEmpty(comando.cuit))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la CUIT";
        //                return res;
        //            }
        //            if (string.IsNullOrEmpty(comando.telefono))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso el telefono";
        //                return res;
        //            }

        //            if (string.IsNullOrEmpty(comando.email))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la Email";
        //                return res;
        //            }
        //            if (comando.area == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso el area";
        //                return res;
        //            }

        //            var prov = bd.Proveedores.Where(x => x.Idproveedor == id).FirstOrDefault();
        //            if (prov != null)
        //            {

        //                prov.Nombre = comando.nombre;
        //                prov.Direccion = comando.direccion;
        //                prov.Estado = true;
        //                prov.Email = comando.email;
        //                prov.Cuit = comando.cuit;
        //                prov.Telefono = comando.telefono;
        //                prov.Idarea = comando.area;

        //                id = prov.Idproveedor;

        //                bd.Proveedores.Update(prov);
        //                bd.SaveChanges();

        //                if (comando.FormasDeEnvio != null)
        //                {
        //                    var formaDeEnvio = bd.Proveedoresxformadeenvios.Where(f => f.Idproveedor == id).ToList();
        //                    foreach(var i in formaDeEnvio)
        //                    {
        //                        bd.Proveedoresxformadeenvios.Remove(i);
        //                    }
        //                    foreach (var x in comando.FormasDeEnvio)
        //                    {
        //                        Proveedoresxformadeenvio pxe = new Proveedoresxformadeenvio();
        //                        pxe.Idformadeenvio = x.envio;
        //                        //pxe.Idproveedor = prov.Idproveedor;
        //                        pxe.Idproveedor = id;

        //                        bd.Proveedoresxformadeenvios.Add(pxe);
        //                        bd.SaveChanges();
        //                    }
        //                }

        //                if (comando.FormasDePago != null)
        //                {
        //                    var formaDeEnvio = bd.Proveedoresxformasdepagos.Where(f => f.Idproveedor == id).ToList();
        //                    foreach (var i in formaDeEnvio)
        //                    {
        //                        bd.Proveedoresxformasdepagos.Remove(i);
        //                    }
        //                    foreach (var i in comando.FormasDePago)
        //                    {
        //                        Proveedoresxformasdepago pxp = new Proveedoresxformasdepago();
        //                        pxp.Idformapago = i.pago;
        //                        //pxp.Idproveedor = prov.Idproveedor;
        //                        pxp.Idproveedor = id;

        //                        bd.Proveedoresxformasdepagos.Add(pxp);
        //                        bd.SaveChanges();
        //                    }
        //                    bd.SaveChanges();

        //                }


        //                res.Ok = true;
        //                res.Respuesta = "Proveedor modificado";
        //                return res;

        //            }
        //            else
        //            {
        //                res.Respuesta = "Proveedor no encontrado";
        //                res.Ok = false;
        //                return res;
        //            }

        //        }
        //        catch (Exception e)
        //        {
        //            res.Respuesta = "Error: " + e;
        //            res.Ok = false;
        //            return res;
        //        }
        //    }

        //}


        [HttpPost]
        [Route("proveedores")]
        public ActionResult<RespuestaAPI> Post([FromBody] ComandoRegistrarProveedor comando)
        {
            var res = new RespuestaAPI();

            if (string.IsNullOrEmpty(comando.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(comando.direccion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la Direccion";
                return res;
            }
            if (string.IsNullOrEmpty(comando.cuit))
            {
                res.Ok = false;
                res.Error = "No se ingreso la CUIT";
                return res;
            }
            if (string.IsNullOrEmpty(comando.telefono))
            {
                res.Ok = false;
                res.Error = "No se ingreso el telefono";
                return res;
            }

            if (string.IsNullOrEmpty(comando.email))
            {
                res.Ok = false;
                res.Error = "No se ingreso la Email";
                return res;
            }
            if (comando.area == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el area";
                return res;
            }

            Proveedore prov = new Proveedore();
            prov.Nombre = comando.nombre;
            prov.Direccion = comando.direccion;
            prov.Estado = true;
            prov.Email = comando.email;
            prov.Cuit = comando.cuit;
            prov.Telefono = comando.telefono;
            prov.Idarea = comando.area;

            bd.Proveedores.Add(prov);
            bd.SaveChanges();

            res.Ok = true;
            res.Respuesta = "Se ingreso el proveedor correctamente";
            return res;

        }

        //[HttpPost]
        //[Route("proveedores")]
        //public ActionResult<RespuestaAPI> Post([FromBody] ComandoRegistrarProveedor comando)
        //{
        //    var res = new RespuestaAPI();

        //    if (string.IsNullOrEmpty(comando.nombre))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el nombre";
        //        return res;
        //    }
        //    if (string.IsNullOrEmpty(comando.direccion))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la Direccion";
        //        return res;
        //    }
        //    if (string.IsNullOrEmpty(comando.cuit))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la CUIT";
        //        return res;
        //    }
        //    if (string.IsNullOrEmpty(comando.telefono))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el telefono";
        //        return res;
        //    }

        //    if (string.IsNullOrEmpty(comando.email))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la Email";
        //        return res;
        //    }
        //    if (comando.area == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el area";
        //        return res;
        //    }

        //    Proveedore prov = new Proveedore();
        //    prov.Nombre = comando.nombre;
        //    prov.Direccion = comando.direccion;
        //    prov.Estado = true;
        //    prov.Email = comando.email;
        //    prov.Cuit = comando.cuit;
        //    prov.Telefono = comando.telefono;
        //    prov.Idarea = comando.area;

        //    bd.Proveedores.Add(prov);
        //    bd.SaveChanges();

        //    foreach(var x in comando.FormasDeEnvio)
        //    {
        //        Proveedoresxformadeenvio pxe = new Proveedoresxformadeenvio();
        //        pxe.Idformadeenvio = x.envio;
        //        pxe.Idproveedor = prov.Idproveedor;

        //        bd.Proveedoresxformadeenvios.Add(pxe);
        //        bd.SaveChanges();
        //    }

        //    foreach(var i in comando.FormasDePago)
        //    {
        //        Proveedoresxformasdepago pxp = new Proveedoresxformasdepago();
        //        pxp.Idformapago = i.pago;
        //        pxp.Idproveedor = prov.Idproveedor;
        //        bd.Proveedoresxformasdepagos.Add(pxp);
        //        bd.SaveChanges();
        //    }
        //    bd.SaveChanges();

        //    res.Ok = true;
        //    res.Respuesta = "Se ingreso el proveedor correctamente";
        //    return res;

        //}

    }
}
