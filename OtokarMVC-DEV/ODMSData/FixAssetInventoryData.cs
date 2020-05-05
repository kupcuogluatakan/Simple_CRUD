using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.FixAssetInventory;
using System;

namespace ODMSData
{
    public class FixAssetInventoryData : DataAccessBase
    {
        public List<FixAssetInventoryListModel> ListFixAssetInventory(UserInfo user, FixAssetInventoryListModel filter, out int totalCount)
        {
            
            var retVal = new List<FixAssetInventoryListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FIX_ASSET_INVENTORY");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(filter.EquipmentTypeId));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(filter.StatusId));
                db.AddInParameter(cmd, "FIX_ASSET_NAME", DbType.String, MakeDbNull(filter.Name));
                db.AddInParameter(cmd, "SERIAL_NO", DbType.String, MakeDbNull(filter.SerialNo));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                //db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                //db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                //db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                //db.AddInParameter(cmd, "OFFSET", DbType.Int32, MakeDbNull(filter.Offset));
                //db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                //db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var fixAssetInventoryListModel = new FixAssetInventoryListModel
                        {
                            Code = reader["FIX_ASSET_CODE"].GetValue<string>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            EquipmentTypeId = reader["EQUIPMENT_TYPE_ID"].GetValue<int>(),
                            EquipmentTypeName = reader["EQUIPMENT_TYPE_NAME"].GetValue<string>(),
                            FixAssetInventoryId = reader["FIX_ASSET_INVENTORY_ID"].GetValue<int>(),
                            Name = reader["FIX_ASSET_NAME"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            RackId = reader["RACK_ID"].GetValue<int>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            RackWarehouse = reader["RACK_WAREHOUSE"].GetValue<string>(),
                            RestockReason = reader["RESTOCK_REASON"].GetValue<string>(),
                            SerialNo = reader["SERIAL_NO"].GetValue<string>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            Unit = reader["UNIT"].GetValue<string>(),
                            VehicleGroupId = reader["VEHICLE_GROUP_ID"].GetValue<int>(),
                            VehicleGroupName = reader["VEHICLE_GROUP_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),

                        };
                        retVal.Add(fixAssetInventoryListModel);
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

        public FixAssetInventoryViewModel GetFixAssetInventory(UserInfo user, FixAssetInventoryViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FIX_ASSET_INVENTORY");
                db.AddInParameter(cmd, "FIX_ASSET_INVENTORY_ID", DbType.Int32, MakeDbNull(filter.FixAssetInventoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.Code = dReader["FIX_ASSET_CODE"].GetValue<string>();
                    filter.Description = dReader["DESCRIPTION"].GetValue<string>();
                    filter.EquipmentTypeId = dReader["EQUIPMENT_TYPE_ID"].GetValue<int>();
                    filter.EquipmentTypeName = dReader["EQUIPMENT_TYPE_NAME"].GetValue<string>();
                    filter.FixAssetInventoryId = dReader["FIX_ASSET_INVENTORY_ID"].GetValue<int>();
                    filter.Name = dReader["FIX_ASSET_NAME"].GetValue<string>();
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.RackId = dReader["RACK_ID"].GetValue<int>();
                    filter.RackName = dReader["RACK_NAME"].GetValue<string>();
                    filter.RackWarehouse = dReader["RACK_WAREHOUSE"].GetValue<string>();
                    filter.RestockReason = dReader["RESTOCK_REASON"].GetValue<string>();
                    filter.SerialNo = dReader["SERIAL_NO"].GetValue<string>();
                    filter.StatusId = dReader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = dReader["STATUS_NAME"].GetValue<string>();
                    filter.StockTypeId = dReader["STOCK_TYPE_ID"].GetValue<int>();
                    filter.StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>();
                    filter.Unit = dReader["UNIT"].GetValue<string>();
                    filter.VehicleGroupId = dReader["VEHICLE_GROUP_ID"].GetValue<int>();
                    filter.VehicleGroupName = dReader["VEHICLE_GROUP_NAME"].GetValue<string>();
                    filter.WarehouseId = dReader["WAREHOUSE_ID"].GetValue<int>();
                    filter.WarehouseName = dReader["WAREHOUSE_NAME"].GetValue<string>();
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

        public List<SelectListItem> ListEquipmentTypeAsSelectList(UserInfo user)
        {
            var listItems = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_EQUIPMENT_TYPE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = dr["EQUIPMENT_TYPE_ID"].GetValue<string>(),
                            Text = dr["EQUIPMENT_NAME"].GetValue<string>()
                        };

                        listItems.Add(item);
                    }
                    dr.Close();
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

            return listItems;
        }

        public void DMLFixAssetInventory(UserInfo user, FixAssetInventoryViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_FIX_ASSET_INVENTORY");
                db.AddParameter(cmd, "FIX_ASSET_INVENTORY_ID", DbType.Int32, ParameterDirection.InputOutput,
                    "FIX_ASSET_INVENTORY_ID", DataRowVersion.Default, model.FixAssetInventoryId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "EQUIPMENT_TYPE_ID", DbType.Int32, MakeDbNull(model.EquipmentTypeId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "FIX_ASSET_CODE", DbType.String, MakeDbNull(model.Code));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "FIX_ASSET_NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "SERIAL_NO", DbType.String, MakeDbNull(model.SerialNo));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(model.VehicleGroupId));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(model.StatusId));
                db.AddInParameter(cmd, "UNIT", DbType.String, MakeDbNull(model.Unit));
                db.AddInParameter(cmd, "RESTOCK_REASON", DbType.String, MakeDbNull(model.RestockReason));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.FixAssetInventoryId = db.GetParameterValue(cmd, "FIX_ASSET_INVENTORY_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 1)
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
    }
}
