using LinkDev.IKEACompany.BLL.Models.Employees;
using LinkDev.IKEACompany.DAL.Models.Employees;
using LinkDev.IKEACompany.DAL.Persistance.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEACompany.BLL.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address ?? "",
                IsActive = employeeDto.IsActive,
                Salary = employeeDto.Salary,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Age = employeeDto.Age,
                Address = employeeDto.Address ?? "",
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                Salary = employeeDto.Salary,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                CreatedBy = 1,
                CreatedOn = DateTime.UtcNow,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };
            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {

            var employee = _employeeRepository.Get(id);
            if (employee is { })
                return _employeeRepository.Delete(employee) > 0;
            return false;

        }

        public IEnumerable<EmployeeToReturnDto> GetAllEmployees()
        {

            var employees = _employeeRepository
                .GetIQueryable()
                .Where(E => !E.IsDeleted)
                .Select(employee => new EmployeeToReturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Address = employee.Address ?? "",
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                }).ToList();

            return employees;
        }

        public EmployeeDetailsToReturnDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
                return new EmployeeDetailsToReturnDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Age = employee.Age,
                    Salary = employee.Salary,
                    Address = employee.Address ?? "",
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,
                };
            return null;
        }
    }
}
