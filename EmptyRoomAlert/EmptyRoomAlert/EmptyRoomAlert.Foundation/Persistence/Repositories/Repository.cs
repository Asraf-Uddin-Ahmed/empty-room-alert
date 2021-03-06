﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Reflection;
using Ninject;
using Ratul.Utility;
using EmptyRoomAlert.Foundation.Core.Repositories;
using EmptyRoomAlert.Foundation.Core.Aggregates;
using System.Linq.Expressions;
using EmptyRoomAlert.Foundation.Core.SearchData;

namespace EmptyRoomAlert.Foundation.Persistence.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly DbSet<TEntity> dbSet;
        private readonly DbContext _context;
        public Repository(DbContext context)
        {
            _context = context;
            dbSet = _context.Set<TEntity>();
        }


        public void Add(TEntity entity)
        {
            dbSet.Add(entity);
        }
        public void AddRange(ICollection<TEntity> entities)
        {
            dbSet.AddRange(entities);
        }


        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }


        public void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }
        public void Remove(Guid ID)
        {
            TEntity currentItem = this.Get(ID);
            this.Remove(currentItem);
        }
        public void RemoveRange(ICollection<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }


        public TEntity Get(Guid ID)
        {
            return dbSet.Find(ID);
        }
        public ICollection<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public ICollection<TEntity> GetBy(Pagination pagination, OrderBy<TEntity> orderBy)
        {
            ICollection<TEntity> listEntity = dbSet
                .OrderByDirection(orderBy.PredicateOrderBy, orderBy.IsAscending)
                .Skip(pagination.DisplayStart).Take(pagination.DisplaySize).ToList();
            return listEntity;
        }


        public int GetTotal()
        {
            return dbSet.Count();
        }

    }
}
