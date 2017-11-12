namespace WebGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class smallChangesInComment : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comment", name: "Game_Id", newName: "GameId");
            RenameIndex(table: "dbo.Comment", name: "IX_Game_Id", newName: "IX_GameId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comment", name: "IX_GameId", newName: "IX_Game_Id");
            RenameColumn(table: "dbo.Comment", name: "GameId", newName: "Game_Id");
        }
    }
}
