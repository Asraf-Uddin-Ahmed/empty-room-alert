﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;

namespace EmptyRoomAlert.Foundation.Core.Services
{
    public interface IUserService
    {
        bool DeleteUser(Guid userID);
        ICollection<User> GetUserBy(Pagination pagination, OrderBy<User> orderBy);
        User GetUser(Guid userID);
        User GetUserByEmail(string email);
        User GetUserByUserName(string userName);
        bool IsEmailAddressAlreadyInUse(string email);
        bool IsUserNameAlreadyInUse(string userName);
        bool UpdateUserInformation(User user);
        ICollection<User> GetAll();
        User GetUserByPasswordVerificationCode(string verificationCode);
        int GetTotal();
    }
}
