namespace SurveyPage.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReviewTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "Professionalism", c => c.Int(nullable: false));
            AddColumn("dbo.Reviews", "Accountability", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Reviews", "Accountability");
            DropColumn("dbo.Reviews", "Professionalism");
        }
    }
}
