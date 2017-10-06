namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addblogevents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Content = c.String(),
                        Image = c.String(),
                        Layout = c.String(),
                        IsPublished = c.Boolean(nullable: false),
                        DatePublishment = c.DateTime(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        CreationUserName = c.String(),
                        ModifDate = c.DateTime(nullable: false),
                        ModifUserName = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        Description = c.String(),
                        Date = c.DateTime(nullable: false),
                        Content = c.String(),
                        Timetable = c.String(),
                        TimetableNotes = c.String(),
                        Location = c.String(),
                        LocationNotes = c.String(),
                        Contact = c.String(),
                        ContactNotes = c.String(),
                        Pdf = c.String(),
                        PdfNotes = c.String(),
                        Image = c.String(),
                        Layout = c.String(),
                        IsPublished = c.Boolean(nullable: false),
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
            DropTable("dbo.Events");
            DropTable("dbo.Blogs");
        }
    }
}
