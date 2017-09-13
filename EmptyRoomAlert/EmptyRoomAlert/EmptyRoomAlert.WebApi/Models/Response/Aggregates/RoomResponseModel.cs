using EmptyRoomAlert.Foundation.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmptyRoomAlert.WebApi.Models.Response.Aggregates
{
    public class RoomResponseModel : ResponseModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public RoomType Type { get; set; }

        public ICollection<RoomStateResponseModel> RoomStates { get; set; }
    }
}