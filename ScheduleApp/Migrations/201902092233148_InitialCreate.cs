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
                        Tutor_Id = c.String(),
                        Subject = c.String(),
                        Start_Date = c.DateTime(nullable: false),
                        End_Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Availables");
        }
    }
}
