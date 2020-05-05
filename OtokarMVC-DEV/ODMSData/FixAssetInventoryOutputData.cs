using ODMSCommon.Security;
using ODMSModel.FixAssetInventoryOutput;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class FixAssetInventoryOutputData : DataAccessBase
    {
        public List<FixAssetInventoryOutputListModel> ListFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputListModel filter, out int totalCount)
        {
            var retVal = new List<FixAssetInventoryOutputListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FIX_ASSET_INVENTORY_OUTPUT");
                db.AddInParameter(cmd, "FIX_ASSET_NAME", DbType.String, MakeDbNull(filter.FixAssetName));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.StockType));
                db.AddInParameter(cmd, "FIX_ASSET_STATUS", DbType.Int32, MakeDbNull(filter.FixAssetStatus));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));

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
                        var fixAssetInventoryOutputListModel = new FixAssetInventoryOutputListModel
                        {
                            FixAssetName = reader["FIX_ASSET_NAME"].GetValue<string>(),
                            SerialNo = reader["SERIAL_NO"].GetValue<string>(),
                            ExitDesc = reader["EXIT_DESC"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime?>(),
                            IdFixAssetInventory = reader["FIX_ASSET_INVENTORY_ID"].GetValue<int>(),
                            StockTypeName = reader["STOCK_TYPE"].GetValue<string>(),
                            ExitDate = reader["EXIT_DATE"].GetValue<DateTime?>()
                        };

                        retVal.Add(fixAssetInventoryOutputListModel);
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

        public FixAssetInventoryOutputViewModel GetFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FIX_ASSET_INVENTORY_OUTPUT");
                db.AddInParameter(cmd, "FIX_ASSET_INVENTORY_ID", DbType.Int32, MakeDbNull(filter.IdFixAssetInventory));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.FixAssetCode = dReader["FIX_ASSET_CODE"].GetValue<string>();
                    filter.Description = dReader["DESCRIPTION"].GetValue<string>();
                    filter.IdFixAssetInventory = dReader["FIX_ASSET_INVENTORY_ID"].GetValue<int>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.FixAssetName = dReader["FIX_ASSET_NAME"].GetValue<string>();
                    filter.IdPart = dReader["ID_PART"].GetValue<Int64>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.IdRack = dReader["ID_RACK"].GetValue<int>();
                    filter.IdWarehouse = dReader["ID_WAREHOUSE"].GetValue<int>();
                    filter.RestockReason = dReader["RESTOCK_REASON"].GetValue<string>();
                    filter.SerialNo = dReader["SERIAL_NO"].GetValue<string>();
                    filter.FixAssetStatus = dReader["FIX_ASSET_STATUS_LOOKVAL"].GetValue<int>();
                    filter.FixAssetStatusName = dReader["STATUS_NAME"].GetValue<string>();

                    if (dReader["ID_STOCK_TYPE"].GetValue<int>() != 0)
                        filter.StockType = dReader["ID_STOCK_TYPE"].GetValue<int>();

                    filter.StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>();
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
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

        //TODO : Id set edilmeli
        public void DMLFixAssetInventoryOutput(UserInfo user, FixAssetInventoryOutputViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_FIX_ASSET_INVENTORY_OUTPUT");
                db.AddInParameter(cmd, "FIX_ASSET_INVENTORY_ID", DbType.Int32, model.IdFixAssetInventory);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, model.StockType);
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, model.IdRack);
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, model.FixAssetStatus);
                db.AddInParameter(cmd, "RESTOCK_REASON", DbType.String, model.RestockReason);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                //db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.IdFixAssetInventory = Int32.Parse(db.GetParameterValue(cmd, "FIX_ASSET_INVENTORY_ID").ToString());

                if (db.GetParameterValue(cmd, "STOCK_TYPE_ID").ToString() != "")
                    model.StockType = Int32.Parse(db.GetParameterValue(cmd, "STOCK_TYPE_ID").ToString());

                if (db.GetParameterValue(cmd, "RACK_ID").ToString() != "")
                    model.IdRack = Int32.Parse(db.GetParameterValue(cmd, "RACK_ID").ToString());
                model.FixAssetStatus = Int32.Parse(db.GetParameterValue(cmd, "STATUS_ID").ToString());
                model.RestockReason = db.GetParameterValue(cmd, "RESTOCK_REASON").ToString();
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
