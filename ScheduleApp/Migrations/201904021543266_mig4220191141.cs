namespace ScheduleApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig4220191141 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Availables");
        }
        
        public override void Down()
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
            
        }
    }
}
