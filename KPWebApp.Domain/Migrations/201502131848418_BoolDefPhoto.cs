namespace KPWebApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolDefPhoto : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "IsProfilePhotoOfTeacher", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "IsProfilePhotoOfTeacher");
        }
    }
}
