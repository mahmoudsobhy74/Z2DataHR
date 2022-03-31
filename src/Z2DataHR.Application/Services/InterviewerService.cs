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
    public class InterviewerService : IInterviewer
    {

        private readonly string sqlDataSource;

        public InterviewerService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }


        public Interviewer GetInterviewerById(int Id)
        {
            DataTable table = new DataTable();

            Interviewer newInterviewer = new Interviewer();

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
                    newInterviewer.Interviewer_Id = Convert.ToInt32(item[" Interviewer_Id"].ToString());
                    newInterviewer.Interviewer_Name = item[" Interviewer_Name"].ToString();


                    myReader.Close();
                    myCon.Close();
                }
            }

            return newInterviewer;
        }                       //  Get InterviewerByID

        DocumentFormat.OpenXml.Bibliography.Interviewer IInterviewer.GetInterviewerById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
