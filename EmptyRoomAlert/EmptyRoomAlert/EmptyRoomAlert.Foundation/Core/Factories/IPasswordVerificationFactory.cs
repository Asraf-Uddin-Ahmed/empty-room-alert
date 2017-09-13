using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;

namespace EmptyRoomAlert.Foundation.Core.Factories
{
    public interface IPasswordVerificationFactory
    {
        PasswordVerification Create();
    }
}
