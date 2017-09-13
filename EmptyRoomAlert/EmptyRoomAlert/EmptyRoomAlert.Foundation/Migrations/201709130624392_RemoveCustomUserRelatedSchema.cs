namespace EmptyRoomAlert.Foundation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveCustomUserRelatedSchema : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PasswordVerifications", "UserID", "dbo.Users");
            DropForeignKey("dbo.UserVerifications", "UserID", "dbo.Users");
            DropIndex("dbo.PasswordVerifications", new[] { "UserID" });
            DropIndex("dbo.UserVerifications", new[] { "UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.PasswordVerifications");
            DropTable("dbo.UserVerifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UserVerifications",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        VerificationCode = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PasswordVerifications",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserID = c.Guid(nullable: false),
                        VerificationCode = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 255),
                        Password = c.String(nullable: false),
                        EmailAddress = c.String(nullable: false, maxLength: 255),
                        Name = c.String(nullable: false, maxLength: 255),
                        TypeOfUser = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        LastLogin = c.DateTime(),
                        WrongPasswordAttempt = c.Int(nullable: false),
                        LastWrongPasswordAttempt = c.DateTime(),
                        CreationTime = c.DateTime(nullable: false),
                        UpdateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateIndex("dbo.UserVerifications", "UserID");
            CreateIndex("dbo.PasswordVerifications", "UserID");
            AddForeignKey("dbo.UserVerifications", "UserID", "dbo.Users", "ID", cascadeDelete: true);
            AddForeignKey("dbo.PasswordVerifications", "UserID", "dbo.Users", "ID", cascadeDelete: true);
        }
    }
}
