using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z2DataHR.Application.Services;

namespace Z2DataHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviews _interviewsService;

        public InterviewsController(IInterviews interviewsService)
        {
            _interviewsService = interviewsService;
        }

        [HttpGet]
        [Route("GetInterviews")]
        public IActionResult GetInterviews()
        {
            return Ok(_interviewsService.GetInterviews());
        }

    }
}
