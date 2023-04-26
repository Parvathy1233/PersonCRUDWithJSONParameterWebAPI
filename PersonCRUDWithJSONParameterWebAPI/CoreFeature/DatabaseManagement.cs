using Newtonsoft.Json;
using PersonCRUDWithJSONParameterWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;

namespace PersonCRUDWithJSONParameterWebAPI.CoreFeature
{
    public enum Opration
    {
        Save,
        SimpleList,
        ListWithParam,
        Details,
        Delete,
        ScalarOutput
    }
    public class DatabaseManagement<TOutput, TInput>
    {
        public Response<TOutput> DatabaseOpration(string storedProcedure, TInput model, Opration opration)
        {
            string outputJson = string.Empty;
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = storedProcedure;
            Response<TOutput> responseBase = new Response<TOutput>();

            switch (opration)
            {
                case Opration.Save:
                    try
                    {
                        command.Parameters.AddWithValue("@RequestJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@RetVal", SqlDbType.Int, 0);
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@ErrorNumber", SqlDbType.Int, 0);
                        command.Parameters["@ErrorNumber"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@Message", SqlDbType.NVarChar, 300);
                        command.Parameters["@Message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        responseBase.Message = Convert.ToString(command.Parameters["@Message"].Value); ;
                        responseBase.RetVal = (int)(command.Parameters["@RetVal"].Value);
                        responseBase.ErrorNumber = Convert.ToInt32((command.Parameters["@ErrorNumber"].Value));
                        connection.Close();
                        return responseBase;
                    }
                    catch (Exception e)
                    {
                        responseBase.Message = Convert.ToString(command.Parameters["@Message"].Value);
                        responseBase.ErrorNumber = Convert.ToInt32((command.Parameters["@ErrorNumber"].Value));
                        connection.Close();
                        return responseBase;
                    }
                case Opration.ScalarOutput:
                    try
                    {
                        command.Parameters.AddWithValue("@RequestJson", JsonConvert.SerializeObject(model));
                        command.Parameters.Add("@RetVal", SqlDbType.Int, 0);
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@ErrorNumber", SqlDbType.Int, 0);
                        command.Parameters["@ErrorNumber"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@Message", SqlDbType.NVarChar, 300);
                        command.Parameters["@Message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        responseBase.Message = (string)command.Parameters["@Message"].Value;
                        responseBase.RetVal = (int)(command.Parameters["@RetVal"].Value);
                        responseBase.Message = (string)command.Parameters["@Message"].Value;
                        responseBase.ErrorNumber = Convert.ToInt32(command.Parameters["@ErrorNumber"].Value);
                        connection.Close();
                        return responseBase;
                    }
                    catch (Exception)
                    {
                        responseBase.Message = (string)command.Parameters["@Message"].Value;
                        responseBase.ErrorNumber = Convert.ToInt32(command.Parameters["@ErrorNumber"].Value);
                        connection.Close();
                        return responseBase;
                    }
                case Opration.SimpleList: 
                    try
                    {
                        command.Parameters.AddWithValue("@RequestJson", JsonConvert.SerializeObject(model));

                        command.Parameters.Add("@RetVal", SqlDbType.Int, 0);
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@ErrorNumber", SqlDbType.Int, 0);
                        command.Parameters["@ErrorNumber"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@Message", SqlDbType.NVarChar, 300);
                        command.Parameters["@Message"].Direction = ParameterDirection.Output;
                        SqlDataReader reader = command.ExecuteReader();
                        while (reader.Read())
                        {
                            outputJson += reader.IsDBNull(0) ? string.Empty : reader[0].ToString();
                        }
                        reader.Close();

                        responseBase.RetVal = (int)command.Parameters["@RetVal"].Value;
                        responseBase.ErrorNumber = (int)command.Parameters["@ErrorNumber"].Value;
                        responseBase.Message = Convert.ToString(command.Parameters["@Message"].Value);
                        responseBase.Model = JsonConvert.DeserializeObject<TOutput>(outputJson);
                        connection.Close();
                        return responseBase;
                    }
                    catch (Exception e)
                    {
                        responseBase.Message = Convert.ToString(command.Parameters["@Message"].Value);
                        responseBase.ErrorNumber = Convert.ToInt32((command.Parameters["@ErrorNumber"].Value));
                        connection.Close();
                        return responseBase;
                    }
                case Opration.Delete:
                    try
                    {
                        command.Parameters.AddWithValue("@RequestJson", JsonConvert.SerializeObject(model));
                        command.Parameters.Add("@RetVal", SqlDbType.Int, 0);
                        command.Parameters["@RetVal"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@ErrorNumber", SqlDbType.Int, 0);
                        command.Parameters["@ErrorNumber"].Direction = ParameterDirection.Output;

                        command.Parameters.Add("@Message", SqlDbType.NVarChar, 300);
                        command.Parameters["@Message"].Direction = ParameterDirection.Output;

                        command.ExecuteNonQuery();
                        responseBase.RetVal = (int)(command.Parameters["@RetVal"].Value);
                        responseBase.Message = (string)command.Parameters["@Message"].Value;
                        responseBase.ErrorNumber = Convert.ToInt32(command.Parameters["@ErrorNumber"].Value);
                        connection.Close();
                        return responseBase;
                    }
                    catch (Exception e)
                    {
                        responseBase.RetVal = 0;
                        responseBase.Message = (string)command.Parameters["@Message"].Value;
                        responseBase.ErrorNumber = Convert.ToInt32(command.Parameters["@ErrorNumber"].Value);
                        connection.Close();
                        return responseBase;
                    }

                default:
                    connection.Close();
                    return null;

            }
        }
    }
}