using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Foundation.Core.Services
{
    public interface IRoomStateService
    {
        void GenerateValues(int timeInMinute, int frequencyInMinute);
    }
}
