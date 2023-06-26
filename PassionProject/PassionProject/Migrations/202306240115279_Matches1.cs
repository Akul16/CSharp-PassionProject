namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Matches1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "HomeTeam_TeamId", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "HomeTeam_TeamId" });
            DropColumn("dbo.Matches", "HomeTeamId");
            RenameColumn(table: "dbo.Matches", name: "HomeTeam_TeamId", newName: "HomeTeamId");
            AlterColumn("dbo.Matches", "HomeTeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "HomeTeamId");
            AddForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams", "TeamId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "HomeTeamId", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "HomeTeamId" });
            AlterColumn("dbo.Matches", "HomeTeamId", c => c.Int());
            RenameColumn(table: "dbo.Matches", name: "HomeTeamId", newName: "HomeTeam_TeamId");
            AddColumn("dbo.Matches", "HomeTeamId", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "HomeTeam_TeamId");
            AddForeignKey("dbo.Matches", "HomeTeam_TeamId", "dbo.Teams", "TeamId");
        }
    }
}
