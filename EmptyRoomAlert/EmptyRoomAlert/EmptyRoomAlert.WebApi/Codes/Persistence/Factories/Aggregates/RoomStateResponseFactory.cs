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
    public class RoomStateResponseFactory : ResponseFactory<RoomStateResponseModel>, IRoomStateResponseFactory
    {
        private MapperConfiguration _mapperConfiguration;
        public RoomStateResponseFactory(HttpRequestMessage httpRequestMessage)
            : base(httpRequestMessage)
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RoomState, RoomStateResponseModel>();
                    //.ForMember(dest => dest.Url, opt => opt.MapFrom(src => UrlHelper.Link(UriName.Identity.Roles.GET_ROLE, new { id = src.ID })));

                cfg.CreateMap<Room, RoomResponseModel>()
                    .ForMember(dest => dest.RoomStates, opt => opt.Ignore());

            });
        }

        public RoomStateResponseModel Create(RoomState roomState)
        {
            return _mapperConfiguration.CreateMapper().Map<RoomStateResponseModel>(roomState);
        }

        public ICollection<RoomStateResponseModel> Create(ICollection<RoomState> roomStates)
        {
            return roomStates.Select(r => this.Create(r)).ToList();
        }

        public ResponseCollectionModel<RoomStateResponseModel> Create(ICollection<RoomState> roomStates, Pagination pagination, SortBy sortBy, int total)
        {
            return base.Create(roomStates.Select(u => this.Create(u)).ToList(), pagination, sortBy, total);
        }
    }
}