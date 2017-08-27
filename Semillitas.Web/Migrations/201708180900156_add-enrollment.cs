namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addenrollment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Enrollments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Boolean(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                        Notes = c.String(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "User_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Enrollments", new[] { "User_Id" });
            DropTable("dbo.Enrollments");
        }
    }
}
