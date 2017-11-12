namespace WebGame.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenereEdittedWithQunqueKeyToName : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Genre", "Name", c => c.String(maxLength: 50));
            CreateIndex("dbo.Genre", "Name", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Genre", new[] { "Name" });
            AlterColumn("dbo.Genre", "Name", c => c.String());
        }
    }
}
