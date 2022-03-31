using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
    public interface IEmployees
    {
        public List<Employees> GetEmployees();
        public dynamic GetEmployeeCounts();

        //public int CreateEmployees(Employees employee);

        public int CreateEmployee(Employees employee);

        int UpdateEmployees(int Id, Employees employee);
        Employees GetEmployeeById(int Id);

        void DeleteEmployeeById(int Id);
    }
}
