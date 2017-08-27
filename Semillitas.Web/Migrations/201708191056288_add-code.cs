namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Memberships", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Memberships", "Code");
        }
    }
}
