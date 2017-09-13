using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmptyRoomAlert.WebApi.Models.Response.Aggregates
{
    public class RoomStateResponseModel : ResponseModel
    {
        public Guid ID { get; set; }
        public bool IsEmpty { get; set; }
        public DateTime LogTime { get; set; }

        public Guid RoomID { get; set; }
        public RoomResponseModel Room { get; set; }
    }
}