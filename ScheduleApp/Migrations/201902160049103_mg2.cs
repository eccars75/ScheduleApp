namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Availables", "Tutor_Name", c => c.String());
            AddColumn("dbo.TutorSubjects", "Tutor_Name", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TutorSubjects", "Tutor_Name");
            DropColumn("dbo.Availables", "Tutor_Name");
        }
    }
}
