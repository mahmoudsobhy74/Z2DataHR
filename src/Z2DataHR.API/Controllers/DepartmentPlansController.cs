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
    public class DepartmentPlansController : ControllerBase
    {
        private readonly IDepartmentPlan _departmentPlansService;

        public DepartmentPlansController(IDepartmentPlan departmentPlansService)
        {
            _departmentPlansService = departmentPlansService;
        }


        [HttpGet]
        [Route("GetAllDepartmentPlans")]
        public IActionResult GetDepartmentPlans()
        {
            return Ok(_departmentPlansService.GetDepartmentPlans());
        }

        
        [HttpGet]
        [Route("GetDepartmentPlan_byID")]
        public IActionResult GetDepartmentPlan_byID(int DepartmentPlanID)
        {
            return Ok(_departmentPlansService.GetDepartmentPlan_byID(DepartmentPlanID));
        }


        [HttpPost]
        [Route("CreateDepartmentPlans")]
        public IActionResult CreateDepartmentPlans([FromBody] DepartmentPlans departmentPlans)
        {
            return Created("departmentPlans created", _departmentPlansService.CreateDepartmentPlans(departmentPlans));
        }


        [HttpDelete]
        [Route("DeleteDepartmentById")]
        public IActionResult DeleteDepartmentPlanById(int DepartmentId)
        {
            try
            {
                _departmentPlansService.DeleteDepartmentPlanById(DepartmentId);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPost]
        [Route("UpdateDepartmentPlans")]
        public IActionResult UpdateDepartmentPlans(int Id, [FromBody] DepartmentPlans departmentPlans)
        {
            return Ok(_departmentPlansService.UpdateDepartmentPlans(Id, departmentPlans));
        }
    }

}
