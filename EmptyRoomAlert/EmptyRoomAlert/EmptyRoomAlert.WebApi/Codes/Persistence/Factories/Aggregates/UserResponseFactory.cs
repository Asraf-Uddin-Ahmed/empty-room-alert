using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.WebApi.Codes.Core.Factories;
using EmptyRoomAlert.WebApi.Models;
using EmptyRoomAlert.WebApi.Models.Request;
using EmptyRoomAlert.WebApi.Models.Response;

namespace EmptyRoomAlert.WebApi.Codes.Persistence.Factories
{
    public class UserResponseFactory : ResponseFactory<UserResponseModel>, IUserResponseFactory
    {
        private MapperConfiguration _mapperConfiguration;
        public UserResponseFactory(HttpRequestMessage httpRequestMessage)
            : base(httpRequestMessage)
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserResponseModel>()
                    .ForMember(dest => dest.Url, opt => opt.UseValue<string>("#NoUrl"));//opt.MapFrom(src => UrlHelper.Link("#NoUrl", new { id = src.ID }))
            });
        }

        public UserResponseModel Create(User user)
        {
            return _mapperConfiguration.CreateMapper().Map<UserResponseModel>(user);
        }

        public ResponseCollectionModel<UserResponseModel> Create(IEnumerable<User> users, Pagination pagination, SortBy sortBy, int totalItem)
        {
            return base.Create(users.Select(r => this.Create(r)), pagination, sortBy, totalItem);
        }
    }
}