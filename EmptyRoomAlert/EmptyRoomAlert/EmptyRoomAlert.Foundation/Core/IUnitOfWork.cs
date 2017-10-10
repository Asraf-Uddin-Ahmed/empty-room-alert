using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Repositories;

namespace EmptyRoomAlert.Foundation.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IAreaRepository Areas { get; }
        IRoomRepository Rooms { get; }
        IRoomStateRepository RoomStates { get; }
        ISettingsRepository Settings { get; }


        void Commit();
    }
}
