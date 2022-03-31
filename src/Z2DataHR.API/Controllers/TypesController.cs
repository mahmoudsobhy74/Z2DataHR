using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z2DataHR.Application.Services;
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
    public class TypesController : ControllerBase
    {

        private readonly ITypes _typesService;

        public TypesController(ITypes typesService)
        {
            _typesService = typesService;

        }

        [HttpGet]
        [Route("GetTypes")]
        public IActionResult GetTypes()
        {
            return Ok(_typesService.GetTypes());
        }
    }
}
