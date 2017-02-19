namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedbargamefoosball : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (21, 'Foosball')");
        }
        
        public override void Down()
        {
        }
    }
}
