using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Domain.Interfaces
{
    public interface IRepository<TEntity> where TEntity : IBaseEntity
    {
        bool Create(TEntity entity);
        bool Delete(TEntity entity);
        bool Delete(Guid id);
        bool Edit(TEntity entity);
        TEntity GetById(Guid id);
        IEnumerable<TEntity> Filter(Func<TEntity, bool> predicate);
        bool SaveChanges();
    }
}
