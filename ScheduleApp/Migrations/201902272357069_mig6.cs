namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sessions", "Subject_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Sessions", "Subject_Id");
            AddForeignKey("dbo.Sessions", "Subject_Id", "dbo.Subjects", "Id", cascadeDelete: true);
            DropColumn("dbo.Sessions", "Tutor_Name");
            DropColumn("dbo.Sessions", "Subject");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sessions", "Subject", c => c.String());
            AddColumn("dbo.Sessions", "Tutor_Name", c => c.String());
            DropForeignKey("dbo.Sessions", "Subject_Id", "dbo.Subjects");
            DropIndex("dbo.Sessions", new[] { "Subject_Id" });
            DropColumn("dbo.Sessions", "Subject_Id");
        }
    }
}
