using System;
using System.Collections.Generic;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
namespace EmptyRoomAlert.Foundation.Core.Repositories
{
    public interface IUserRepository : IRepositorySearch<User, UserSearch>
    {
        bool IsEmailExist(string email);
        bool IsUserNameExist(string userName);
        User GetByUserName(string userName);
        User GetByEmail(string email);
    }
}
