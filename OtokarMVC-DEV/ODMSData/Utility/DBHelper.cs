using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using Kendo.Mvc.Extensions;
using ODMSCommon;

namespace ODMSData.Utility
{
    public class DbHelper
    {
        private readonly SqlConnection _connection;
        private readonly bool _makeParamsDbNull;
        private List<SqlParameterInfo> _parameterInfos;
        private readonly Dictionary<string, object> _outputValues = new Dictionary<string, object>();
        private static readonly Dictionary<string, List<SqlParameterInfo>> _parametersCache = new Dictionary<string, List<SqlParameterInfo>>();
        private const string SqlParameterProviderProcedure = "P_LIST_SP_PARAM_NAMES";

        private DbHelper()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionStrings["OtokarDMS_DB"].ConnectionString);
        }

        public DbHelper(bool makeParamsDbNull = true)
            : this()
        {
            _makeParamsDbNull = makeParamsDbNull;
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        public object GetOutputValue(string key)
        {
            object value = null;
            key = "@" + key;
            if (_outputValues.ContainsKey(key))
                _outputValues.TryGetValue(key, out value);
            return value;
        }

        private void CloseConnection()
        {
            if (_connection.State != ConnectionState.Closed)
                _connection.Close();
        }

        private SqlCommand BuildCommand(string sql, params SqlParameter[] parameters)
        {
            var command = _connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sql.Trim();
            if (parameters != null)
                command.Parameters.AddRange(parameters);
            return command;
        }

        private SqlCommand BuildCommand(string sql, params object[] args)
        {
            var command = _connection.CreateCommand();
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sql.Trim();

            if (args != null)
            {
                var cmd = new SqlCommand(SqlParameterProviderProcedure, _connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("PROCEDURE_NAME", sql.Trim());
                _parameterInfos = GetParameters(cmd);
                int index = 0;
                foreach (var arg in args)
                {
                    var param = new SqlParameter();
                    param.DbType = GetDbType(_parameterInfos[index].ParameterType);
                    if (param.DbType != DbType.Object)
                    {
                        param.Value = _makeParamsDbNull ? MakeDbNull(arg) : arg;
                        param.ParameterName = _parameterInfos[index].ParameterName;
                        param.Size = _parameterInfos[index].MaxSize;
                        param.Direction = _parameterInfos[index].IsOutput
                            ? ParameterDirection.InputOutput
                            : ParameterDirection.Input;
                        command.Parameters.Add(param);
                    }
                    else
                    {
                        command.Parameters.Add(new SqlParameter(_parameterInfos[index].ParameterName, arg));
                    }
                    index++;
                }
            }
            return command;
        }

        //NOTE: desteklemeyen var sa eklenmeli
        private DbType GetDbType(string parameterType)
        {
            switch (parameterType.ToLower())
            {
                case "nvarchar":
                case "varchar":
                    return DbType.String;
                case "int":
                    return DbType.Int32;
                case "bigint":
                    return DbType.Int64;
                case "smallint":
                    return DbType.Int16;
                case "single":
                    return DbType.Single;
                case "datetime":
                    return DbType.DateTime;
                case "date":
                    return DbType.Date;
                case "datetime2":
                    return DbType.DateTime2;
                case "double":
                    return DbType.Double;
                case "numeric":
                case "decimal":
                    return DbType.Decimal;
                case "bit":
                    return DbType.Boolean;
                default:
                    return DbType.Object;
            }
        }

        private object MakeDbNull(object obj)
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

        private int ExecuteNonQueryCommand(SqlCommand cmd)
        {
            var result = -1;
            try
            {
                OpenConnection();

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);

                cmd.Dispose();
            }
            return result;
        }

        private void SetOutputValues(SqlCommand cmd)
        {
            _outputValues.Clear();
            foreach (SqlParameter param in cmd.Parameters)
            {
                if (param.Direction == ParameterDirection.Output || param.Direction == ParameterDirection.InputOutput)
                {
                    _outputValues.Add(param.ParameterName, param.Value);
                }
            }
        }

        private T ExecuteScalar<T>(SqlCommand cmd)
        {
            T t;
            try
            {
                OpenConnection();

                var value = cmd.ExecuteScalar();
                if (value != null && value != DBNull.Value)
                    if (Nullable.GetUnderlyingType(typeof(T)) == null)
                        t = (T)Convert.ChangeType(value, typeof(T));
                    else
                    {
                        t = (T)value;

                    }

                else
                {
                    t = default(T);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);
                cmd.Dispose();
            }
            return t;
        }

        private T ExecuteReader<T>(SqlCommand cmd) where T : class, new()
        {
            var t = new T();

            var properties = t.GetType().GetProperties();

            try
            {
                OpenConnection();

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var schemaTable = reader.GetSchemaTable(); if (schemaTable != null)
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                            string columnName = schemaTable.Rows[i].ItemArray[0].ToString();
                            var property = properties.FirstOrDefault(c => c.Name == columnName);
                            if (property != null)
                                if (reader[columnName] != DBNull.Value)
                                    if (Nullable.GetUnderlyingType(property.PropertyType) == null)
                                        property.SetValue(t,
                                            Convert.ChangeType(reader[columnName], property.PropertyType));
                                    else
                                    {
                                        property.SetValue(t,
                                        Convert.ChangeType(reader[columnName], Nullable.GetUnderlyingType(property.PropertyType)));
                                    }
                        }
                    }
                }
                reader.Close();
                reader.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);
                cmd.Dispose();
            }
            return t;
        }

        private List<SqlParameterInfo> GetParameters(SqlCommand cmd)
        {
            string storedProcedureName = cmd.Parameters["PROCEDURE_NAME"].Value.ToString();
            if (_parametersCache.ContainsKey(storedProcedureName))
                return _parametersCache[storedProcedureName];

            var t = new SqlParameterInfo();

            var properties = t.GetType().GetProperties();
            var list = new List<SqlParameterInfo>();
            try
            {
                OpenConnection();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                    {
                        var item = new SqlParameterInfo();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                            string columnName = schemaTable.Rows[i].ItemArray[0].ToString();
                            var property = properties.FirstOrDefault(c => c.Name == columnName);
                            if (property != null)
                                if (reader[columnName] != DBNull.Value)
                                    if (Nullable.GetUnderlyingType(property.PropertyType) == null)
                                        property.SetValue(item,
                                            Convert.ChangeType(reader[columnName], property.PropertyType));
                                    else
                                    {
                                        property.SetValue(item,
                                        Convert.ChangeType(reader[columnName], Nullable.GetUnderlyingType(property.PropertyType)));
                                    }

                        }
                        list.Add(item);
                    }

                }

                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);
                cmd.Dispose();
            }
            _parametersCache[storedProcedureName] = list;
            return list;
        }

        private List<T> ExecuteListReader<T>(SqlCommand cmd) where T : class, new()
        {
            var t = new T();

            var properties = t.GetType().GetProperties();
            var list = new List<T>();
            try
            {
                OpenConnection();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var schemaTable = reader.GetSchemaTable();
                    if (schemaTable != null)
                    {
                        var item = new T();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {

                            string columnName = schemaTable.Rows[i].ItemArray[0].ToString();
                            var property = properties.FirstOrDefault(c => c.Name == columnName);
                            if (property != null)
                                if (reader[columnName] != DBNull.Value)
                                    if (Nullable.GetUnderlyingType(property.PropertyType) == null)
                                        property.SetValue(item,
                                            Convert.ChangeType(reader[columnName], property.PropertyType));
                                    else
                                    {
                                        property.SetValue(item,
                                        Convert.ChangeType(reader[columnName], Nullable.GetUnderlyingType(property.PropertyType)));
                                    }

                        }
                        list.Add(item);
                    }

                }

                reader.Close();
                reader.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);
                cmd.Dispose();
            }
            return list;
        }

        public virtual int ExecuteNonQuery(string storedProcedureName)
        {
            var cmd = BuildCommand(storedProcedureName, null);
            return ExecuteNonQueryCommand(cmd);
        }

        public virtual int ExecuteNonQuery<TK>(string storedProcedureName, params object[] parameters)
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            return ExecuteNonQueryCommand(cmd);
        }
        public virtual int ExecuteNonQuery(string storedProcedureName, params object[] parameters)
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            return ExecuteNonQueryCommand(cmd);
        }

        public virtual T ExecuteScalar<T>(string storedProcedureName)
        {
            var cmd = BuildCommand(storedProcedureName, null);
            return ExecuteScalar<T>(cmd);
        }

        public virtual T ExecuteScalar<T>(string storedProcedureName, params object[] parameters)
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            return ExecuteScalar<T>(cmd);
        }

        public virtual T ExecuteReader<T>(string storedProcedureName) where T : class, new()
        {
            var cmd = BuildCommand(storedProcedureName, null);
            return ExecuteReader<T>(cmd);
        }

        public virtual T ExecuteReader<T>(string storedProcedureName, params object[] parameters)
            where T : class, new()
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            return ExecuteReader<T>(cmd);
        }

        public virtual List<T> ExecuteListReader<T>(string storedProcedureName) where T : class, new()
        {
            var cmd = BuildCommand(storedProcedureName, null);
            return ExecuteListReader<T>(cmd);
        }

        public virtual List<T> ExecuteListReader<T>(string storedProcedureName, params object[] parameters)
            where T : class, new()
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            return ExecuteListReader<T>(cmd);
        }

        public virtual List<T> ExecuteListReader<T>(string storedProcedureName, int commandTimeout = 60, params object[] parameters)
            where T : class, new()
        {
            var cmd = BuildCommand(storedProcedureName, parameters);
            cmd.CommandTimeout = commandTimeout;
            return ExecuteListReader<T>(cmd);
        }


        public virtual DataTable GetDataTable(string storedProcedureName, int commandTimeout = 300, params object[] parameters)
        {
            var cmd = BuildCommand(storedProcedureName, parameters);

            var dataTable = new DataTable();
            try
            {
                OpenConnection();
                cmd.CommandTimeout = commandTimeout;
                cmd.ExecuteNonQuery();
                var reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
                reader.Dispose();

            }
            catch
            {
                throw;
            }
            finally
            {
                CloseConnection();
                SetOutputValues(cmd);
                cmd.Dispose();
            }
            return dataTable;
        }

        private class SqlParameterInfo
        {
            public string ParameterName { get; set; }
            public bool IsOutput { get; set; }
            public string ParameterType { get; set; }
            public int MaxSize { get; set; }
        }
    }
}
