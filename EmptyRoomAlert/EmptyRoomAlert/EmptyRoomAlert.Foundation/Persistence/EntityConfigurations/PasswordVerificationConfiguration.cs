using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Persistence.EntityConfigurations
{
    public class PasswordVerificationConfiguration : EntityTypeConfiguration<PasswordVerification>
    {
        public PasswordVerificationConfiguration()
        {
            Property(u => u.CreationTime)
                .IsRequired();

            Property(u => u.VerificationCode)
                .IsRequired();

        }
    }
}
