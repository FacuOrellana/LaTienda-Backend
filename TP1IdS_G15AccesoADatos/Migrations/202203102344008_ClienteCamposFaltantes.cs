namespace TP1IdS_G15AccesoADatos.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClienteCamposFaltantes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clientes", "NombreApellido", c => c.String());
            AddColumn("dbo.Clientes", "Telefono", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clientes", "Telefono");
            DropColumn("dbo.Clientes", "NombreApellido");
        }
    }
}
