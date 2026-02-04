using EmployeeManagement.Models;

namespace EmployeeManagement.Interfaces
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        Employee? GetById(int id);
        List<Employee> GetByDepartment(string department);
        List<Employee> GetByStatus(bool isActive);
        List<Employee> Search(string name);
        Dictionary<string, int> CountByDepartment();
    }
}