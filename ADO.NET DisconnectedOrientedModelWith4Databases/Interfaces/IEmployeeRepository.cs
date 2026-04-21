using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();
        Task<Employee> GetEmployeeById(int empId);
        Task<int> AddEmployee(Employee empdetail);
        Task<bool>UpdateEmployee(Employee empdetail);
        Task<bool>DeleteEmployee(int empId);
    }
}
