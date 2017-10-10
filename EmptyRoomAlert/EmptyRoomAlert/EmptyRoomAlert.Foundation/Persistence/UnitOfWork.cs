using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Persistence.Repositories;

namespace EmptyRoomAlert.Foundation.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _context;

        private IAreaRepository _areas;
        private IRoomRepository _rooms;
        private IRoomStateRepository _roomStates;
        private ISettingsRepository _settings;

        [Inject]
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IAreaRepository Areas
        {
            get
            {
                if (_areas == null)
                {
                    _areas = new AreaRepository(_context);
                }
                return _areas;
            }
        }
        public IRoomRepository Rooms
        {
            get
            {
                if (_rooms == null)
                {
                    _rooms = new RoomRepository(_context);
                }
                return _rooms;
            }
        }
        public IRoomStateRepository RoomStates
        {
            get
            {
                if (_roomStates == null)
                {
                    _roomStates = new RoomStateRepository(_context);
                }
                return _roomStates;
            }
        }
        public ISettingsRepository Settings
        {
            get
            {
                if (_settings == null)
                {
                    _settings = new SettingsRepository(_context);
                }
                return _settings;
            }
        }
        


        public void Commit()
        {
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
