using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Identity.Constants;

namespace EmptyRoomAlert.Identity.Models
{
    public class ExternalLocalAccessToken
    {
        [Required]
        public ExternalLoginProviderName Provider { get; set; }
        [Required]
        public string ClientID { get; set; }
        [Required]
        public string ExternalAccessToken { get; set; }
    }
}
