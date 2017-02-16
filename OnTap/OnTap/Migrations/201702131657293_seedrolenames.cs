namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedrolenames : DbMigration
    {
        public override void Up()
        {

            Sql("SET IDENTITY_INSERT RoleNames ON INSERT INTO RoleNames(Id, Name) VALUES (1, 'Patron')");
            Sql("SET IDENTITY_INSERT RoleNames ON INSERT INTO RoleNames(Id, Name) VALUES (2, 'Bar')");
        }
        
        public override void Down()
        {
        }
    }
}
