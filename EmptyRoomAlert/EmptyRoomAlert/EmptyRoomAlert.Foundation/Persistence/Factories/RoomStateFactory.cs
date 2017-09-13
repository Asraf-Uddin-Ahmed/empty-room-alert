using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Factories;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Foundation.Persistence.Factories
{
    public class RoomStateFactory : IRoomStateFactory
    {
        public RoomState Create()
        {
            RoomState roomState = new RoomState();
            roomState.ID = GuidUtility.GetNewSequentialGuid();
            return roomState;
        }
    }
}
