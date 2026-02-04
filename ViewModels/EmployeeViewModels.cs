using EmployeeManagement.Models;

namespace EmployeeManagement.ViewModels
{
    public class EmployeeListViewModel
    {
        public List<Employee> Employees { get; set; } = new();
        public string Department { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
        public string SearchTerm { get; set; } = string.Empty;
        public string SelectedFilter { get; set; } = string.Empty;
    }

    public class DepartmentCountViewModel
    {
        public string Department { get; set; } = string.Empty;
        public int TotalEmployees { get; set; }
    }
}
