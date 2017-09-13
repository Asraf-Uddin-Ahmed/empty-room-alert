using System;
using System.Collections.Generic;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.WebApi.Models;
using EmptyRoomAlert.WebApi.Models.Request;
using EmptyRoomAlert.WebApi.Models.Response;
namespace EmptyRoomAlert.WebApi.Codes.Core.Factories
{
    public interface IUserResponseFactory
    {
        ResponseCollectionModel<UserResponseModel> Create(IEnumerable<User> users, Pagination pagination, SortBy sortBy, int totalItem);
        UserResponseModel Create(User user);
    }
}
