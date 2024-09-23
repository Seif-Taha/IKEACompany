using LinkDev.IKEACompany.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Repositories._Generic
{
    public interface IGenericRepository<T> where T : ModelBase
    {
        IEnumerable<T> GetAll(bool WithAsNoTracking = true);
        IQueryable<T> GetIQueryable();
        IEnumerable<T> GetIEnumerable();
        T? Get(int? id);
        public int Add(T entity);
        public int Update(T entity);
        public int Delete(T entity);

    }
}
