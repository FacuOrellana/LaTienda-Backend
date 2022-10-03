using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1IdS_G15Modelo.Entidades;

namespace TP1IdS_G15AccesoADatos
{

    public class DataContext : DbContext
    {
        public DataContext() : base("TP1IDS_G15_DB")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sesion>()
                .ToTable("Sesiones");
            modelBuilder.Entity<Producto>()
                .ToTable("Productos");
            modelBuilder.Entity<Producto>()
                .Property(p => p.CodigoDeBarra)
                .HasMaxLength(50);
            modelBuilder.Entity<Producto>()
                .HasIndex(p => p.CodigoDeBarra)
                .IsUnique()
                .IsClustered(false);
            modelBuilder.Entity<Marca>()
                .ToTable("Marcas");
            modelBuilder.Entity<Rubro>()
                .ToTable("Rubros");
            modelBuilder.Entity<Venta>()
                .ToTable("Ventas");
            modelBuilder.Entity<TipoFactura>()
                .ToTable("TiposDeFactura");
            modelBuilder.Entity<Cliente>()
                .ToTable("Clientes");
            modelBuilder.Entity<Cliente>()
                .HasIndex(c => c.Cuit)
                .IsUnique();
            modelBuilder.Entity<Sucursal>()
                .ToTable("Sucursales");
            modelBuilder.Entity<PuntoDeVenta>()
                .ToTable("PuntosDeVenta");
            modelBuilder.Entity<Empleado>()
                .ToTable("Empleados");
            modelBuilder.Entity<ProductoEnStock>()
                .ToTable("ProductosEnStock");
            modelBuilder.Entity<Color>()
                .ToTable("Colores");
            modelBuilder.Entity<Talle>()
                .ToTable("Talles");
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Sesion> Sesiones { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Marca> Marcas { get; set; }
        public DbSet<Rubro> Rubros { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<TipoFactura> TiposDeFactura { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<PuntoDeVenta> PuntosDeVenta { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<ProductoEnStock> ProductosEnStock { get; set; }
        public DbSet<Talle> Talles { get; set; }
        public DbSet<Color> Colores { get; set; }
    }
}