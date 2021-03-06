﻿using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.Foundation.Persistence.EntityConfigurations;

namespace EmptyRoomAlert.Foundation.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, CustomRole, Guid, CustomUserLogin, CustomUserRole, CustomUserClaim>
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public DbSet<Area> Areas { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomState> RoomStates { get; set; }
        public DbSet<Settings> Settings { get; set; }





        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AreaConfiguration());
            modelBuilder.Configurations.Add(new RoomConfiguration());
            modelBuilder.Configurations.Add(new RoomStateConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}