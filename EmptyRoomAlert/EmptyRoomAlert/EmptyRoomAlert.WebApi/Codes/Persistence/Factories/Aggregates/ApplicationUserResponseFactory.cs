using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Identity.Managers;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.WebApi.Codes.Core.Factories;
using EmptyRoomAlert.WebApi.Models.Response;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;
using EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates;
using EmptyRoomAlert.WebApi.Codes.Core.Constant;

namespace EmptyRoomAlert.WebApi.Codes.Persistence.Factories.Aggregates
{
    public class ApplicationUserResponseFactory : ResponseFactory<ApplicationUserResponseModel>, IApplicationUserResponseFactory
    {
        private MapperConfiguration _mapperConfiguration;
        public ApplicationUserResponseFactory(HttpRequestMessage httpRequestMessage)
            : base(httpRequestMessage)
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, ApplicationUserResponseModel>()
                    .ForMember(dest => dest.Url, opt => opt.MapFrom(src => UrlHelper.Link(UriName.Identity.Accounts.GET_USER, new { id = src.Id })))
                    .ForMember(dest => dest.RoleUrl, opt => opt.MapFrom(src => UrlHelper.Link(UriName.Identity.Roles.GET_ROLE_BY_USER_ID, new { userID = src.Id })))
                    .ForMember(dest => dest.ClaimUrl, opt => opt.MapFrom(src => UrlHelper.Link(UriName.Identity.Claims.GET_CLAIM_BY_USER_ID, new { userID = src.Id })));
            });
        }


        public ApplicationUserResponseModel Create(ApplicationUser applicationUser)
        {
            return _mapperConfiguration.CreateMapper().Map<ApplicationUserResponseModel>(applicationUser);
        }

        public ICollection<ApplicationUserResponseModel> Create(ICollection<ApplicationUser> applicationUsers)
        {
            return applicationUsers.Select(u => this.Create(u)).ToList();
        }
    }
}