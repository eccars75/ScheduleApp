namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tutors", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tutors", "Email");
        }
    }
}
