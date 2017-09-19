using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EmptyRoomAlert.Foundation.Core;
using Moq;
using Ninject.MockingKernel.Moq;
using EmptyRoomAlert.Foundation.Persistence;
using Ninject;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.SearchData;
using System.Collections.Generic;
using System.Linq;

namespace EmptyRoomAlert.Test
{
    [TestClass]
    public class RepositoryTest
    {
        private readonly Guid ROOM_ID = Guid.NewGuid();

        private readonly MoqMockingKernel _kernel;

        public RepositoryTest()
        {
            _kernel = NinjectMoq.Create();
        }

        [TestMethod]
        public void RoomStateRepo_GetIncludedRoomByAndSearch_Full()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();
            RoomStateSearch searchItem = new RoomStateSearch();
            Pagination pagination = new Pagination { DisplaySize = int.MaxValue, DisplayStart = 0 };
            OrderBy<RoomState> orderBy = new OrderBy<RoomState>();

            // Act
            ICollection<RoomState> roomStates = unitOfWork.RoomStates.GetIncludedRoomByAndSearch(searchItem, pagination, orderBy);

            // Assert
            Assert.IsTrue(roomStates.Any(), "Please generate room state from index page.");
        }

        [TestMethod]
        public void RoomStateRepo_GetIncludedRoomByAndSearch_Empty()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();
            RoomStateSearch searchItem = new RoomStateSearch();
            Pagination pagination = new Pagination { DisplaySize = 0, DisplayStart = 0 };
            OrderBy<RoomState> orderBy = new OrderBy<RoomState>();

            // Act
            ICollection<RoomState> roomStates = unitOfWork.RoomStates.GetIncludedRoomByAndSearch(searchItem, pagination, orderBy);

            // Assert
            Assert.IsFalse(roomStates.Any());
        }

        [TestMethod]
        public void RoomStateRepo_GetLastRecordByLogTime()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();

            // Act
            RoomState roomState = unitOfWork.RoomStates.GetLastRecordByLogTime();

            // Assert
            Assert.IsTrue(roomState != null, "Please generate room state from index page.");
        }

        [TestMethod]
        public void RoomRepo_GetAll()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();

            // Act
            ICollection<Room> rooms = unitOfWork.Rooms.GetAll();

            // Assert
            Assert.IsTrue(rooms.Any(), "Please run Seed() method from Foundation project.");
        }

        [TestMethod]
        public void RoomRepo_Crud()
        {
            RoomRepo_Add();
            RoomRepo_Update();
            RoomRepo_Remove();
        }

        private void RoomRepo_Add()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();
            Room room = new Room() { ID = ROOM_ID, Address = "address 99", Name = "Class Room 99", Type = RoomType.ClassRoom };

            // Act
            int beforeTotal = unitOfWork.Rooms.GetTotal();
            unitOfWork.Rooms.Add(room);
            unitOfWork.Commit();
            int afterTotal = unitOfWork.Rooms.GetTotal();

            // Assert
            Assert.IsTrue(afterTotal > beforeTotal);
        }
        private void RoomRepo_Update()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();

            // Act
            Room room = unitOfWork.Rooms.Get(ROOM_ID);
            if (room != null)
            {
                room.Address = "address 000";
                unitOfWork.Rooms.Update(room);
                unitOfWork.Commit();
            }

            // Assert
            Assert.IsTrue(room != null);
        }
        private void RoomRepo_Remove()
        {
            // Arrange
            var unitOfWork = _kernel.Get<IUnitOfWork>();

            // Act
            int beforeTotal = unitOfWork.Rooms.GetTotal();
            Room room = unitOfWork.Rooms.Get(ROOM_ID);
            if (room != null)
            {
                unitOfWork.Rooms.Remove(room);
                unitOfWork.Commit();
            }
            int afterTotal = unitOfWork.Rooms.GetTotal();

            // Assert
            Assert.IsTrue(afterTotal < beforeTotal);
        }
    }
}
