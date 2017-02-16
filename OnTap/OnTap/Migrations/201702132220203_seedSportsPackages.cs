namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedSportsPackages : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT SportsPackages ON INSERT INTO SportsPackages(Id, PackageName) VALUES (1, 'NFL Sunday Ticket')");
            Sql("SET IDENTITY_INSERT SportsPackages ON INSERT INTO SportsPackages(Id, PackageName) VALUES (2, 'NHL Cener Ice')");
            Sql("SET IDENTITY_INSERT SportsPackages ON INSERT INTO SportsPackages(Id, PackageName) VALUES (3, 'MLB Extra Innings')");
            Sql("SET IDENTITY_INSERT SportsPackages ON INSERT INTO SportsPackages(Id, PackageName) VALUES (4, 'NBA League Pass')");
            Sql("SET IDENTITY_INSERT SportsPackages ON INSERT INTO SportsPackages(Id, PackageName) VALUES (5, 'MLS Direct Kick')");
        }
        
        public override void Down()
        {
        }
    }
}
