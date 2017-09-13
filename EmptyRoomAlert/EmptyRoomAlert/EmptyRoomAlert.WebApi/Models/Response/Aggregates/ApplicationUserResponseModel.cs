﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.WebApi.Models.Response.Aggregates;

namespace EmptyRoomAlert.WebApi.Models.Response.Aggregates
{
    public class ApplicationUserResponseModel : ResponseModel
    {
        public string RoleUrl { get; set; }
        public string ClaimUrl { get; set; }
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

    }
}