using Microsoft.AspNetCore.Mvc;
using EmployeeManagement.Interfaces;
using EmployeeManagement.ViewModels;
using EmployeeManagement.Models;

namespace EmployeeManagement.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;`
        }

        [HttpGet] 
        public IActionResult Index()
        {
            try
            {
                var employees = _employeeService.GetAll();
                var viewModel = CreateEmployeeListViewModel(employees, selectedFilter: "All");
                return View(viewModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var employee = _employeeService.GetById(id);

                if (employee == null)
                    return BadRequest();

                return View(employee);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ByDepartment(string department)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(department))
                    return BadRequest();

                var employees = _employeeService.GetByDepartment(department);
                var viewModel = CreateEmployeeListViewModel(
                    employees, 
                    department: department, 
                    selectedFilter: department);

                return View("Index", viewModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ByStatus(bool isActive)
        {
            try
            {
                var employees = _employeeService.GetByStatus(isActive);
                var status = isActive ? "Active" : "Inactive";
                var viewModel = CreateEmployeeListViewModel(
                    employees, 
                    isActive: isActive, 
                    selectedFilter: status);

                return View("Index", viewModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Search(string name)
        {
            try
            {
                var employees = _employeeService.Search(name);
                var viewModel = CreateEmployeeListViewModel(
                    employees, 
                    searchTerm: name, 
                    selectedFilter: "All");

                return View(viewModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult Filter(string option)
        {
            if (string.IsNullOrWhiteSpace(option) || option == "All")
                return RedirectToAction("Index");

            if (option == "Active")
                return RedirectToAction("ByStatus", new { isActive = true });

            if (option == "Inactive")
                return RedirectToAction("ByStatus", new { isActive = false });

            return RedirectToAction("ByDepartment", new { department = option });
        }

        [HttpGet]
        public IActionResult DepartmentCount()
        {
            try
            {
                var data = _employeeService.CountByDepartment();

                var viewModel = data.Select(item => new DepartmentCountViewModel
                {
                    Department = item.Key,
                    TotalEmployees = item.Value
                }).ToList();

                return View(viewModel);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private EmployeeListViewModel CreateEmployeeListViewModel(
            IEnumerable<Employee> employees, 
            string department = "", 
            bool isActive = false, 
            string searchTerm = "", 
            string selectedFilter = "")
        {
            return new EmployeeListViewModel
            {
                Employees = employees.ToList(),
                Department = department,
                IsActive = isActive,
                SearchTerm = searchTerm,
                SelectedFilter = selectedFilter
            };
        }
    }
}

