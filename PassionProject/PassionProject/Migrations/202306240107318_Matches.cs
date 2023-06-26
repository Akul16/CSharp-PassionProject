namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Matches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        MatchId = c.Int(nullable: false, identity: true),
                        HomeTeamId = c.Int(nullable: false),
                        OpponentTeam = c.String(),
                        Date = c.DateTime(nullable: false),
                        VenueId = c.Int(nullable: false),
                        HomeTeam_TeamId = c.Int(),
                    })
                .PrimaryKey(t => t.MatchId)
                .ForeignKey("dbo.Teams", t => t.HomeTeam_TeamId)
                .ForeignKey("dbo.Venues", t => t.VenueId, cascadeDelete: true)
                .Index(t => t.VenueId)
                .Index(t => t.HomeTeam_TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matches", "VenueId", "dbo.Venues");
            DropForeignKey("dbo.Matches", "HomeTeam_TeamId", "dbo.Teams");
            DropIndex("dbo.Matches", new[] { "HomeTeam_TeamId" });
            DropIndex("dbo.Matches", new[] { "VenueId" });
            DropTable("dbo.Matches");
        }
    }
}
