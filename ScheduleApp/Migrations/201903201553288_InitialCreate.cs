namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Availables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tutor_Name = c.String(),
                        Subject = c.String(),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Student_Name = c.String(),
                        Subject_Id = c.Int(nullable: false),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        NoShow = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tutor_Id = c.Int(nullable: false),
                        Subject = c.String(),
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sessions", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "Tutor_Id", "dbo.Tutors");
            DropIndex("dbo.Subjects", new[] { "Tutor_Id" });
            DropIndex("dbo.Sessions", new[] { "Subject_Id" });
            DropTable("dbo.Tutors");
            DropTable("dbo.Subjects");
            DropTable("dbo.Sessions");
            DropTable("dbo.Availables");
        }
    }
}
