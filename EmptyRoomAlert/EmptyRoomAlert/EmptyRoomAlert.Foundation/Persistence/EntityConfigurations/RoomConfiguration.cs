using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Persistence.EntityConfigurations
{
    public class RoomConfiguration : EntityTypeConfiguration<Room>
    {
        public RoomConfiguration()
        {
            Property(r => r.Address)
                .IsRequired();

            Property(r => r.Name)
                .IsRequired();
            
            Property(r => r.Type)
                .IsRequired();
            
        }
    }
}
