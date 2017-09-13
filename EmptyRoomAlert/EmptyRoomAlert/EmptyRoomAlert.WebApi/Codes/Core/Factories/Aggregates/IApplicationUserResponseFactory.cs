using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.WebApi.Models.Response;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;

namespace EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates
{
    public interface IApplicationUserResponseFactory
    {
        ApplicationUserResponseModel Create(ApplicationUser applicationUser);
        ICollection<ApplicationUserResponseModel> Create(ICollection<ApplicationUser> applicationUsers);
    }
}
