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
    public class AreasController : BaseApiController
    {
        private ILogger _logger;
        private IAreaService _areaService;
        private IAreaResponseFactory _areaResponseFactory;
        public AreasController(ILogger logger,
            IAreaResponseFactory areaResponseFactory,
            IAreaService areaService)
            : base(logger)
        {
            _logger = logger;
            _areaResponseFactory = areaResponseFactory;
            _areaService = areaService;
        }

        [Route("areas")]
        [HttpGet]
        public IHttpActionResult GetAreas()
        {
            try
            {
                ICollection<Area> areas = _areaService.GetAll();
                return base.Ok(_areaResponseFactory.Create(areas));
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to GetAreas");
                return InternalServerError(ex, "Failed to GetAreas");
            }
        }
    }
}
