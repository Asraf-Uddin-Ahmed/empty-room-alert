using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Aggregates;
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
    public class RoomService : IRoomService
    {
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        [Inject]
        public RoomService(ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public ICollection<Room> GetAll()
        {
            return _unitOfWork.Rooms.GetAll();
        }

        public ICollection<Room> GetByArea(Guid areaID)
        {
            return _unitOfWork.Rooms.GetByArea(areaID);
        }
    }
}
