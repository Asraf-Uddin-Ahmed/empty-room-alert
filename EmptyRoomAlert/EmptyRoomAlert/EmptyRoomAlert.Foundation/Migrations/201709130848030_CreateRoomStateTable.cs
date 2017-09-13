namespace EmptyRoomAlert.Foundation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoomStateTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RoomStates",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        IsEmpty = c.Boolean(nullable: false),
                        LogTime = c.DateTime(nullable: false),
                        RoomID = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Rooms", t => t.RoomID, cascadeDelete: true)
                .Index(t => t.RoomID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomStates", "RoomID", "dbo.Rooms");
            DropIndex("dbo.RoomStates", new[] { "RoomID" });
            DropTable("dbo.RoomStates");
        }
    }
}
