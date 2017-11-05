namespace HotelMgr.Persistence.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_create : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CountryName = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Guests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerNr = c.Int(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Address = c.String(),
                        ZipCode = c.String(),
                        City = c.String(),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Country_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.Country_Id)
                .Index(t => t.Country_Id);
            
            CreateTable(
                "dbo.ReservationRooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Guest_Id = c.Int(nullable: false),
                        Room_Id = c.Int(nullable: false),
                        From = c.DateTime(nullable: false),
                        To = c.DateTime(nullable: false),
                        BookingNr = c.String(),
                        NumOfAdults = c.Int(nullable: false),
                        NumOfChilds = c.Int(nullable: false),
                        AgeOfChilds = c.String(),
                        FinalCleaning = c.Int(nullable: false),
                        Towels = c.Int(nullable: false),
                        BedLinen = c.Int(nullable: false),
                        CostsRoom = c.Double(nullable: false),
                        CostsFinalCleaning = c.Double(nullable: false),
                        CostsTowels = c.Double(nullable: false),
                        CostsBedLinen = c.Double(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Guests", t => t.Guest_Id, cascadeDelete: true)
                .ForeignKey("dbo.Rooms", t => t.Room_Id, cascadeDelete: true)
                .Index(t => t.Guest_Id)
                .Index(t => t.Room_Id);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoomName = c.String(nullable: false),
                        RoomNumber = c.String(nullable: false),
                        Timestamp = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservationRooms", "Room_Id", "dbo.Rooms");
            DropForeignKey("dbo.ReservationRooms", "Guest_Id", "dbo.Guests");
            DropForeignKey("dbo.Guests", "Country_Id", "dbo.Countries");
            DropIndex("dbo.ReservationRooms", new[] { "Room_Id" });
            DropIndex("dbo.ReservationRooms", new[] { "Guest_Id" });
            DropIndex("dbo.Guests", new[] { "Country_Id" });
            DropTable("dbo.Rooms");
            DropTable("dbo.ReservationRooms");
            DropTable("dbo.Guests");
            DropTable("dbo.Countries");
        }
    }
}
