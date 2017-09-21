using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;
using EmptyRoomAlert.Foundation.Core.Services;
using EmptyRoomAlert.WebApi.Codes.Core.Factories.Aggregates;
using EmptyRoomAlert.WebApi.Models.Request;
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
        private IRoomStateResponseFactory _roomStateResponseFactory;
        public RoomStatesController(ILogger logger,
            IRoomStateResponseFactory roomStateResponseFactory,
            IRoomStateService roomStateService)
            : base(logger)
        {
            _logger = logger;
            _roomStateResponseFactory = roomStateResponseFactory;
            _roomStateService = roomStateService;
        }

        [Route("room-states/generate")]
        [HttpGet]
        public IHttpActionResult GenerateRoomStates()
        {
            int timeInMinute = 60;
            try
            {
                DateTime endLogTimeOfGeneration = _roomStateService.GenerateValues(timeInMinute, 2);
                DateTime startLogTimeOfGeneration = endLogTimeOfGeneration.AddMinutes(-timeInMinute);
                return Ok("Random room state values have been generated from " + startLogTimeOfGeneration + " to " + endLogTimeOfGeneration);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GenerateRoomStates");
                return InternalServerError(ex, "Failed to GenerateRoomStates");
            }
        }

        [Route("room-states")]
        [HttpGet]
        public IHttpActionResult GetRoomStates([FromUri] RequestSearchModel<RoomState, RoomStateSearch> searchModel)
        {
            try
            {
                ICollection<RoomState> roomStates = _roomStateService.GetByAndSearch(searchModel.SearchItem, searchModel.Pagination, searchModel.OrderBy);
                return base.Ok(_roomStateResponseFactory.Create(roomStates, searchModel.Pagination, searchModel.SortBy, _roomStateService.GetTotalByAndSearch(searchModel.SearchItem)));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GetRoomStates");
                return InternalServerError(ex, "Failed to GetRoomStates");
            }
        }
    }
}
