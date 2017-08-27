namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpayment : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Payments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Amount = c.Double(nullable: false),
                        Type = c.String(),
                        Date = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreationUserName = c.String(),
                        ModifDate = c.DateTime(nullable: false),
                        ModifUserName = c.String(),
                        Notes = c.String(),
                        Enrollment_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Enrollments", t => t.Enrollment_ID)
                .Index(t => t.Enrollment_ID);
            
            AddColumn("dbo.Enrollments", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.Enrollments", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "CreationUserName", c => c.String());
            AddColumn("dbo.Enrollments", "ModifDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "ModifUserName", c => c.String());
            AddColumn("dbo.Memberships", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Memberships", "CreationUserName", c => c.String());
            AddColumn("dbo.Memberships", "ModifDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Memberships", "ModifUserName", c => c.String());
            DropColumn("dbo.Enrollments", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Enrollments", "Status", c => c.Boolean(nullable: false));
            DropForeignKey("dbo.Payments", "Enrollment_ID", "dbo.Enrollments");
            DropIndex("dbo.Payments", new[] { "Enrollment_ID" });
            DropColumn("dbo.Memberships", "ModifUserName");
            DropColumn("dbo.Memberships", "ModifDate");
            DropColumn("dbo.Memberships", "CreationUserName");
            DropColumn("dbo.Memberships", "CreationDate");
            DropColumn("dbo.Enrollments", "ModifUserName");
            DropColumn("dbo.Enrollments", "ModifDate");
            DropColumn("dbo.Enrollments", "CreationUserName");
            DropColumn("dbo.Enrollments", "CreationDate");
            DropColumn("dbo.Enrollments", "IsActive");
            DropTable("dbo.Payments");
        }
    }
}
