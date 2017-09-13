using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.Foundation.Persistence.Repositories
{
    public class RoomStateRepository : Repository<RoomState>, IRoomStateRepository
    {
        private ApplicationDbContext _context;
        public RoomStateRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

    }
}
