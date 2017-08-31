namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifmembership : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "Duration", c => c.String());
            DropColumn("dbo.Memberships", "Inscription");
            DropColumn("dbo.Memberships", "Price");
            DropColumn("dbo.Memberships", "DurationInDays");
            DropColumn("dbo.Payments", "IsPaid");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Payments", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.Memberships", "DurationInDays", c => c.Int(nullable: false));
            AddColumn("dbo.Memberships", "Price", c => c.Double(nullable: false));
            AddColumn("dbo.Memberships", "Inscription", c => c.Double(nullable: false));
            DropColumn("dbo.Memberships", "Duration");
        }
    }
}
