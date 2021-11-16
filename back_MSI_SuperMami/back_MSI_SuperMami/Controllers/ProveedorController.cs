using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.DTOs;
using back_MSI_SuperMami.Helpers;
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
                    if (proveedore.Estado == true)
                    {
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

                        
                }

                respuestas.Respuesta = lista.OrderBy(f => f.nombre); 
                return respuestas;
            }

            return respuestas;
        }

        [HttpGet]
        [Route("proveedores-baja")]
        public ActionResult<RespuestaAPI> GetBaja()
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
                    if (proveedore.Estado == false)
                    {
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


                }

                respuestas.Respuesta = lista.OrderBy(f => f.nombre); 
                return respuestas;
            }

            return respuestas;
        }



        [HttpGet]
        [Route("proveedores/{id}")]
        public ActionResult<RespuestaAPI> GetId(int id)
        {
            Metodos m = new Metodos();
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var pro = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == id);

            var dto = new DTOProveedoresId();

            if (pro != null)
            {

                var area = bd.Areas.FirstOrDefault(f => f.Idarea == pro.Idarea);
                var listaPagos = m.obtenerPagos(id);
                var listaEnvios = m.obtenerEnvios(id);
                var listaProductos = m.obtenerProductosxProveedor(id);


                dto = new DTOProveedoresId
                {
                    idproveedor = id,
                    nombre = pro.Nombre,
                    direccion = pro.Direccion,
                    cuit = pro.Cuit,
                    telefono = pro.Telefono,
                    email = pro.Email,
                    idarea = area.Idarea,
                    formasPago = listaPagos,
                    formasEntrega = listaEnvios,
                    productos = listaProductos

                };

                respuestas.Respuesta = dto;
                return respuestas;
            }
            respuestas.Respuesta = dto;
            return respuestas;
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
                catch (Exception e)
                {
                    respuesta.Respuesta = "Error " + e;
                    respuesta.Ok = false;
                    return respuesta;
                }
            }

        }

       
       

        [HttpPut]
        [Route("proveedores/{id}")]
        public RespuestaAPI PutProveedores(int id, [FromBody] DTOProveedoresPut comando)
        {
            var res = new RespuestaAPI();
            Proveedore prov = new Proveedore();

            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese un proveedor a modificar";
                return res;
            }
            else
            {
                try
                {
                    prov = bd.Proveedores.FirstOrDefault(x => x.Idproveedor == id);

                    if (prov != null)
                    {
                        
                        prov.Nombre = comando.nombre;
                        prov.Direccion = comando.direccion;
                        prov.Cuit = comando.cuit;
                        prov.Telefono = comando.telefono;
                        prov.Email = comando.email;
                        prov.Idarea = comando.idarea;

                        id = prov.Idproveedor;

                        bd.Proveedores.Update(prov);
                        bd.SaveChanges();

                        if (comando.Productos != null)
                        {
                            var prod = bd.Productosxproveedores.Where(f => f.Idproveedor == id).ToList();
                            foreach (var i in prod)
                            {
                                bd.Productosxproveedores.Remove(i);
                            }

                            foreach (var producto in comando.Productos)
                            {
                                Productosxproveedore p = new Productosxproveedore();
                                p.Idproducto = producto.idproducto;
                                p.Precio = producto.precio;
                                p.Idproveedor = id;
                                p.Estado = true;

                                bd.Productosxproveedores.Add(p);
                                bd.SaveChanges();
                            }

                            if (comando.Pagos != null)

                            {

                                var formaspag = bd.Proveedoresxformasdepagos.Where(f => f.Idproveedor == id).ToList();
                                foreach (var i in formaspag)
                                {
                                    bd.Proveedoresxformasdepagos.Remove(i);
                                }

                                foreach (var pagos in comando.Pagos)
                                {
                                    Proveedoresxformasdepago f = new Proveedoresxformasdepago();
                                    f.Idproveedor = id;
                                    f.Idformapago = pagos.formasPago;
                                    bd.Proveedoresxformasdepagos.Add(f);
                                    bd.SaveChanges();

                                }
                            }

                            if (comando.Entregas != null)
                            {
                                var formasent = bd.Proveedoresxformadeenvios.Where(f => f.Idproveedor == id).ToList();
                                foreach (var i in formasent)
                                {
                                    bd.Proveedoresxformadeenvios.Remove(i);
                                }

                                foreach (var entregas in comando.Entregas)
                                {
                                    Proveedoresxformadeenvio f = new Proveedoresxformadeenvio();
                                    f.Idproveedor = id;
                                    f.Idformadeenvio = entregas.formasEntrega;

                                    bd.Proveedoresxformadeenvios.Add(f);
                                    bd.SaveChanges();

                                }
                            }
                        }

                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Proveedor no encontrado";
                        return res;
                    }

                }
                catch
                {
                    res.Ok = false;
                    res.Respuesta = "Proveedor no encontrada";
                    return res;
                }
            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Proveedor modificado correctamente";
            return res;
        }


        

        [HttpPost]
        [Route("proveedores")]
        public RespuestaAPI PostOrden([FromBody] DTOProveedoresPut comando)
        {
            var res = new RespuestaAPI();
            if (comando.nombre == "")
            {
                res.Ok = false;
                res.Error = "No se ingreso el Nombre del Proveedor";
                return res;
            }
            if (comando.direccion == "")
            {
                res.Ok = false;
                res.Error = "No se ingreso la direccion";
                return res;
            }

            if (comando.cuit == "")
            {
                res.Ok = false;
                res.Error = "No se ingreso el cuit";
                return res;
            }

            if (comando.telefono == "")
            {
                res.Ok = false;
                res.Error = "No se ingreso el telefono";
                return res;
            }

            if (comando.email == "")
            {
                res.Ok = false;
                res.Error = "No se ingreso el email";
                return res;
            }

            if (comando.idarea == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el idarea";
                return res;
            }


            Proveedore prov = new Proveedore();

            prov.Nombre = comando.nombre;
            prov.Direccion = comando.direccion;
            prov.Cuit = comando.cuit;
            prov.Telefono = comando.telefono;
            prov.Email = comando.email;
            prov.Estado = true;
            prov.Idarea = comando.idarea;

            //int id = orden.Idordendecompra;

            bd.Proveedores.Add(prov);
            bd.SaveChanges();

            foreach (var producto in comando.Productos)
            {
                Productosxproveedore p = new Productosxproveedore();
                p.Idproducto = producto.idproducto;
                p.Precio = producto.precio;
                p.Estado = true;
                p.Idproveedor = prov.Idproveedor;

                bd.Productosxproveedores.Add(p);
                bd.SaveChanges();
            }

            foreach (var pagos in comando.Pagos)
            {
                Proveedoresxformasdepago f = new Proveedoresxformasdepago();
                f.Idproveedor = prov.Idproveedor;
                f.Idformapago = pagos.formasPago;
                bd.Proveedoresxformasdepagos.Add(f);
                bd.SaveChanges();

            }

            foreach (var entregas in comando.Entregas)
            {
                Proveedoresxformadeenvio f = new Proveedoresxformadeenvio();
                f.Idproveedor = prov.Idproveedor;
                f.Idformadeenvio = entregas.formasEntrega;

                bd.Proveedoresxformadeenvios.Add(f);
                bd.SaveChanges();

            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Proveedor insertado correctamente";
            return res;
        }


    }
}
