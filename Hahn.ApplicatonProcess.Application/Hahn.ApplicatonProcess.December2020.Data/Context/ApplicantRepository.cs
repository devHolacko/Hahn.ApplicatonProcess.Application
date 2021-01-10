using Hahn.ApplicatonProcess.December2020.Domain.Interfaces;
using Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Context
{
    public class ApplicantRepository<T> : IRepository<T>
        where T : class, IBaseEntity
    {
        private readonly ApplicantDbContext _context;

        public ApplicantRepository(ApplicantDbContext context) => _context = context;

        public bool Create(T entity)
        {
            _context.Set<T>().Add(entity);
            return SaveChanges();
        }

        public bool Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return SaveChanges();
        }

        public bool Delete(int id)
        {
            var entityToDelete = _context.Set<T>().FirstOrDefault(e => e.Id == id);
            if (entityToDelete != null)
            {
                _context.Set<T>().Remove(entityToDelete);
                return SaveChanges();
            }
            return false;
        }

        public bool Edit(T entity)
        {
            var local = _context.Set<T>()
                .Local
                .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                // detach
                _context.Entry(local).State = EntityState.Detached;
            }
            _context.Entry(entity).State = EntityState.Modified;
            return SaveChanges();
        }

        public T GetById(int id)
        {
            var query = _context.Set<T>().AsQueryable();
            return query.FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> Filter(Func<T, bool> predicate)
        {
            return _context.Set<T>().Where(predicate);
        }

        public bool SaveChanges() => _context.SaveChanges() > 0;
    }
}
