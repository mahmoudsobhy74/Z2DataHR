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
    public class TypesService : ITypes
    {
        private readonly string sqlDataSource;

        public TypesService(IOptions<DbConnectionString> Connection)
        {
            sqlDataSource = Connection.Value.InterviewerAppCon;
        }

        public int CreateTypes(Types types)
        {
            throw new NotImplementedException();
        }

        List<Types> ITypes.GetTypes()
        {
            {
                DataTable table = new DataTable();

                List<Types> TypesList = new List<Types>();

                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand("sp_Get_Types", myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        foreach (DataRow item in table.Rows)
                        {
                            TypesList.Add(new Types()
                            {
                                //GeneralTypeID = Convert.ToInt32(item["GeneralTypeID"].ToString()),
                                TypeName = item["TypeName"].ToString(),
                                TypeID = Convert.ToInt32(item["TypeID"].ToString())
                            });
                        }

                        myReader.Close();
                        myCon.Close();
                    }
                }

                return TypesList;
            }
        }

        //public int CreateTypes(Types types)                    //  Create GeneralTypes
        //{
        //    SqlDataReader myReader;
        //    using (SqlConnection myCon = new SqlConnection(sqlDataSource))
        //    {
        //        myCon.Open();
        //        using (SqlCommand myCommand = new SqlCommand($"sp_GeneralTypes_Create", myCon))
        //        {
        //            myCommand.CommandType = System.Data.CommandType.StoredProcedure;


        //            myCommand.Parameters.Add(new SqlParameter("@GeneralType", generalTypes.GeneralType));
        //            myCommand.Parameters.Add(new SqlParameter("@TypeID", generalTypes.TypeID));
        //            myCommand.Parameters.Add(new SqlParameter("@CreatedBy", generalTypes.CreatedBy));

        //            int a = myCommand.ExecuteNonQuery();

        //            myCon.Close();
        //            return a;
        //        }
        //    }
        }
    }

