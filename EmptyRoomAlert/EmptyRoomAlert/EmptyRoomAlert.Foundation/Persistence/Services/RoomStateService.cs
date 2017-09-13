using EmptyRoomAlert.Foundation.Core;
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
        [Inject]
        public RoomStateService(ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
    }
}
