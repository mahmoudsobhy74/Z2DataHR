using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z2DataHR.Application.Services;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {

        private readonly IEmployees _EmployeesService;

        public EmployeesController(IEmployees EmployeesService)
        {
            _EmployeesService = EmployeesService;

        }

        [HttpGet]
        [Route("GetAllEmployees")]
        public IActionResult GetEmployees()
        {
            return Ok(_EmployeesService.GetEmployees());
        }

        [HttpGet]
        [Route("GetEmployeeById")]
        public IActionResult GetEmployeeById(int EmployeeID)
        {
            return Ok(_EmployeesService.GetEmployeeById(EmployeeID));
        }

        [HttpGet]
        [Route("GetEmployeeCounts")]
        public IActionResult GetEmployeeCounts()
        {
            return Ok(_EmployeesService.GetEmployeeCounts());
        }
        [HttpPost]
        [Route("CreateEmployees")]
        public IActionResult CreateEmployees([FromBody] Employees employee)
        {
            return Ok(_EmployeesService.CreateEmployee(employee));
        }

        [HttpPost]
        [Route("UpdateEmployees")]
        public IActionResult UpdateEmployees(int Id, [FromBody] Employees employee)
        {
            return Ok(_EmployeesService.UpdateEmployees(Id, employee));
        }

        [HttpDelete]
        [Route("DeleteEmployeeById")]
        public IActionResult DeleteEmployeeById(int Employee_ID)
        {
            try
            {
                _EmployeesService.DeleteEmployeeById(Employee_ID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


    }
}
