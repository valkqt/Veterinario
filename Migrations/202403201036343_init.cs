namespace Veterinario3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admissions",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataInizio = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DataFine = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        animalID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Animals", t => t.animalID, cascadeDelete: true)
                .Index(t => t.animalID);
            
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DataReg = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Name = c.String(),
                        Tipologia = c.String(nullable: false),
                        Colore = c.String(),
                        DataNascita = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IdMicrochip = c.String(maxLength: 32),
                        NomeProprietario = c.String(nullable: false),
                        CognomeProprietario = c.String(),
                        TelefonoProprietario = c.String(nullable: false),
                        Foto = c.String(nullable: false),
                        Presunta = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .Index(t => t.IdMicrochip, unique: true);
            
            CreateTable(
                "dbo.Therapies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataCura = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Diagnosi = c.String(nullable: false),
                        DescrizioneCura = c.String(nullable: false),
                        Animal_id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Animals", t => t.Animal_id)
                .Index(t => t.Animal_id);
            
            CreateTable(
                "dbo.Boxes",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CodiceCassetto = c.String(),
                        CodiceProdotto = c.String(),
                        product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.product_Id)
                .Index(t => t.product_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Tipologia = c.String(),
                        Quantità = c.Int(nullable: false),
                        CompanyId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Companies", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Companies",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Indirizzo = c.String(),
                        Recapito = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Containers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        CodiceArmadietto = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Placements",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Box_id = c.Int(),
                        Container_id = c.Int(),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Boxes", t => t.Box_id)
                .ForeignKey("dbo.Containers", t => t.Container_id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .Index(t => t.Box_id)
                .Index(t => t.Container_id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.ProductsUsages",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        product_Id = c.Int(),
                        usage_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Products", t => t.product_Id)
                .ForeignKey("dbo.Usages", t => t.usage_Id)
                .Index(t => t.product_Id)
                .Index(t => t.usage_Id);
            
            CreateTable(
                "dbo.Usages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Utilizzo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CodiceFiscale = c.String(maxLength: 16),
                        DataVendita = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RicettaMedica = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        role = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductsUsages", "usage_Id", "dbo.Usages");
            DropForeignKey("dbo.ProductsUsages", "product_Id", "dbo.Products");
            DropForeignKey("dbo.Placements", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Placements", "Container_id", "dbo.Containers");
            DropForeignKey("dbo.Placements", "Box_id", "dbo.Boxes");
            DropForeignKey("dbo.Boxes", "product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "CompanyId", "dbo.Companies");
            DropForeignKey("dbo.Admissions", "animalID", "dbo.Animals");
            DropForeignKey("dbo.Therapies", "Animal_id", "dbo.Animals");
            DropIndex("dbo.ProductsUsages", new[] { "usage_Id" });
            DropIndex("dbo.ProductsUsages", new[] { "product_Id" });
            DropIndex("dbo.Placements", new[] { "Product_Id" });
            DropIndex("dbo.Placements", new[] { "Container_id" });
            DropIndex("dbo.Placements", new[] { "Box_id" });
            DropIndex("dbo.Products", new[] { "CompanyId" });
            DropIndex("dbo.Boxes", new[] { "product_Id" });
            DropIndex("dbo.Therapies", new[] { "Animal_id" });
            DropIndex("dbo.Animals", new[] { "IdMicrochip" });
            DropIndex("dbo.Admissions", new[] { "animalID" });
            DropTable("dbo.Users");
            DropTable("dbo.Sales");
            DropTable("dbo.Usages");
            DropTable("dbo.ProductsUsages");
            DropTable("dbo.Placements");
            DropTable("dbo.Containers");
            DropTable("dbo.Companies");
            DropTable("dbo.Products");
            DropTable("dbo.Boxes");
            DropTable("dbo.Therapies");
            DropTable("dbo.Animals");
            DropTable("dbo.Admissions");
        }
    }
}
