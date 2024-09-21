using LinkDev.IKEACompany.BLL.Models.Employees;
using LinkDev.IKEACompany.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEACompany.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService,
                                    ILogger<EmployeeController> logger,
                                    IWebHostEnvironment environment
                                    )
        {
            _employeeService = employeeService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _environment = environment;
        }

        #endregion

        #region All Employeess
        [HttpGet] // GET: /Employees/Index
        public IActionResult Index()
        {
            var employees = _employeeService.GetAllEmployees();
            return View(employees);
        }
        #endregion

        #region Create
        [HttpGet] // GET: /Employees/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost] // POST:
        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);
                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    message = "Employees Is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);
                // 2. Set Message
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(employee);
                }
                else
                {
                    message = "Employees Is Not Created";
                    return View("Erorr", message);
                }

            }

        }
        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(employee);

        }
        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
                return NotFound();

            return View(new UpdatedEmployeeDto()
            {
                Name = employee.Name,
                Address = employee.Address,
                Email = employee.Email,
                Salary = employee.Salary,
                Age = employee.Age,
                PhoneNumber = employee.PhoneNumber,
                EmployeeType = employee.EmployeeType,
                Gender = employee.Gender,
                IsActive = employee.IsActive,
            });

        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id, UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);
            var message = string.Empty;
            try
            {

                var result = _employeeService.UpdateEmployee(employee) > 0;
                if (result)
                    return RedirectToAction("Index");
                message = "an error Has occurd during updateing the employee :(";


            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error Has occurd during updateing the employee :(";

            }
            ModelState.AddModelError(string.Empty, message);
            return View(employee);

        }
        #endregion

        #region Delete

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);
                if (deleted)
                    return RedirectToAction("Index");
                message = "an error Has occurd during deleting the employee :(";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error Has occurd during deleting the employee :(";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction("Index");
        }
        #endregion
    }
}
