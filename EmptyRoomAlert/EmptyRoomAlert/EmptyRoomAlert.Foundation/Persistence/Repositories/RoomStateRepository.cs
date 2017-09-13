using Ratul.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using EmptyRoomAlert.Foundation.Core.Enums;
using EmptyRoomAlert.Foundation.Core.SearchData;
using System.Data.Entity;

namespace EmptyRoomAlert.Foundation.Persistence.Repositories
{
    public class RoomStateRepository : RepositorySearch<RoomState, RoomStateSearch>, IRoomStateRepository
    {
        public RoomStateRepository(ApplicationDbContext context) : base(context) { }

        public RoomState GetLastRecordByLogTime()
        {
            return base.dbSet.OrderByDescending(r => r.LogTime).FirstOrDefault();
        }
        public ICollection<RoomState> GetIncludedRoomByAndSearch(RoomStateSearch searchItem, Pagination pagination, OrderBy<RoomState> orderBy)
        {
            return base.dbSet
                .Include(r => r.Room)
                .AsNoTracking()
                .Where(this.GetAndSearchCondition(searchItem))
                .OrderByDirection(orderBy.PredicateOrderBy, orderBy.IsAscending)
                .Skip(pagination.DisplayStart).Take(pagination.DisplaySize).ToList();
        }
        


        protected override Func<RoomState, bool> GetAndSearchCondition(RoomStateSearch searchItem)
        {
            Func<RoomState, bool> predicate = (col) =>
                (searchItem != null)
                && (!searchItem.ID.HasValue || col.ID == searchItem.ID.Value)
                && (!searchItem.IsEmpty.HasValue || col.IsEmpty == searchItem.IsEmpty.Value)
                && (!searchItem.LogTimeFrom.HasValue || col.LogTime >= searchItem.LogTimeFrom.Value)
                && (!searchItem.LogTimeTo.HasValue || col.LogTime <= searchItem.LogTimeTo.Value)
                && (!searchItem.RoomID.HasValue || col.RoomID == searchItem.RoomID.Value);
            return predicate;
        }
        protected override Func<RoomState, bool> GetOrSearchCondition(RoomStateSearch searchItem)
        {
            bool isAllNull = base.IsAllPropertyNull(searchItem);
            Func<RoomState, bool> predicate = (col) =>
                (searchItem == null)
                || (searchItem.ID.HasValue && col.ID == searchItem.ID.Value)
                || (searchItem.IsEmpty.HasValue && col.IsEmpty == searchItem.IsEmpty.Value)
                || ((searchItem.LogTimeFrom.HasValue && col.LogTime >= searchItem.LogTimeFrom.Value)
                    && (searchItem.LogTimeTo.HasValue && col.LogTime <= searchItem.LogTimeTo.Value))
                || (searchItem.RoomID.HasValue && col.RoomID == searchItem.RoomID.Value)
                || isAllNull;
            return predicate;
        }

    }
}
