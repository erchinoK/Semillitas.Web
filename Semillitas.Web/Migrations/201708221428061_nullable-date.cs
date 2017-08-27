namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullabledate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Enrollments", "StartDate", c => c.DateTime());
            AlterColumn("dbo.Enrollments", "ExpirationDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Enrollments", "ExpirationDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Enrollments", "StartDate", c => c.DateTime(nullable: false));
        }
    }
}
