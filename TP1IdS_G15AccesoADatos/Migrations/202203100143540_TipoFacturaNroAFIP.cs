namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TipoFacturaNroAFIP : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TiposDeFactura", "NroTipoFacturaAFIP", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TiposDeFactura", "NroTipoFacturaAFIP");
        }
    }
}
