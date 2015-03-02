namespace KPWebApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsTeacherPropAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "IsTeacher", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "IsTeacher");
        }
    }
}
