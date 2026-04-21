using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetEmployees();
        Task<EmployeeDto> GetEmployeeById(int empId);
        Task<int> AddEmployee(EmployeeDto empdetail);
        Task<bool> UpdateEmployee(EmployeeDto empdetail);
        Task<bool> DeleteEmployee(int empId);

    }
}
