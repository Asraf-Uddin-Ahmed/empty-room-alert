using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.Foundation.Core.Aggregates
{
    public class RoomState : Entity
    {
        public bool IsEmpty { get; set; }
        public DateTime LogTime { get; set; }

        public Guid RoomID { get; set; }
        public Room Room { get; set; }

    }
}
