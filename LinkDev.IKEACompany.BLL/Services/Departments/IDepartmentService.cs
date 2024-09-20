using LinkDev.IKEACompany.BLL.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.BLL.Services.Departments
{
    public interface IDepartmentService
    {

        IEnumerable<DepartmentToReturnDto> GetAllDepartments();

        DepartmentDetailsToReturnDto? GetDepartmentById(int id);

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment(UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);

    }
}
