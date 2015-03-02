namespace KPWebApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BoolByAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Photos", "AddedByAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Photos", "AddedByAdmin");
        }
    }
}
