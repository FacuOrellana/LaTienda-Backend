namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClienteIdIsCUIT : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ventas", "CUIT", c => c.Int(nullable: false));
            DropColumn("dbo.Ventas", "ClienteId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ventas", "ClienteId", c => c.Int(nullable: false));
            DropColumn("dbo.Ventas", "CUIT");
        }
    }
}
