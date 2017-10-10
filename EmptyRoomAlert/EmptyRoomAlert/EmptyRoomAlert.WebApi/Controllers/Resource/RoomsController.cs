using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Services;
using EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates;
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
    public class RoomsController : BaseApiController
    {
        private ILogger _logger;
        private IRoomService _roomService;
        private IRoomResponseFactory _roomResponseFactory;
        public RoomsController(ILogger logger,
            IRoomResponseFactory roomResponseFactory,
            IRoomService roomService)
            : base(logger)
        {
            _logger = logger;
            _roomResponseFactory = roomResponseFactory;
            _roomService = roomService;
        }

        [Route("rooms")]
        [HttpGet]
        public IHttpActionResult GetRooms()
        {
            try
            {
                ICollection<Room> rooms = _roomService.GetAll();
                return base.Ok(_roomResponseFactory.Create(rooms));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GetRooms");
                return InternalServerError(ex, "Failed to GetRooms");
            }
        }

        [Route("areas/{areaID:guid}/rooms")]
        [HttpGet]
        public IHttpActionResult GetRoomsByArea(Guid areaID)
        {
            try
            {
                ICollection<Room> rooms = _roomService.GetByArea(areaID);
                return base.Ok(_roomResponseFactory.Create(rooms));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GetRoomsByArea");
                return InternalServerError(ex, "Failed to GetRoomsByArea");
            }
        }
    }
}
