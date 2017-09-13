using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.Foundation.Core.Aggregates
{
    public class Room : Entity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public RoomType Type { get; set; }
    }
}
