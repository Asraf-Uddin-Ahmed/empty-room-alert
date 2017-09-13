using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Core.Services
{
    public interface IRoomStateService
    {
        void GenerateValues(int timeInMinute, int frequencyInMinute);
        ICollection<RoomState> GetByAndSearch(RoomStateSearch searchItem, Pagination pagination, OrderBy<RoomState> orderBy);
        int GetTotalByAndSearch(RoomStateSearch searchItem);
    }
}
