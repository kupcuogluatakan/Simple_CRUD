using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DamagedItemDispose;

namespace ODMSData
{
    public class DamagedItemDisposeData : DataAccessBase
    {

        public List<DamagedItemDisposeListModel> ListDamagedItemDisposes(UserInfo user,DamagedItemDisposeListModel filter, out int totalCount)
        {
            var retVal = new List<DamagedItemDisposeListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DAMAGED_ITEM_DISPOSES");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, MakeDbNull(filter.IsOriginal));
                db.AddInParameter(cmd, "START_DATE", DbType.Date, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.Date, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(filter.WarehouseId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var damagedItemDisposeListModel = new DamagedItemDisposeListModel
                        {
                            DamageDisposeId = reader["DAMAGE_DISPOSE_ID"].GetValue<int>(),
                            DealerId = reader["DEALER_ID"].GetValue<int?>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            Description = reader["DESCRIPTION"].GetValue<string>(),
                            DocId = reader["DOC_ID"].GetValue<int?>(),
                            DocName = reader["DOC_NAME"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            IsOriginal = reader["IS_ORIGINAL"].GetValue<bool?>(),
                            IsOriginalName = reader["IS_ORIGINAL"].GetValue<bool>()
                                                     ? MessageResource.Global_Display_Yes
                                                     : MessageResource.Global_Display_No,
                            PartId = reader["PART_ID"].GetValue<int?>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            RackId = reader["RACK_ID"].GetValue<int?>(),
                            RackName = reader["RACK_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int?>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            WarehouseId = reader["WAREHOUSE_ID"].GetValue<int?>(),
                            WarehouseName = reader["WAREHOUSE_NAME"].GetValue<string>(),
                        };
                        retVal.Add(damagedItemDisposeListModel);
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
        public void DMLDamagedItemDispose(UserInfo user, DamagedItemDisposeViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DAMAGED_ITEM_DISPOSE");
                db.AddParameter(cmd, "DAMAGE_DISPOSE_ID", DbType.Int32, ParameterDirection.InputOutput, "DAMAGE_DISPOSE_ID", DataRowVersion.Default, model.DamageDisposeId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "WAREHOUSE_ID", DbType.Int32, MakeDbNull(model.WarehouseId));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(model.RackId));
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "DOC_ID", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(model.Quantity));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DamageDisposeId =
                    db.GetParameterValue(cmd, "DAMAGE_DISPOSE_ID").GetValue<int>();
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
        public DamagedItemDisposeViewModel GetDamagedItemDispose(UserInfo user, DamagedItemDisposeViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DAMAGED_ITEM_DISPOSE");
                db.AddInParameter(cmd, "DAMAGE_DISPOSE_ID", DbType.String, MakeDbNull(filter.DamageDisposeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.DamageDisposeId = dReader["DAMAGE_DISPOSE_ID"].GetValue<int>();
                    filter.DealerId = dReader["DEALER_ID"].GetValue<int?>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.Description = dReader["DESCRIPTION"].GetValue<string>();
                    filter.DocId = dReader["DOC_ID"].GetValue<int?>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                    filter.IsOriginal = dReader["IS_ORIGINAL"].GetValue<bool?>();
                    filter.IsOriginalName = filter.IsOriginal.GetValueOrDefault()
                                                                 ? MessageResource.Global_Display_Yes
                                                                 : MessageResource.Global_Display_No;
                    filter.PartId = dReader["PART_ID"].GetValue<int?>();
                    filter.PartName = dReader["PART_NAME"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    filter.RackId = dReader["RACK_ID"].GetValue<int?>();
                    filter.RackName = dReader["RACK_NAME"].GetValue<string>();
                    filter.StockTypeId = dReader["STOCK_TYPE_ID"].GetValue<int?>();
                    filter.StockTypeName = dReader["STOCK_TYPE_NAME"].GetValue<string>();
                    filter.WarehouseId = dReader["WAREHOUSE_ID"].GetValue<int?>();
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
    }
}
