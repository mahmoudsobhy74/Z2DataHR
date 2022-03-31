using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.Extensions.Configuration;
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
    public class CandidateService : ICandidate
    {
        private readonly string sqlDataSource;

        public CandidateService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }

        public List<Candidate> GetCandidates()                              // Get all Candidates 
        {

            DataTable table = new DataTable();
            List<Candidate> CandidateList = new List<Candidate>();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("GetCandidate", myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    foreach (DataRow item in table.Rows)
                    {
                        CandidateList.Add(new Candidate()
                        {
                            Candidate_Name = item["Candidate_Name"].ToString(),
                            Candidate_Id = Convert.ToInt32(item["Candidate_Id"].ToString()),
                            Source_Name = item["Source_Name"].ToString(),
                            Department_Name = item["Department_Name"].ToString(),
                            EmployeeName_EN = item["Interviewer_Name"] != DBNull.Value ? item["Interviewer_Name"].ToString() : "",
                            Position_Name = item["Position_Name"].ToString(),
                            Candidate_Status_Name = item["Candidate_Status_Name"].ToString(),
                            Source_Id = Convert.ToInt32(item["Source_Id"].ToString()),
                            Department_Id = Convert.ToInt32(item["Department_Id"].ToString()),
                            Position_Id = Convert.ToInt32(item["Position_Id"].ToString()),
                            Candidate_Status_Id = Convert.ToInt32(item["Candidate_Status_Id"].ToString()),
                            Employee_ID = (item["Employee_ID"] != DBNull.Value) ? Convert.ToInt32(item["Employee_ID"].ToString()) : 0,
                            Cv_File = item["Cv_File"].ToString(),
                            Gender = Convert.ToInt32(item["Gender"]),
                            MobileNumber = (item["MobileNumber"].ToString()),
                            National_ID_NO = item["National_ID_NO"].ToString(),
                            Notice = item["Notice"].ToString(),
                            BirthDate = item["BirthDate"].ToString(),
                            Hiring_Date = item["Hiring_Date"].ToString(),
                            Date_Of_Interview = item["Date_Of_Interview"].ToString(),
                            //Interviewer_Id = Convert.ToInt32(item["Interviewer_Id"].ToString()),

                        });
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }

            return CandidateList;
        }

        public Candidate GetCandidateById(int Id)                           // Get Candidate By ID
        {
            DataTable table = new DataTable();
            Candidate newCandidate = new Candidate();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"GetCandidate", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Id", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    DataRow item = table.Rows.Count != 0 ? table.Rows[0] : null;
                    if (item == null)
                    {
                        return null;
                    }

                    newCandidate.Candidate_Id = Convert.ToInt32(item["Candidate_Id"].ToString());
                    newCandidate.Candidate_Name = item["Candidate_Name"].ToString();
                    newCandidate.Department_Name = item["Department_Name"].ToString();
                    newCandidate.Department_Id = Convert.ToInt32(item["Department_Id"].ToString());
                    newCandidate.Employee_ID = Convert.ToInt32(item["Employee_ID"].ToString());
                    //newCandidate.EmployeeName_EN = item["EmployeeName_EN"].ToString();
                    newCandidate.Notice = item["Notice"].ToString();
                    newCandidate.Source_Id = Convert.ToInt32(item["Source_Id"].ToString());
                    newCandidate.Cv_File = item["Cv_File"].ToString();
                    newCandidate.Gender = Convert.ToInt32(item["Gender"]);
                    newCandidate.MobileNumber = (item["MobileNumber"].ToString());
                    newCandidate.National_ID_NO = item["National_ID_NO"].ToString();
                    newCandidate.Position_Id = Convert.ToInt32(item["Position_Id"].ToString());
                    newCandidate.BirthDate = item["BirthDate"].ToString();
                    //newCandidate.Hiring_Date = item["Hiring_Date"].ToString();
                    newCandidate.Candidate_Status_Id = Convert.ToInt32(item["Candidate_Status_Id"].ToString());
                    newCandidate.Date_Of_Interview = item["Date_Of_Interview"].ToString();

                    //newCandidate.Interviewer_Id = Convert.ToInt32(item["Interviewer_Id"].ToString());

                    myReader.Close();
                    myCon.Close();
                }
            }

            return newCandidate;
        }

        public int CreateCandidates(Candidate candidate)                    //  Create Candidates
        {
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"CreateCandidate", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;


                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Name", candidate.Candidate_Name));
                    myCommand.Parameters.Add(new SqlParameter("@Source_Id", candidate.Source_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Cv_File", candidate.Cv_File));
                    myCommand.Parameters.Add(new SqlParameter("@Gender", candidate.Gender));
                    myCommand.Parameters.Add(new SqlParameter("@MobileNumber", candidate.MobileNumber));
                    myCommand.Parameters.Add(new SqlParameter("@National_ID_NO", candidate.National_ID_NO));
                    myCommand.Parameters.Add(new SqlParameter("@Position_Id", candidate.Position_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Notice", candidate.Notice));
                    myCommand.Parameters.Add(new SqlParameter("@BirthDate", candidate.BirthDate));
                    myCommand.Parameters.Add(new SqlParameter("@Hiring_Date", candidate.Hiring_Date));
                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Status_Id", candidate.Candidate_Status_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Date_Of_Interview", candidate.Date_Of_Interview));
                    myCommand.Parameters.Add(new SqlParameter("@Department_Id", candidate.Department_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Employee_ID", candidate.Employee_ID));
                    //myCommand.Parameters.Add(new SqlParameter("@Interviewer_Id", candidate.Interviewer_Id));
                    //myCommand.Parameters.Add(new SqlParameter("@Position_Name", candidate.Position_Name));
                    //myCommand.Parameters.Add(new SqlParameter("@Source_Name", candidate.Source_Name));


                    //myReader = myCommand.ExecuteReader();

                    int a = myCommand.ExecuteNonQuery();
                    //table.Load(myReader);
                    //DataRow item = table.Rows[0];
                    //newCandidate.Candidate_Name = item["Candidate_Name"].ToString();
                    //newCandidate.Candidate_Id = Convert.ToInt32(item["Candidate_Id"].ToString());
                    //myReader.Close();
                    myCon.Close();
                    return a;
                }
            }
        }

        public void DeleteCandidateById(int Id)                             // Delete Candidate By ID
        {
            DataTable table = new DataTable();
            Candidate newCandidate = new Candidate();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("DeleteCandidate", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Id", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    //DataRow item = table.Rows.Count != 0?table.Rows[0]:"";
                    myReader.Close();
                    myCon.Close();
                }
            }
        }

        public int UpdateCandidates(int Id, Candidate candidate)              // Update Candidates
        {
            DataTable table = new DataTable();
            Candidate newCandidate = new Candidate();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"UpdateCandidate", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Id", Id));
                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Name", candidate.Candidate_Name));
                    myCommand.Parameters.Add(new SqlParameter("@Source_Id", candidate.Source_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Cv_File", candidate.Cv_File));
                    myCommand.Parameters.Add(new SqlParameter("@Gender", candidate.Gender));
                    myCommand.Parameters.Add(new SqlParameter("@MobileNumber", candidate.MobileNumber));
                    myCommand.Parameters.Add(new SqlParameter("@National_ID_NO", candidate.National_ID_NO));
                    myCommand.Parameters.Add(new SqlParameter("@Position_Id", candidate.Position_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Notice", candidate.Notice));
                    myCommand.Parameters.Add(new SqlParameter("@BirthDate", candidate.BirthDate));
                    myCommand.Parameters.Add(new SqlParameter("@Hiring_Date", candidate.Hiring_Date));
                    myCommand.Parameters.Add(new SqlParameter("@Candidate_Status_Id", candidate.Candidate_Status_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Date_Of_Interview", candidate.Date_Of_Interview));
                    myCommand.Parameters.Add(new SqlParameter("@Department_Id", candidate.Department_Id));
                    myCommand.Parameters.Add(new SqlParameter("@Employee_ID", candidate.Employee_ID));

                    //myCommand.Parameters.Add(new SqlParameter("@Interviewer_Id", candidate.Interviewer_Id));
                    //myCommand.Parameters.Add(new SqlParameter("@Source_Name", candidate.Source_Name));
                    //myCommand.Parameters.Add(new SqlParameter("@Position_Name", candidate.Position_Name));

                    int a = myCommand.ExecuteNonQuery();
                    myCon.Close();
                    //if (a == 0)
                    //{ return newCandidate; }

                    return a;
                }
            }
            //return GetCandidateById(Id);
        }

        public List<CandidateStatusCount> Get_CandidateWith_Status()           // Get all Candidates 
        {

            DataTable table = new DataTable();
            List<CandidateStatusCount> CandidateList = new List<CandidateStatusCount>();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("Get_CandidateWith_Status", myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    foreach (DataRow item in table.Rows)
                    {
                        CandidateList.Add(new CandidateStatusCount()
                        {

                            Candidate_Status_Name = item["Candidate_Status_Name"].ToString(),
                            count = Convert.ToInt32(item["_count"].ToString()),

                        });
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }
            return CandidateList;
        }
    }
}

