namespace KPWebApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CoursesAdded_2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CourseId);
            
            CreateTable(
                "dbo.UserCourse",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserInfoId, t.CourseId })
                .ForeignKey("dbo.UserInfoes", t => t.UserInfoId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.UserInfoId)
                .Index(t => t.CourseId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserCourse", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.UserCourse", "UserInfoId", "dbo.UserInfoes");
            DropIndex("dbo.UserCourse", new[] { "CourseId" });
            DropIndex("dbo.UserCourse", new[] { "UserInfoId" });
            DropTable("dbo.UserCourse");
            DropTable("dbo.Courses");
        }
    }
}
