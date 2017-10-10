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
    public class RoomRepository : Repository<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context) { }

        public Room GetFirstByType(RoomType type)
        {
            return base.dbSet.FirstOrDefault(r => r.Type == type);
        }

        public ICollection<Room> GetByArea(Guid areaID)
        {
            return base.dbSet.Where(r => r.AreaID == areaID).ToList();
        }
    }
}
