namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedmodels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patrons",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        RoleNameId = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        StreetOne = c.String(),
                        StreetTwo = c.String(),
                        CityId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        ZipCodeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.RoleNames", t => t.RoleNameId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: true)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCodeId, cascadeDelete: true)
                .Index(t => t.RoleNameId)
                .Index(t => t.CityId)
                .Index(t => t.StateId)
                .Index(t => t.ZipCodeId);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatronId = c.Int(nullable: false),
                        BarId = c.Int(nullable: false),
                        Subject = c.String(),
                        Content = c.String(),
                        Rating = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bars", t => t.BarId, cascadeDelete: true)
                .ForeignKey("dbo.Patrons", t => t.PatronId, cascadeDelete: true)
                .Index(t => t.PatronId)
                .Index(t => t.BarId);
            
            CreateTable(
                "dbo.Bars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 100),
                        ConfirmPassword = c.String(),
                        RoleNameId = c.Int(nullable: false),
                        BarName = c.String(),
                        LastName = c.String(),
                        StreetOne = c.String(),
                        StreetTwo = c.String(),
                        CityId = c.Int(nullable: false),
                        StateId = c.Int(nullable: false),
                        ZipCodeId = c.Int(nullable: false),
                        HasWifi = c.Boolean(nullable: false),
                        HasJukebox = c.Boolean(nullable: false),
                        Rating = c.Int(),
                        RatingCount = c.Int(),
                        Patron_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: false)
                .ForeignKey("dbo.RoleNames", t => t.RoleNameId, cascadeDelete: false)
                .ForeignKey("dbo.States", t => t.StateId, cascadeDelete: false)
                .ForeignKey("dbo.ZipCodes", t => t.ZipCodeId, cascadeDelete: false)
                .ForeignKey("dbo.Patrons", t => t.Patron_Id)
                .Index(t => t.RoleNameId)
                .Index(t => t.CityId)
                .Index(t => t.StateId)
                .Index(t => t.ZipCodeId)
                .Index(t => t.Patron_Id);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FeedMessages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarId = c.Int(nullable: false),
                        Subject = c.String(),
                        Content = c.String(),
                        Created = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bars", t => t.BarId, cascadeDelete: true)
                .Index(t => t.BarId);
            
            CreateTable(
                "dbo.HoursOfOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BarId = c.Int(nullable: false),
                        DayOfWeek = c.DateTime(nullable: false),
                        DayOfWeekId = c.Int(nullable: false),
                        OpenTime = c.DateTime(nullable: false),
                        CloseTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Bars", t => t.BarId, cascadeDelete: true)
                .Index(t => t.BarId);
            
            CreateTable(
                "dbo.Specials",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeekId = c.Int(nullable: false),
                        SpecialDescription = c.String(),
                        Bar_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DayOfWeeks", t => t.DayOfWeekId, cascadeDelete: true)
                .ForeignKey("dbo.Bars", t => t.Bar_Id)
                .Index(t => t.DayOfWeekId)
                .Index(t => t.Bar_Id);
            
            CreateTable(
                "dbo.DayOfWeeks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DayOfWeekName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ZipCodes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ZipCodeNum = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patrons", "ZipCodeId", "dbo.ZipCodes");
            DropForeignKey("dbo.Patrons", "StateId", "dbo.States");
            DropForeignKey("dbo.Patrons", "RoleNameId", "dbo.RoleNames");
            DropForeignKey("dbo.Bars", "Patron_Id", "dbo.Patrons");
            DropForeignKey("dbo.Patrons", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Reviews", "PatronId", "dbo.Patrons");
            DropForeignKey("dbo.Bars", "ZipCodeId", "dbo.ZipCodes");
            DropForeignKey("dbo.Bars", "StateId", "dbo.States");
            DropForeignKey("dbo.Specials", "Bar_Id", "dbo.Bars");
            DropForeignKey("dbo.Specials", "DayOfWeekId", "dbo.DayOfWeeks");
            DropForeignKey("dbo.Bars", "RoleNameId", "dbo.RoleNames");
            DropForeignKey("dbo.HoursOfOperations", "BarId", "dbo.Bars");
            DropForeignKey("dbo.FeedMessages", "BarId", "dbo.Bars");
            DropForeignKey("dbo.Bars", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Reviews", "BarId", "dbo.Bars");
            DropIndex("dbo.Specials", new[] { "Bar_Id" });
            DropIndex("dbo.Specials", new[] { "DayOfWeekId" });
            DropIndex("dbo.HoursOfOperations", new[] { "BarId" });
            DropIndex("dbo.FeedMessages", new[] { "BarId" });
            DropIndex("dbo.Bars", new[] { "Patron_Id" });
            DropIndex("dbo.Bars", new[] { "ZipCodeId" });
            DropIndex("dbo.Bars", new[] { "StateId" });
            DropIndex("dbo.Bars", new[] { "CityId" });
            DropIndex("dbo.Bars", new[] { "RoleNameId" });
            DropIndex("dbo.Reviews", new[] { "BarId" });
            DropIndex("dbo.Reviews", new[] { "PatronId" });
            DropIndex("dbo.Patrons", new[] { "ZipCodeId" });
            DropIndex("dbo.Patrons", new[] { "StateId" });
            DropIndex("dbo.Patrons", new[] { "CityId" });
            DropIndex("dbo.Patrons", new[] { "RoleNameId" });
            DropTable("dbo.ZipCodes");
            DropTable("dbo.States");
            DropTable("dbo.DayOfWeeks");
            DropTable("dbo.Specials");
            DropTable("dbo.HoursOfOperations");
            DropTable("dbo.FeedMessages");
            DropTable("dbo.Cities");
            DropTable("dbo.Bars");
            DropTable("dbo.Reviews");
            DropTable("dbo.Patrons");
        }
    }
}
