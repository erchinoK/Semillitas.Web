namespace Semillitas.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addchildenrollment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Enrollments", "ChildFirstName", c => c.String());
            AddColumn("dbo.Enrollments", "ChildLastName", c => c.String());
            AddColumn("dbo.Enrollments", "ChildBirthDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Enrollments", "HasSpecialNeed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Enrollments", "SpecialNeedNotes", c => c.String());
            AddColumn("dbo.Enrollments", "ChildNotes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Enrollments", "ChildNotes");
            DropColumn("dbo.Enrollments", "SpecialNeedNotes");
            DropColumn("dbo.Enrollments", "HasSpecialNeed");
            DropColumn("dbo.Enrollments", "ChildBirthDate");
            DropColumn("dbo.Enrollments", "ChildLastName");
            DropColumn("dbo.Enrollments", "ChildFirstName");
        }
    }
}
