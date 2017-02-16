namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedCities : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (1, 'Milwaukee')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (2, 'West Allis')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (3, 'Whitefish Bay')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (4, 'Brookfield')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (5, 'Mequon')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (6, 'Cudahy')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (7, 'South Milwaukee')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (8, 'Wauwatosa')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (9, 'Oak Creek')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (10, 'Greenfield')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (11, 'St. Francis')");
            Sql("SET IDENTITY_INSERT Cities ON INSERT INTO Cities(Id, CityName) VALUES (12, 'Glendale')");
        }
        
        public override void Down()
        {
        }
    }
}
