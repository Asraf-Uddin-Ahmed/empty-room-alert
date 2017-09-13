using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EmptyRoomAlert.Identity.Constants.Claims;
using EmptyRoomAlert.Identity.Constants.Roles;
using EmptyRoomAlert.Foundation.Core.Constant;

namespace EmptyRoomAlert.Identity.Providers
{
    public class RolesFromClaims
    {
        public static IEnumerable<Claim> CreateRolesBasedOnClaims(ClaimsIdentity identity)
        {
            List<Claim> claims = new List<Claim>();

            if (identity.HasClaim(c => c.Type == PhoneNumberConfirmed.CLAIM_TYPE && c.Value == PhoneNumberConfirmed.CLAIM_VALUE.TRUE)
                && identity.HasClaim(ClaimTypes.Role, ApplicationRoles.ADMIN))
            {
                claims.Add(new Claim(ClaimTypes.Role, CustomRoles.INCIDENT_RESOLVERS));
            }

            return claims;
        }
    }
}