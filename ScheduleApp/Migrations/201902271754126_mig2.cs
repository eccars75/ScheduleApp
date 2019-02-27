namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tutor_Id = c.Int(nullable: false),
                        subject = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tutors", t => t.Tutor_Id, cascadeDelete: true)
                .Index(t => t.Tutor_Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Tutor_Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.TutorSubjects");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TutorSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tutor_Name = c.String(),
                        Subjects = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Subjects", "Tutor_Id", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_Id" });
            DropTable("dbo.Tutors");
            DropTable("dbo.Subjects");
        }
    }
}
