using System;
using EmptyRoomAlert.Foundation.Core.Aggregates;
namespace EmptyRoomAlert.Foundation.Core.Services
{
    public interface IMembershipService
    {
        bool BlockUser(Guid userID);
        bool ChangeUserPassword(Guid userID, string newPassword);
        bool ChangeUserPassword(Guid userID, string oldPassword, string newPassword);
        User CreateUser(User user);
        EmptyRoomAlert.Foundation.Core.Enums.LoginStatus ProcessLogin(string userName, string password);
        bool UnblockUser(Guid userID);
        EmptyRoomAlert.Foundation.Core.Enums.VerificationStatus VerifyForPasswordChange(string verificationCode);
        EmptyRoomAlert.Foundation.Core.Enums.VerificationStatus VerifyForUserStatus(string verificationCode);
        PasswordVerification ProcessForgotPassword(User user);
    }
}
