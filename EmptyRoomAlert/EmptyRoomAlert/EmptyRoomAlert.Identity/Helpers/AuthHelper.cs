using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.Identity.Constants;
using EmptyRoomAlert.Identity.Managers;
using EmptyRoomAlert.Identity.Providers;

namespace EmptyRoomAlert.Identity.Helpers
{
    public class AuthHelper : IAuthHelper
    {
        public async Task<ClaimsIdentity> GetClaimIdentityAsync(ApplicationUser appUser, ApplicationUserManager appUserManager)
        {
            ClaimsIdentity oAuthIdentity = await appUser.GenerateUserIdentityAsync(appUserManager, "JWT");
            oAuthIdentity.AddClaims(ExtendedClaimsProvider.GetClaims(appUser));
            oAuthIdentity.AddClaims(RolesFromClaims.CreateRolesBasedOnClaims(oAuthIdentity));
            return oAuthIdentity;
        }

        public AuthenticationProperties GetAuthenticationProperties(string userName, string clientID)
        {
            AuthenticationProperties authProperties = new AuthenticationProperties(new Dictionary<string, string>
            {
                { AuthenticationPropertyKeys.CLIENT_ID, (clientID == null) ? string.Empty : clientID },
                { AuthenticationPropertyKeys.USER_NAME, userName }
            });
            return authProperties;
        }
    }
}
