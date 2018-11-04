using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace STP.MS.WorkSample.Core.Interfaces
{
    public interface IRepository<T> where T : IEntity
    {
        IEnumerable<T> Get(
            out int totalCount,
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IQueryable<T>> pageClause = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        T GetByID(object id);

        void Insert(T entity);

        void Delete(object id);

        void Delete(T entityToDelete);

        void Update(T entityToUpdate);
    }
}
