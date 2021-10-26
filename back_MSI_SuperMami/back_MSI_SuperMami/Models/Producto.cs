using System;
using System.Collections.Generic;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class Producto
    {
        public Producto()
        {
            DetalleOrdens = new HashSet<DetalleOrden>();
            Productosxproveedores = new HashSet<Productosxproveedore>();
        }

        public int Idproducto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public DateTime Vencimiento { get; set; }
        public bool Estado { get; set; }
        public int Idunidadmedida { get; set; }
        public int Idcategoria { get; set; }
        public int Idmarca { get; set; }

        public virtual Categoria IdcategoriaNavigation { get; set; }
        public virtual Marca IdmarcaNavigation { get; set; }
        public virtual UnidadDeMedidum IdunidadmedidaNavigation { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrdens { get; set; }
        public virtual ICollection<Productosxproveedore> Productosxproveedores { get; set; }
    }
}
