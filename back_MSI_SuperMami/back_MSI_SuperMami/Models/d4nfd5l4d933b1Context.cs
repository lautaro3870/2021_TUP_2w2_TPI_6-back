using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace back_MSI_SuperMami.Models
{
    public partial class d4nfd5l4d933b1Context : DbContext
    {
        public d4nfd5l4d933b1Context()
        {
        }

        public d4nfd5l4d933b1Context(DbContextOptions<d4nfd5l4d933b1Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Area> Areas { get; set; }
        public virtual DbSet<Categoria> Categorias { get; set; }
        public virtual DbSet<DetalleOrden> DetalleOrdens { get; set; }
        public virtual DbSet<EstadoOrdendecompra> EstadoOrdendecompras { get; set; }
        public virtual DbSet<FormaDeEnvio> FormaDeEnvios { get; set; }
        public virtual DbSet<FormaDePago> FormaDePagos { get; set; }
        public virtual DbSet<OrdenesDeCompra> OrdenesDeCompras { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<Productosxproveedore> Productosxproveedores { get; set; }
        public virtual DbSet<Proveedore> Proveedores { get; set; }
        public virtual DbSet<Proveedoresxformadeenvio> Proveedoresxformadeenvios { get; set; }
        public virtual DbSet<Proveedoresxformasdepago> Proveedoresxformasdepagos { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<UnidadDeMedidum> UnidadDeMedida { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=ec2-44-198-29-193.compute-1.amazonaws.com; port=5432; user id = auilxbgtnzocdy; password = 40ffe977a9c5e826fc09e7d1f5cccbab44fe7fed9bb66c227645f91fe15f6f03; database=d4nfd5l4d933b1; pooling = true; SSL Mode=Prefer;Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF-8");

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.Idarea)
                    .HasName("areas_pk");

                entity.ToTable("areas");

                entity.Property(e => e.Idarea)
                    .HasColumnName("idarea")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Idcategoria)
                    .HasName("categorias_pk");

                entity.ToTable("categorias");

                entity.Property(e => e.Idcategoria)
                    .HasColumnName("idcategoria")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<DetalleOrden>(entity =>
            {
                entity.HasKey(e => e.Iddetalle)
                    .HasName("detalle_orden_pk");

                entity.ToTable("detalle_orden");

                entity.Property(e => e.Iddetalle)
                    .HasColumnName("iddetalle")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Cantidad).HasColumnName("cantidad");

                entity.Property(e => e.Idordendecompra).HasColumnName("idordendecompra");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdordendecompraNavigation)
                    .WithMany(p => p.DetalleOrdens)
                    .HasForeignKey(d => d.Idordendecompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_orden_ordenes_de_compra");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.DetalleOrdens)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("detalle_orden_productos");
            });

            modelBuilder.Entity<EstadoOrdendecompra>(entity =>
            {
                entity.HasKey(e => e.Idestado)
                    .HasName("estado_ordendecompra_pk");

                entity.ToTable("estado_ordendecompra");

                entity.Property(e => e.Idestado)
                    .ValueGeneratedNever()
                    .HasColumnName("idestado");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("estado");
            });

            modelBuilder.Entity<FormaDeEnvio>(entity =>
            {
                entity.HasKey(e => e.Idformadeenvio)
                    .HasName("forma_de_envio_pk");

                entity.ToTable("forma_de_envio");

                entity.Property(e => e.Idformadeenvio)
                    .HasColumnName("idformadeenvio")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Precio).HasColumnName("precio");
            });

            modelBuilder.Entity<FormaDePago>(entity =>
            {
                entity.HasKey(e => e.Idformapago)
                    .HasName("forma_de_pago_pk");

                entity.ToTable("forma_de_pago");

                entity.Property(e => e.Idformapago)
                    .HasColumnName("idformapago")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.Property(e => e.Porcentaje).HasColumnName("porcentaje");
            });

            modelBuilder.Entity<OrdenesDeCompra>(entity =>
            {
                entity.HasKey(e => e.Idordendecompra)
                    .HasName("ordenes_de_compra_pk");

                entity.ToTable("ordenes_de_compra");

                entity.Property(e => e.Idordendecompra)
                    .HasColumnName("idordendecompra")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.FechaRegistro)
                    .HasColumnType("date")
                    .HasColumnName("fecha_registro");

                entity.Property(e => e.Idestado).HasColumnName("idestado");

                entity.Property(e => e.Idformadeenvio).HasColumnName("idformadeenvio");

                entity.Property(e => e.Idformapago).HasColumnName("idformapago");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.HasOne(d => d.IdestadoNavigation)
                    .WithMany(p => p.OrdenesDeCompras)
                    .HasForeignKey(d => d.Idestado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ordenes_de_compra_estado_ordendecompra");

                entity.HasOne(d => d.IdformadeenvioNavigation)
                    .WithMany(p => p.OrdenesDeCompras)
                    .HasForeignKey(d => d.Idformadeenvio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ordenes_de_compra_forma_de_entrega");

                entity.HasOne(d => d.IdformapagoNavigation)
                    .WithMany(p => p.OrdenesDeCompras)
                    .HasForeignKey(d => d.Idformapago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ordenes_de_compra_forma_de_pago");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.OrdenesDeCompras)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("ordenes_de_compra_proveedores");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.Idproducto)
                    .HasName("productos_pk");

                entity.ToTable("productos");

                entity.Property(e => e.Idproducto)
                    .HasColumnName("idproducto")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Idcategoria).HasColumnName("idcategoria");

                entity.Property(e => e.Idunidadmedida).HasColumnName("idunidadmedida");

                entity.Property(e => e.Imagen).HasColumnName("imagen");

                entity.Property(e => e.Marca).HasColumnName("marca");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");

                entity.HasOne(d => d.IdcategoriaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.Idcategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productos_categorias");

                entity.HasOne(d => d.IdunidadmedidaNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.Idunidadmedida)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productos_unidad_de_medida");
            });

            modelBuilder.Entity<Productosxproveedore>(entity =>
            {
                entity.HasKey(e => e.Idproductoproveedor)
                    .HasName("idproductoproveedor_pk");

                entity.ToTable("productosxproveedores");

                entity.Property(e => e.Idproductoproveedor)
                    .HasColumnName("idproductoproveedor")
                    .UseIdentityAlwaysColumn()
                    .HasIdentityOptions(null, null, null, 289566456L, null, null);

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Idproducto).HasColumnName("idproducto");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.Property(e => e.Precio).HasColumnName("precio");

                entity.HasOne(d => d.IdproductoNavigation)
                    .WithMany(p => p.Productosxproveedores)
                    .HasForeignKey(d => d.Idproducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productosxproveedores_productos");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.Productosxproveedores)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("productosxproveedores_proveedores");
            });

            modelBuilder.Entity<Proveedore>(entity =>
            {
                entity.HasKey(e => e.Idproveedor)
                    .HasName("proveedores_pk");

                entity.ToTable("proveedores");

                entity.Property(e => e.Idproveedor)
                    .HasColumnName("idproveedor")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Cuit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("cuit");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("direccion");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Idarea).HasColumnName("idarea");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("nombre");

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("telefono");

                entity.HasOne(d => d.IdareaNavigation)
                    .WithMany(p => p.Proveedores)
                    .HasForeignKey(d => d.Idarea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proveedores_areas");
            });

            modelBuilder.Entity<Proveedoresxformadeenvio>(entity =>
            {
                entity.HasKey(e => new { e.Idproveedor, e.Idformadeenvio })
                    .HasName("proveedoresxformadeenvio_pk");

                entity.ToTable("proveedoresxformadeenvio");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.Property(e => e.Idformadeenvio).HasColumnName("idformadeenvio");

                entity.HasOne(d => d.IdformadeenvioNavigation)
                    .WithMany(p => p.Proveedoresxformadeenvios)
                    .HasForeignKey(d => d.Idformadeenvio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proveedoresxformadeenvio_forma_de_envio");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.Proveedoresxformadeenvios)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proveedoresxformadeenvio_proveedores");
            });

            modelBuilder.Entity<Proveedoresxformasdepago>(entity =>
            {
                entity.HasKey(e => new { e.Idproveedor, e.Idformapago })
                    .HasName("proveedoresxformasdepago_pk");

                entity.ToTable("proveedoresxformasdepago");

                entity.Property(e => e.Idproveedor).HasColumnName("idproveedor");

                entity.Property(e => e.Idformapago).HasColumnName("idformapago");

                entity.HasOne(d => d.IdformapagoNavigation)
                    .WithMany(p => p.Proveedoresxformasdepagos)
                    .HasForeignKey(d => d.Idformapago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proveedoresxformasdepago_forma_de_pago");

                entity.HasOne(d => d.IdproveedorNavigation)
                    .WithMany(p => p.Proveedoresxformasdepagos)
                    .HasForeignKey(d => d.Idproveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("proveedoresxformasdepago_proveedores");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Idrol)
                    .HasName("roles_pk");

                entity.ToTable("roles");

                entity.Property(e => e.Idrol)
                    .ValueGeneratedNever()
                    .HasColumnName("idrol");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("descripcion");

                entity.Property(e => e.Rol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("rol");
            });

            modelBuilder.Entity<UnidadDeMedidum>(entity =>
            {
                entity.HasKey(e => e.Idunidadmedida)
                    .HasName("unidad_de_medida_pk");

                entity.ToTable("unidad_de_medida");

                entity.Property(e => e.Idunidadmedida)
                    .HasColumnName("idunidadmedida")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Descripcion).HasColumnName("descripcion");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("nombre");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario)
                    .HasName("usuarios_pk");

                entity.ToTable("usuarios");

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("id_usuario")
                    .UseIdentityAlwaysColumn();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("email");

                entity.Property(e => e.Estado).HasColumnName("estado");

                entity.Property(e => e.Idrol).HasColumnName("idrol");

                entity.Property(e => e.NombreUsuario)
                    .HasMaxLength(50)
                    .HasColumnName("nombre_usuario");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password");

                //entity.HasOne(d => d.IdrolNavigation)
                //    .WithMany(p => p.Usuarios)
                //    .HasForeignKey(d => d.Idrol)
                //    .OnDelete(DeleteBehavior.ClientSetNull)
                //    .HasConstraintName("usuarios_roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
