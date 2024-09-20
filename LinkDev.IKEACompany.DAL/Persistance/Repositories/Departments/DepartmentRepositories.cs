using LinkDev.IKEACompany.DAL.Models.Departments;
using LinkDev.IKEACompany.DAL.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public DepartmentRepository(ApplicationDbContext dbContext)
        {
            _dbcontext = dbContext;
        }

        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        {
            if (withAsNoTracking)
                return _dbcontext.Departments.AsNoTracking().ToList();

            return _dbcontext.Departments.ToList();

        }

        public IQueryable<Department> GetAllAsIQueryable()
        {
            return _dbcontext.Departments;
        }

        public Department? Get(int id)
        {
            ///var department = _dbcontext.Departments.Local.FirstOrDefault(D => D.Id == id);
            ///if(department is null)
            ///    department = _dbcontext.Departments.FirstOrDefault(D => D.Id == id);
            ///return department;

            return _dbcontext.Departments.Find(id);

        }

        public int Add(Department entity)
        {
            _dbcontext.Departments.Add(entity);
            return _dbcontext.SaveChanges();
        }

        public int Update(Department entity)
        {
            _dbcontext.Departments.Update(entity);
            return _dbcontext.SaveChanges();
        }

        public int Delete(Department entity)
        {
            _dbcontext.Departments.Remove(entity);
            return _dbcontext.SaveChanges();
        }

    }
}
