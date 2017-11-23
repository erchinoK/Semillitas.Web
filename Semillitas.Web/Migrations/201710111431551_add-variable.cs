namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvariable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Description = c.String(),
                        Value = c.String(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreationUserName = c.String(),
                        ModifDate = c.DateTime(nullable: false),
                        ModifUserName = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Variables");
        }
    }
}
