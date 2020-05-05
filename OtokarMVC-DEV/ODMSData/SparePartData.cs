using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.SparePart;
using ODMSModel.StockCardPriceListModel;
using ODMSModel.ViewModel;
using ODMSModel.ServiceCallSchedule;
using System.Data.SqlClient;
using ODMSCommon.Logging;
using ODMSData.Utility;
using ODMSModel;
using System.Linq;

namespace ODMSData
{
    public class SparePartData : DataAccessBase
    {
        private readonly DbHelper _dbHelper;

        public SparePartData()
        {
            _dbHelper = new DbHelper();
        }

        public int GetFreeQuantity(int partId, int dealerId, int stockTypeId)
        {
            int result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd =
                    db.GetStoredProcCommand("P_GET_FREE_STOCK_COUNT");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(stockTypeId));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<int>();
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
            return result;
        }
        public decimal GetCustomerPartDiscount(int? partId, int? dealerId, int? customerId, string actionType)
        {
            decimal result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd =
                   db.GetStoredProcCommand("P_GET_CUSTOMER_PART_DISCOUNT");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "ACTION_TYPE", DbType.String, MakeDbNull(actionType));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(customerId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<decimal>();
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
            return result;
        }
        public decimal GetDiscountPrice(int partId, int dealerId, int stockTypeId)
        {
            decimal result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd =
                   db.GetStoredProcCommand("P_GET_DEALER_PART_STOCK_TYPE_PRICE");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(stockTypeId));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<decimal>();
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
            return result;
        }
        public List<SelectListItem> GetSparePartClassCodeListForComboBox(UserInfo user)
        {
            var list = new List<SelectListItem>();
            int errorNo = 0;
            string errorMessage = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_CLASSES");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var country = new SelectListItem
                        {
                            Value = reader["SPARE_PART_CLASS_CODE"].GetValue<string>(),
                            Text = reader["SPARE_PART_CLASS_CODE"].GetValue<string>() + " / " + reader["ADMIN_DESC"].GetValue<string>()
                        };
                        list.Add(country);
                    }
                    reader.Close();
                }
                errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                errorMessage = errorNo > 0 ? db.GetParameterValue(cmd, "ERROR_DESC").ToString() : string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();

                if (errorNo > 0)
                {
                    //logging error message
                    var logger = new Loggable();
                    logger.ErrorAsync(ResolveDatabaseErrorXml(errorMessage));
                }
            }
            return list;
        }

        public List<SparePartSupplyDiscountRatioListModel> ListSparePartsSupplyDiscountRatios(UserInfo user, SparePartSupplyDiscountRatioListModel filter, out int total)
        {
            var retVal = new List<SparePartSupplyDiscountRatioListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("PLIST_SPARE_PART_SUPPLY_DISCOUNT_RATIO");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                AddPagingParameters(cmd, filter);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SparePartSupplyDiscountRatioListModel
                        {
                            PartId = filter.PartId,
                            ChannelName = reader["CHANNEL_NAME"].GetValue<string>(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>()
                        };
                        retVal.Add(item);
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

        public List<SelectListItem> ListStockCardsAsSelectListItem(UserInfo user, bool? isOriginal)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARDS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "PART_ID", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, MakeDbNull(isOriginal));
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, MakeDbNull(0));

                AddPagingParametersWithLanguage(user, cmd, new BaseListWithPagingModel());

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["STOCK_CARD_ID"].GetValue<string>(),
                            Text =
                                    reader["STOCK_CARD_ID"].GetValue<string>() + CommonValues.EmptySpace +
                                    reader["PART_CODE"].GetValue<string>() + CommonValues.EmptySpace + reader["PART_NAME"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

        public List<SparePartListModel> ListSparePart(UserInfo user, SparePartListModel filter, out int totalCnt)
        {
            var retVal = new List<SparePartListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS");
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, filter.IsOriginal);
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(null));
                db.AddInParameter(cmd, "ORIGINAL_PART_ID", DbType.Int32, MakeDbNull(filter.OriginalPartId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "PART_TYPE_CODE", DbType.String, filter.PartTypeCode);
                db.AddInParameter(cmd, "PART_CODE", DbType.String, filter.PartCode);
                db.AddInParameter(cmd, "UNIT", DbType.String, filter.Unit);
                db.AddInParameter(cmd, "BRAND", DbType.String, filter.Brand);
                db.AddInParameter(cmd, "GUARANTEE_AUTHORITY_NEED", DbType.Boolean, filter.GuaranteeAuthorityNeed);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, filter.PartId);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sparePartListModel = new SparePartListModel
                        {
                            PartId = reader["PART_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            OriginalPartName = reader["ORIGINAL_PART_NAME"].GetValue<string>(),
                            PartSection = reader["PART_SECTION"].GetValue<string>(),
                            PartTypeCode = reader["PART_TYPE_CODE"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            IsOriginal = reader["ORIGINAL_PART_ID"] == DBNull.Value ? 1 : 0,
                            IsOriginalName = reader["ORIGINAL_PART_ID"] == DBNull.Value
                                                     ? MessageResource.SparePart_Display_Original
                                                     : MessageResource.SparePart_Display_NotOriginal,
                            GuaranteeAuthorityNeed = reader["GUARANTEE_AUTHORITY_NEED"].GetValue<bool>(),
                            GuaranteeAuthorityNeedName = reader["GUARANTEE_AUTHORITY_NEED"].GetValue<bool>()
                                                                 ? MessageResource.Global_Display_Yes
                                                                 : MessageResource.Global_Display_No,
                            ShipQuantity = reader["SHIP_QUANT"].GetValue<decimal>()
                        };
                        retVal.Add(sparePartListModel);
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

            return retVal;
        }

        public SparePartIndexViewModel GetSparePart(UserInfo user, SparePartIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.PartId = dReader["PART_ID"].GetValue<int>();
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
                    filter.Brand = dReader["BRAND"].GetValue<string>();
                    filter.CompatibleGuaranteeUsage = dReader["COMPATIBLE_GUARANTEE_USAGE"].GetValue<int>();
                    filter.CompatibleGuaranteeUsageName = dReader["COMPATIBLE_GUARANTEE_USAGE_NAME"].GetValue<string>();
                    filter.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.NSN = dReader["NSN"].GetValue<string>();
                    filter.OriginalPartId = dReader["ORIGINAL_PART_ID"].GetValue<int>();
                    filter.OriginalPartName = dReader["ORIGINAL_PART_NAME"].GetValue<string>();
                    filter.IsOriginal = dReader["ORIGINAL_PART_ID"] == DBNull.Value ? 1 : 0;
                    filter.IsOriginalName = dReader["ORIGINAL_PART_ID"] == DBNull.Value ? MessageResource.SparePart_Display_Original : MessageResource.SparePart_Display_NotOriginal;
                    filter.PartSection = dReader["PART_SECTION"].GetValue<string>();
                    filter.PartTypeCode = dReader["PART_TYPE_CODE"].GetValue<string>();
                    filter.PartTypeName = dReader["PART_TYPE_NAME"].GetValue<string>();
                    filter.ShipQuantity = dReader["SHIP_QUANTITY"].GetValue<decimal?>();
                    filter.Unit = dReader["UNIT"].GetValue<int>();
                    filter.UnitName = dReader["UNIT_NAME"].GetValue<string>();
                    filter.Volume = dReader["VOLUME"].GetValue<decimal>();
                    filter.Weight = dReader["WEIGHT"].GetValue<decimal>();
                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].GetValue<string>();
                    filter.ClassCode = dReader["SPARE_PART_CLASS_CODE"].GetValue<string>();
                    filter.DiscountRatio = dReader["DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.GuaranteeAuthorityNeed = dReader["GUARANTEE_AUTHORITY_NEED"].GetValue<bool>();
                    filter.GuaranteeAuthorityNeedName = dReader["GUARANTEE_AUTHORITY_NEED"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No;
                    filter.LeadTime = dReader["LEAD_TIME"].GetValue<int>();
                    filter.IsOrderAllowed = dReader["IS_ORDER_ALLOWED"].GetValue<bool>();
                    filter.Barcode = dReader["BARCODE"].GetValue<string>();
                    filter.PartNameInLanguage = dReader["PART_NAME_IN_LANGUAGE"].GetValue<string>();
                    filter.AlternatePart = dReader["ALTERNATE_PART"].GetValue<string>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
                    filter.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "PART_NAME");
                    filter.PartName = (MultiLanguageModel)CommonUtility.DeepClone(filter.PartName);
                    filter.PartName.MultiLanguageContentAsText = filter.MultiLanguageContentAsText;
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

        private DataTable CreateDataTableFromList(List<SparePartIndexViewModel> list)
        {
            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("PART_CODE");

            col1.DataType = System.Type.GetType("System.String");

            table.Columns.Add(col1);
            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                row[0] = item.PartCode;
                table.Rows.Add(row);
            }

            return table;
        }

        public List<SparePartIndexViewModel> GetSparePartFromTable(List<SparePartIndexViewModel> list)
        {
            var retVal = new ModelBase();
            var dt = CreateDataTableFromList(list);
            return _dbHelper.ExecuteListReader<SparePartIndexViewModel>("P_GET_SPARE_PART_FROM_TABLE", dt);

        }
        public void DMLSparePart(UserInfo user, SparePartIndexViewModel sparePartModel)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_MAIN");
                db.AddParameter(cmd, "PART_ID", DbType.Int32, ParameterDirection.InputOutput, "PART_ID", DataRowVersion.Default, sparePartModel.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, sparePartModel.CommandType);
                db.AddInParameter(cmd, "BRAND", DbType.String, MakeDbNull(sparePartModel.Brand));
                if (user.IsDealer)
                {
                    db.AddInParameter(cmd, "COMPATIBLE_GUARANTEE_USAGE", DbType.String,
                        false);
                }
                else
                {
                    db.AddInParameter(cmd, "COMPATIBLE_GUARANTEE_USAGE", DbType.String,
                       MakeDbNull(sparePartModel.CompatibleGuaranteeUsage));
                }
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(sparePartModel.DealerId));
                db.AddInParameter(cmd, "NSN", DbType.String, MakeDbNull(sparePartModel.NSN));
                db.AddInParameter(cmd, "ORIGINAL_PART_ID", DbType.Int32, MakeDbNull(sparePartModel.OriginalPartId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(sparePartModel.PartCode));
                db.AddInParameter(cmd, "PART_SECTION", DbType.String, MakeDbNull(sparePartModel.PartSection));
                db.AddInParameter(cmd, "LEAD_TIME", DbType.Int32, MakeDbNull(sparePartModel.LeadTime));
                db.AddInParameter(cmd, "PART_TYPE_CODE", DbType.String, MakeDbNull(sparePartModel.PartTypeCode));
                db.AddInParameter(cmd, "SHIP_QUANTITY", DbType.Decimal, MakeDbNull(sparePartModel.ShipQuantity));
                db.AddInParameter(cmd, "UNIT", DbType.Int32, MakeDbNull(sparePartModel.Unit));
                db.AddInParameter(cmd, "VOLUME", DbType.Decimal, MakeDbNull(sparePartModel.Volume));
                db.AddInParameter(cmd, "GUARANTEE_AUTHORITY_NEED", DbType.Boolean, MakeDbNull(sparePartModel.GuaranteeAuthorityNeed));
                db.AddInParameter(cmd, "WEIGHT", DbType.Decimal, MakeDbNull(sparePartModel.Weight));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(sparePartModel.AdminDesc));
                db.AddInParameter(cmd, "IS_ORDER_ALLOWED", DbType.Boolean, sparePartModel.IsOrderAllowed);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, sparePartModel.IsActive);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(sparePartModel.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                sparePartModel.PartId = db.GetParameterValue(cmd, "PART_ID").GetValue<int>();
                sparePartModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                sparePartModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (sparePartModel.ErrorNo > 0)
                    sparePartModel.ErrorMessage = ResolveDatabaseErrorXml(sparePartModel.ErrorMessage);
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

        public List<AutocompleteSearchListModel> ListSparePartAsAutoCompSearch(UserInfo user, string strSearch, string dealerId)
        {
            List<AutocompleteSearchListModel> list_ACSearchModel = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_AUTOSEARCH");
                db.AddInParameter(cmd, "VALUE", DbType.String, strSearch);
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AutocompleteSearchListModel model = new AutocompleteSearchListModel
                        {
                            Column1 = dr["PART_CODE"].GetValue<string>(),
                            Column2 = dr["PART_NAME"].GetValue<string>(),
                            Id = dr["ID_PART"].GetValue<int>()
                        };

                        list_ACSearchModel.Add(model);
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
            return list_ACSearchModel;
        }

        public List<AutocompleteSearchListModel> ListOriginalSparePartAsAutoCompSearch(UserInfo user, string searchString)
        {
            List<AutocompleteSearchListModel> list_ACSearchModel = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_AUTOCOMPLETE_ORIGINAL");
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(searchString));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AutocompleteSearchListModel model = new AutocompleteSearchListModel
                        {
                            Column1 = dr["PART_CODE"].GetValue<string>(),
                            Column2 = dr["PART_NAME"].GetValue<string>(),
                            Id = dr["ID_PART"].GetValue<int>()
                        };

                        list_ACSearchModel.Add(model);
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
            return list_ACSearchModel;
        }
        public List<AutocompleteSearchListModel> LisNotOriginalSparePartAsAutoCompSearch(UserInfo user, string searchString)
        {
            List<AutocompleteSearchListModel> list_ACSearchModel = new List<AutocompleteSearchListModel>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_AUTOCOMPLETE_NON_ORIGINAL");
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(searchString));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        AutocompleteSearchListModel model = new AutocompleteSearchListModel
                        {
                            Column1 = dr["PART_CODE"].GetValue<string>(),
                            Column2 = dr["PART_NAME"].GetValue<string>(),
                            Id = dr["ID_PART"].GetValue<int>()
                        };

                        list_ACSearchModel.Add(model);
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
            return list_ACSearchModel;
        }

        public List<SelectListItem> ListSSIDPriceListAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_PRICE_LIST_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_PRICE_LIST"].GetValue<string>(),
                            Text = reader["DESCRIPTION"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

        public int IsPartChanged(int partId, string partCode)
        {
            int result = 0;
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_CHANGED_PART");
                db.AddInParameter(cmd, "ID_PART", DbType.String, MakeDbNull(partId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(partCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<int>();
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
            return result;
        }


        public void XMLtoDBSparePartSupplyDiscountRatio(List<SparePartSupplyDiscountRatioXMLModel> listModel, ServiceCallLogModel logModel)
        {
            //DbTransaction transaction = null;
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SUPPLY_DISCOUNT_RATIO_XML");
                cmd.CommandTimeout = 14400;

                CreateConnection(cmd);
                //transaction = connection.BeginTransaction();
                foreach (var model in listModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "SS_ID", DbType.String, MakeDbNull(model.SSID));
                    db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(model.PartId));
                    db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.DiscountRatio));
                    db.AddInParameter(cmd, "VALID_START_DATE", DbType.DateTime, MakeDbNull(model.ValidStartDate));
                    db.AddInParameter(cmd, "VALID_END_DATE", DbType.DateTime, MakeDbNull(model.ValidEndDate));
                    db.AddInParameter(cmd, "CHANNEL_CODE", DbType.String, MakeDbNull(model.ChannelCode));
                    db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    //db.ExecuteNonQuery(cmd, transaction);
                    db.ExecuteNonQuery(cmd);

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.PartCode + " - " + MessageResource.SparePart_Display_PartCode,
                            Error = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>()
                        });

                        logModel.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logModel.IsSuccess = false;
                logModel.LogErrorDesc = ex.Message;
            }
            finally
            {
                if (!logModel.IsSuccess)
                {
                    logModel.ErrorModel.AddRange(listError);
                }

                CloseConnection();
            }
        }

        public ServiceCallLogModel XMLtoDBSparePartLang(DataTable dtPartLangList)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var dTime = DateTime.Now;
            var listError = new List<ServiceCallScheduleErrorListModel>();

            var pageNo = 0;
            var pageCount = 100;
            int count = 0;
            var rowCount = dtPartLangList.Rows.Count;
            count = ((int)rowCount / pageCount) + (rowCount % pageCount == 0 ? 0 : 1);

            //100.000 den fazla kayıt geliyor.
            //Sayfalama yapılarak sp ye gönderiliyor.
            for (int i = 0; i < count; i++)
            {
                pageNo = i;
                var diff = (dtPartLangList.Rows.Count - (count * pageCount));
                DataTable newDataTable = new DataTable();
                //son sayfa
                if (pageNo == (count + 1) && diff > 0)
                {
                    newDataTable = dtPartLangList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(diff).CopyToDataTable();
                }
                else if (pageNo == count && diff == 0)
                {
                    newDataTable = dtPartLangList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(pageCount).CopyToDataTable();
                }
                else
                {
                    newDataTable = dtPartLangList.AsEnumerable()
                              .Skip(pageNo * pageCount)
                              .Take(pageCount).CopyToDataTable();

                }

                //Sayfalama ile alınan datatable tek tek sp ye gönderilir.
                try
                {
                    CreateDatabase();
                    DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_LANG_BATCH");
                    CreateConnection(cmd);
                    cmd.Parameters.Add(new SqlParameter("@TBL_SPARE_PART_LANG", newDataTable));
                    db.AddInParameter(cmd, "ACTION_DATE_TIME", DbType.DateTime, dTime);
                    cmd.CommandTimeout = 14400;

                    var ds = db.ExecuteDataSet(cmd);

                    listError.AddRange(ds.Tables[0].AsEnumerable().Select(c => new ServiceCallScheduleErrorListModel
                    {
                        Action = c.Field<string>("PART_CODE"),
                        Error = c.Field<string>("ERROR_DESC")
                    }));
                }
                catch (Exception ex)
                {
                    rModel.IsSuccess = false;
                    rModel.LogErrorDesc = ex.Message;
                }
                finally
                {
                    CloseConnection();
                }
            }

            rModel.ErrorModel = listError;
            if (listError.Count > 0)
                rModel.IsSuccess = false;

            return rModel;
        }
        public ServiceCallLogModel XMLtoDBSparePart(DataTable dtPartList)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var dTime = DateTime.Now;
            var listError = new List<ServiceCallScheduleErrorListModel>();

            var pageNo = 0;
            var pageCount = 100;
            int count = 0;
            var rowCount = dtPartList.Rows.Count;
            count = ((int)rowCount / pageCount) + (rowCount % pageCount == 0 ? 0 : 1);

            //100.000 den fazla kayıt geliyor.
            //Sayfalama yapılarak sp ye gönderiliyor.
            for (int i = 0; i < count ; i++)
            {
                pageNo = i;
                var diff = (dtPartList.Rows.Count - (count * pageCount));
                DataTable newDataTable = new DataTable();
                //son sayfa
                if (pageNo == (count + 1) && diff > 0)
                {
                    newDataTable = dtPartList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(diff).CopyToDataTable();
                }
                else if (pageNo == count && diff == 0)
                {
                    newDataTable = dtPartList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(pageCount).CopyToDataTable();
                }
                else
                {
                    newDataTable = dtPartList.AsEnumerable()
                              .Skip(pageNo * pageCount)
                              .Take(pageCount).CopyToDataTable();

                }

                //Sayfalama ile alınan datatable tek tek sp ye gönderilir.
                try
                {
                    CreateDatabase();
                    DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_BATCH");
                    CreateConnection(cmd);
                    cmd.Parameters.Add(new SqlParameter("@TBL_SPARE_PART", newDataTable));
                    db.AddInParameter(cmd, "ACTION_DATE_TIME", DbType.DateTime, dTime);
                    cmd.CommandTimeout = 14400;

                    var ds = db.ExecuteDataSet(cmd);

                    listError.AddRange(ds.Tables[0].AsEnumerable().Select(c => new ServiceCallScheduleErrorListModel
                    {
                        Action = c.Field<string>("PART_CODE"),
                        Error = c.Field<string>("ERROR_DESC")
                    }));
                }
                catch (Exception ex)
                {
                    rModel.IsSuccess = false;
                    rModel.LogErrorDesc = ex.Message;
                }
                finally
                {
                    CloseConnection();
                }
            }

            rModel.ErrorModel = listError;
            if (listError.Count > 0)
                rModel.IsSuccess = false;

            return rModel;
        }
        public decimal UpdateSparePartUnserve(UserInfo user, int dealerId, long partId, int stockTypeId, decimal quantity, string transactionDesc, int transactionTypeId)
        {
            decimal result = 0;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_SPARE_PART_UNRESERVE");
                db.AddParameter(cmd, "RESULT", DbType.Decimal, ParameterDirection.InputOutput, "RESULT", DataRowVersion.Default, result);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(stockTypeId));
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, MakeDbNull(quantity));
                db.AddInParameter(cmd, "TRANSACTION_DESC", DbType.String, MakeDbNull(transactionDesc));
                db.AddInParameter(cmd, "TRANSACTION_TYPE_LOOKVAL", DbType.Int32, MakeDbNull(transactionTypeId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = db.GetParameterValue(cmd, "RESULT").GetValue<int>();
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
        public void XMLtoDBSparePartLang(List<SparePartXMLLangModel> langListModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_LANG_XML");
                cmd.CommandTimeout = 14400;

                CreateConnection(cmd);
                foreach (var model in langListModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(model.PartCode));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(model.LanguageCode));
                    db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(model.PartName));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());


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
        }


        public ServiceCallLogModel XMLtoDBSparePartPrice(DataTable dtPriceList)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var dTime = DateTime.Now;
            var listError = new List<ServiceCallScheduleErrorListModel>();

            var pageNo = 0;
            var pageCount = 100;
            int count = 0;
            var rowCount = dtPriceList.Rows.Count;
            count = ((int)rowCount / pageCount) + (rowCount % pageCount == 0 ? 0 : 1);

            //100.000 den fazla kayıt geliyor.
            //Sayfalama yapılarak sp ye gönderiliyor.
            for (int i = 0; i < count ; i++)
            {
                pageNo = i;
                var diff = (dtPriceList.Rows.Count - (count * pageCount));
                DataTable newDataTable = new DataTable();
                //son sayfa
                if (pageNo == (count + 1) && diff > 0)
                {
                    newDataTable = dtPriceList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(diff).CopyToDataTable();
                }
                else if (pageNo == count && diff == 0)
                {
                    newDataTable = dtPriceList.AsEnumerable()
                              .Skip((pageNo - 1) * pageCount)
                              .Take(pageCount).CopyToDataTable();
                }
                else
                {
                    newDataTable = dtPriceList.AsEnumerable()
                              .Skip(pageNo * pageCount)
                              .Take(pageCount).CopyToDataTable();

                }

                //Sayfalama ile alınan datatable tek tek sp ye gönderilir.
                try
                {
                    CreateDatabase();
                    DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_PRICE_BATCH");
                    CreateConnection(cmd);
                    cmd.Parameters.Add(new SqlParameter("@TBL_SPARE_PART_PRICE", newDataTable));
                    db.AddInParameter(cmd, "ACTION_DATE_TIME", DbType.DateTime, dTime);
                    cmd.CommandTimeout = 14400;

                    var ds = db.ExecuteDataSet(cmd);

                    listError.AddRange(ds.Tables[0].AsEnumerable().Select(c => new ServiceCallScheduleErrorListModel
                    {
                        Action = c.Field<string>("SSID"),
                        Error = c.Field<string>("ERROR_DESC")
                    }));

                    cmd = db.GetStoredProcCommand("P_CONTROL_SPARE_PART_PRICE_LIST_DATE");

                    cmd.Parameters.Clear();
                    cmd.Connection = connection;

                    db.AddInParameter(cmd, "CONTROL_DATE", DbType.DateTime, dTime);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 2000);
                    cmd.CommandTimeout = 14400;
                    cmd.ExecuteNonQuery();

                    var errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (errorNo > 0)
                    {
                        var splitted = db.GetParameterValue(cmd, "ERROR_DESC").ToString().Split(',');
                        foreach (var s in splitted)
                        {
                            listError.Add(new ServiceCallScheduleErrorListModel()
                            {
                                Action = s + " - SSID",
                                Error = "Tarih aralığı çakışması yaşanmaktadır."
                            });
                        }
                        rModel.IsSuccess = false;
                    }
                }
                catch (Exception ex)
                {
                    rModel.IsSuccess = false;
                    rModel.LogErrorDesc = ex.Message;
                }
                finally
                {
                    CloseConnection();
                }


            }

            rModel.ErrorModel = listError;
            if (listError.Count > 0)
                rModel.IsSuccess = false;

            return rModel;
        }

        public void XMLtoDBSparePartSplit(List<SparePartSplitterXMLModel> listModel)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SPLITTING_XML");
                cmd.CommandTimeout = 14400;

                CreateConnection(cmd);
                foreach (var model in listModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "COUNTER_NO", DbType.String, MakeDbNull(model.CounterNo));
                    db.AddInParameter(cmd, "CREATE_DATE", DbType.String, MakeDbNull(model.CreateDate));
                    db.AddInParameter(cmd, "CREATE_USER", DbType.String, MakeDbNull(model.CreateUser));
                    db.AddInParameter(cmd, "GROUP_ID", DbType.String, MakeDbNull(model.GroupId));
                    db.AddInParameter(cmd, "NEW_PART_CODE", DbType.String, MakeDbNull(model.NewPartCode));
                    db.AddInParameter(cmd, "OLD_PART_CODE", DbType.String, MakeDbNull(model.OldPartCode));
                    db.AddInParameter(cmd, "QUANTITY", DbType.String, MakeDbNull(model.Quantity));
                    db.AddInParameter(cmd, "RANK_NO", DbType.String, MakeDbNull(model.RankNo));
                    db.AddInParameter(cmd, "USABLE", DbType.String, MakeDbNull(model.Usable));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, CommonValues.DMLType.Insert);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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
        }

        public List<AutocompleteSearchListModel> ListStockCardPartsAsAutoCompSearch(UserInfo user, string strSearch)
        {
            List<AutocompleteSearchListModel> retVal = new List<AutocompleteSearchListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_STOCK_CARD_SPARE_PART_AUTOSEARCH");
                db.AddInParameter(cmd, "VALUE", DbType.String, MakeDbNull(strSearch));
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AutocompleteSearchListModel model = new AutocompleteSearchListModel
                        {
                            Column1 = reader["PART_CODE"].GetValue<string>(),
                            Column2 = reader["PART_NAME"].GetValue<string>(),
                            Id = reader["ID_PART"].GetValue<int>()
                        };
                        retVal.Add(model);
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

        public void DeleteSparePartSplitting()
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SPLITTING_XML");
                db.AddInParameter(cmd, "COUNTER_NO", DbType.String, String.Empty);
                db.AddInParameter(cmd, "CREATE_DATE", DbType.String, String.Empty);
                db.AddInParameter(cmd, "CREATE_USER", DbType.String, String.Empty);
                db.AddInParameter(cmd, "GROUP_ID", DbType.String, String.Empty);
                db.AddInParameter(cmd, "NEW_PART_CODE", DbType.String, String.Empty);
                db.AddInParameter(cmd, "OLD_PART_CODE", DbType.String, String.Empty);
                db.AddInParameter(cmd, "QUANTITY", DbType.String, String.Empty);
                db.AddInParameter(cmd, "RANK_NO", DbType.String, String.Empty);
                db.AddInParameter(cmd, "USABLE", DbType.String, String.Empty);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, CommonValues.DMLType.Delete);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
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

        public List<SparePartSplittingViewModel> ListSparePartsSplitting(long partId)
        {
            var retVal = new List<SparePartSplittingViewModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_SPLITTING");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(partId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SparePartSplittingViewModel
                        {
                            PartId = reader["PART_ID"].GetValue<long>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>()
                        };
                        retVal.Add(item);
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

        public List<string> ListPriceListSSIDs()
        {
            var listItem = new List<string>();

            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_PRICE_LIST_SSIDS");
                CreateConnection(cmd);
                using (var read = cmd.ExecuteReader())
                {
                    while (read.Read())
                    {
                        listItem.Add(read["Text"].ToString());
                    }
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
            return listItem;
        }

        public string GetPriceListSsidByPriceListId(int priceListId)
        {
            string result;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PRICE_LIST_SSID_BY_PRICE_LIST_ID");
                db.AddInParameter(cmd, "PRICE_LIST_ID", DbType.Int32, priceListId);
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().ToString();
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
        public List<SelectListItem> ListPartClassCodes(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_CLASS_CODE_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["Value"].GetValue<string>(),
                            Text = reader["Text"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

