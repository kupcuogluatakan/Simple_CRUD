using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.DealerRegionResponsible;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerRegionResponsibleData : DataAccessBase
    {
        public DealerRegionResponsibleIndexModel GetDealerRegionResponsibleIndexModel()
        {
            return new DealerRegionResponsibleIndexModel { DealerRegionList = GetDealerRegionList() };
        }

        //TODO : Id set edilmeli
        public void DMLDealerRegionResponsible(UserInfo user, DealerRegionResponsibleDetailModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_DEALER_REGION_RESPONSIBLE");
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, model.DealerRegionId);
                db.AddInParameter(cmd, "ID_USER", DbType.Int32, model.UserId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.DealerRegion_Error_NullRegionId;
                else if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
        }

        public DealerRegionResponsibleDetailModel GetDealerRegionResponsible(DealerRegionResponsibleDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_REGION_RESPONSIBLE");
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, filter.DealerRegionId);
                db.AddInParameter(cmd, "ID_USER", DbType.Int32, filter.UserId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.DealerRegionId = reader["ID_DEALER_REGION"].GetValue<int>();
                    filter.UserId = reader["ID_DMS_USER"].GetValue<int>();
                    filter.DealerRegionName = reader["DEALER_REGION_NAME"].ToString();
                    filter.Name = reader["USER_NAME"].ToString();
                    filter.Surname = reader["USER_LAST_NAME"].ToString();
                    filter.Phone = reader["PHONE"].ToString();
                    filter.Email = reader["EMAIL"].ToString();
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return filter;
        }

        public List<DealerRegionResponsibleListModel> ListDealerRegionResponsibles(DealerRegionResponsibleListModel filter, out int totalCount)
        {
            var result = new List<DealerRegionResponsibleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_REGION_RESPONSIBLE");
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int64, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "USER_NAME", DbType.String, MakeDbNull(filter.Name));
                db.AddInParameter(cmd, "USER_SURNAME", DbType.String, MakeDbNull(filter.Surname));
                db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(filter.Phone));
                db.AddInParameter(cmd, "EMAIL", DbType.String, MakeDbNull(filter.Email));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.SearchIsActive));
                AddPagingParameters(cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new DealerRegionResponsibleListModel
                        {
                            DealerRegionId = reader["ID_DEALER_REGION"].GetValue<int>(),
                            UserId = reader["ID_DMS_USER"].GetValue<int>(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].ToString(),
                            Name = reader["USER_NAME"].ToString(),
                            Surname = reader["USER_LAST_NAME"].ToString(),
                            Phone = reader["PHONE"].ToString(),
                            Email = reader["EMAIL"].ToString(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public List<SelectListItem> GetDealerRegionList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_REGION_SHORT");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DEALER_REGION"].ToString(),
                            Text = reader["DEALER_REGION_NAME"].ToString(),
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }

        public List<SelectListItem> GetUserList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_USERS_SHORT_NAMESURNAME");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_TECHNICIAN", DbType.Int32, false);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, true);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DMS_USER"].ToString(),
                            Text = reader["WORKER_NAME"].ToString(),
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return result;
        }
    }
}
