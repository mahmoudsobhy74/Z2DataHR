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
    public class GeneralTypesService : IGeneralTypes
    {
        private readonly string sqlDataSource;

        public GeneralTypesService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }


        List<GeneralTypes> IGeneralTypes.GetGeneralTypes()
        {
            {
                DataTable table = new DataTable();

                List<GeneralTypes> GeneralTypesList = new List<GeneralTypes>();

                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand("spGetGeneralTypes", myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        foreach (DataRow item in table.Rows)
                        {
                            GeneralTypesList.Add(new GeneralTypes()
                            {
                                GeneralTypeID = Convert.ToInt32(item["GeneralTypeID"].ToString()),
                                GeneralType = item["GeneralType"].ToString(),
                                TypeID = item["TypeID"].ToString(),
                                //TypeID = Convert.ToInt32(item["TypeID"].ToString())
                            });
                        }

                        myReader.Close();
                        myCon.Close();
                    }
                }

                return GeneralTypesList;
            }
        }                           //  Get GeneralTypes


        public int CreateGeneralTypes(GeneralTypes generalTypes)                    //  Create GeneralTypes
        {
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand($"sp_Types_Create", myCon))
                {
                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;


                    myCommand.Parameters.Add(new SqlParameter("@GeneralType", generalTypes.GeneralType));
                    myCommand.Parameters.Add(new SqlParameter("@TypeID", generalTypes.TypeID));


                    int a = myCommand.ExecuteNonQuery();

                    myCon.Close();
                    return a;
                }
            }


        }

        public void DeleteGeneralTypeById(int Id)
        {
            DataTable table = new DataTable();

            GeneralTypes newGeneralType = new GeneralTypes();

            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand("sp_GeneralType_Delete", myCon))
                {

                    myCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    myCommand.Parameters.Add(new SqlParameter("@GeneralTypeID", Id));

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    //DataRow item = table.Rows.Count != 0?table.Rows[0]:"";

                    myReader.Close();
                    myCon.Close();
                }
            }

        }                                 //  Delete GeneralTypes
    }
}


