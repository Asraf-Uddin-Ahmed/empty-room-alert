using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.SearchData;

namespace EmptyRoomAlert.Foundation.Core.Repositories
{
    public interface IRoomStateRepository : IRepositorySearch<RoomState, RoomStateSearch>
    {
        RoomState GetLastRecordByLogTime();
        ICollection<RoomState> GetIncludedRoomByAndSearch(RoomStateSearch searchItem, Pagination pagination, OrderBy<RoomState> orderBy);
    }
}
