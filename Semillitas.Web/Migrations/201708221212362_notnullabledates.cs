namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notnullabledates : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "StartDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Enrollments", "ExpirationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "ExpirationDate", c => c.DateTime());
            AlterColumn("dbo.Enrollments", "StartDate", c => c.DateTime());
        }
    }
}
