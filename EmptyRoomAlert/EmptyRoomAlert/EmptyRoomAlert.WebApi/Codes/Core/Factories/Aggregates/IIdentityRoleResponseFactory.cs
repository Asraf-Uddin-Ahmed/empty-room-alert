using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.WebApi.Models.Response;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;

namespace EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates
{
    public interface IIdentityRoleResponseFactory
    {
        IdentityRoleResponseModel Create(CustomRole appRole);
        ICollection<IdentityRoleResponseModel> Create(ICollection<CustomRole> identityRoles);
    }
}
