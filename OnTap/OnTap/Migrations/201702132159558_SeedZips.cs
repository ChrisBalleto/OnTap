namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedZips : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Patrons", "PhoneNumber", c => c.String());
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (1, 53224)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (2, 53223)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (3, 53225)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (4, 53218)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (5, 53116)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (6, 53222)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (7, 53209)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (8, 53216)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (9, 53210)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (10, 53206)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (11, 53212)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (12, 53202)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (13, 53211)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (14, 53204)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (15, 53233)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (16, 53205)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (17, 53215)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (18, 53219)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (19, 53207)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (20, 53221)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (21, 53214)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (22, 53203)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (23, 53213)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (24, 53235)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (25, 53110)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (26, 53220)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (27, 53217)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (28, 53132)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (29, 53154)");
            Sql("SET IDENTITY_INSERT ZipCodes ON INSERT INTO ZipCodes(Id, ZipCodeNum) VALUES (30, 53172)");
        }
        
        public override void Down()
        {
            DropColumn("dbo.Patrons", "PhoneNumber");
        }
    }
}
