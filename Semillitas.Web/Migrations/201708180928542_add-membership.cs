namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addmembership : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Memberships",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                        DurationInDays = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Enrollments", "Membership_ID", c => c.Int());
            CreateIndex("dbo.Enrollments", "Membership_ID");
            AddForeignKey("dbo.Enrollments", "Membership_ID", "dbo.Memberships", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Enrollments", "Membership_ID", "dbo.Memberships");
            DropIndex("dbo.Enrollments", new[] { "Membership_ID" });
            DropColumn("dbo.Enrollments", "Membership_ID");
            DropTable("dbo.Memberships");
        }
    }
}
