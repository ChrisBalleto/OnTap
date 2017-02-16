namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBsetBarBarGamesZipsStatesCities : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Specials", "Bar_Id", "dbo.Bars");
            DropIndex("dbo.Specials", new[] { "Bar_Id" });
            RenameColumn(table: "dbo.Specials", name: "Bar_Id", newName: "BarId");
            CreateTable(
                "dbo.BarGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GameName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SportsPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PackageName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TapBeers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BreweryDatabaseId = c.String(),
                        Name = c.String(),
                        Description = c.String(),
                        Abv = c.String(),
                        ImageLink = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BarBarGames",
                c => new
                    {
                        Bar_Id = c.Int(nullable: false),
                        BarGame_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Bar_Id, t.BarGame_Id })
                .ForeignKey("dbo.Bars", t => t.Bar_Id, cascadeDelete: true)
                .ForeignKey("dbo.BarGames", t => t.BarGame_Id, cascadeDelete: true)
                .Index(t => t.Bar_Id)
                .Index(t => t.BarGame_Id);
            
            CreateTable(
                "dbo.SportsPackageBars",
                c => new
                    {
                        SportsPackage_Id = c.Int(nullable: false),
                        Bar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.SportsPackage_Id, t.Bar_Id })
                .ForeignKey("dbo.SportsPackages", t => t.SportsPackage_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bars", t => t.Bar_Id, cascadeDelete: true)
                .Index(t => t.SportsPackage_Id)
                .Index(t => t.Bar_Id);
            
            CreateTable(
                "dbo.TapBeerBars",
                c => new
                    {
                        TapBeer_Id = c.Int(nullable: false),
                        Bar_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TapBeer_Id, t.Bar_Id })
                .ForeignKey("dbo.TapBeers", t => t.TapBeer_Id, cascadeDelete: true)
                .ForeignKey("dbo.Bars", t => t.Bar_Id, cascadeDelete: true)
                .Index(t => t.TapBeer_Id)
                .Index(t => t.Bar_Id);
            
            AlterColumn("dbo.Specials", "BarId", c => c.Int(nullable: false));
            CreateIndex("dbo.Specials", "BarId");
            AddForeignKey("dbo.Specials", "BarId", "dbo.Bars", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Specials", "BarId", "dbo.Bars");
            DropForeignKey("dbo.TapBeerBars", "Bar_Id", "dbo.Bars");
            DropForeignKey("dbo.TapBeerBars", "TapBeer_Id", "dbo.TapBeers");
            DropForeignKey("dbo.SportsPackageBars", "Bar_Id", "dbo.Bars");
            DropForeignKey("dbo.SportsPackageBars", "SportsPackage_Id", "dbo.SportsPackages");
            DropForeignKey("dbo.BarBarGames", "BarGame_Id", "dbo.BarGames");
            DropForeignKey("dbo.BarBarGames", "Bar_Id", "dbo.Bars");
            DropIndex("dbo.TapBeerBars", new[] { "Bar_Id" });
            DropIndex("dbo.TapBeerBars", new[] { "TapBeer_Id" });
            DropIndex("dbo.SportsPackageBars", new[] { "Bar_Id" });
            DropIndex("dbo.SportsPackageBars", new[] { "SportsPackage_Id" });
            DropIndex("dbo.BarBarGames", new[] { "BarGame_Id" });
            DropIndex("dbo.BarBarGames", new[] { "Bar_Id" });
            DropIndex("dbo.Specials", new[] { "BarId" });
            AlterColumn("dbo.Specials", "BarId", c => c.Int());
            DropTable("dbo.TapBeerBars");
            DropTable("dbo.SportsPackageBars");
            DropTable("dbo.BarBarGames");
            DropTable("dbo.TapBeers");
            DropTable("dbo.SportsPackages");
            DropTable("dbo.BarGames");
            RenameColumn(table: "dbo.Specials", name: "BarId", newName: "Bar_Id");
            CreateIndex("dbo.Specials", "Bar_Id");
            AddForeignKey("dbo.Specials", "Bar_Id", "dbo.Bars", "Id");
        }
    }
}
