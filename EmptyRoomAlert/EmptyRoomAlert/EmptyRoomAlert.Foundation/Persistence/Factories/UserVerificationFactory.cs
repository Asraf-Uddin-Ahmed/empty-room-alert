using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Factories;

namespace EmptyRoomAlert.Foundation.Persistence.Factories
{
    public class UserVerificationFactory : IUserVerificationFactory
    {
        public UserVerification Create()
        {
            UserVerification userVerification = new UserVerification();
            userVerification.ID = GuidUtility.GetNewSequentialGuid();
            userVerification.CreationTime = DateTime.UtcNow;
            userVerification.VerificationCode = UserUtility.GetNewVerificationCode();
            return userVerification;
        }
    }
}
