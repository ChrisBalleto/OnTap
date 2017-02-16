namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedDaysOfWeek : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (1, 'Sunday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (2, 'Monday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (3, 'Tuesday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (4, 'Wednesday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (5, 'Thursday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (6, 'Friday')");
            Sql("SET IDENTITY_INSERT DayOfWeeks ON INSERT INTO DayOfWeeks(Id, DayOfWeekName) VALUES (7, 'Saturday')");
        }
        
        public override void Down()
        {
        }
    }
}
