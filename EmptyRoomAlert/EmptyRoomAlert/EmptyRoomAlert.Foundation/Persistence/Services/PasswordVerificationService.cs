using Ninject;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.Services;

namespace EmptyRoomAlert.Foundation.Persistence.Services
{
    public class PasswordVerificationService : IPasswordVerificationService
    {
        private ILogger _logger;
        private IUnitOfWork _unitOfWork;
        [Inject]
        public PasswordVerificationService(ILogger logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public void RemoveByUserID(Guid userID)
        {
            _unitOfWork.PasswordVerifications.RemoveByUserID(userID);
            _unitOfWork.Commit();
        }
    }
}
