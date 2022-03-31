using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2DataHR.Application.ViewModels
{
    public class Interviews
    {
        public int Interview_Id { get; set; }
        public int Candidate_Id { get; set; }
        public int Interviewer_Id { get; set; }
        public int Department_Id { get; set; }
        public DateTime Interview_Date { get; set; }
        public DateTime Interview_Hour { get; set; }
        public int Result_Status_Id { get; set; }
        public string Notice { get; set; }
    }
}
