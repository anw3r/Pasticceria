namespace Pasticceria.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class propAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        username = c.String(nullable: false, maxLength: 50),
                        password = c.String(maxLength: 50),
                        role = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.username);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        idProduct = c.Int(nullable: false, identity: true),
                        ProdName = c.String(maxLength: 50),
                        Ingredients = c.String(maxLength: 200),
                        ProdDate = c.DateTime(storeType: "date"),
                        Details = c.String(maxLength: 50),
                        Photo = c.String(maxLength: 50),
                        Price = c.Decimal(precision: 3, scale: 1),
                        NPieces = c.Int(),
                    })
                .PrimaryKey(t => t.idProduct);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Product");
            DropTable("dbo.Logins");
        }
    }
}
