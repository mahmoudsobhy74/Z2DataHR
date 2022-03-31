using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2DataHR.Application.Services
{
   public interface IInterviewer
    {
       
        Interviewer GetInterviewerById(int Id);
    }
}
