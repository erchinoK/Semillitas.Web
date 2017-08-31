namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class decimalmoney : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Memberships", "PriceRegistration", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Memberships", "PriceFee", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Payments", "Amount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Payments", "Amount", c => c.Double(nullable: false));
            AlterColumn("dbo.Memberships", "PriceFee", c => c.Double(nullable: false));
            AlterColumn("dbo.Memberships", "PriceRegistration", c => c.Double(nullable: false));
        }
    }
}
