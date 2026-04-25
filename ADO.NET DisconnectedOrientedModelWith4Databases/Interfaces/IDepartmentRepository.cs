using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartments();
         Task<Department> GetDepartmentById(int deptId);
         Task<int> AddDepartment(Department deptdetail);
         Task<bool> DeleteDepartment(int deptId);
         Task<bool> UpdateDepartment(Department deptdetail);
    }
}
