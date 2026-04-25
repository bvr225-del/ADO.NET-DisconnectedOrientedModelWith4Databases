namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Utils
{
    public static class StoredProcedures
    {
        #region Employee Storedprocedures
        public static string AddEmployee = "Usp_AddEmployee";
        public static string GetEmployee = "Usp_GetEmployee";
        public static string GetEmployeeById = "Usp_GetEmployeeId";
        public static string UpdateEmployee = "Usp_UpdateEmployee";
        public static string DeleteEmployee = "Usp_DeleteEmployee";
        #endregion

        #region Orders Soredprocedures
        public static string AddOrder = "Usp_AddOrder";
        public static string GetOrder = "Usp_GetOrders";
        public static string GetOrderById = "Usp_GetOrderById";
        public static string UpdateOrder = "Usp_UpdateOrder";
        public static string DeleteOrder = "Usp_DeleteOrder";
        #endregion

        #region Department Storedprocedures
        public static string AddDepartment = "Usp_AddDepartment";
        public static string GetDepartment = "Usp_GetDepartment";
        public static string GetDepartmentById = "Usp_GetDepartmentById";
        public static string UpdateDepartment = "Usp_UpdateDepartment";
        public static string DeleteDepartment = "Usp_DeleteDepartment";
        #endregion
    }
}
