namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addpaymentmethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Payments", "PaymentMethod", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Payments", "PaymentMethod");
        }
    }
}
