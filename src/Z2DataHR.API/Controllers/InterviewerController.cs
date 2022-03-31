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
    public class InterviewerController : ControllerBase
    {
        private readonly IInterviewer _interviewerService;

        public InterviewerController(IInterviewer interviewerService)
        {
            _interviewerService = interviewerService;

        }



        [HttpGet]
        [Route("GetInterviewerById")]
        public IActionResult GetInterviewerById(int Id)
        {
            return Ok(_interviewerService.GetInterviewerById(Id));
        }
    }
}
