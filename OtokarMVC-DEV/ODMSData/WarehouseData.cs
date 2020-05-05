using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.Warehouse;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WarehouseData : DataAccessBase
    {
        public List<SelectListItem> ListWarehousesOfDealerAsSelectList(int? dealerId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WAREHOUSES_OF_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, dealerId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_WAREHOUSE"].GetValue<string>(),
                            Text = reader["WAREHOUSE_NAME"].GetValue<string>()
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

        public List<WarehouseListModel> ListWarehouses(UserInfo user, WarehouseListModel filter, out int totalCnt)
        {
            var result = new List<WarehouseListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WAREHOUSES");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "WAREHOUSE_CODE", DbType.String, MakeDbNull(filter.Code));
                db.AddInParameter(cmd, "STORAGE_TYPE", DbType.Int32, MakeDbNull(filter.StorageType));
                db.AddInParameter(cmd, "WAREHOUSE_NAME", DbType.String, MakeDbNull(filter.Name));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(filter.SearchIsActive));
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
                        var listModel = new WarehouseListModel
                        {
                            Id = reader["ID_WAREHOUSE"].GetValue<int>(),
                            Code = reader["WAREHOUSE_CODE"].GetValue<string>(),
                            Name = reader["WAREHOUSE_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            StorageTypeName = reader["STORAGE_TYPE_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public WarehouseDetailModel GetWarehouse(UserInfo user, WarehouseDetailModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WAREHOUSE");
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(filter.Id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.Code = reader["WAREHOUSE_CODE"].GetValue<string>();
                    filter.Name = reader["WAREHOUSE_NAME"].GetValue<string>();
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                    filter.DealerId = reader["ID_DEALER"].GetValue<int>();
                    filter.DealerName = reader["DEALER_NAME"].GetValue<string>();
                    filter.StorageTypeName = reader["STORAGE_TYPE_NAME"].GetValue<string>();
                    filter.StorageType = reader["STORAGE_TYPE"].GetValue<int>();
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

        public void DMLWarehouse(UserInfo user, WarehouseDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WAREHOUSE");
                db.AddParameter(cmd, "ID_WAREHOUSE", DbType.Int32, ParameterDirection.InputOutput, "ID_WAREHOUSE", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, model.DealerId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "WAREHOUSE_CODE", DbType.String, MakeDbNull(model.Code));
                db.AddInParameter(cmd, "WAREHOUSE_NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "ID_WAREHOUSE").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Warehouse_Error_NullId;
                else if (model.ErrorNo == 1)
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

        public WarehouseIndexModel GetWarehouseIndexModel(int dealerId)
        {
            var dealers = new List<SelectListItem>();
            if (dealerId > 0)
                dealers.Add(GetDealer(dealerId));
            else
                dealers.AddRange(GetDealers());

            return new WarehouseIndexModel
            {
                DealerId = dealerId,
                DealerList = dealers
            };
        }

        private IEnumerable<SelectListItem> GetDealers()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
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

        private SelectListItem GetDealer(int dealerId)
        {
            System.Data.Common.DbDataReader reader = null;
            var result = new SelectListItem();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_SHORT");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    result.Value = reader["ID_DEALER"].GetValue<string>();
                    result.Text = reader["DEALER_NAME"].GetValue<string>();
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
            return result;
        }
    }
}
