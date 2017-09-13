﻿using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using EmptyRoomAlert.Identity.Constants.Roles;
using EmptyRoomAlert.Identity.Managers;
using EmptyRoomAlert.Foundation.Core.Constant;
using EmptyRoomAlert.WebApi.Codes.Core.Constant;

namespace EmptyRoomAlert.WebApi.Controllers.Identity
{
    [RoutePrefix("claims")]
    public class ClaimsController : BaseApiController
    {
        private ApplicationUserManager _applicationUserManager;
        public ClaimsController(ILogger logger, ApplicationUserManager applicationUserManager)
            : base(logger)
        {
            _applicationUserManager = applicationUserManager;
        }

        [Authorize]
        [Route("")]
        public IHttpActionResult GetClaims()
        {
            var identity = User.Identity as ClaimsIdentity;

            var claims = from c in identity.Claims
                         select new
                         {
                             subject = c.Subject.Name,
                             type = c.Type,
                             value = c.Value
                         };

            return Ok(claims);
        }

        [Route("user/{userID:guid}", Name = UriName.Identity.Claims.GET_CLAIM_BY_USER_ID)]
        [Authorize(Roles = ApplicationRoles.ADMIN)]
        public async Task<IHttpActionResult> GetClaimByUserID(Guid userID)
        {
            IList<Claim> claims = await _applicationUserManager.GetClaimsAsync(userID);
            return Ok(claims);
        }
    }
}
