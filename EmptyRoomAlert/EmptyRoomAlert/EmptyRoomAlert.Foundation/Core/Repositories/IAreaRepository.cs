using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.Repositories;

namespace EmptyRoomAlert.Foundation.Core.Repositories
{
    public interface IAreaRepository : IRepository<Area>
    {
    }
}
