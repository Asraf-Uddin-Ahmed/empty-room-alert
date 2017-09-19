using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Factories;
using EmptyRoomAlert.Foundation.Core.Services;
using EmptyRoomAlert.Foundation.Persistence;
using EmptyRoomAlert.Foundation.Persistence.Factories;
using EmptyRoomAlert.Foundation.Persistence.Services;
using Ninject.MockingKernel.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Test
{
    internal static class NinjectMoq
    {
        internal static MoqMockingKernel Create()
        {
            MoqMockingKernel kernel = new MoqMockingKernel();
            kernel.Bind<ApplicationDbContext>().ToSelf();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
            kernel.Bind<IRoomStateFactory>().To<RoomStateFactory>();
            kernel.Bind<IRoomService>().To<RoomService>();
            kernel.Bind<IRoomStateService>().To<RoomStateService>();
            return kernel;
        }
    }
}
