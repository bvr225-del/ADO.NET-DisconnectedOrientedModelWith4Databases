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
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
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
                var empData = await _employeeService.GetEmployees();
                if (empData == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, empData);
                }
            }
            catch(Exception ex)
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