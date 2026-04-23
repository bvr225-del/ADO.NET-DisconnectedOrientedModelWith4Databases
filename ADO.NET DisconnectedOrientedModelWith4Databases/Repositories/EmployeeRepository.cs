using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Utils;
using Microsoft.Data.SqlClient;
using System.Data;
namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
         private readonly IConnectionFactory _connectionFactory;
        public EmployeeRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory= connectionFactory;
        }
        public async Task<int> AddEmployee(Employee empdetail)
        {
            using (SqlConnection con=_connectionFactory.Hotelmanagement1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.AddEmployee, con);
                cmd.CommandType = CommandType.StoredProcedure;
                //pass the data to input partameters of your storedprocedure
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeName,empdetail.empName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeSalary,empdetail.empSalary);
                //below code is used to store the stoedprocedure return value.
                SqlParameter outputParam = new SqlParameter(StoredProcedureParameters.Insertedvariable, SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;//if stooredprocedure returns any output params value.by using this process we can return
                cmd.Parameters.Add(outputParam);//need to add output parameter to sqlcommand object.this is the rule.

                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds,ClassNames.Employee);
                var employeeCount = (int)cmd.Parameters[StoredProcedureParameters.Insertedvariable].Value;
                return employeeCount;
            }
        }

        public async Task<bool> DeleteEmployee(int empId)
        {
            using (SqlConnection con = _connectionFactory.Hotelmanagement1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.DeleteEmployee,con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId,empId);
                SqlDataAdapter da= new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
            }
            return true;
        }

        public async Task<Employee> GetEmployeeById(int empId)
        {
            Employee emp = new Employee();
            using (SqlConnection con = _connectionFactory.Hotelmanagement1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.GetEmployeeById,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId, empId);
                SqlDataAdapter da= new SqlDataAdapter( cmd);
                DataSet ds = new DataSet();
                da.Fill(ds,ClassNames.Employee);
                foreach(DataRow row in ds.Tables[ClassNames.Employee].Rows)
                {
                    emp.empId = Convert.ToInt16(row[StoredProcedureParameters.EmployeeId]);
                    emp.empName = Convert.ToString(row[StoredProcedureParameters.EmployeeName]);
                    emp.empSalary = Convert.ToInt16(row[StoredProcedureParameters.EmployeeSalary]);

                }

            }
            return emp;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            using (SqlConnection con = _connectionFactory.Hotelmanagement1SqlConnectionString())
            {
                List<Employee> listEmployee= new List<Employee>();
                SqlCommand cmd=new SqlCommand(StoredProcedures.GetEmployee,con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da=new SqlDataAdapter( cmd);
                DataSet ds = new DataSet();
                da.Fill(ds,ClassNames.Employee);
                foreach(DataRow row in ds.Tables[ClassNames.Employee].Rows)
                {
                    Employee emp = new Employee();
                    emp.empId = Convert.ToInt16(row[StoredProcedureParameters.EmployeeId]);
                    emp.empName = Convert.ToString(row[StoredProcedureParameters.EmployeeName]);
                    emp.empSalary = Convert.ToInt16(row[StoredProcedureParameters.EmployeeSalary]);
                    listEmployee.Add( emp );
                }
                return listEmployee;
            }

        }

        public async Task<bool> UpdateEmployee(Employee empdetail)
        {
            using (SqlConnection con = _connectionFactory.Hotelmanagement1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.UpdateEmployee,con);
                cmd.CommandType=CommandType.StoredProcedure;
                //we are passing values to storedprocedure inputparatmennters by using below code
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeId, empdetail.empId);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeName, empdetail.empName);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.EmployeeSalary, empdetail.empSalary);

                SqlDataAdapter da= new SqlDataAdapter( cmd);
                DataSet ds = new DataSet();
                da.Fill(ds, ClassNames.Employee);
                return true;
            }
            return true;

        }
    }
}
