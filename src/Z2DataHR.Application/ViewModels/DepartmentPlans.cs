using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Z2DataHR.Application.ViewModels
{
    public class DepartmentPlans
    {

        public string DepartmentName { get; set; }
        public int DepartmentPlanID { get; set; }
        public int DepartmentID         { get; set; }
        public int PlanYear             { get; set; }
        public int CountOfTargetEmployees { get; set; }
        public int CountOfCurrentEmployees { get; set; }
        public int GapEmployees { get; set; }
        public DateTime CreatedDate  { get; set; }
        public int CreatedBy            { get; set; }

        public int Count_Called { get; set; }
        public int Count_Waiting { get; set; }

        
            

    }
}


   
