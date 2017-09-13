using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmptyRoomAlert.Identity.Models
{
    public class ParsedExternalAccessToken
    {
        public string UserID { get; set; }
        public string AppID { get; set; }
        public string Email { get; set; }
    }
}
