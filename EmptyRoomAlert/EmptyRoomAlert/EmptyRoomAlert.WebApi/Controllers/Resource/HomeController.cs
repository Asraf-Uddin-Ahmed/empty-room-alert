﻿using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmptyRoomAlert.WebApi.Controllers.Resource
{
    public class HomeController : BaseApiController
    {
        public HomeController(ILogger logger) : base(logger) { }

        // GET: Home
        public string Get()
        {
            return "API is running...";
        }

    }
}
