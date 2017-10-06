namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addimagepreview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Blogs", "ImagePreview", c => c.String());
            AddColumn("dbo.Events", "ImagePreview", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Events", "ImagePreview");
            DropColumn("dbo.Blogs", "ImagePreview");
        }
    }
}
