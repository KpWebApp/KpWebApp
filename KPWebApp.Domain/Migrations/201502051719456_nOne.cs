namespace KPWebApp.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nOne : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        PhotoId = c.Int(nullable: false, identity: true),
                        ImageData = c.Binary(storeType: "image"),
                        ImageMimeType = c.String(),
                        PhotoHeader = c.String(nullable: false),
                        UserInfo_UserInfoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PhotoId)
                .ForeignKey("dbo.UserInfoes", t => t.UserInfo_UserInfoId, cascadeDelete: true)
                .Index(t => t.UserInfo_UserInfoId);
            
            CreateTable(
                "dbo.UserInfoes",
                c => new
                    {
                        UserInfoId = c.Int(nullable: false),
                        Email = c.String(),
                        Status = c.String(),
                        Gender = c.Int(nullable: false),
                        BIO = c.String(),
                        ContactInfo_Skype = c.String(),
                        ContactInfo_Facebook = c.String(),
                        ContactInfo_HomeNumber = c.String(),
                        ContactInfo_MobNumber = c.String(),
                        GraduateInfo_University = c.String(),
                        GraduateInfo_Speciality = c.Int(nullable: false),
                        GraduateInfo_EntranceYear = c.Int(nullable: false),
                        GraduateInfo_GraduationYear = c.Int(nullable: false),
                        Teacher_Chiar = c.String(),
                        Teacher_Degree = c.String(),
                        Teacher_WorksFrom = c.DateTime(precision: 7, storeType: "datetime2"),
                        Teacher_WorkedTill = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.UserInfoId)
                .ForeignKey("dbo.Users", t => t.UserInfoId)
                .Index(t => t.UserInfoId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        Role = c.Int(nullable: false),
                        FullName = c.String(),
                        IsRegistred = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        Header = c.String(nullable: false),
                        Text = c.String(nullable: false),
                        Time = c.DateTime(nullable: false),
                        Category = c.Int(nullable: false),
                        ImageData = c.Binary(),
                        ImageMimeType = c.String(),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserInfoes", "UserInfoId", "dbo.Users");
            DropForeignKey("dbo.Posts", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Photos", "UserInfo_UserInfoId", "dbo.UserInfoes");
            DropIndex("dbo.Posts", new[] { "User_UserId" });
            DropIndex("dbo.UserInfoes", new[] { "UserInfoId" });
            DropIndex("dbo.Photos", new[] { "UserInfo_UserInfoId" });
            DropTable("dbo.Posts");
            DropTable("dbo.Users");
            DropTable("dbo.UserInfoes");
            DropTable("dbo.Photos");
        }
    }
}
