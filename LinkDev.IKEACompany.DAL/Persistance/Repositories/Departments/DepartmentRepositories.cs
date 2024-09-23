using LinkDev.IKEACompany.DAL.Models.Departments;
using LinkDev.IKEACompany.DAL.Persistance.Data;
using LinkDev.IKEACompany.DAL.Persistance.Repositories._Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.DAL.Persistance.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {

        public DepartmentRepository(ApplicationDbContext dbContext):base(dbContext)
        {
            
        }

    }
}
