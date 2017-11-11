namespace WebGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateGame : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Body = c.String(),
                        ParentCommentId = c.Int(),
                        Game_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comment", t => t.ParentCommentId)
                .ForeignKey("dbo.Game", t => t.Game_Id)
                .Index(t => t.ParentCommentId)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.Game",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 20),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true);
            
            CreateTable(
                "dbo.Genre",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ParentGenreId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genre", t => t.ParentGenreId)
                .Index(t => t.ParentGenreId);
            
            CreateTable(
                "dbo.PlatformType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.GenreGame",
                c => new
                    {
                        Genre_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Game_Id })
                .ForeignKey("dbo.Genre", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.PlatformTypeGame",
                c => new
                    {
                        PlatformType_Id = c.Int(nullable: false),
                        Game_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlatformType_Id, t.Game_Id })
                .ForeignKey("dbo.PlatformType", t => t.PlatformType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Game", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PlatformType_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlatformTypeGame", "Game_Id", "dbo.Game");
            DropForeignKey("dbo.PlatformTypeGame", "PlatformType_Id", "dbo.PlatformType");
            DropForeignKey("dbo.GenreGame", "Game_Id", "dbo.Game");
            DropForeignKey("dbo.GenreGame", "Genre_Id", "dbo.Genre");
            DropForeignKey("dbo.Genre", "ParentGenreId", "dbo.Genre");
            DropForeignKey("dbo.Comment", "Game_Id", "dbo.Game");
            DropForeignKey("dbo.Comment", "ParentCommentId", "dbo.Comment");
            DropIndex("dbo.PlatformTypeGame", new[] { "Game_Id" });
            DropIndex("dbo.PlatformTypeGame", new[] { "PlatformType_Id" });
            DropIndex("dbo.GenreGame", new[] { "Game_Id" });
            DropIndex("dbo.GenreGame", new[] { "Genre_Id" });
            DropIndex("dbo.Genre", new[] { "ParentGenreId" });
            DropIndex("dbo.Game", new[] { "Key" });
            DropIndex("dbo.Comment", new[] { "Game_Id" });
            DropIndex("dbo.Comment", new[] { "ParentCommentId" });
            DropTable("dbo.PlatformTypeGame");
            DropTable("dbo.GenreGame");
            DropTable("dbo.PlatformType");
            DropTable("dbo.Genre");
            DropTable("dbo.Game");
            DropTable("dbo.Comment");
        }
    }
}
