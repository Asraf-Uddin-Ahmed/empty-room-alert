using AutoMapper;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using EmptyRoomAlert.WebApi.Codes.Core.Factories;
using EmptyRoomAlert.WebApi.Models.Response;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;
using EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates;
using EmptyRoomAlert.WebApi.Codes.Core.Constant;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.WebApi.Models;

namespace EmptyRoomAlert.WebApi.Codes.Persistence.Factories.Aggregates
{
    public class RoomResponseFactory : ResponseFactory<RoomResponseModel>, IRoomResponseFactory
    {
        private MapperConfiguration _mapperConfiguration;
        public RoomResponseFactory(HttpRequestMessage httpRequestMessage)
            : base(httpRequestMessage)
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Room, RoomResponseModel>()
                    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => UrlHelper.Link(UriName.Identity.Roles.GET_ROLE, new { id = src.ID })));
                    .ForMember(dest => dest.RoomStates, opt => opt.Ignore());

            });
        }

        public RoomResponseModel Create(Room room)
        {
            return _mapperConfiguration.CreateMapper().Map<RoomResponseModel>(room);
        }

        public ICollection<RoomResponseModel> Create(ICollection<Room> rooms)
        {
            return rooms.Select(r => this.Create(r)).ToList();
        }

    }
}