using back_MSI_SuperMami.Comandos;
using back_MSI_SuperMami.DTOs;
using back_MSI_SuperMami.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace back_MSI_SuperMami.Helpers
{
    public class Metodos
    {
        private readonly d4nfd5l4d933b1Context bd = new d4nfd5l4d933b1Context();
        public List<ComandoRegistrarFormaPago> obtenerPagos(int id)
        {
            var forXpro = bd.Proveedoresxformasdepagos.Where(f => f.Idproveedor == id).ToList();

            var lista = new List<ComandoRegistrarFormaPago>();

            if (forXpro != null)
            {

                foreach (var i in forXpro)
                {
                    var f = bd.FormaDePagos.FirstOrDefault(f => f.Idformapago == i.Idformapago);

                    var forma = new ComandoRegistrarFormaPago
                    {
                        nombre = f.Nombre,
                        descripcion = f.Descripcion,
                        idformadepago = f.Idformapago
                    };

                    lista.Add(forma);
                }
                return lista;
            }

            return lista;
        }

        public List<ComandoRegistrarFormaEnvio> obtenerEnvios(int id)
        {
            var envXpro = bd.Proveedoresxformadeenvios.Where(f => f.Idproveedor == id).ToList();

            var lista = new List<ComandoRegistrarFormaEnvio>();

            if (envXpro != null)
            {

                foreach (var i in envXpro)
                {
                    var f = bd.FormaDeEnvios.FirstOrDefault(f => f.Idformadeenvio == i.Idformadeenvio);

                    var forma = new ComandoRegistrarFormaEnvio
                    {
                        nombre = f.Nombre,
                        descripcion = f.Descripcion,
                        idformadeenvio = f.Idformadeenvio
                    };

                    lista.Add(forma);
                }
                return lista;
            }

            return lista;
        }

        public List<DTOProductosPrecioProveedor> obtenerProductosxProveedor(int id)
        {
            //var pro = bd.Proveedores.FirstOrDefault(f => f.Idproveedor == id);

            var proXpro = bd.Productosxproveedores.Where(f => f.Idproveedor == id).ToList();

            var lista = new List<DTOProductosPrecioProveedor>();

            if (proXpro != null)
            {
                foreach (var i in proXpro)
                {
                    var proxProv = bd.Productosxproveedores.FirstOrDefault(f => f.Idproductoproveedor == i.Idproductoproveedor);
                    var prod = bd.Productos.FirstOrDefault(f => f.Idproducto == i.Idproducto);
                    var uni = bd.UnidadDeMedida.FirstOrDefault(f => f.Idunidadmedida == prod.Idunidadmedida);
                    var cat = bd.Categorias.FirstOrDefault(f => f.Idcategoria == prod.Idcategoria);

                    if (proxProv.Estado == true)
                    {
                        var dto = new DTOProductosPrecioProveedor
                        {
                            idproducto = prod.Idproducto,
                            nombre = prod.Nombre,
                            descripcion = prod.Descripcion,
                            unidadMedida = uni.Nombre,
                            categoria = cat.Nombre,
                            marca = prod.Marca,
                            imagen = prod.Imagen,
                            precio = proxProv.Precio
                        };

                        lista.Add(dto);
                    }
                }

                return lista;
            }

            return lista;
        }


    }
}
