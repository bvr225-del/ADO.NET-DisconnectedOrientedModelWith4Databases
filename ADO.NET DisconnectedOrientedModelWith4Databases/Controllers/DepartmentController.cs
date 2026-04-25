using ADO.NET_DisconnectedOrientedModelWith4Databases.Dtos;
using ADO.NET_DisconnectedOrientedModelWith4Databases.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ADO.NET_DisconnectedOrientedModelWith4Databases.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpPost]
        [Route("AddDepartment")]
        public async Task<IActionResult> Post([FromBody] DepartmentDto deptDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    var deptData = await _departmentService.AddDepartment(deptDto);
                    return StatusCode(StatusCodes.Status201Created, deptData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");
            }
        }
        [HttpDelete]
        [Route("DeleteDepartmentBydeptId/{deptId}")]
        public async Task<IActionResult> Delete(int deptId)
        {
            if (deptId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var deptData = await _departmentService.DeleteDepartment(deptId);
                if (deptData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "deptData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, "deleted successfully");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");
            }


        }
        [HttpGet]
        [Route("GetDepartmentById/{deptId}")]
        public async Task<IActionResult> GetDepartmentById(int deptId)
        {
            if (deptId < 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "bad request");
            }
            try
            {
                var deptData = await _departmentService.GetDepartmentById(deptId);
                if (deptData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "deptData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, deptData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");
            }
        }
        [HttpGet]
        [Route("GetDepartments")]
        public async Task<IActionResult> GetDepartments()
        {
            try
            {
                var deptData = await _departmentService.GetDepartments();
                if (deptData == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "deptData not found");
                }
                else
                {
                    return StatusCode(StatusCodes.Status200OK, deptData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");
            }
        }
        [HttpPut]
        [Route("UpdateDepartment")]
        public async Task<IActionResult> Put([FromBody] DepartmentDto deptDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "bad request");
                }
                else
                {
                    var deptData = await _departmentService.UpdateDepartment(deptDto);
                    return StatusCode(StatusCodes.Status200OK, deptData);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server not found");
            }
        }
    }
}