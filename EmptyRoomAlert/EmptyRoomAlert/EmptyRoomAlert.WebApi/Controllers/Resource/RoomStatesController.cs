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
    [RoutePrefix("")]
    public class RoomStatesController : BaseApiController
    {
        private ILogger _logger;
        private IRoomStateService _roomStateService;
        public RoomStatesController(ILogger logger,
            IRoomStateService roomStateService)
            : base(logger)
        {
            _logger = logger;
            _roomStateService = roomStateService;
        }

        [Route("room-states/generate")]
        [HttpGet]
        public IHttpActionResult GenerateRoomStates()
        {
            int timeInMinute = 30;
            try
            {
                _roomStateService.GenerateValues(timeInMinute, 2);
                return Ok("Random room state values have generated for " + timeInMinute.ToString() + " minutes.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GenerateRoomStates");
                return InternalServerError(ex, "Failed to GenerateRoomStates");
            }
        }
    }
}
