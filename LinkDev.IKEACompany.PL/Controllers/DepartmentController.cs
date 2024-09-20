using LinkDev.IKEACompany.BLL.Models.Departments;
using LinkDev.IKEACompany.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEACompany.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid)
                return View(department);

            var message = string.Empty;

            try
            {
                var result = _departmentService.CreateDepartment(department);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(department);
                }

            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Friendly Message 
                if (_environment.IsDevelopment())
                {
                    message = ex.Message;
                    return View(department);
                }
                else
                {
                    message = "Department is Not Created";
                    return View("Error", message);
                }
            }

        }

        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }

    }
}
