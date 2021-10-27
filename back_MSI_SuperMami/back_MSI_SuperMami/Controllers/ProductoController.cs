﻿using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.Comandos.Modificar;
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

        //Productos habilitados
        [HttpGet]
        [Route("productos")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            respuestas.Respuesta = bd.Productos.Where(x => x.Estado == true).ToList();
            return respuestas;
        }
        //Get Productos por id
        [HttpGet]
        [Route("productos/{id}")]
        public ActionResult<RespuestaAPI> GetProductos(int id)
        {
            var respusta = new RespuestaAPI();
            if (id == 0)
            {
                respusta.Ok = false;
                respusta.Respuesta = "Ingrese el producto a dar de baja";
                return respusta;
            }
            else
            {
                var prod = bd.Productos.Find(id);
                try
                {
                    if (prod != null)
                    {
                        respusta.Ok = true;
                        respusta.Respuesta = prod;
                        return respusta;
                    }
                    return respusta;

                }
                catch
                {
                    respusta.Ok = false;
                    respusta.Respuesta = "No se encuentra el producto solicitado";
                    return respusta;
                }

            }

        }

        //Dar de baja producto
        [HttpDelete]
        [Route("productos/{id}")]
        public ActionResult<RespuestaAPI> DarDeBaja(int id)
        {
            var res = new RespuestaAPI();
            

            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Producto no encontrado";
                return res;
            }
            else
            {
                try
                {
                    var pro = bd.Productos.Where(x => x.Idproducto == id).FirstOrDefault();
                    if (pro != null && pro.Estado == true)
                    {
                        pro.Estado = false;
                        res.Ok = true;
                        res.Respuesta = pro;
                        res.Error = "Producto dado de baja";
                        bd.Productos.Update(pro);
                        bd.SaveChanges();

                    }
                    else
                    {
                        res.Ok = false;
                        res.Respuesta = "Producto no encontrado";
                        return res;
                    }
                    return res;

                }
                catch (Exception e)
                {
                    res.Ok = false;
                    res.Error = "No hay Productos habilitados";
                    return res;
                }
            }
        }

        [HttpPut]
        [Route("productos")]
        public ActionResult<RespuestaAPI> Put([FromBody] ComandoModificarProducto comando)
        {
            var res = new RespuestaAPI();

            try
            {
                if (string.IsNullOrEmpty(comando.nombre))
                {
                    res.Ok = false;
                    res.Error = "No se ingreso el nombre";
                    return res;
                }
                if (string.IsNullOrEmpty(comando.descripcion))
                {
                    res.Ok = false;
                    res.Error = "No se ingreso la descripción";
                    return res;
                }

                if (comando.precio == 0)
                {
                    res.Ok = false;
                    res.Error = "No se ingreso el precio";
                    return res;
                }
                if (comando.vencimiento.Equals(""))
                {
                    res.Ok = false;
                    res.Error = "No se ingreso la fecha de vencimiento";
                    return res;
                }

                if (comando.unidadMedida == 0)
                {
                    res.Ok = false;
                    res.Error = "No se ingreso la unidad de medida";
                    return res;
                }

                if (comando.categoria == 0)
                {
                    res.Ok = false;
                    res.Error = "No se ingreso la categoria";
                    return res;
                }
                if (comando.marca == 0)
                {
                    res.Ok = false;
                    res.Error = "No se ingreso la marca";
                    return res;
                }

                var p = bd.Productos.Where(x => x.Idproducto == comando.id).FirstOrDefault();

                if (p != null)
                {
                    p.Nombre = comando.nombre;
                    p.Descripcion = comando.descripcion;
                    p.Precio = comando.precio;
                    p.Vencimiento = comando.vencimiento;
                    p.Estado = true;
                    p.Idunidadmedida = comando.unidadMedida;
                    p.Idcategoria = comando.categoria;
                    p.Idmarca = comando.marca;

                    bd.Productos.Update(p);
                    bd.SaveChanges();

                    res.Ok = true;
                    res.Respuesta = "Producto modificado";
                    return res;
                }
                else
                {
                    res.Ok = false;
                    res.Respuesta = "Producto no encontrado";
                    return res;
                }
            }
            catch (Exception e)
            {
                res.Ok = false;
                res.Respuesta = "Producto no encontrado";
                return res;
            }
            
        }

        //Insertar producto
        [HttpPost]
        [Route("productos")]
        public RespuestaAPI PostProducto([FromBody] ComandoRegistrarProducto comando)
        {
            RespuestaAPI res = new RespuestaAPI();

            if (string.IsNullOrEmpty(comando.nombre))
            {
                res.Ok = false;
                res.Error = "No se ingreso el nombre";
                return res;
            }
            if (string.IsNullOrEmpty(comando.descripcion))
            {
                res.Ok = false;
                res.Error = "No se ingreso la descripción";
                return res;
            }

            if (comando.precio == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso el precio";
                return res;
            }
            if (comando.vencimiento.Equals(""))
            {
                res.Ok = false;
                res.Error = "No se ingreso la fecha de vencimiento";
                return res;
            }
            
            if (comando.unidadMedida == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso la unidad de medida";
                return res;
            }

            if (comando.categoria == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso la categoria";
                return res;
            }
            if (comando.marca == 0)
            {
                res.Ok = false;
                res.Error = "No se ingreso la marca";
                return res;
            }

            Producto p = new Producto();


            p.Nombre = comando.nombre;
            p.Descripcion = comando.descripcion;
            p.Precio = comando.precio;
            p.Vencimiento = comando.vencimiento;
            p.Estado = true;
            p.Idunidadmedida = comando.unidadMedida;
            p.Idcategoria = comando.categoria;
            p.Idmarca = comando.marca;
            
            bd.Productos.Add(p);
            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Producto insertado correctamente";
            return res;

        }

    }

    
}
