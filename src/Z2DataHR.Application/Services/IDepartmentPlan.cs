using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
   public interface IDepartmentPlan
    {
        public List<DepartmentPlans> GetDepartmentPlans();
        public int UpdateDepartmentPlans(int Id, DepartmentPlans departmentPlans);
        public int CreateDepartmentPlans(DepartmentPlans departmentPlans);
        void DeleteDepartmentPlanById(int Id);

        public DepartmentPlans GetDepartmentPlan_byID(int DepartmentPlanID);
    }
}
