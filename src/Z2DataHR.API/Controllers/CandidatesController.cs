using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Z2DataHR.Application.Services;
using Z2DataHR.Application.ViewModels;


namespace Z2DataHR.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidate _candidateService;
        private readonly IWebHostEnvironment _can;

        public CandidatesController(ICandidate candidateService, IWebHostEnvironment can)
        {
            _candidateService = candidateService;
            _can = can;
        }

        [HttpGet]
        [Route("GetAllCandidates")]
        public IActionResult GetCandidates()
        {
            return Ok(_candidateService.GetCandidates());
        }


        [HttpGet]
        [Route("Get_CandidateWith_Status")]
        public IActionResult Get_CandidateWith_Status()
        {
            return Ok(_candidateService.Get_CandidateWith_Status());
        }


        [HttpGet]
        [Route("GetCandidateById")]
        public IActionResult GetCandidateById(int Id)
        {
            return Ok(_candidateService.GetCandidateById(Id));
        }


        [HttpPost]
        [Route("CreateCandidates")]
        public IActionResult CreateCandidates([FromBody] Candidate candidate)
        {
            return Created("candidate created", _candidateService.CreateCandidates(candidate));
        }


        [HttpDelete]
        [Route("DeleteCandidateById")]
        public IActionResult DeleteCandidateById(int CandidateId)
        {
            try
            {
                var candidate = _candidateService.GetCandidateById(CandidateId);
                if (candidate == null)
                {
                    return NotFound();
                }
                _candidateService.DeleteCandidateById(CandidateId);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }


        [HttpPut]
        [Route("UpdateCandidates")]
        public IActionResult UpdateCandidates(int Id, [FromBody] Candidate candidate)
        {
            return Ok(_candidateService.UpdateCandidates(Id, candidate));
        }



        [Route("SaveFile")]
        [HttpPost ]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(40000000)]

        public JsonResult SaveFile()
        {
            try
            {

               
                var postedFile = Request.Form.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _can.ContentRootPath + "/Files/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }
                return new JsonResult(filename);
            }
            catch (Exception)
            {
                return new JsonResult("anonymous.png");
            }
        }


        [Route("DownloadFile")]
        [HttpGet /* DisableRequestSizeLimit*/]
        //[DisableRequestSizeLimit]
        //[RequestFormLimits(MultipartBodyLengthLimit = 209715200)]
        [RequestSizeLimit(40000000)]
        public IActionResult DownloadFile(string filename)
        {
            try
            {
                var physicalPath = _can.ContentRootPath + "/Files/" + filename;
                if (System.IO.File.Exists(physicalPath))
                {
                    byte[] mas = System.IO.File.ReadAllBytes(physicalPath);
                    string file_type = GetContentType(physicalPath);
                    return File(mas, file_type, Path.GetFileName(physicalPath));
                }
                return NotFound("file not found");
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".dot", "application/vnd.ms-word"},
            {".dotx","application/vnd.openxmlformats-officedocument.wordprocessingml.template" },
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".xls", "application/vnd.ms-excel"},
            {".xlt", "application/vnd.ms-excel"},
            {".xla", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".png", "image/png"},
            {".jpg", "image/jpeg"},
            {".jpeg", "image/jpeg"},
            {".gif", "image/gif"},
            {".csv", "text/csv"},{".docm", "application/vnd.ms-word.document.macroEnabled.12" },
            {".dotm", "application/vnd.ms-word.template.macroEnabled.12" },
            {".xltx", "application/vnd.openxmlformats-officedocument.spreadsheetml.template" },
            {".xlsm", "application/vnd.ms-excel.sheet.macroEnabled.12" },
            {".xltm", "application/vnd.ms-excel.template.macroEnabled.12" },
            {".xlam", "application/vnd.ms-excel.addin.macroEnabled.12" },
            {".xlsb", "application/vnd.ms-excel.sheet.binary.macroEnabled.12" },
            {".ppt", "application/vnd.ms-powerpoint" },
            {".pot", "application/vnd.ms-powerpoint" },
            {".pps", "application/vnd.ms-powerpoint" },
            {".ppa", "application/vnd.ms-powerpoint" },
            {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
            {".potx", "application/vnd.openxmlformats-officedocument.presentationml.template" },
            {".ppsx", "application/vnd.openxmlformats-officedocument.presentationml.slideshow" },
            {".ppam", "application/vnd.ms-powerpoint.addin.macroEnabled.12" },
            {".pptm", "application/vnd.ms-powerpoint.presentation.macroEnabled.12" },
            {".potm", "application/vnd.ms-powerpoint.template.macroEnabled.12" },
            {".ppsm", "application/vnd.ms-powerpoint.slideshow.macroEnabled.12" },
            {".mdb", "application/vnd.ms-access" }, };
          }

        }
    }



