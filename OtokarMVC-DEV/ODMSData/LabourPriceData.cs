using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.LabourPrice;
using System.Web.Mvc;
using System.Linq;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LabourPriceData : DataAccessBase
    {

        public List<LabourPriceListModel> ListLabourPrices(UserInfo user, LabourPriceListModel filter, out int total)
        {
            var retVal = new List<LabourPriceListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_PRICE");
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(filter.ModelKod));
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(filter.CurrencyCode));
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(filter.DealerClass));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "ID_VEHICLE_GROUP", DbType.Int32, MakeDbNull(filter.VehicleGroupId));
                db.AddInParameter(cmd, "ID_LABOUR_PRICE_TYPE", DbType.Int32, MakeDbNull(filter.LabourPriceTypeId));
                db.AddInParameter(cmd, "VALID_FROM_DATE", DbType.DateTime, MakeDbNull(filter.ValidFromDate));
                db.AddInParameter(cmd, "VALID_END_DATE", DbType.DateTime, MakeDbNull(filter.ValidEndDate));
                db.AddInParameter(cmd, "VALID_DATE", DbType.DateTime, MakeDbNull(filter.ValidDate));
                db.AddInParameter(cmd, "TS_CERT_CHCK", DbType.Boolean, filter.SearchHasTsPaper);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.SearchIsActive);
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
                        var priceListModel = new LabourPriceListModel
                        {
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            DealerClass = reader["DEALER_CLASS_NAME"].GetValue<string>(),
                            DealerRegionId = reader["DEALER_REGION_ID"].GetValue<int>(),
                            DealerRegionName = reader["DEALER_REGION_NAME"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            LabourPriceTypeId = reader["ID_LABOUR_PRICE_TYPE"].GetValue<int>(),
                            LabourPriceType = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            ModelName = reader["MODEL_NAME"].GetValue<string>(),
                            ModelKod = reader["MODEL_KOD"].GetValue<string>(),
                            LabourPriceId = reader["ID_LABOUR_PRICE"].GetValue<string>(),
                            ValidEndDate = reader["VALID_END_DATE"].GetValue<DateTime>(),
                            ValidFromDate = reader["VALID_START_DATE"].GetValue<DateTime>(),
                            VehicleGroupName = reader["VHCL_GRP_NAME"].GetValue<string>(),
                            HasTSUnitPrice = reader["TS_UNIT_PRICE"].GetValue<decimal>(),
                            HasNoTSUnitPrice = reader["NOTS_UNIT_PRICE"].GetValue<decimal>()
                        };

                        retVal.Add(priceListModel);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLLabourPrice(UserInfo user, LabourPriceViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_PRICE");
                db.AddInParameter(cmd, "ID_LABOUR_PRICE", DbType.Int32, MakeDbNull(model.LabourPriceId));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(model.DealerRegionId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(model.ModelCode));
                db.AddInParameter(cmd, "VALID_START_DATE", DbType.DateTime, MakeDbNull(model.ValidFromDate));
                db.AddInParameter(cmd, "VALID_END_DATE", DbType.DateTime, MakeDbNull(model.ValidEndDate));
                db.AddInParameter(cmd, "ID_LABOUR_PRICE_TYPE", DbType.Int32, MakeDbNull(model.LabourPriceTypeId));
                db.AddInParameter(cmd, "TS_CERT_CHCK", DbType.Boolean, model.HasTsPaper);
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(model.CurrencyCode));
                db.AddInParameter(cmd, "UNIT_PRICE", DbType.Decimal, MakeDbNull(model.UnitPrice));
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(model.DealerClass));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
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

        public LabourPriceViewModel GetLabourPrice(UserInfo user, int labourPriceId)
        {
            var labourPrice = new LabourPriceViewModel { LabourPriceId = labourPriceId };
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_PRICE");
                db.AddInParameter(cmd, "ID_LABOUR_PRICE", DbType.Int32, MakeDbNull(labourPriceId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    labourPrice.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    labourPrice.CurrencyName = dReader["CURRENCY_NAME"].GetValue<string>();
                    labourPrice.DealerClass = dReader["DEALER_CLASS_CODE"].GetValue<string>();
                    labourPrice.DealerClassName = dReader["DEALER_CLASS_NAME"].GetValue<string>();
                    labourPrice.DealerRegionId = dReader["ID_DEALER_REGION"].GetValue<int>();
                    labourPrice.DealerRegionName = dReader["DEALER_REGION_NAME"].GetValue<string>();
                    labourPrice.HasTsPaper = dReader["TS_CERT_CHCK"].GetValue<bool>();
                    labourPrice.HasTsPaperString = dReader["TS_CERT_CHCK_STRING"].GetValue<string>();
                    labourPrice.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    labourPrice.IsActiveString = dReader["IS_ACTIVE_STRING"].GetValue<string>();
                    labourPrice.LabourPriceTypeId = dReader["ID_LABOUR_PRICE_TYPE"].GetValue<int>();//*
                    labourPrice.LabourPriceType = dReader["LABOUR_PRICE_TYPE_DESC"].GetValue<string>();//*
                    labourPrice.ModelCode = dReader["MODEL_KOD"].GetValue<string>();
                    labourPrice.ModelName = dReader["MODEL_NAME"].GetValue<string>();
                    labourPrice.UnitPrice = dReader["UNIT_PRICE"].GetValue<decimal>();
                    labourPrice.ValidEndDate = dReader["VALID_END_DATE"].GetValue<DateTime>();
                    labourPrice.ValidFromDate = dReader["VALID_START_DATE"].GetValue<DateTime>();
                    labourPrice.VehicleGroupId = dReader["ID_VEHICLE_GROUP"].GetValue<int>();
                    labourPrice.VehicleGroup = dReader["VHCL_GRP_NAME"].GetValue<string>();
                    labourPrice.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    labourPrice.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (labourPrice.ErrorNo > 0)
                    {
                        labourPrice.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        labourPrice.ErrorMessage = ResolveDatabaseErrorXml(labourPrice.ErrorMessage);
                    }
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
            return labourPrice;
        }

        public List<SelectListItem> ListLabourPriceTypesAsSelectListItems(UserInfo user)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_PRICE_TYPES_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                        {
                            Value = reader["ID_LABOUR_PRICE_TYPE"].GetValue<string>(),
                            Text = reader["LABOUR_PRICE_TYPE_DESC"].GetValue<string>()
                        };
                        retVal.Add(listItem);
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

            return retVal;
        }
    }
}
