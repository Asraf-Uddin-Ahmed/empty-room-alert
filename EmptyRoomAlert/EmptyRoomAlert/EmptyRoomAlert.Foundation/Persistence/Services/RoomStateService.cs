using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Factories;
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

        public void GenerateValues(int timeInMinute, int frequencyInMinute)
        {
            RoomState lastRoomState = _unitOfWork.RoomStates.GetLastRecordByLogTime();
            DateTime currentLogTime = lastRoomState == null ? DateTime.Now : lastRoomState.LogTime;
            int totalRecord = timeInMinute * frequencyInMinute;
            int timeIntervalInSecond = 60 / frequencyInMinute;
            int totalMemberInRoomType = Enum.GetNames(typeof(RoomType)).Length;
            for (int I = 0; I < totalRecord; I++)
            {
                currentLogTime = currentLogTime.AddSeconds(timeIntervalInSecond);
                RoomState roomState = _roomStateFactory.Create();
                roomState.IsEmpty = (I / totalMemberInRoomType) % 2 == 0;
                roomState.LogTime = currentLogTime;
                roomState.Room = _unitOfWork.Rooms.GetFirstByType((RoomType)(I % totalMemberInRoomType + 1));
                _unitOfWork.RoomStates.Add(roomState);
            }
            _unitOfWork.Commit();
        }
    }
}
