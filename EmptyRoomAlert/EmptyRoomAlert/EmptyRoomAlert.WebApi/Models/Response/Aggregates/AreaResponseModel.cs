using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.WebApi.Models.Response.Aggregates
{
    public class AreaResponseModel : ResponseModel
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public ICollection<RoomResponseModel> Rooms { get; set; }

    }
}
