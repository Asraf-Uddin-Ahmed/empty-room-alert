using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Factories;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.Foundation.Core.Services;
using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Persistence.Services
{
    public class RoomStateService : IRoomStateService
    {
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        private IRoomStateFactory _roomStateFactory;
        [Inject]
        public RoomStateService(ILogger logger,
            IRoomStateFactory roomStateFactory,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _roomStateFactory = roomStateFactory;
            _unitOfWork = unitOfWork;
        }

        public DateTime GenerateValues(int timeInMinute, int frequencyInMinute)
        {
            RoomState lastRoomState = _unitOfWork.RoomStates.GetLastRecordByLogTime();
            DateTime currentLogTime = lastRoomState == null ? DateTime.Now : lastRoomState.LogTime;
            int totalRecord = timeInMinute * frequencyInMinute;
            int timeIntervalInSecond = 60 / frequencyInMinute;
            List<Room> rooms = _unitOfWork.Rooms.GetAll().ToList();
            for (int I = 0; I < totalRecord; I++)
            {
                currentLogTime = currentLogTime.AddSeconds(timeIntervalInSecond);
                RoomState roomState = _roomStateFactory.Create();
                roomState.IsEmpty = (I / rooms.Count) % 2 == 0;
                roomState.LogTime = currentLogTime;
                roomState.Room = rooms[I % rooms.Count];
                _unitOfWork.RoomStates.Add(roomState);
            }
            _unitOfWork.Commit();
            return currentLogTime;
        }

        public ICollection<RoomState> GetByAndSearch(RoomStateSearch searchItem, Pagination pagination, OrderBy<RoomState> orderBy)
        {
            return _unitOfWork.RoomStates.GetIncludedRoomByAndSearch(searchItem, pagination, orderBy);
        }
        public int GetTotalByAndSearch(RoomStateSearch searchItem)
        {
            return _unitOfWork.RoomStates.GetTotalAnd(searchItem);
        }
    }
}
