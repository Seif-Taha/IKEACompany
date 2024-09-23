using LinkDev.IKEACompany.BLL.Models.Departments;
using LinkDev.IKEACompany.BLL.Services.Departments;
using LinkDev.IKEACompany.DAL.Models.Departments;
using LinkDev.IKEACompany.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEACompany.PL.Controllers
{
    public class DepartmentController : Controller
    {

        #region Services

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

        #endregion


        #region Index

        [HttpGet]
        public IActionResult Index()
        {

            /// 1. ViewData is a Dictionary Type Property 

            ViewData["Message"] = " Hello ";

            /// 2. ViewBag is a Dynamic Type Property 

            ViewBag.Message = " ViewBag ";


            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        } 

        #endregion


        #region Create

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {
                var departmentToCreate = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };


                var result = _departmentService.CreateDepartment(departmentToCreate);

                if (result > 0)
                    return RedirectToAction(nameof(Index));
                else
                {
                    message = "Department is Not Created";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVM);
                }

            }
            catch (Exception ex)
            {

                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Friendly Message 

                message = _environment.IsDevelopment() ? ex.Message : "Department is Not Created";

                ///if (_environment.IsDevelopment())
                ///{
                ///    message = ex.Message;
                ///    return View(department);
                ///}
                ///else
                ///{
                ///    message = "Department is Not Created";
                ///    return View("Error", message);
                ///}
            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }

        #endregion


        #region Details

        public IActionResult Details(int? id)
        {

            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);


        }

        #endregion


        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate,
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            var message = string.Empty;

            try
            {

                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate,
                };

                var updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;

                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "an error Has occurd during updateing the department :( ";


            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error Has occurd during updateing the department :(";


                ///if (_environment.IsDevelopment()) 
                ///{
                ///message=ex.Message;
                ///    return View(department);
                ///}
                ///else 
                ///{
                ///    message = "an error Has occurd during updateing the department :(";
                ///    return View("Erorr",message);
                ///}  
                
            }

            ModelState.AddModelError(string.Empty, message);
            return View(departmentVM);

        }

        #endregion


        #region Delete

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction(nameof(Index));
                message = "an error Has occurd during deleting the department :(";
            }
            catch (Exception ex)
            {
                // 1. Log Exception
                _logger.LogError(ex, ex.Message);

                // 2. Set Message
                message = _environment.IsDevelopment() ? ex.Message : "an error Has occurd during deleting the department :(";
            }
            ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));
        }

        #endregion


    }
}
