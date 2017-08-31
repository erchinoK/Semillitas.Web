namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrenewalnumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "RenewalNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "RenewalNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Payments", "IsPaid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "IsPaid");
            DropColumn("dbo.Payments", "RenewalNumber");
            DropColumn("dbo.Enrollments", "RenewalNumber");
        }
    }
}
