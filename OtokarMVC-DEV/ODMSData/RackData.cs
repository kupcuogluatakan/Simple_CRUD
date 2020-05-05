using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.Rack;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class RackData : DataAccessBase
    {
        public RackIndexModel GetRackIndexModel(int dealerId)
        {
            var dealers = new List<SelectListItem>();
            if (dealerId > 0)
                dealers.Add(GetDealer(dealerId));
            else
                dealers.AddRange(GetDealers());

            return new RackIndexModel
            {
                DealerId = dealerId,
                DealerList = dealers,
                WarehouseList = new List<SelectListItem>()
            };
        }

        public List<SelectListItem> ListWarehousesOfDealer(int dealerId)
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

        public List<RackListModel> ListRacks(RackListModel filter, out int totalCount)
        {
            var result = new List<RackListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_RACKS");
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(filter.WarehouseId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "RACK_CODE", DbType.String, MakeDbNull(filter.Code));
                db.AddInParameter(cmd, "RACK_NAME", DbType.String, MakeDbNull(filter.Name));
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
                        var listModel = new RackListModel
                            {
                                Id = reader["ID_RACK"].GetValue<int>(),
                                WarehouseId = reader["ID_WAREHOUSE"].GetValue<int>(),
                                WarehouseCode = reader["WAREHOUSE_CODE"].GetValue<string>(),
                                WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                                Code = reader["RACK_CODE"].GetValue<string>(),
                                Name = reader["RACK_NAME"].GetValue<string>(),
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

        public void DMLRack(UserInfo user,RackDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_RACK");
                db.AddParameter(cmd, "ID_RACK", DbType.Int32, ParameterDirection.InputOutput, "ID_RACK", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_WAREHOUSE", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "RACK_CODE", DbType.String, MakeDbNull(model.Code));
                db.AddInParameter(cmd, "RACK_NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "ID_RACK").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Rack_Error_NullId;
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

        public RackDetailModel GetRack(RackDetailModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_RACK");
                db.AddInParameter(cmd, "ID_RACK", DbType.Int32, MakeDbNull(filter.Id));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.WarehouseId = reader["ID_WAREHOUSE"].GetValue<int>();
                    filter.WarehouseCode = reader["WAREHOUSE_CODE"].GetValue<string>();
                    filter.WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>();
                    filter.Code = reader["RACK_CODE"].GetValue<string>();
                    filter.Name = reader["RACK_NAME"].GetValue<string>();
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

        #region Helper Methods
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
        #endregion


        
    }
}
