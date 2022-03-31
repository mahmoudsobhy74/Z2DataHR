using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2DataHR.Application.ViewModels
{
    public class Candidate
    {

        public int Candidate_Id { get; set; }
        public string Candidate_Name { get; set; }
        public int Source_Id { get; set; }
        public string Source_Name { get; set; }
        public int Department_Id { get; set; }
        public string Department_Name { get; set; }
        public int Employee_ID { get; set; }
        public string EmployeeName_EN { get; set; }
        public int Position_Id { get; set; }
        public string Position_Name { get; set; }
        public int Candidate_Status_Id { get; set; }
        public string Candidate_Status_Name { get; set; }
        public string Cv_File { get; set; }
        public int Gender { get; set; }
        public string MobileNumber { get; set; }
        public string National_ID_NO { get; set; }
        public string Notice { get; set; }
        public string BirthDate { get; set; }
        public string Hiring_Date { get; set; }
        public string Date_Of_Interview { get; set; }

        //public int Interviewer_Id { get; set; }
    }


    public class CandidateStatusCount
    {
        public string Candidate_Status_Name { get; set; }
        public int count { get; set; }

    }
}
