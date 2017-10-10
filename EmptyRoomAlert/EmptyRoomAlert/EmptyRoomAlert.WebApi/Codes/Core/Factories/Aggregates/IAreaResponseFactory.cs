using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.WebApi.Models.Response;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.WebApi.Models;

namespace EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates
{
    public interface IAreaResponseFactory
    {
        AreaResponseModel Create(Area area);
        ICollection<AreaResponseModel> Create(ICollection<Area> areas);
    }
}
