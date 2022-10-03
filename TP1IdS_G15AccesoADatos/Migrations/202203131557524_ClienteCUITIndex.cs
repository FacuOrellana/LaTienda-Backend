namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClienteCUITIndex : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Clientes", "Cuit", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Clientes", new[] { "Cuit" });
        }
    }
}
