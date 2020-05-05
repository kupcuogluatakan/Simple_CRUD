using System;
using System.Data;
using System.Data.SqlClient;
using System.Xml;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using System.Data.Common;
using ODMSCommon.Exception;
using ODMSCommon.Logging;
using System.Collections.Generic;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using System.Text;
using ODMSModel.Filter;

namespace ODMSData
{
    public abstract class DataAccessBase : Loggable
    {
        static DataAccessBase()
        {
            DatabaseFactory.SetDatabaseProviderFactory(new DatabaseProviderFactory());
        }
        protected Database db { get; set; }
        protected DbConnection connection { get; set; }

        public string ExecuteSql { get; set; }

        protected void CreateDatabase()
        {
            db = DatabaseFactory.CreateDatabase(CommonValues.OtokarDatabase);
        }

        protected static string GetLanguageContentFromDataSet(DataTable dt, string contentColumnName)
        {
            var retVal = string.Empty;
            bool isTurkishFound = false;
            string turkishContent = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["LANGUAGE_CODE"].ToString() == "TR")
                {
                    isTurkishFound = true;
                    turkishContent = "TR" + CommonValues.Pipe + dr[contentColumnName] + CommonValues.Pipe;
                }
                else
                {
                    retVal += dr["LANGUAGE_CODE"] + CommonValues.Pipe;
                    retVal += dr[contentColumnName] + CommonValues.Pipe;
                }
            }
            if (isTurkishFound)
                retVal = turkishContent + retVal;
            return string.IsNullOrWhiteSpace(retVal) ? "TR" + CommonValues.Pipe : retVal.Substring(0, retVal.Length - 2);
        }

        protected void CreateConnection()
        {
            //added 3 retries 
            string errorMessage = string.Empty;
            Func<bool> createConnection = () =>
            {
                bool result;
                try
                {
                    connection = db.CreateConnection();
                    connection.Open();
                    result = true;
                }
                catch (Exception ex)
                {
                    result = false;
                    errorMessage = ex.Message;
                }
                return result;
            };
            for (int i = 0; i < 1; i++)
            {
                var result = createConnection();
                if (result)
                {
                    break;
                }
                //System.Threading.Thread.Sleep(TimeSpan.FromSeconds(5));
                if (i == 0)
                {
                    var logger = new Loggable();
                    logger.FatalAsync(string.Format(MessageResource.Global_Warning_DBConnectionError) + errorMessage);
                    throw new ODMSDatabaseConnectionException(MessageResource.Global_Warning_CannotConnectToDB);
                }
            }
        }

        protected void CreateConnection(DbCommand cmd)
        {
            if (connection == null)
                CreateConnection();//connection = db.CreateConnection();
            if (connection.State != ConnectionState.Open)
                connection.Open();

            cmd.Connection = connection;
            cmd.CommandTimeout = 300;

            if (cmd.CommandType == CommandType.StoredProcedure)
            {
                var sqlBuilder = new StringBuilder(string.Format("EXEC {0}", cmd.CommandText));
                var hasParam = false;
                foreach (DbParameter item in cmd.Parameters)
                {
                    if ((item.Value == DBNull.Value || item.Value == null || string.IsNullOrEmpty(item.Value.ToString())))
                        sqlBuilder.Append(string.Format(" {0} = NULL , ", item.ParameterName));
                    else
                        sqlBuilder.Append(string.Format(" {0} = '{1}' , ", item.ParameterName, item.Value.ToString()));
                    hasParam = true;
                }
                var executeSql = sqlBuilder.ToString();
                if (hasParam)
                    executeSql = executeSql.Substring(0, executeSql.Length - 2);
                ExecuteSql = executeSql;
            }
        }

        protected void CloseConnection()
        {
            if (connection != null && connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
        }

        protected static object MakeDbNull(object obj)
        {
            if (obj == null) return DBNull.Value;

            if (obj is String)
            {
                if (string.IsNullOrWhiteSpace(obj.ToString())) return DBNull.Value;
            }
            if (obj is Int16 || obj is Int32 || obj is Int64)
            {
                if (obj.ToString().Equals("0")) return DBNull.Value;
            }
            if (obj.GetType() == TypeCode.Double.GetType())
            {
                if (obj.GetValue<double>() == 0) return DBNull.Value;
            }
            if (obj is Decimal && (obj.GetValue<decimal>() == 0))
            {
                return DBNull.Value;
            }
            if (obj is DateTime)
            {
                if (((DateTime)obj).Equals(DateTime.MinValue))
                    return DBNull.Value;
            }

            return obj;
        }

        protected string ResolveDatabaseErrorXml(string xmlError)
        {
            if (string.IsNullOrEmpty(xmlError))
                return string.Empty;

            var errCode = string.Empty;
            var errParamList = new List<string>();

            if (string.IsNullOrWhiteSpace(xmlError) || xmlError.IndexOf("<Error>") < 0)
            {
                return MessageResource.Err_Generic_Unexpected;
            }

            var startIdx = xmlError.IndexOf("<Error>");
            var endIdx = xmlError.IndexOf("</Error>") + "</Error>".Length;

            var xmlDoc = new XmlDocument
            {
                InnerXml =
                        xmlError.Substring(startIdx, endIdx - startIdx)
                                .Replace("&", "&amp;")
                                .Replace("'", "&apos;")
                                .Replace("\"", "&quot;")
            };
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList nodeList = root.GetElementsByTagName("ErrorMessage");
            if (nodeList.Count > 0)
            {
                errCode = nodeList[0].InnerText;
            }

            nodeList = root.GetElementsByTagName("ErrParam1");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            nodeList = root.GetElementsByTagName("ErrParam2");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            nodeList = root.GetElementsByTagName("ErrParam3");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            if (string.IsNullOrWhiteSpace(errCode))
            { return MessageResource.Err_Generic_Unexpected; }

            return string.Format(CommonUtility.GetResourceValue(errCode), errParamList.ToArray());

        }

        protected virtual void AddPagingParameters(DbCommand cmd, BaseListWithPagingModel model)
        {
            db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(model.SortColumn));
            db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(model.SortDirection));
            db.AddInParameter(cmd, "OFFSET", DbType.Int32, model.Offset);
            db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(model.PageSize));
            db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
            db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
            db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
        }

        protected virtual void AddPagingParameters(DbCommand cmd, FilterBase model)
        {
            db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(model.SortColumn.Value));
            db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(model.SortDirection.Value));
            db.AddInParameter(cmd, "OFFSET", DbType.Int32, model.Offset.Value);
            db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(model.PageSize.Value));
            db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
            db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
            db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
        }

        protected virtual void AddPagingParametersWithLanguage(UserInfo user,DbCommand cmd, BaseListWithPagingModel model)
        {
            db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
            AddPagingParameters(cmd, model);
        }
        public T ExecSqlFunction<T>(string functionName, params object[] parameters)
        {
            var sb = new StringBuilder();
            var sql = "SELECT dbo." + functionName + "({0})";
            for (int i = 0; i < parameters.Length; i++)
            {
                sb.Append(string.Format("@p{0},", i));
            }

            sql = string.Format(sql, sb.ToString().Substring(0, sb.Length - 1));
            var cmd = new SqlCommand(sql);
            cmd.CommandText = sql;
            int j = 0;
            foreach (var param in parameters)
            {
                cmd.Parameters.Add(new SqlParameter(string.Format("@p{0}", j), param));
                j++;
            }
            return db.ExecuteScalar(cmd).GetValue<T>();

        }
    }
}
