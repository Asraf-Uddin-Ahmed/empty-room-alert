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
    public class AreaService : IAreaService
    {
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        [Inject]
        public AreaService(ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public ICollection<Area> GetAll()
        {
            return _unitOfWork.Areas.GetAll();
        }
    }
}
