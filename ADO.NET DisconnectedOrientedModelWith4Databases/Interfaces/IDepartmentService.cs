using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDto>> GetDepartments();
        Task<DepartmentDto> GetDepartmentById(int deptId);
        Task<int> AddDepartment(DepartmentDto deptdetail);
        Task<bool> UpdateDepartment(DepartmentDto deptdetail);
        Task<bool> DeleteDepartment(int deptId);
    }
}
