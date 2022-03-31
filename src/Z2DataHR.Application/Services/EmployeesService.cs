using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Z2DataHR.Application.ViewModels;

namespace Z2DataHR.Application.Services
{
    public class EmployeesService : IEmployees
    {

        private readonly string sqlDataSource;

        public EmployeesService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }

        public List<Employees> GetEmployees()
        {
            DataTable table = new DataTable();
            List<Employees> EmployeesList = new List<Employees>();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("sp_EmployeesGetAll", myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    foreach (DataRow item in table.Rows)
                    {


                        DateTime currentDate = DateTime.Now;
                        int year = currentDate.Year;

                        Employees emp = new Employees()
                        {
                            EmployeeName_AR = item[3].ToString(),
                            EmployeeName_EN = item["EmployeeName_EN"].ToString(),
                            DepartmentName = item["DepartmentName"].ToString(),
                            EmployeeStatus_Name = item["EmployeeStatus_Name"].ToString(),
                            jobTitle_Name = item["jobTitle_Name"].ToString(),
                            Branch_Name = item["Branch_Name"].ToString(),
                            Section_Name = item["Section_Name"].ToString(),
                            Governorate_Name = item["Governorate_Name"].ToString(),
                            Employee_ID = Convert.ToInt32(item["Employee_ID"]),
                            DepartmentID = Convert.ToInt32(item["DepartmentID"]),
                            IsInterviewer = (item["IsInterviewer"] != DBNull.Value) ? Convert.ToInt32(item["IsInterviewer"]) : 0,
                            Code = Convert.ToInt32(item["Code"]),
                            HiringDate = Convert.ToDateTime(item["Hiring_Date"]),
                            Resignation_Date = (item["Resignation_Date"] != DBNull.Value) ? item["Resignation_Date"].ToString() : "",
                            Leave_Date = (item["Leave_Date"] != DBNull.Value) ? item["Leave_Date"].ToString() : "",
                            Birth_Date = Convert.ToDateTime(item["Birth_Date"]),
                            Issue_Date = (item["Issue_Date"].ToString()),
                            EmployeeStatus_id = Convert.ToInt32(item["EmployeeStatus_id"]),
                            jobTitle_id = Convert.ToInt32(item["jobTitle_id"]),
                            Branch_id = Convert.ToInt32(item["Branch_id"]),
                            Section_id = Convert.ToInt32(item["Section_id"]),
                            Governorate_id = Convert.ToInt32(item["Governorate_id"].ToString()),
                            National_ID_Number = Convert.ToDouble(item["National_ID_Number"]),
                            Mobile_Number = (item["Mobile_Number"].ToString()),
                            MartialStatus = (item["MartialStatus"] != DBNull.Value) ? item["MartialStatus"].ToString() : "",
                            Citizenship = item["Citizenship"].ToString(),
                            University = item["University"].ToString(),
                            Address = item["Address"].ToString(),
                            Faculty = item["Faculty"].ToString(),
                            Business_E_mail = item["Business_E_mail"].ToString(),
                            Gender = (item["Gender"].ToString()),
                            //Gender = Convert.ToInt32(item["Gender"]),
                            CurrentInsurance_Status = item["CurrentInsurance_Status"].ToString(),
                            Insurance_Nubmer = Convert.ToInt32(item["Insurance_Nubmer"]),
                            Graduation_Year = Convert.ToInt32(item["Graduation_year"]),



                        };
                        emp.Age = year - emp.Birth_Date.Year;

                        int nYear = currentDate.Year - emp.HiringDate.Year;
                        int nMonth = currentDate.Month - emp.HiringDate.Month;

                        if (nMonth < 0)
                        {
                            nMonth = nMonth + 12;
                            nYear = nYear - 1;
                        }
                        int nDay = currentDate.Day - emp.HiringDate.Day;

                        if (nDay < 0)
                        {
                            nDay = nDay + 30;
                            nMonth = nMonth - 1;

                        }

                        emp.Service_Period = $"{nYear}  Y, {nMonth}  M, {nDay}  D";


                        EmployeesList.Add(emp);
                    }
                    myReader.Close();
                    myCon.Close();
                }
            }
            return EmployeesList;
        }                                   // Get Employee

        //public int CreateEmployees(Employees employee)                      Create GeneralTypes
        //{
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand($"sp_Employees_Create", myCon))
        //        {
        //            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


        //            myCommand.Parameters.Add(new SqlParameter("@DepartmentID", employee.DepartmentID));
        //            myCommand.Parameters.Add(new SqlParameter("@EmployeeName", employee.EmployeeName));
        //            myCommand.Parameters.Add(new SqlParameter("@Code", employee.Code));
        //            myCommand.Parameters.Add(new SqlParameter("@IsInterviewer", employee.IsInterviewer));
        //            myCommand.Parameters.Add(new SqlParameter("@EmployeeStatus_id", employee.EmployeeStatus_id));
        //            myCommand.Parameters.Add(new SqlParameter("@jobTitle_id", employee.jobTitle_id));
        //            myCommand.Parameters.Add(new SqlParameter("@Branch_id", employee.Branch_id));
        //            myCommand.Parameters.Add(new SqlParameter("@Section_id", employee.Section_id));
        //            myCommand.Parameters.Add(new SqlParameter("@HiringDate", employee.HiringDate));
        //            myCommand.Parameters.Add(new SqlParameter("@Resignation_Date", employee.Resignation_Date));
        //            myCommand.Parameters.Add(new SqlParameter("@Leave_Date", employee.Leave_Date));


        //            int a = myCommand.ExecuteNonQuery();

        //            myCon.Close();
        //            return a;
        //        }
        //    }
        //}

        public Employees GetEmployeeById(int Id)
        {
            DataTable table = new DataTable();

            Employees newEmployee = new Employees();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_EmployeesGetAll", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@Employee_ID", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    DataRow item = table.Rows.Count != 0 ? table.Rows[0] : null;

                    if (item == null)
                    {
                        return null;
                    }
                    DateTime currentDate = DateTime.Now;
                    int year = currentDate.Year;

                    Employees emp = new Employees()
                    {
                        EmployeeName_AR = item[3].ToString(),
                        EmployeeName_EN = item["EmployeeName_EN"].ToString(),
                        DepartmentName = item["DepartmentName"].ToString(),
                        EmployeeStatus_Name = item["EmployeeStatus_Name"].ToString(),
                        jobTitle_Name = item["jobTitle_Name"].ToString(),
                        Branch_Name = item["Branch_Name"].ToString(),
                        Section_Name = item["Section_Name"].ToString(),
                        Governorate_Name = item["Governorate_Name"].ToString(),
                        Employee_ID = Convert.ToInt32(item["Employee_ID"]),
                        DepartmentID = Convert.ToInt32(item["DepartmentID"]),
                        IsInterviewer = (item["IsInterviewer"] != DBNull.Value) ? Convert.ToInt32(item["IsInterviewer"]) : 0,
                        Code = Convert.ToInt32(item["Code"]),
                        HiringDate = Convert.ToDateTime(item["Hiring_Date"]),
                        Resignation_Date = (item["Resignation_Date"] != DBNull.Value) ? item["Resignation_Date"].ToString() : "",
                        Leave_Date = (item["Leave_Date"] != DBNull.Value) ? item["Leave_Date"].ToString() : "",
                        Birth_Date = Convert.ToDateTime(item["Birth_Date"]),
                        Issue_Date = (item["Issue_Date"].ToString()),
                        EmployeeStatus_id = Convert.ToInt32(item["EmployeeStatus_id"]),
                        jobTitle_id = Convert.ToInt32(item["jobTitle_id"]),
                        Branch_id = Convert.ToInt32(item["Branch_id"]),
                        Section_id = Convert.ToInt32(item["Section_id"]),
                        National_ID_Number = Convert.ToDouble(item["National_ID_Number"]),
                        Mobile_Number = (item["Mobile_Number"].ToString()),
                        MartialStatus = (item["MartialStatus"] != DBNull.Value) ? item["MartialStatus"].ToString() : "",
                        Citizenship = item["Citizenship"].ToString(),
                        University = item["University"].ToString(),
                        Governorate_id = Convert.ToInt32(item["Governorate_id"].ToString()),
                        Address = item["Address"].ToString(),
                        Faculty = item["Faculty"].ToString(),
                        Business_E_mail = item["Business_E_mail"].ToString(),
                        CurrentInsurance_Status = item["CurrentInsurance_Status"].ToString(),
                        Insurance_Nubmer = Convert.ToInt32(item["Insurance_Nubmer"]),
                        Graduation_Year = Convert.ToInt32(item["Graduation_year"]),
                        Gender = (item["Gender"].ToString()),


                    };

                    emp.Age = year - emp.Birth_Date.Year;

                    int nYear = currentDate.Year - emp.HiringDate.Year;
                    int nMonth = currentDate.Month - emp.HiringDate.Month;
                    int nDay = currentDate.Day - emp.HiringDate.Day;


                    if (nMonth < 0)
                    {
                        nMonth = nMonth + 12;
                        nYear = nYear - 1;
                    }

                    if (nDay < 0)
                    {
                        nDay = nDay + 30;
                        nMonth = nMonth - 1;

                    }

                    emp.Service_Period = $"{nYear}  Y, {nMonth}  M, {nDay}  D";
                    newEmployee = emp;
                    myReader.Close();
                    myCon.Close();
                }
            }

            return newEmployee;
        }                        // GetEmployeeByID


        public void DeleteEmployeeById(int Id)
        {
            DataTable table = new DataTable();

            Employees newEmployees = new Employees();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("sp_Employees_T_Delete", myCon))
                {

                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@Employee_ID", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    //DataRow item = table.Rows.Count != 0?table.Rows[0]:"";

                    myReader.Close();
                    myCon.Close();
                }
            }

        }                      // Delete Employee

        public int CreateEmployee(Employees employee)
        {

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_Employees_T_Create", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@Code", employee.Code));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeName_EN", employee.EmployeeName_EN));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeName_AR", employee.EmployeeName_AR));
                    myCommand.Parameters.Add(new SqlParameter("@DepartmentID", employee.DepartmentID));
                    myCommand.Parameters.Add(new SqlParameter("@IsInterviewer", employee.IsInterviewer));
                    myCommand.Parameters.Add(new SqlParameter("@Hiring_Date", employee.HiringDate));
                    myCommand.Parameters.Add(new SqlParameter("@Branch_id", employee.Branch_id));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeStatus_id", employee.EmployeeStatus_id));
                    if (!string.IsNullOrEmpty(employee.Resignation_Date))
                    {
                        myCommand.Parameters.Add(new SqlParameter("@Resignation_Date", employee.Resignation_Date));
                    }

                    if (!string.IsNullOrEmpty(employee.Leave_Date))
                    {
                        myCommand.Parameters.Add(new SqlParameter("@Leave_Date", employee.Leave_Date));

                    }
                    myCommand.Parameters.Add(new SqlParameter("@jobTitle_id", employee.jobTitle_id));
                    myCommand.Parameters.Add(new SqlParameter("@Section_id", employee.Section_id));
                    myCommand.Parameters.Add(new SqlParameter("@DirectManager_ID", employee.DirectManager_ID));
                    myCommand.Parameters.Add(new SqlParameter("@DeptManger_ID", employee.DeptManger_ID));
                    myCommand.Parameters.Add(new SqlParameter("@Birth_Date", employee.Birth_Date));

                    //if (employee.Gender == 1)
                    //{
                    myCommand.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    //}
                    //else {
                    //    myCommand.Parameters.Add(new SqlParameter("@Gender", "Female"));
                    //}

                    myCommand.Parameters.Add(new SqlParameter("@National_ID_Number", employee.National_ID_Number));
                    if (!string.IsNullOrEmpty(employee.Issue_Date))
                    {
                        myCommand.Parameters.Add(new SqlParameter("@Issue_Date", employee.Issue_Date));
                    }

                    myCommand.Parameters.Add(new SqlParameter("@Citizenship", employee.Citizenship));
                    myCommand.Parameters.Add(new SqlParameter("@MartialStatus", employee.MartialStatus));
                    myCommand.Parameters.Add(new SqlParameter("@Address", employee.Address));
                    myCommand.Parameters.Add(new SqlParameter("@Mobile_Number", employee.Mobile_Number));
                    myCommand.Parameters.Add(new SqlParameter("@Business_E_mail", employee.Business_E_mail));
                    myCommand.Parameters.Add(new SqlParameter("@Faculty", employee.Faculty));
                    myCommand.Parameters.Add(new SqlParameter("@University", employee.University));
                    myCommand.Parameters.Add(new SqlParameter("@Graduation_year", employee.Graduation_Year));
                    myCommand.Parameters.Add(new SqlParameter("@Insurance_Nubmer", employee.Insurance_Nubmer));
                    myCommand.Parameters.Add(new SqlParameter("@CurrentInsurance_Status", employee.CurrentInsurance_Status));
                    myCommand.Parameters.Add(new SqlParameter("@Governorate_id", employee.Governorate_id));

                    int a = myCommand.ExecuteNonQuery();
                    myCon.Close();
                    return a;
                }
            }
        }             // Create Employee

        public int UpdateEmployees(int Id, Employees employee)      // Update Employees
        {
            DataTable table = new DataTable();
            Employees newEmployee = new Employees();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_Employees_T_Update", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@Employee_ID", Id));
                    myCommand.Parameters.Add(new SqlParameter("@Code", employee.Code));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeName_EN", employee.EmployeeName_EN));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeName_AR", employee.EmployeeName_AR));
                    myCommand.Parameters.Add(new SqlParameter("@DepartmentID", employee.DepartmentID));
                    myCommand.Parameters.Add(new SqlParameter("@IsInterviewer", employee.IsInterviewer));
                    myCommand.Parameters.Add(new SqlParameter("@Hiring_Date", employee.HiringDate));
                    myCommand.Parameters.Add(new SqlParameter("@Branch_id", employee.Branch_id));
                    myCommand.Parameters.Add(new SqlParameter("@EmployeeStatus_id", employee.EmployeeStatus_id));
                    myCommand.Parameters.Add(new SqlParameter("@Resignation_Date", employee.Resignation_Date));
                    myCommand.Parameters.Add(new SqlParameter("@Leave_Date", employee.Leave_Date));
                    myCommand.Parameters.Add(new SqlParameter("@jobTitle_id", employee.jobTitle_id));
                    myCommand.Parameters.Add(new SqlParameter("@Section_id", employee.Section_id));
                    myCommand.Parameters.Add(new SqlParameter("@DirectManager_ID", employee.DirectManager_ID));
                    myCommand.Parameters.Add(new SqlParameter("@DeptManger_ID", employee.DeptManger_ID));
                    myCommand.Parameters.Add(new SqlParameter("@Birth_Date", employee.Birth_Date));
                    myCommand.Parameters.Add(new SqlParameter("@Gender", employee.Gender));
                    //if (employee.Gender == 1)
                    //{
                    //    myCommand.Parameters.Add(new SqlParameter("@Gender", "Male"));
                    //}
                    //else
                    //{
                    //    myCommand.Parameters.Add(new SqlParameter("@Gender", "Female"));
                    //}

                    myCommand.Parameters.Add(new SqlParameter("@National_ID_Number", employee.National_ID_Number));
                    myCommand.Parameters.Add(new SqlParameter("@Issue_Date", employee.Issue_Date));
                    myCommand.Parameters.Add(new SqlParameter("@Citizenship", employee.Citizenship));
                    myCommand.Parameters.Add(new SqlParameter("@MartialStatus", employee.MartialStatus));
                    myCommand.Parameters.Add(new SqlParameter("@Address", employee.Address));
                    myCommand.Parameters.Add(new SqlParameter("@Mobile_Number", employee.Mobile_Number));
                    myCommand.Parameters.Add(new SqlParameter("@Business_E_mail", employee.Business_E_mail));
                    myCommand.Parameters.Add(new SqlParameter("@Faculty", employee.Faculty));
                    myCommand.Parameters.Add(new SqlParameter("@University", employee.University));
                    myCommand.Parameters.Add(new SqlParameter("@Graduation_year", employee.Graduation_Year));
                    myCommand.Parameters.Add(new SqlParameter("@Insurance_Nubmer", employee.Insurance_Nubmer));
                    myCommand.Parameters.Add(new SqlParameter("@CurrentInsurance_Status", employee.CurrentInsurance_Status));
                    myCommand.Parameters.Add(new SqlParameter("@Governorate_id", employee.Governorate_id));

                    int a = myCommand.ExecuteNonQuery();
                    myCon.Close();
                    return a;

                }
            }
        }
        public dynamic GetEmployeeCounts()
        {
            DataTable table = new DataTable();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("EmployeeCountBranchs_Get", myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new { BanhaCount = table.Rows[0]["BanhaCount"], CairoCount = table.Rows[0]["CairoCount"] };
        }                        //  GetEmployee Counts


    }
}

