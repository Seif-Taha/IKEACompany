using LinkDev.IKEACompany.DAL.Models;
using LinkDev.IKEACompany.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Repositories._Generic
{
    public class GenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext _dbContext;
        public GenericRepository(ApplicationDbContext dbContext) // Ask CLR for  Object from ApplicationDbContext 
        {
            _dbContext = dbContext;
        }
        public IEnumerable<T> GetAll(bool WithAsNoTracking = true)
        {
            if (WithAsNoTracking)
                return _dbContext.Set<T>().AsNoTracking().ToList();

            return _dbContext.Set<T>().ToList();
        }
        public IQueryable<T> GetAllAsQueryable()
        {

            return _dbContext.Set<T>();
        }

        public T? Get(int? id)
        {
            return _dbContext.Set<T>().Find(id);

            /// if (id is null)
            ///     return null;

            /// var T = _dbContext.Ts.FirstOrDefault(D => D.Id == id);

            /// if (T is null)
            ///     return null;

            /// return T;
        }


        public int Add(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            return _dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            return _dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return _dbContext.SaveChanges();
        }
    }
}

