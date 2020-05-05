using System.Collections.Generic;
using System.Data;
using ODMSModel.Permission;
using ODMSModel.Shared;
using ODMSModel.ViewModel;
using ODMSCommon;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PermissionData : DataAccessBase
    {
        public List<PermissionListModel> ListPermissions(UserInfo user, PermissionListModel filter, out int totalCount)
        {
            var retVal = new List<PermissionListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PERMISSIONS");
                db.AddInParameter(cmd, "PERMISSION_CODE", DbType.String, MakeDbNull(filter.PermissionCode));
                db.AddInParameter(cmd, "PERMISSION_NAME", DbType.String, MakeDbNull(filter.PermissionName));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var permissionListModel = new PermissionListModel
                            {
                                PermissionId = reader["PERMISSION_ID"].GetValue<int>(),
                                AdminDesc = reader["PERMISSION_ADMIN_DESC"].GetValue<string>(),
                                PermissionCode = reader["PERMISSION_CODE"].GetValue<string>(),
                                PermissionName = reader["PERMISSION_NAME"].GetValue<string>(),
                                Status = reader["STATUS"].GetValue<string>()
                            };
                        retVal.Add(permissionListModel);
                    }
                    reader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

                CloseConnection();
            }

            return retVal;
        }
        
        public PermissionIndexViewModel GetPermission(UserInfo user, PermissionIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PERMISSION");
                db.AddInParameter(cmd, "PERMISSION_ID", DbType.Int32, MakeDbNull(filter.PermissionId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.AdminDesc = dReader["PERMISSION_ADMIN_DESC"].GetValue<string>();
                    filter.PermissionCode = dReader["PERMISSION_CODE"].GetValue<string>();
                    filter.StatusId = (CommonValues.Status) dReader["STATUS_ID"].GetValue<int>();
                    filter.Status = dReader["STATUS"].GetValue<string>();
                    filter.IsOtokarScreen = dReader["IS_OTOKAR_SCREEN"].GetValue<bool>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table,"PERMISSION_NAME");
                    filter.PermissionName = (MultiLanguageModel)CommonUtility.DeepClone(filter.PermissionName);
                    filter.PermissionName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }


            return filter;
        }

        public List<PermissionInfo> GetUserPermissions(UserInfo user,int userId)
        {
            System.Data.Common.DbDataReader dReader = null;
            var listOfPermissions = new List<PermissionInfo>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_USER_PERMISSIONS");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(userId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    var permInfo = new PermissionInfo
                        {
                            PermissionCode = dReader["PERMISSION_CODE"].GetValue<string>(),
                            PermissionName = dReader["PERMISSION_NAME"].GetValue<string>(),
                            IsOtokarScreen = dReader["IS_OTOKAR_SCREEN"].GetValue<int>()
                        };

                    listOfPermissions.Add(permInfo);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dReader != null && !dReader.IsClosed)
                {
                    dReader.Close();
                    dReader.Dispose();
                }
                CloseConnection();
            }


            return listOfPermissions;
        }
    }
}
