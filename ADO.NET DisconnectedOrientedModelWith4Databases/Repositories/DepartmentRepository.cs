using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Models;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Utils;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public DepartmentRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<int> AddDepartment(Department deptdetail)
        {
            using(SqlConnection con=_connectionFactory.Nortwind_DB1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.AddDepartment,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentName,deptdetail.deptname);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentLocation,deptdetail.deptlocation);
                SqlParameter outputParam=new SqlParameter(StoredProcedureParameters.departmentInsertedVariable,SqlDbType.Int);
                outputParam.Direction=ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,ClassNames.Department);
                var deptCount=(int)cmd.Parameters[StoredProcedureParameters.departmentInsertedVariable].Value;
                return deptCount;
            }
        }
           
        

        public async Task<bool> DeleteDepartment(int deptId)
        {
            using(SqlConnection con=_connectionFactory.Nortwind_DB1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.DeleteDepartment,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentId,deptId);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds);
            }
            return true;

        }

        public async Task<List<Department>> GetAllDepartments()
        {
            using(SqlConnection con=_connectionFactory.Nortwind_DB1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.GetDepartment,con);
                cmd.CommandType=CommandType.StoredProcedure;
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,ClassNames.Department);
                List<Department> deptList=new List<Department>();
                foreach(DataRow row in ds.Tables[ClassNames.Department].Rows)
                {
                    Department dept=new Department
                    {
                        deptid=Convert.ToInt16(row[StoredProcedureParameters.departmentId]),
                        deptname=Convert.ToString(row[StoredProcedureParameters.departmentName]),
                        deptlocation=Convert.ToString(row[StoredProcedureParameters.departmentLocation])
                    };
                    deptList.Add(dept);
                }
                return deptList;
            }

        }

        public async Task<Department> GetDepartmentById(int deptId)
        {
            using(SqlConnection con=_connectionFactory.Nortwind_DB1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.GetDepartmentById,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentId,deptId);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,ClassNames.Department);
                Department dept=new Department();
                if(ds.Tables[ClassNames.Department].Rows.Count>0)
                {
                    DataRow row=ds.Tables[ClassNames.Department].Rows[0];
                    dept.deptid=Convert.ToInt32(row[StoredProcedureParameters.departmentId]);
                    dept.deptname=Convert.ToString(row[StoredProcedureParameters.departmentName]);
                    dept.deptlocation=Convert.ToString(row[StoredProcedureParameters.departmentLocation]);
                }
                return dept;
            }
        }

        public async Task<bool> UpdateDepartment(Department deptdetail)
        {
            using(SqlConnection con=_connectionFactory.Nortwind_DB1SqlConnectionString())
            {
                SqlCommand cmd=new SqlCommand(StoredProcedures.UpdateDepartment,con);
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentId,deptdetail.deptid);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentName,deptdetail.deptname);
                cmd.Parameters.AddWithValue(StoredProcedureParameters.departmentLocation,deptdetail.deptlocation);
                SqlDataAdapter da=new SqlDataAdapter(cmd);
                DataSet ds=new DataSet();
                da.Fill(ds,ClassNames.Department);
                return true;
            }

        }
    }
}
