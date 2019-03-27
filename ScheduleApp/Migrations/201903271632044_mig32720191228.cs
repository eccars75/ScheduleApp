namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig32720191228 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TutorSchedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tutor_Id = c.Int(nullable: false),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tutors", t => t.Tutor_Id, cascadeDelete: true)
                .Index(t => t.Tutor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TutorSchedules", "Tutor_Id", "dbo.Tutors");
            DropIndex("dbo.TutorSchedules", new[] { "Tutor_Id" });
            DropTable("dbo.TutorSchedules");
        }
    }
}
