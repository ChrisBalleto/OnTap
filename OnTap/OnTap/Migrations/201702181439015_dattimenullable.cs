namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dattimenullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HoursOfOperations", "OpenTime", c => c.DateTime());
            AlterColumn("dbo.HoursOfOperations", "CloseTime", c => c.DateTime());
            CreateIndex("dbo.HoursOfOperations", "DayOfWeekId");
            AddForeignKey("dbo.HoursOfOperations", "DayOfWeekId", "dbo.DayOfWeeks", "Id", cascadeDelete: true);
            DropColumn("dbo.HoursOfOperations", "DayOfWeek");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HoursOfOperations", "DayOfWeek", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.HoursOfOperations", "DayOfWeekId", "dbo.DayOfWeeks");
            DropIndex("dbo.HoursOfOperations", new[] { "DayOfWeekId" });
            AlterColumn("dbo.HoursOfOperations", "CloseTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.HoursOfOperations", "OpenTime", c => c.DateTime(nullable: false));
        }
    }
}
