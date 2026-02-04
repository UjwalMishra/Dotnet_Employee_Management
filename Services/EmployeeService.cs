using EmployeeManagement.Data;
using EmployeeManagement.Interfaces;
using EmployeeManagement.Models;

namespace EmployeeManagement.Services
{
    public class EmployeeService : IEmployeeService
    {
        public List<Employee> GetAll()
        {
            return EmployeeData.Employees;
        }

        public Employee? GetById(int id)
        {
            return EmployeeData.Employees
                .FirstOrDefault(e => e.Id == id);
        }

        public List<Employee> GetByDepartment(string department)
        {
            return EmployeeData.Employees
                .Where(e => e.Department.Equals(department, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Employee> GetByStatus(bool isActive)
        {
            return EmployeeData.Employees
                .Where(e => e.IsActive == isActive)
                .ToList();
        }

        public List<Employee> Search(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return GetAll();

            return EmployeeData.Employees
                .Where(e => e.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public Dictionary<string, int> CountByDepartment()
        {
            return EmployeeData.Employees
                .GroupBy(e => e.Department)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}