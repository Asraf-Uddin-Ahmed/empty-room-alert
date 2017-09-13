using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;

namespace EmptyRoomAlert.Foundation.Persistence.Template.Email
{
    public partial class ForgotPassword
    {
        public ApplicationUser RegisteredUser
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public ForgotPassword(ApplicationUser registeredUser, string url)
        {
            RegisteredUser = registeredUser;
            Url = url;
        }
    }
}