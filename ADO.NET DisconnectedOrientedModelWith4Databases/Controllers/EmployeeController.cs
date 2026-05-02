using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        //Dependency Injection is used to develop loosly coupling between the classes.(Don't create object of depency class directly into the controller)
        //(By using interfaces and interface implanted classe we can achieve loosly coupling between the classes.)
        //Depency injection used to avoid tightly coupling between the classes.

        //Dependency injection TYPES we can implement in 3 ways at controller level
        //1.constructor injection(Realtime used 99%)
        //2.property injection
        //3.method injection
        //**You can use any one the type .realtime mainly used constructor injection

        #region before dependency injection we are creating the object of the service class in the controller class to access the members of that service class process
        //employeeservice obj=new employeeservice();//this called tightly coupling process (Don't create object of depency class directly).
        //this process not used in real time application development because it will create tight coupling between the classes and it will create the dependency between the classes and it will create the maintenance issue in the application.
        #endregion
        #region 1.constructor injection process
        //IN the  constructor Injection, inject/pass the dependecies to the constructor 
        //Injection means add your depencies to constructior.
        //all depenceies we are passing/Injecting into to the constructor.
        private readonly IEmployeeService _employeeService;//constructor injection.
        private readonly IDepartmentService _departmentService;
        private readonly IOrderService _orderService;
        //synatx:private readonly interface interfacerefrencevariable;

        //this process is called constructor injection,a class level dependencies are adding here
        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, IOrderService orderService)//we are inject the dependency in the constructor of the controller class and then we are assigning that dependency to the private readonly field of the interface type and then we can use that private readonly field to access the members of the service class in the controller class.
        {
            _employeeService = employeeService;//Assiging the interface refence variables here
            _departmentService = departmentService;
            _orderService = orderService;
        }

        #endregion

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> Post([FromBody] EmployeeDto empDto)
        {//dtos are used to transafer the data purpose used.
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, ModelState);
                }
                else
                {
                    var empData = await _employeeService.AddEmployee(empDto);
                    return StatusCode(StatusCodes.Status201Created, empData);
                }
            }
            catch (Exception ex)
            {//if you got any error we are using this statuscode:Status500InternalServerError
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");

            }
        }
        [HttpDelete]
        [Route("DeleteEmployeeByempId/{empId}")]
        public async Task<IActionResult> Delete(int empId)
        {
            if (empId < 0)
            {
                //If input parameters are wrongly sent or empty, we will get 400 badrequest statuscode:Status400BadRequest
                return StatusCode(StatusCodes.Status400BadRequest, "badrequest");
            }
            try
            {
                var empData = await _employeeService.DeleteEmployee(empId);
                if (empData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "empData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetEmployee")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var empdata = await this._employeeService.GetEmployees();//here we are getting the employee data from the employee service and then we are sending that data to the client in a single response.
                var departmentData = await this._departmentService.GetDepartments();//here we are getting the userlist data from the user service and then we are sending that data to the client in a single response.
                var ordersData = await this._orderService.GetOrders();//here we are getting the filesupload data from the filesupload service and then we are sending that data to the client in a single response.
                //here 3 results data we are assiging into object and we are return.
                var response = new
                { //aliasname we can give to the data which we are sending to the client.
                    EmployeeData = empdata,
                    DepartmentData = departmentData,
                    OrdersData = ordersData
                };
                return StatusCode(StatusCodes.Status200OK, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpGet]
        [Route("GetEmployeeByEmpId/{empId}")]
        public async Task<IActionResult> GetEmployeeById(int empId)
        {
            try
            {
                if (empId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
                else
                {
                    var empData = await _employeeService.GetEmployeeById(empId);
                    return StatusCode(StatusCodes.Status200OK, empData);
                }

                    
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }
        }
        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> Put([FromBody] EmployeeDto empDto)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    var empData = await _employeeService.UpdateEmployee(empDto);
                    return StatusCode(StatusCodes.Status200OK, empData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "server not found");
            }

        }
    }
}
/*
1.What is depency injection and how to implement it in dotnetcore?
========================================================================
A)
1.Dependency injection is a design pattern.
2.which is used to develop the loosly coupling between the classes.(Loosly coulping means dont create direct obect of the repository class in the controller class to access the members of that repository class.)
3)To implement loosly coupling between the classes ,by using interfaces and interface implemented classes we can achive the dependency injection.
=================================================================
4)Depenceny injection is avoid the tight coupling between the classes. that mens we can not create direct object of the class in another class to access the members of that class. 
5)In old application development we are creating the object of the class in another class to access the members of that class.
6)but in dependency injection we are not creating the object of the class in another class to access the members of that class.
7)these interfaces we are injecting to controller class by using constructor injection.
8)After that in program.cs we have inbulit depency dency injection  container is there,that is called builder and in that we need to register 
the interface and its implemented classes into the dependency injection container of the application by using AddScoped or AddSingleton or AddTransient method of the builder.Services object.

*/
