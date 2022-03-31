using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Z2DataHR.Application.Services;
using Z2DataHR.Application.ViewModels;
namespace Z2DataHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralTypesController : ControllerBase
    {
        private readonly IGeneralTypes _generalTypesService;

        public GeneralTypesController(IGeneralTypes generalTypesService)
        {
            _generalTypesService = generalTypesService;

        }

        [HttpGet]
        [Route("GetGeneralTypes")]
        public IActionResult GetGeneralTypes()
        {
            return Ok(_generalTypesService.GetGeneralTypes());
        }

        [HttpPost]
        [Route("CreateGeneralTypes")]
        public IActionResult CreateGeneralTypes([FromBody] GeneralTypes generalTypes)
        {
            return Created("GeneralType created", _generalTypesService.CreateGeneralTypes(generalTypes));
        }


        [HttpDelete]
        [Route("DeleteGeneralTypeById")]
        public IActionResult DeleteGeneralTypeById(int GeneralTypeID)
        {
            try
            {
                _generalTypesService.DeleteGeneralTypeById(GeneralTypeID);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
