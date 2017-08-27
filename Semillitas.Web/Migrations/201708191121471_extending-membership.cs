namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extendingmembership : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "Description", c => c.String());
            AddColumn("dbo.Memberships", "Order", c => c.Int(nullable: false));
            AddColumn("dbo.Memberships", "IsPublished", c => c.Boolean(nullable: false));
            AddColumn("dbo.Memberships", "Class", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memberships", "Class");
            DropColumn("dbo.Memberships", "IsPublished");
            DropColumn("dbo.Memberships", "Order");
            DropColumn("dbo.Memberships", "Description");
        }
    }
}
