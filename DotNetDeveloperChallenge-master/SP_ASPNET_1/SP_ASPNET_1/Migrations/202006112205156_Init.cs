namespace SP_ASPNET_1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Author",
                c => new
                    {
                        AuthorID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.AuthorID);
            
            CreateTable(
                "dbo.BlogPost",
                c => new
                    {
                        BlogPostID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateTime = c.DateTime(nullable: false),
                        Content = c.String(),
                        ImageUrl = c.String(),
                        Author_AuthorID = c.Int(),
                    })
                .PrimaryKey(t => t.BlogPostID)
                .ForeignKey("dbo.Author", t => t.Author_AuthorID)
                .Index(t => t.Author_AuthorID);
            
            CreateTable(
                "dbo.ProductItem",
                c => new
                    {
                        ProductItemID = c.Int(nullable: false, identity: true),
                        ImageURL = c.String(),
                        ImageAlt = c.String(),
                        Title = c.String(),
                        ProductLine_ProductLineID = c.Int(),
                    })
                .PrimaryKey(t => t.ProductItemID)
                .ForeignKey("dbo.ProductLine", t => t.ProductLine_ProductLineID)
                .Index(t => t.ProductLine_ProductLineID);
            
            CreateTable(
                "dbo.ProductLine",
                c => new
                    {
                        ProductLineID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ProductLineID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductItem", "ProductLine_ProductLineID", "dbo.ProductLine");
            DropForeignKey("dbo.BlogPost", "Author_AuthorID", "dbo.Author");
            DropIndex("dbo.ProductItem", new[] { "ProductLine_ProductLineID" });
            DropIndex("dbo.BlogPost", new[] { "Author_AuthorID" });
            DropTable("dbo.ProductLine");
            DropTable("dbo.ProductItem");
            DropTable("dbo.BlogPost");
            DropTable("dbo.Author");
        }
    }
}
