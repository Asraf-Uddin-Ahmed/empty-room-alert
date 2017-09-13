using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Factories;

namespace EmptyRoomAlert.Foundation.Persistence.Factories
{
    public class UserFactory : IUserFactory
    {
        public User Create(string password)
        {
            User user = new User();
            user.ID = GuidUtility.GetNewSequentialGuid();
            user.CreationTime = DateTime.UtcNow;
            user.LastLogin = null;
            user.LastWrongPasswordAttempt = null;
            user.UpdateTime = DateTime.UtcNow;
            user.WrongPasswordAttempt = 0;
            user.EncryptedPassword = CryptographicUtility.Encrypt(password, user.ID);
            return user;
        }
    }
}
