using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.Identity.Constants.Claims;

namespace EmptyRoomAlert.Identity.Providers
{
    public static class ExtendedClaimsProvider
    {
        public static IEnumerable<Claim> GetClaims(ApplicationUser user)
        {

            List<Claim> claims = new List<Claim>();

            if (user.PhoneNumberConfirmed)
            {
                claims.Add(CreateClaim(PhoneNumberConfirmed.CLAIM_TYPE, PhoneNumberConfirmed.CLAIM_VALUE.TRUE));

            }
            else
            {
                claims.Add(CreateClaim(PhoneNumberConfirmed.CLAIM_TYPE, PhoneNumberConfirmed.CLAIM_VALUE.FALSE));
            }

            return claims;
        }

        public static Claim CreateClaim(string type, string value)
        {
            return new Claim(type, value, ClaimValueTypes.String);
        }

    }
}