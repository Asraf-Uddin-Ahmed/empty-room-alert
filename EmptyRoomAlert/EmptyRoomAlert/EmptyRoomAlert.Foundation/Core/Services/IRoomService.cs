using EmptyRoomAlert.Foundation.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Core.Services
{
    public interface IRoomService
    {
        ICollection<Room> GetAll();
    }
}
