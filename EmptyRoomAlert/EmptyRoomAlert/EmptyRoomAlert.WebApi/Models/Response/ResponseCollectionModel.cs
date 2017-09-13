using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.SearchData;

namespace EmptyRoomAlert.WebApi.Models.Response
{
    public class ResponseCollectionModel<TModel>
        where TModel : ResponseModel
    {
        public IEnumerable<TModel> Items { get; set; }
        public Pagination Pagination { get; set; }
        public SortBy SortBy { get; set; }
        public int TotalItem { get; set; }

    }
}