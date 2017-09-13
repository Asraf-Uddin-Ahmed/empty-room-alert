using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Foundation.Persistence.Template.Email
{
    public partial class ConfirmUser
    {
        public ApplicationUser NewUser
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public ConfirmUser(ApplicationUser newUser, string url)
        {
            NewUser = newUser;
            Url = url;
        }
    }
}