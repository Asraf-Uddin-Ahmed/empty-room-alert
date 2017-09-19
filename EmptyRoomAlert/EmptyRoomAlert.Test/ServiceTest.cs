using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Ninject.MockingKernel.Moq;
using EmptyRoomAlert.Foundation.Persistence;
using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Services;
using Ninject;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using System.Collections.Generic;
using System.Linq;
using EmptyRoomAlert.Foundation.Core.SearchData;

namespace EmptyRoomAlert.Test
{
    [TestClass]
    public class ServiceTest
    {
        private readonly MoqMockingKernel _kernel;

        public ServiceTest()
        {
            _kernel = NinjectMoq.Create();
        }

        [TestMethod]
        public void RoomService_GetAll()
        {
            // Arrange
            IRoomService roomService = _kernel.Get<IRoomService>();

            // Act
            ICollection<Room> rooms = roomService.GetAll();

            // Assert
            Assert.IsTrue(rooms.Any(), "Please run Seed() method from Foundation project.");
        }

        [TestMethod]
        public void RoomService_GetByAndSearch_Full()
        {
            // Arrange
            IRoomStateService roomStateService = _kernel.Get<IRoomStateService>();
            RoomStateSearch searchItem = new RoomStateSearch();
            Pagination pagination = new Pagination { DisplaySize = int.MaxValue, DisplayStart = 0 };
            OrderBy<RoomState> orderBy = new OrderBy<RoomState>();

            // Act
            ICollection<RoomState> roomStates = roomStateService.GetByAndSearch(searchItem, pagination, orderBy);

            // Assert
            Assert.IsTrue(roomStates.Any(), "Please generate room state from index page.");
        }

        [TestMethod]
        public void RoomService_GetByAndSearch_Empty()
        {
            // Arrange
            IRoomStateService roomStateService = _kernel.Get<IRoomStateService>();
            RoomStateSearch searchItem = new RoomStateSearch();
            Pagination pagination = new Pagination { DisplaySize = 0, DisplayStart = 0 };
            OrderBy<RoomState> orderBy = new OrderBy<RoomState>();

            // Act
            ICollection<RoomState> roomStates = roomStateService.GetByAndSearch(searchItem, pagination, orderBy);

            // Assert
            Assert.IsFalse(roomStates.Any());
        }

        [TestMethod]
        public void RoomService_GetTotalByAndSearch_Full()
        {
            // Arrange
            IRoomStateService roomStateService = _kernel.Get<IRoomStateService>();
            RoomStateSearch searchItem = new RoomStateSearch();

            // Act
            int total = roomStateService.GetTotalByAndSearch(searchItem);

            // Assert
            Assert.IsTrue(total > 0, "Please generate room state from index page.");
        }

    }
}
