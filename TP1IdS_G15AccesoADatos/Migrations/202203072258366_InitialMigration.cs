namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Cuit = c.String(nullable: false, maxLength: 128),
                        RazonSocial = c.String(),
                        Domicilio = c.String(),
                        Condicion = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Cuit);
            
            CreateTable(
                "dbo.Colores",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Empleados",
                c => new
                    {
                        Legajo = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        UserName = c.String(maxLength: 128),
                        SucursalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Legajo)
                .ForeignKey("dbo.Sucursales", t => t.SucursalId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserName)
                .Index(t => t.UserName)
                .Index(t => t.SucursalId);
            
            CreateTable(
                "dbo.Sucursales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Ubicacion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PuntosDeVenta",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumeroPDV = c.Long(nullable: false),
                        SucursalId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sucursales", t => t.SucursalId, cascadeDelete: true)
                .Index(t => t.SucursalId);
            
            CreateTable(
                "dbo.ProductosEnStock",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cantidad = c.Int(nullable: false),
                        SucursalId = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        ColorId = c.Int(nullable: false),
                        TalleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Colores", t => t.ColorId, cascadeDelete: true)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Sucursales", t => t.SucursalId, cascadeDelete: true)
                .ForeignKey("dbo.Talles", t => t.TalleId, cascadeDelete: true)
                .Index(t => t.SucursalId)
                .Index(t => t.ProductoId)
                .Index(t => t.ColorId)
                .Index(t => t.TalleId);
            
            CreateTable(
                "dbo.Productos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodigoDeBarra = c.String(maxLength: 50),
                        Descripcion = c.String(),
                        Costo = c.Decimal(nullable: false, storeType: "money"),
                        MargenDeGanancia = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PorcentajeIVA = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MarcaId = c.Int(nullable: false),
                        RubroId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Marcas", t => t.MarcaId, cascadeDelete: true)
                .ForeignKey("dbo.Rubros", t => t.RubroId, cascadeDelete: true)
                .Index(t => t.CodigoDeBarra, unique: true)
                .Index(t => t.MarcaId)
                .Index(t => t.RubroId);
            
            CreateTable(
                "dbo.Marcas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Rubros",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Talles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        TipoUsuario = c.Int(nullable: false),
                        Email = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, storeType: "money"),
                        Fecha = c.DateTime(nullable: false),
                        NroFacturaAfip = c.Long(nullable: false),
                        PuntoDeVentaId = c.Int(nullable: false),
                        MedioDePago = c.Int(nullable: false),
                        Cuit = c.String(maxLength: 128),
                        Legajo = c.Int(nullable: false),
                        TipoFacturaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cuit)
                .ForeignKey("dbo.PuntosDeVenta", t => t.PuntoDeVentaId, cascadeDelete: true)
                .ForeignKey("dbo.TiposDeFactura", t => t.TipoFacturaId, cascadeDelete: true)
                .ForeignKey("dbo.Empleados", t => t.Legajo, cascadeDelete: false)
                .Index(t => t.PuntoDeVentaId)
                .Index(t => t.Cuit)
                .Index(t => t.Legajo)
                .Index(t => t.TipoFacturaId);
            
            CreateTable(
                "dbo.LineaDeVentas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MontoUnitario = c.Decimal(nullable: false, storeType: "money"),
                        Cantidad = c.Int(nullable: false),
                        ProductoId = c.Int(nullable: false),
                        Venta_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Productos", t => t.ProductoId, cascadeDelete: true)
                .ForeignKey("dbo.Ventas", t => t.Venta_Id)
                .Index(t => t.ProductoId)
                .Index(t => t.Venta_Id);
            
            CreateTable(
                "dbo.TiposDeFactura",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sesiones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsActive = c.Boolean(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        UserName = c.String(maxLength: 128),
                        PuntoDeVentaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PuntosDeVenta", t => t.PuntoDeVentaId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserName)
                .Index(t => t.UserName)
                .Index(t => t.PuntoDeVentaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sesiones", "UserName", "dbo.Users");
            DropForeignKey("dbo.Sesiones", "PuntoDeVentaId", "dbo.PuntosDeVenta");
            DropForeignKey("dbo.Ventas", "Legajo", "dbo.Empleados");
            DropForeignKey("dbo.Ventas", "TipoFacturaId", "dbo.TiposDeFactura");
            DropForeignKey("dbo.Ventas", "PuntoDeVentaId", "dbo.PuntosDeVenta");
            DropForeignKey("dbo.LineaDeVentas", "Venta_Id", "dbo.Ventas");
            DropForeignKey("dbo.LineaDeVentas", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Ventas", "Cuit", "dbo.Clientes");
            DropForeignKey("dbo.Empleados", "UserName", "dbo.Users");
            DropForeignKey("dbo.ProductosEnStock", "TalleId", "dbo.Talles");
            DropForeignKey("dbo.ProductosEnStock", "SucursalId", "dbo.Sucursales");
            DropForeignKey("dbo.ProductosEnStock", "ProductoId", "dbo.Productos");
            DropForeignKey("dbo.Productos", "RubroId", "dbo.Rubros");
            DropForeignKey("dbo.Productos", "MarcaId", "dbo.Marcas");
            DropForeignKey("dbo.ProductosEnStock", "ColorId", "dbo.Colores");
            DropForeignKey("dbo.PuntosDeVenta", "SucursalId", "dbo.Sucursales");
            DropForeignKey("dbo.Empleados", "SucursalId", "dbo.Sucursales");
            DropIndex("dbo.Sesiones", new[] { "PuntoDeVentaId" });
            DropIndex("dbo.Sesiones", new[] { "UserName" });
            DropIndex("dbo.LineaDeVentas", new[] { "Venta_Id" });
            DropIndex("dbo.LineaDeVentas", new[] { "ProductoId" });
            DropIndex("dbo.Ventas", new[] { "TipoFacturaId" });
            DropIndex("dbo.Ventas", new[] { "Legajo" });
            DropIndex("dbo.Ventas", new[] { "Cuit" });
            DropIndex("dbo.Ventas", new[] { "PuntoDeVentaId" });
            DropIndex("dbo.Productos", new[] { "RubroId" });
            DropIndex("dbo.Productos", new[] { "MarcaId" });
            DropIndex("dbo.Productos", new[] { "CodigoDeBarra" });
            DropIndex("dbo.ProductosEnStock", new[] { "TalleId" });
            DropIndex("dbo.ProductosEnStock", new[] { "ColorId" });
            DropIndex("dbo.ProductosEnStock", new[] { "ProductoId" });
            DropIndex("dbo.ProductosEnStock", new[] { "SucursalId" });
            DropIndex("dbo.PuntosDeVenta", new[] { "SucursalId" });
            DropIndex("dbo.Empleados", new[] { "SucursalId" });
            DropIndex("dbo.Empleados", new[] { "UserName" });
            DropTable("dbo.Sesiones");
            DropTable("dbo.TiposDeFactura");
            DropTable("dbo.LineaDeVentas");
            DropTable("dbo.Ventas");
            DropTable("dbo.Users");
            DropTable("dbo.Talles");
            DropTable("dbo.Rubros");
            DropTable("dbo.Marcas");
            DropTable("dbo.Productos");
            DropTable("dbo.ProductosEnStock");
            DropTable("dbo.PuntosDeVenta");
            DropTable("dbo.Sucursales");
            DropTable("dbo.Empleados");
            DropTable("dbo.Colores");
            DropTable("dbo.Clientes");
        }
    }
}
