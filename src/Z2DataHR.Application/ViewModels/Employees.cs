using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2DataHR.Application.ViewModels
{
    public class Employees
    {
        public int Employee_ID { get; set; }
        public string EmployeeName_AR { get; set; }
        public string EmployeeName_EN { get; set; }
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public int Code { get; set; }
        public int IsInterviewer { get; set; }
        public DateTime HiringDate { get; set; } = new DateTime(1900, 1, 1);
        public string Resignation_Date { get; set; }
        public string Leave_Date { get; set; }
        public int EmployeeStatus_id { get; set; }
        public string EmployeeStatus_Name { get; set; }
        public int jobTitle_id { get; set; }
        public string jobTitle_Name { get; set; }
        public int Branch_id { get; set; }
        public string Branch_Name { get; set; }
        public int Section_id { get; set; }
        public string Section_Name { get; set; }
        public int DirectManager_ID { get; set; }
        public int DeptManger_ID { get; set; }
        public DateTime Birth_Date { get; set; } = new DateTime(1900, 1, 1);
        public string Issue_Date { get; set; }
        public int Age { get; set; }
        public double National_ID_Number { get; set; }
        public string Mobile_Number { get; set; }
        public string Citizenship { get; set; }
        public string MartialStatus { get; set; }
        public int Governorate_id { get; set; }
        public string Governorate_Name { get; set; }
        public string Address { get; set; }
        public string Faculty { get; set; }
        public string Service_Period { get; set; }
        public string Business_E_mail { get; set; }
        public string Gender { get; set; }
        public string University { get; set; }
        public int Graduation_Year { get; set; }
        public int Insurance_Nubmer { get; set; }
        public string CurrentInsurance_Status { get; set; }

    }

}
