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
        IPasswordVerificationRepository PasswordVerifications { get; }
        ISettingsRepository Settings { get; }
        IUserRepository Users { get; }
        IUserVerificationRepository UserVerifications { get; }


        void Commit();
    }
}
