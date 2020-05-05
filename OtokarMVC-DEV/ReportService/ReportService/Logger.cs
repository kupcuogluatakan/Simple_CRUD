using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ReportService
{
    public class Logger : ILogger
    {
        public void Log(string message, string methodParameters, out int i)
        {
            i = 0;
            var stackTrace = new StackTrace();
            var stackFrame = stackTrace.GetFrame(1);

            var functionName = stackFrame.GetMethod().Name;
            var className = stackFrame.GetMethod().ReflectedType.FullName;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Otokar_DMSConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LOG_ERROR", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@APP_ERROR_ID", i);
            cmd.Parameters.AddWithValue("@SYSTEM_CODE", functionName);
            cmd.Parameters.AddWithValue("@ERROR_SOURCE", className);
            cmd.Parameters.AddWithValue("@ERROR_DESC", message);
            cmd.Parameters.AddWithValue("@DEBUG_PARAMETERS", methodParameters);
            cmd.Parameters.AddWithValue("@DEBUG_SECTION", functionName);
            cmd.Parameters.AddWithValue("@REF_VALUE", DBNull.Value);

            connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                cmd.Dispose();
            }
        }

        public void Info(string message,string parameters)
        {

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Otokar_DMSConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("LOG_ERROR", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@APP_ERROR_ID", 0);
            cmd.Parameters.AddWithValue("@SYSTEM_CODE", "ReportUtility.Logger");
            cmd.Parameters.AddWithValue("@ERROR_SOURCE", "ReportUtility.Info");
            cmd.Parameters.AddWithValue("@ERROR_DESC", message);
            cmd.Parameters.AddWithValue("@DEBUG_PARAMETERS", parameters);
            cmd.Parameters.AddWithValue("@DEBUG_SECTION", "");
            cmd.Parameters.AddWithValue("@REF_VALUE", DBNull.Value);

            connection.Open();

            try
            {
                cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                cmd.Dispose();
            }
        }
    }
}