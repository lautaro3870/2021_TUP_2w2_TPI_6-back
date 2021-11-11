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
    public class ProductoController : ControllerBase
    {
        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        private readonly ILogger<ProductoController> _logger;

        public ProductoController(ILogger<ProductoController> logger)
        {
            _logger = logger;
        }

        //Productos habilitados
        //[HttpGet]
        //[Route("productos")]
        //public ActionResult<RespuestaAPI> Get()
        //{
        //    var respuestas = new RespuestaAPI();
        //    respuestas.Ok = true;
        //    respuestas.Respuesta = bd.Productos.Where(x => x.Estado == true).ToList();
        //    return respuestas;
        //}

        [HttpGet]
        [Route("productos")]
        public ActionResult<RespuestaAPI> Get()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var pro = bd.Productos.ToList();

            var lista = new List<DTOListaProductos>();

            if (pro != null)
            {
                foreach (var i in pro)
                {
                    var product = bd.Productos.FirstOrDefault(f => f.Idproducto == i.Idproducto);
                    var marc = bd.Marcas.FirstOrDefault(f => f.Idmarca == i.Idmarca);
                    var categ = bd.Categorias.FirstOrDefault(f => f.Idcategoria == i.Idcategoria);
                    var unid = bd.UnidadDeMedida.FirstOrDefault(f => f.Idunidadmedida == i.Idunidadmedida);



                    var dto = new DTOListaProductos
                    {
                        id = product.Idproducto,
                        nombre = product.Nombre,
                        descripcion = product.Descripcion,
                        marca = marc.Nombre,
                        categoria = categ.Nombre,
                        unidadMedida=unid.Nombre

                    };
                    lista.Add(dto);
                }

                respuestas.Respuesta = lista;
                return respuestas;
            }

            respuestas.Respuesta = bd.Productos.Where(x => x.Estado == true).ToList();
            return respuestas;
        }

        //[HttpGet]
        //[Route("productos")]
        //public ActionResult<RespuestaAPI> Get()
        //{
        //    var respuestas = new RespuestaAPI();
        //    respuestas.Ok = true;
        //    var pro = bd.Productos.ToList();

        //    var lista = new List<DTOListaProductos>();

        //    if(pro != null)
        //    {
        //        foreach(var i in pro)
        //        {
        //            var producto = bd.Productos.FirstOrDefault(f => f.Idproducto == i.Idproducto);                 
        //            var marca = bd.Marcas.FirstOrDefault(f => f.Idmarca == i.Idmarca);
        //            var categoria = bd.Categorias.FirstOrDefault(f => f.Idcategoria == i.Idcategoria);
        //            var unidadMedida = bd.UnidadDeMedida.FirstOrDefault(f => f.Idunidadmedida == i.Idunidadmedida);
        //            var proXpro = bd.Productosxproveedores.Where(f => f.Idproducto == i.Idproducto).ToList();

        //            Proveedore proveedor = null;
        //            List<Proveedore> listaproveedores = new List<Proveedore>();

        //            if (proXpro.Count != 0)
        //            {
        //                foreach (var p in proXpro)
        //                {
        //                    proveedor = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == p.Idproveedor);
        //                    listaproveedores.Add(proveedor);
        //                }

        //                foreach(var y in listaproveedores)
        //                {
        //                    var dto = new DTOListaProductos
        //                    {
        //                        nombre = producto.Nombre,
        //                        descripcion = producto.Descripcion,
        //                        marca = marca.Nombre,
        //                        categoria = categoria.Nombre,
        //                        proveedor = y.Nombre,
        //                        unidadMedida = unidadMedida.Nombre
        //                    };
        //                    lista.Add(dto);
        //                }

        //            }
        //        }
        //        respuestas.Respuesta = lista;

        //        return respuestas;
        //    }

        //    respuestas.Respuesta = bd.Productos.Where(x => x.Estado == true).ToList();
        //    return respuestas;
        //}


        //lista de productos
        [HttpGet]
        [Route("productos-lista")]
        public ActionResult<RespuestaAPI> GetProductosLista()
        {
            var respuestas = new RespuestaAPI();
            respuestas.Ok = true;
            var prod = bd.Productos.ToList();

            var lista = new List<DTOProducto>();
            if (prod != null)
            {
                foreach (var x in prod)
                {
                    var nombre = bd.Productos.FirstOrDefault(f => f.Nombre == x.Nombre);
                    var desc = bd.Productos.FirstOrDefault(f => f.Descripcion == x.Descripcion);
                    var marca = bd.Marcas.FirstOrDefault(f => f.Idmarca == x.Idmarca);

                    var dto = new DTOProducto
                    {
                        nombre = nombre.Nombre,
                        descripcion = desc.Descripcion,
                        marca = marca.Descripcion
                    };

                    lista.Add(dto);

                }
            }
            respuestas.Ok = true;
            respuestas.Respuesta = lista;
            return respuestas;
        }

        //lista de productos filtrada por nombre
        [HttpGet]
        [Route("productoss/{nombre}")] // puse productoss porque sino da error en la ruta de los endpoints
        public ActionResult<RespuestaAPI> GetNombre(string nombre)
        {
            var respuestas = new RespuestaAPI();
            try
            {
                var prod = bd.Productos.FirstOrDefault(x => x.Nombre.Equals(nombre));
                var marca = bd.Marcas.FirstOrDefault(x => x.Idmarca == prod.Idmarca);

                var dto = new DTOProducto
                {
                    nombre = prod.Nombre,
                    descripcion = prod.Descripcion,
                    marca = marca.Descripcion
                };

                respuestas.Ok = true;
                respuestas.Respuesta = dto;
                return respuestas;
            }
            catch
            {
                respuestas.Ok = false;
                respuestas.Respuesta = "Producto no encontrado";
                return respuestas;
            }

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

        //[HttpPut]
        //[Route("productos/{id}")]
        //public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarProducto comando)
        //{
        //    var res = new RespuestaAPI();
        //    if (id == 0)
        //    {
        //        res.Ok = false;
        //        res.Respuesta = "Ingrese una forma de pago a dar de baja";
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
        //            if (string.IsNullOrEmpty(comando.descripcion))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la descripción";
        //                return res;
        //            }

        //            if (comando.precio == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso el precio";
        //                return res;
        //            }
        //            if (comando.vencimiento.Equals(""))
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la fecha de vencimiento";
        //                return res;
        //            }

        //            if (comando.unidadMedida == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la unidad de medida";
        //                return res;
        //            }

        //            if (comando.categoria == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la categoria";
        //                return res;
        //            }
        //            if (comando.marca == 0)
        //            {
        //                res.Ok = false;
        //                res.Error = "No se ingreso la marca";
        //                return res;
        //            }

        //            var p = bd.Productos.Where(x => x.Idproducto == id).FirstOrDefault();

        //            if (p != null)
        //            {
        //                p.Nombre = comando.nombre;
        //                p.Descripcion = comando.descripcion;
        //                p.Precio = comando.precio;
        //                p.Vencimiento = comando.vencimiento;
        //                p.Estado = true;
        //                p.Idunidadmedida = comando.unidadMedida;
        //                p.Idcategoria = comando.categoria;
        //                p.Idmarca = comando.marca;

        //                bd.Productos.Update(p);
        //                bd.SaveChanges();

        //                res.Ok = true;
        //                res.Respuesta = "Producto modificado";
        //                return res;
        //            }
        //            else
        //            {
        //                res.Ok = false;
        //                res.Respuesta = "Producto no encontrado";
        //                return res;
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            res.Ok = false;
        //            res.Respuesta = "Producto no encontrado";
        //            return res;
        //        }
        //    }


        //}


        [HttpPut]
        [Route("productos/{id}")]
        public ActionResult<RespuestaAPI> Put(int id, [FromBody] ComandoRegistrarProducto comando)
        {
            var res = new RespuestaAPI();
            if (id == 0)
            {
                res.Ok = false;
                res.Respuesta = "Ingrese el producto a modificar";
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
                    if (string.IsNullOrEmpty(comando.descripcion))
                    {
                        res.Ok = false;
                        res.Error = "No se ingreso la descripción";
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

                    var p = bd.Productos.Where(x => x.Idproducto == id).FirstOrDefault();

                    if (p != null)
                    {
                        p.Nombre = comando.nombre;
                        p.Descripcion = comando.descripcion;
                        p.Estado = true;
                        p.Idunidadmedida = comando.unidadMedida;
                        p.Idcategoria = comando.categoria;
                        p.Idmarca = comando.marca;
                        id = p.Idproducto;

                        bd.Productos.Update(p);
                        bd.SaveChanges();

                        if(comando.Proveedores != null)
                        {
                            var proXprov = bd.Productosxproveedores.Where(f => f.Idproducto == id).ToList();

                            foreach(var i in proXprov)
                            {
                                bd.Productosxproveedores.Remove(i);
                            }

                            foreach (var x in comando.Proveedores)
                            {
                                Productosxproveedore pXp = new Productosxproveedore();
                                pXp.Idproducto = p.Idproducto;
                                pXp.Idproveedor = x.proveedor;

                                bd.Productosxproveedores.Add(pXp);
                                bd.SaveChanges();
                            }
                            bd.SaveChanges();

                        }

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


        }

        [HttpPost]
        [Route("productos")]
        public RespuestaAPI PostProductoProveedores([FromBody] ComandoRegistrarProducto comando)
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
            
            p.Estado = true;
            p.Idunidadmedida = comando.unidadMedida;
            p.Idcategoria = comando.categoria;
            p.Idmarca = comando.marca;

            bd.Productos.Add(p);
            bd.SaveChanges();

            foreach (var x in comando.Proveedores)
            {
                Productosxproveedore pXp = new Productosxproveedore();
                pXp.Idproducto = p.Idproducto;
                pXp.Idproveedor = x.proveedor;

                bd.Productosxproveedores.Add(pXp);
                bd.SaveChanges();
            }

            bd.SaveChanges();

            res.Ok = true;
            res.InfoAdicional = "Producto insertado correctamente";
            return res;
        }

        //Insertar producto
        //[HttpPost]
        //[Route("productos")]
        //public RespuestaAPI PostProducto([FromBody] ComandoRegistrarProducto comando)
        //{
        //    RespuestaAPI res = new RespuestaAPI();

        //    if (string.IsNullOrEmpty(comando.nombre))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el nombre";
        //        return res;
        //    }
        //    if (string.IsNullOrEmpty(comando.descripcion))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la descripción";
        //        return res;
        //    }

        //    if (comando.precio == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso el precio";
        //        return res;
        //    }
        //    if (comando.vencimiento.Equals(""))
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la fecha de vencimiento";
        //        return res;
        //    }

        //    if (comando.unidadMedida == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la unidad de medida";
        //        return res;
        //    }

        //    if (comando.categoria == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la categoria";
        //        return res;
        //    }
        //    if (comando.marca == 0)
        //    {
        //        res.Ok = false;
        //        res.Error = "No se ingreso la marca";
        //        return res;
        //    }

        //    Producto p = new Producto();


        //    p.Nombre = comando.nombre;
        //    p.Descripcion = comando.descripcion;
        //    p.Precio = comando.precio;
        //    p.Vencimiento = comando.vencimiento;
        //    p.Estado = true;
        //    p.Idunidadmedida = comando.unidadMedida;
        //    p.Idcategoria = comando.categoria;
        //    p.Idmarca = comando.marca;

        //    bd.Productos.Add(p);
        //    bd.SaveChanges();

        //    res.Ok = true;
        //    res.InfoAdicional = "Producto insertado correctamente";
        //    return res;

        //}

    }

}



    

