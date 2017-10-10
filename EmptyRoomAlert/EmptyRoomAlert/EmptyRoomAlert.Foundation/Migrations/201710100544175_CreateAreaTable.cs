namespace EmptyRoomAlert.Foundation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAreaTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Rooms", "AreaID", c => c.Guid(nullable: false));
            CreateIndex("dbo.Rooms", "AreaID");
            AddForeignKey("dbo.Rooms", "AreaID", "dbo.Areas", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "AreaID", "dbo.Areas");
            DropIndex("dbo.Rooms", new[] { "AreaID" });
            DropColumn("dbo.Rooms", "AreaID");
            DropTable("dbo.Areas");
        }
    }
}
