namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mg1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Student_Name = c.String(),
                        Tutor_Name = c.String(),
                        Subject = c.String(),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                        Completed = c.Boolean(nullable: false),
                        NoShow = c.Boolean(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TutorSubjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Availables", "Tutor_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Availables", "Tutor_Id", c => c.String());
            DropTable("dbo.TutorSubjects");
            DropTable("dbo.Sessions");
        }
    }
}
