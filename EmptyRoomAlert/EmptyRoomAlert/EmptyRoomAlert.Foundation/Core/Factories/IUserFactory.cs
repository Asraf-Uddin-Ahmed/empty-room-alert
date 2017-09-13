using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.Foundation.Core.Factories
{
    public interface IUserFactory
    {
        User Create(string password);
    }
}
