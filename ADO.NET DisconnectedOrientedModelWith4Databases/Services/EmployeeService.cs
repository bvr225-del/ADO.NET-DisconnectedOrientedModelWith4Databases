using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository= employeeRepository;
        }
        public async Task<int> AddEmployee(EmployeeDto empdetail)
        {//Here i am converting employeedto object data into employee clas object
            Employee emp=new Employee();
            emp.empId = empdetail.empId;
            emp.empName = empdetail.empName;
            emp.empSalary = empdetail.empSalary;

            var res=await _employeeRepository.AddEmployee(emp);
            return res;

        }

        public async Task<bool> DeleteEmployee(int empId)
        {
           await _employeeRepository.DeleteEmployee(empId);
            return true;
        }

        public async  Task<EmployeeDto> GetEmployeeById(int empId)
        {
            var res=await _employeeRepository.GetEmployeeById(empId);
            EmployeeDto empDto=new EmployeeDto();
            empDto.empId = res.empId;
            empDto.empName = res.empName;
            empDto.empSalary=res.empSalary;
            return empDto;
            
        }

        public async  Task<List<EmployeeDto>> GetEmployees()
        {
            List<EmployeeDto> listEmpDto = new List<EmployeeDto>();
            var res=await _employeeRepository.GetEmployees();
            foreach(Employee emp in res)
            {
                EmployeeDto empDto = new EmployeeDto();
                empDto.empId= emp.empId;
                empDto.empName= emp.empName;
                empDto.empSalary= emp.empSalary;
                listEmpDto.Add(empDto);
            }
            return listEmpDto;
            
        }

        public async  Task<bool> UpdateEmployee(EmployeeDto empdetail)
        {
            //here we are transfer the data from employeedto object to employee object and pass to repository layer.
            Employee emp=new Employee();
            emp.empId= empdetail.empId;
            emp.empName= empdetail.empName;
            emp.empSalary = empdetail.empSalary;
            await _employeeRepository.UpdateEmployee(emp);
            return true;
        }
    }
}
