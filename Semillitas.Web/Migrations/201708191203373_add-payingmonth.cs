namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpayingmonth : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PayingMonth", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PayingMonth");
        }
    }
}
