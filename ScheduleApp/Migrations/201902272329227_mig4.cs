namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tutors", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tutors", "Email", c => c.String());
        }
    }
}
