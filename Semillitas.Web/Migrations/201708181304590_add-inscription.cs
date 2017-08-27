namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addinscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "Inscription", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memberships", "Inscription");
        }
    }
}
