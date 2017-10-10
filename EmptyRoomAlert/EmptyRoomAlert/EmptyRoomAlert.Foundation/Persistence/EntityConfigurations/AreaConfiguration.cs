using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Persistence.EntityConfigurations
{
    public class AreaConfiguration : EntityTypeConfiguration<Area>
    {
        public AreaConfiguration()
        {
            Property(r => r.Address)
                .IsOptional();

            Property(r => r.Name)
                .IsRequired();

            HasMany(a => a.Rooms)
                .WithRequired(r => r.Area)
                .HasForeignKey(r => r.AreaID)
                .WillCascadeOnDelete(true);
            
        }
    }
}
