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
    public class RoomStatesController : BaseApiController
    {
        private IRoomStateService _roomStateService;
        public RoomStatesController(ILogger logger,
            IRoomStateService roomStateService)
            : base(logger)
        {
            _roomStateService = roomStateService;
        }
    }
}
