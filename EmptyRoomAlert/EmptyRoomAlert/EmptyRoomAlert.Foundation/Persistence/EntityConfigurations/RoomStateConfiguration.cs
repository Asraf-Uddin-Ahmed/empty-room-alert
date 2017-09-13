using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Persistence.EntityConfigurations
{
    public class RoomStateConfiguration : EntityTypeConfiguration<RoomState>
    {
        public RoomStateConfiguration()
        {
            Property(r => r.IsEmpty)
                .IsRequired();

            Property(r => r.LogTime)
                .IsRequired();

            HasRequired(r => r.Room)
                .WithMany(r => r.RoomStates)
                .HasForeignKey(r => r.RoomID)
                .WillCascadeOnDelete(true);

        }
    }
}
