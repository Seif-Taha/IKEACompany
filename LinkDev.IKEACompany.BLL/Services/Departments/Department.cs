using LinkDev.IKEACompany.BLL.Models.Departments;
using LinkDev.IKEACompany.DAL.Models.Departments;
using LinkDev.IKEACompany.DAL.Persistance.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.BLL.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {

        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public IEnumerable<DepartmentToReturnDto> GetAllDepartments()
        {
            var departments = _departmentRepository
                .GetIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(D => new DepartmentToReturnDto()
                {
                    Id = D.Id,
                    Code = D.Code,
                    Name = D.Name,
                    CreationDate = D.CreationDate,

                }).AsNoTracking().ToList();

            return departments;
                  
        }

        public int CreateDepartment(CreatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                //CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Add(department);

        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,
            };

            return _departmentRepository.Update(department);
        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepository.Get(id);

            if (department is { })
                return _departmentRepository.Delete(department) > 0;

            return false;

        }

        public DepartmentDetailsToReturnDto? GetDepartmentById(int id)
        {
            if (id == null)
                return null;

            var department = _departmentRepository.Get(id);

            if (department is null)
                return null;
            return new DepartmentDetailsToReturnDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
                CreatedOn = department.CreatedOn,
                LastModifiedBy = department.LastModifiedBy,
                LastModifiedOn = department.LastModifiedOn,
            };
        }
    }
}
