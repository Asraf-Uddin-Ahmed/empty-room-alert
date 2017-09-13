using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Enums;

namespace EmptyRoomAlert.Foundation.Core.SearchData
{
    public class RoomStateSearch : EntitySearch
    {
        public bool? IsEmpty { get; set; }
        public DateTime? LogTimeFrom { get; set; }
        public DateTime? LogTimeTo { get; set; }
        public Guid? RoomID { get; set; }

    }
}
