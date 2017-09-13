namespace EmptyRoomAlert.Foundation.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateRoomTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        ID = c.Guid(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Type = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rooms");
        }
    }
}
