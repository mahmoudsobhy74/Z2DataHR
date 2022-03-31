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
    public class DepartmentPlansService : IDepartmentPlan
    {

        private readonly string sqlDataSource;

        public DepartmentPlansService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }


        public List<DepartmentPlans> GetDepartmentPlans()

        {
            {
                DataTable table = new DataTable();

                List<DepartmentPlans> DepartmentPlansList = new List<DepartmentPlans>();

                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand("sp_DepartmentPlans_Get", myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        foreach (DataRow item in table.Rows)
                        {
                            DepartmentPlansList.Add(new DepartmentPlans()
                            {
                                DepartmentName = item["DepartmentName"].ToString(),
                                DepartmentPlanID = Convert.ToInt32(item["DepartmentPlanID"]),
                                DepartmentID = Convert.ToInt32(item["DepartmentID"]),
                                CreatedDate = Convert.ToDateTime(item["CreatedDate"]).Date,
                                PlanYear = Convert.ToInt32(item["PlanYear"]),
                                CountOfTargetEmployees = Convert.ToInt32(item["CountOfTargetEmployees"]),
                                CountOfCurrentEmployees = Convert.ToInt32(item["CountOfCurrentEmployees"]),
                                GapEmployees = Convert.ToInt32(item["GapEmployees"]),
                                Count_Waiting = (item["Count_Waiting"] != DBNull.Value) ? Convert.ToInt32(item["Count_Waiting"]) : 0,
                                Count_Called = (item["Count_Called"] != DBNull.Value) ? Convert.ToInt32(item["Count_Called"]) : 0,
                                //CreatedBy = (item["CreatedBy"]!= DBNull.Value) ? Convert.ToInt32(item["CreatedBy"]):0
                            });
                        }
                        myReader.Close();
                        myCon.Close();
                    }
                }
                return DepartmentPlansList;
            }
        }                                           // Get DepartmentPlans


        public DepartmentPlans GetDepartmentPlan_byID(int DepartmentPlanID)
        {
            DataTable table = new DataTable();

            DepartmentPlans newDepartmentPlans = new DepartmentPlans();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_DepartmentPlans_Get", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@DepartmentID", DepartmentPlanID));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    DataRow item = table.Rows.Count != 0 ? table.Rows[0] : null;

                    if (item == null)
                    {
                        return null;
                    }
                    newDepartmentPlans.DepartmentName = item["DepartmentName"].ToString();
                    newDepartmentPlans.DepartmentPlanID = Convert.ToInt32(item["DepartmentPlanID"]);
                    newDepartmentPlans.DepartmentID = Convert.ToInt32(item["DepartmentID"]);
                    newDepartmentPlans.CreatedDate = Convert.ToDateTime(item["CreatedDate"]).Date;
                    newDepartmentPlans.PlanYear = Convert.ToInt32(item["PlanYear"]);
                    newDepartmentPlans.CountOfTargetEmployees = Convert.ToInt32(item["CountOfTargetEmployees"]);
                    newDepartmentPlans.CountOfCurrentEmployees = Convert.ToInt32(item["CountOfCurrentEmployees"]);
                    newDepartmentPlans.GapEmployees = Convert.ToInt32(item["GapEmployees"]);
                    //newDepartmentPlans.CreatedBy = (item["CreatedBy"] != DBNull.Value) ? Convert.ToInt32(item["CreatedBy"]) : 0;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return newDepartmentPlans;
        }                        // Get DepartmentPlansByID

        public int CreateDepartmentPlans(DepartmentPlans departmentPlans)
        {

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_DepartmentPlans_Create", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@DepartmentID", departmentPlans.DepartmentID));
                    myCommand.Parameters.Add(new SqlParameter("@PlanYear", departmentPlans.PlanYear));
                    myCommand.Parameters.Add(new SqlParameter("@CountOfTargetEmployees", departmentPlans.CountOfTargetEmployees));
                    //myCommand.Parameters.Add(new SqlParameter("@DepartmentPlanID", departmentPlans.DepartmentPlanID));
                    //myCommand.Parameters.Add(new SqlParameter("@DepartmentName", departmentPlans.DepartmentName));
                    //myCommand.Parameters.Add(new SqlParameter("@CreatedBy", departmentPlans.CreatedBy));
                    //myCommand.Parameters.Add(new SqlParameter("@CountOfCurrentEmployees", departmentPlans.CountOfCurrentEmployees));
                    //myCommand.Parameters.Add(new SqlParameter("@GapEmployees", departmentPlans.GapEmployees));
                    //myCommand.Parameters.Add(new SqlParameter("@CreatedDate", departmentPlans.CreatedDate));
                    //myReader = myCommand.ExecuteReader();
                    int a = myCommand.ExecuteNonQuery();
                    myCon.Close();
                    return a;
                }
            }

        }                           // Create DepartmentPlans


        public void DeleteDepartmentPlanById(int Id)
        {
            DataTable table = new DataTable();

            DepartmentPlans newDepartmentPlan = new DepartmentPlans();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("sp_DepartmentPlans_Delete", myCon))
                {

                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@DepartmentPlanID", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    //DataRow item = table.Rows.Count != 0?table.Rows[0]:"";

                    myReader.Close();
                    myCon.Close();
                }
            }
        }                                               // Delete DepartmentPlans


        public int UpdateDepartmentPlans(int Id, DepartmentPlans departmentPlans)
        {
            DataTable table = new DataTable();

            DepartmentPlans newDepartmentPlans = new DepartmentPlans();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_DepartmentPlans_Update", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@DepartmentPlanID", Id));
                    myCommand.Parameters.Add(new SqlParameter("@DepartmentID", departmentPlans.DepartmentID));
                    myCommand.Parameters.Add(new SqlParameter("@PlanYear", departmentPlans.PlanYear));
                    myCommand.Parameters.Add(new SqlParameter("@CountOfTargetEmployees", departmentPlans.CountOfTargetEmployees));
                    //myCommand.Parameters.Add(new SqlParameter("@DepartmentName", departmentPlans.DepartmentName));
                    //myCommand.Parameters.Add(new SqlParameter("@CreatedDate", departmentPlans.@CreatedDate));
                    //myCommand.Parameters.Add(new SqlParameter("@CreatedBy", departmentPlans.@CreatedBy));
                    int a = myCommand.ExecuteNonQuery();
                    myCon.Close();
                    return a;

                }
            }

        }                    // Update DepartmentPlans
    }
}
