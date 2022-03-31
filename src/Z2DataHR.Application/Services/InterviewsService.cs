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
    public class InterviewsService : IInterviews
    {
        private readonly string sqlDataSource;

        public InterviewsService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }


        public List<Interviews> GetInterviews()
        {

            DataTable table = new DataTable();

            List<Interviews> InterviewsList = new List<Interviews>();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("GetInterviews", myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    foreach (DataRow item in table.Rows)
                    {
                        InterviewsList.Add(new Interviews()
                        {
                            //Candidate_Name = item["Candidate_Name"].ToString(),
                            Interview_Id = Convert.ToInt32(item["Interview_Id"].ToString())
                        });
                    }

                    myReader.Close();
                    myCon.Close();
                }
            }

            return InterviewsList;
        }

       
    }
    }

