using Microsoft.Owin.Security;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using EmptyRoomAlert.Foundation.Core.Aggregates.Identity;
using EmptyRoomAlert.Identity.Managers;
namespace EmptyRoomAlert.Identity.Helpers
{
    public interface IAuthHelper
    {
        Task<ClaimsIdentity> GetClaimIdentityAsync(ApplicationUser appUser, ApplicationUserManager appUserManager);
        AuthenticationProperties GetAuthenticationProperties(string userName, string clientID);
    }
}
