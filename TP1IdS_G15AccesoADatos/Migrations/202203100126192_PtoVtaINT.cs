namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PtoVtaINT : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PuntosDeVenta", "NumeroPDV", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PuntosDeVenta", "NumeroPDV", c => c.Long(nullable: false));
        }
    }
}
