using EmptyRoomAlert.Foundation.Core.Services;
using Ninject.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmptyRoomAlert.WebApi.Controllers.Resource
{
    public class RoomsController : BaseApiController
    {
        private IRoomService _roomService;
        public RoomsController(ILogger logger,
            IRoomService roomService)
            : base(logger)
        {
            _roomService = roomService;
        }
    }
}
