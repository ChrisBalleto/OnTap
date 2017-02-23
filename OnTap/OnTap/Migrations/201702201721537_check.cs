namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class check : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Bars", "Patron_Id", "dbo.Patrons");
            DropIndex("dbo.Bars", new[] { "Patron_Id" });
            CreateTable(
                "dbo.PatronBars",
                c => new
                    {
                        Patron_Id = c.Int(nullable: false),
                        Bar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Patron_Id, t.Bar_Id })
                .ForeignKey("dbo.Patrons", t => t.Patron_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bars", t => t.Bar_Id, cascadeDelete: true)
                .Index(t => t.Patron_Id)
                .Index(t => t.Bar_Id);
            
            DropColumn("dbo.Bars", "Patron_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bars", "Patron_Id", c => c.Int());
            DropForeignKey("dbo.PatronBars", "Bar_Id", "dbo.Bars");
            DropForeignKey("dbo.PatronBars", "Patron_Id", "dbo.Patrons");
            DropIndex("dbo.PatronBars", new[] { "Bar_Id" });
            DropIndex("dbo.PatronBars", new[] { "Patron_Id" });
            DropTable("dbo.PatronBars");
            CreateIndex("dbo.Bars", "Patron_Id");
            AddForeignKey("dbo.Bars", "Patron_Id", "dbo.Patrons", "Id");
        }
    }
}
