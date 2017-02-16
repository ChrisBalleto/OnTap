namespace OnTap.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class seedBarGames : DbMigration
    {
        public override void Up()
        {
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (1, 'Pool/Billiards')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (2, 'Darts (Electronic)')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (3, 'Darts (Cork)')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (4, 'Skee-Ball')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (5, 'Shuffle Board')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (6, 'Pop-A-Shot Basketball')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (7, 'Air-Hockey')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (8, 'Ping Pong')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (9, 'Pin-Ball')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (10, 'Board Games')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (11, 'Jenga')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (12, 'Punching Bag')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (13, 'Video Slots')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (14, 'Video Poker/Blackjack')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (15, 'SlapShot')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (16, 'Pull-tabs')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (17, 'Megatouch Bartop Games')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (18, 'Buck Hunter')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (19, 'Golden Tee')");
            Sql("SET IDENTITY_INSERT BarGames ON INSERT INTO BarGames(Id, GameName) VALUES (20, 'Misc. Arcade Game(s)')");
        }
        
        public override void Down()
        {
        }
    }
}
