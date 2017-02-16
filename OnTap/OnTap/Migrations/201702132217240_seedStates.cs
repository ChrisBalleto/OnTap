namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedStates : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT States ON INSERT INTO States(Id, StateName) VALUES (1, 'Wisconsin')");
            Sql("SET IDENTITY_INSERT States ON INSERT INTO States(Id, StateName) VALUES (2, 'Illinois')");
        }
        
        public override void Down()
        {
        }
    }
}
