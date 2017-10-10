namespace EmptyRoomAlert.Foundation.Migrations
{
    using EmptyRoomAlert.Foundation.Core.Aggregates;
    using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
    using EmptyRoomAlert.Foundation.Core.Constant;
    using EmptyRoomAlert.Foundation.Core.Enums;
    using EmptyRoomAlert.Foundation.Persistence;
    using EmptyRoomAlert.Foundation.Persistence.Services;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Ratul.Utility;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Clients.Any())
            {
                PopulateClientsTable(context);
            }

            var roleManager = new RoleManager<CustomRole, Guid>(new RoleStore<CustomRole, Guid, CustomUserRole>(context));
            if (!roleManager.Roles.Any())
            {
                PopulateRolesTable(roleManager);
            }

            var manager = new UserManager<ApplicationUser, Guid>(new UserStore<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>(context));
            var user = new ApplicationUser()
            {
                Id = GuidUtility.GetNewSequentialGuid(),
                UserName = "SuperPowerUser",
                Email = "admin@test.com",
                EmailConfirmed = true
            };
            manager.Create(user, "MySuperP@ssword!");
            var adminUser = manager.FindByName(user.UserName);
            manager.AddToRoles(adminUser.Id, new string[] { ApplicationRoles.ADMIN });

            if (!context.Settings.Any())
            {
                PopulateSetingsTable(context);
            }

            if (!context.Rooms.Any() || !context.Areas.Any())
            {
                PopulateAreaAndRoomTable(context);
            }

            context.SaveChanges();
        }

        
        private static void PopulateRolesTable(RoleManager<CustomRole, Guid> roleManager)
        {
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.ADMIN });
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.SUPER_ADMIN });
            roleManager.Create(new CustomRole { Id = GuidUtility.GetNewSequentialGuid(), Name = ApplicationRoles.USER });
        }

        private static void PopulateClientsTable(ApplicationDbContext context)
        {
            List<Client> clientList = new List<Client> 
            {
                new Client
                { 
                    Id = "ngAuthApp", 
                    Secret= HashGenerator.GetHash("abc@123"), 
                    Name="AngularJS front-end Application", 
                    ApplicationType =  ApplicationType.JavaScript, 
                    Active = true, 
                    RefreshTokenLifeTime = 7200, 
                    AllowedOrigin = "*"
                },
                new Client
                { 
                    Id = "consoleApp", 
                    Secret=HashGenerator.GetHash("123@abc"), 
                    Name="Console Application", 
                    ApplicationType =ApplicationType.NativeConfidential, 
                    Active = true, 
                    RefreshTokenLifeTime = 14400, 
                    AllowedOrigin = "*"
                }
            };
            context.Clients.AddRange(clientList);
        }

        private static void PopulateSetingsTable(ApplicationDbContext context)
        {
            List<Settings> listSettings = new List<Settings>
            {
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Max Password Mistake", Name = SettingsName.MaxPasswordMistake.ToString(), Type = SettingsType.Integer, Value = "5"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Host", Name = SettingsName.EmailHost.ToString(), Type = SettingsType.String, Value = "smtp.gmail.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email User Name", Name = SettingsName.EmailUserName.ToString(), Type = SettingsType.String, Value = "projectinfo@gmail.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Password", Name = SettingsName.EmailPassword.ToString(), Type = SettingsType.String, Value = "projectinfo"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Port", Name = SettingsName.EmailPort.ToString(), Type = SettingsType.Integer, Value = "587"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "Email Enable SSL", Name = SettingsName.EmailEnableSSL.ToString(), Type = SettingsType.Boolean, Value = "true"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "System Email Address", Name = SettingsName.SystemEmailAddress.ToString(), Type = SettingsType.String, Value = "info@system.com"},
                new Settings(){ID = GuidUtility.GetNewSequentialGuid(), DisplayName = "System Email Name", Name = SettingsName.SystemEmailName.ToString(), Type = SettingsType.String, Value = "System_Name"}
            };
            listSettings.ForEach(s => context.Settings.AddOrUpdate(p => p.ID, s));
        }
        private static void PopulateAreaAndRoomTable(ApplicationDbContext context)
        {
            List<Area> listArea = new List<Area>
            {
                new Area(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 1", Name = "Area 1"},
                new Area(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 2", Name = "Area 2"},
                new Area(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 3", Name = "Area 3"},
                new Area(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 4", Name = "Area 4"},
            };
            listArea.ForEach(s => context.Areas.AddOrUpdate(p => p.ID, s));
            
            List<Room> listRooms = new List<Room>
            {
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 1_1", Name = "Class Room", Type = RoomType.ClassRoom, AreaID = listArea[0].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 1_2", Name = "Class Room", Type = RoomType.ClassRoom, AreaID = listArea[0].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 2_1", Name = "Your Room", Type = RoomType.Normal, AreaID = listArea[1].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 2_2", Name = "Your Room", Type = RoomType.Normal, AreaID = listArea[1].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 3_1", Name = "Parking Lot", Type = RoomType.ParkingLot, AreaID = listArea[2].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 3_2", Name = "Parking Lot", Type = RoomType.ParkingLot, AreaID = listArea[2].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 4_1", Name = "Parking Lot", Type = RoomType.ParkingLot, AreaID = listArea[3].ID},
                new Room(){ID = GuidUtility.GetNewSequentialGuid(), Address = "address 4_2", Name = "Parking Lot", Type = RoomType.ParkingLot, AreaID = listArea[3].ID},
            };
            listRooms.ForEach(s => context.Rooms.AddOrUpdate(p => p.ID, s));
        }
        
    }
}
