using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MyMusicApp.Datos.MyMusicModel
{
    public partial class DB_A4C98C_MusicStoreDBContext : DbContext
    {
        public DB_A4C98C_MusicStoreDBContext()
        {
        }

        public DB_A4C98C_MusicStoreDBContext(DbContextOptions<DB_A4C98C_MusicStoreDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; }
        public virtual DbSet<DetalleCompra> DetalleCompras { get; set; }
        public virtual DbSet<OrdenCompra> OrdenCompras { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }
        public virtual DbSet<SolicitudEnvioDomic> SolicitudEnvioDomics { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<Vendedor> Vendedors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=SQL5103.site4now.net;Database=DB_A4C98C_MusicStoreDB;User Id=DB_A4C98C_MusicStoreDB_admin;Password=zxcv1234");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.PkCliente)
                    .HasName("Cliente_PK");

                entity.ToTable("Cliente");

                entity.Property(e => e.DesCedula).HasMaxLength(50);

                entity.Property(e => e.DesContrasena).HasMaxLength(100);

                entity.Property(e => e.EmlDirCliente)
                    .HasMaxLength(30)
                    .HasColumnName("emlDirCliente");

                entity.Property(e => e.FecNacimiento).HasColumnType("date");

                entity.Property(e => e.NomCliente).HasMaxLength(300);

                entity.Property(e => e.TelCliente).HasMaxLength(40);

                entity.Property(e => e.TipSexo).HasMaxLength(1);

                entity.Property(e => e.UsrCliente).HasMaxLength(80);
            });

            modelBuilder.Entity<DetalleCompra>(entity =>
            {
                entity.HasKey(e => e.PkDetalleOrden)
                    .HasName("DetalleCompra_PK");

                entity.ToTable("DetalleCompra");

                entity.HasOne(d => d.FkOrdenCompraNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.FkOrdenCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleCompra_OrdenCompra_FK");

                entity.HasOne(d => d.FkProductoNavigation)
                    .WithMany(p => p.DetalleCompras)
                    .HasForeignKey(d => d.FkProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("DetalleCompra_Producto_FK");
            });

            modelBuilder.Entity<OrdenCompra>(entity =>
            {
                entity.HasKey(e => e.PkOrdenCompra)
                    .HasName("OrdenCompra_PK");

                entity.ToTable("OrdenCompra");

                entity.Property(e => e.FecOrden).HasColumnType("date");

                entity.Property(e => e.MntTotalOrden).HasColumnType("numeric(18, 2)");

                entity.HasOne(d => d.FkClienteNavigation)
                    .WithMany(p => p.OrdenCompras)
                    .HasForeignKey(d => d.FkCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrdenCompra_Cliente_FK");

                entity.HasOne(d => d.FkVendedorNavigation)
                    .WithMany(p => p.OrdenCompras)
                    .HasForeignKey(d => d.FkVendedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("OrdenCompra_Vendedor_FK");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.PkProducto)
                    .HasName("Producto_PK");

                entity.ToTable("Producto");

                entity.Property(e => e.MtoPrecioUnitario).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NomProducto).HasMaxLength(100);

                entity.HasOne(d => d.FkSucursalNavigation)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.FkSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Producto_Sucursal_FK");
            });

            modelBuilder.Entity<SolicitudEnvioDomic>(entity =>
            {
                entity.HasKey(e => e.PkSolicitudEnvio)
                    .HasName("SolicitudEnvioDomic_PK");

                entity.ToTable("SolicitudEnvioDomic");

                entity.Property(e => e.DesUbicEnvio)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(e => e.FecEnvio).HasColumnType("date");

                entity.Property(e => e.FecRecibo).HasColumnType("date");

                entity.HasOne(d => d.FkOrdenCompraNavigation)
                    .WithMany(p => p.SolicitudEnvioDomics)
                    .HasForeignKey(d => d.FkOrdenCompra)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("SolicitudEnvioDomic_OrdenCompra_FK");
            });

            modelBuilder.Entity<Sucursal>(entity =>
            {
                entity.HasKey(e => e.PkSucursal)
                    .HasName("Sucursal_PK");

                entity.ToTable("Sucursal");

                entity.Property(e => e.DesHorario).HasMaxLength(300);

                entity.Property(e => e.DirUbicacion).HasMaxLength(300);

                entity.Property(e => e.EmSucursal).HasMaxLength(300);

                entity.Property(e => e.TelSucursal).HasMaxLength(50);
            });

            modelBuilder.Entity<Vendedor>(entity =>
            {
                entity.HasKey(e => e.PkVendedor)
                    .HasName("Vendedor_PK");

                entity.ToTable("Vendedor");

                entity.Property(e => e.CodCedula).HasMaxLength(50);

                entity.Property(e => e.DesPuesto).HasMaxLength(100);

                entity.Property(e => e.NomVendedor).HasMaxLength(80);

                entity.HasOne(d => d.FkSucursalNavigation)
                    .WithMany(p => p.Vendedors)
                    .HasForeignKey(d => d.FkSucursal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Vendedor_Sucursal_FK");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
