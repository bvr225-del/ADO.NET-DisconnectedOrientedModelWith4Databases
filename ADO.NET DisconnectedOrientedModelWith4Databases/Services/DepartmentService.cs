using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Services
{
    public class DepartmentService : IDepartmentService
    {
            private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository)
            {
            _departmentRepository = departmentRepository;
        }
        public async Task<int> AddDepartment(DepartmentDto deptdetail)
        {
            Department dept = new Department();
            dept.deptid = deptdetail.deptid;
            dept.deptname = deptdetail.deptname;
            dept.deptlocation = deptdetail.deptlocation;
            var result = await _departmentRepository.AddDepartment(dept);
            return result;





        }

        public async Task<bool> DeleteDepartment(int deptId)
        {
            await _departmentRepository.DeleteDepartment(deptId);
            return true;

        }

        public async Task<DepartmentDto> GetDepartmentById(int deptId)
        {   
            var res = await _departmentRepository.GetDepartmentById(deptId);
            DepartmentDto deptDto = new DepartmentDto();
            deptDto.deptid = res.deptid;
            deptDto.deptname = res.deptname;
            deptDto.deptlocation = res.deptlocation;
            return deptDto;

        }

        public async Task<List<DepartmentDto>> GetDepartments()
        {
            List<DepartmentDto> lstdeptDto = new List<DepartmentDto>();
            var res = await _departmentRepository.GetAllDepartments();
            foreach (Department dept in res)
            {
                DepartmentDto deptDto = new DepartmentDto();
                deptDto.deptid = dept.deptid;
                deptDto.deptname = dept.deptname;
                deptDto.deptlocation = dept.deptlocation;
                lstdeptDto.Add(deptDto);
            }
            return lstdeptDto;

        }

        public async Task<bool> UpdateDepartment(DepartmentDto deptdetail)
        {
            Department dept = new Department();
            dept.deptid = deptdetail.deptid;
            dept.deptname = deptdetail.deptname;
            dept.deptlocation = deptdetail.deptlocation;
            var result = await _departmentRepository.UpdateDepartment(dept);
            return true;

        }
    }
}
