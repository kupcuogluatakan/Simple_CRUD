using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSaleDetail;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class SparePartSaleDetailData : DataAccessBase
    {
        public void DMLSparePartSaleDetail(UserInfo user,SparePartSaleDetailDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_DETAIL");
                db.AddInParameter(cmd, "SPARE_PART_SALE_DETAIL_ID", DbType.Int64, model.SparePartSaleDetailId);
                db.AddInParameter(cmd, "ID_PART_SALE", DbType.Int32, model.PartSaleId);
                db.AddInParameter(cmd, "ID_PART", DbType.String, model.SparePartId);
                db.AddInParameter(cmd, "CURRENCY_CODE", DbType.String, MakeDbNull(model.CurrencyCode));
                db.AddInParameter(cmd, "LIST_PRICE", DbType.Double, MakeDbNull(model.ListPrice));
                db.AddInParameter(cmd, "DEALER_PRICE", DbType.Double, (model.DealerPrice == null) ? 0 : model.DealerPrice);
                db.AddInParameter(cmd, "DISCOUNT_PRICE", DbType.Double, MakeDbNull(model.DiscountPrice));
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Double, MakeDbNull(model.DiscountRatio));
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(model.VatRatio));
                db.AddInParameter(cmd, "STATUS_ID", DbType.Int32, MakeDbNull(model.StatusId));
                db.AddInParameter(cmd, "PLAN_QUANTITY", DbType.Decimal, MakeDbNull(model.PlanQuantity));
                db.AddInParameter(cmd, "RETURN_REASON_TEXT", DbType.String, MakeDbNull(model.ReturnReasonText));
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, MakeDbNull(model.DeliverySeqNo));
                db.AddInParameter(cmd, "IS_PRICE_FIXED", DbType.Int16, MakeDbNull(model.IsPriceFixed));
                db.AddInParameter(cmd, "PRICE_LIST_DATE", DbType.DateTime, MakeDbNull(model.PriceListDate));
                db.AddInParameter(cmd, "PICK_PLANNED_QUANTITY", DbType.Decimal, MakeDbNull(model.PickQuantity));
                db.AddInParameter(cmd, "PICKED_QUANTITY", DbType.Decimal, MakeDbNull(model.PickedQuantity));
                db.AddInParameter(cmd, "RETURNED_QUANTITY", DbType.Int64, MakeDbNull(model.ReturnedQuantity));
                db.AddInParameter(cmd, "SO_DET_SEQ_NO", DbType.Int16, MakeDbNull(null));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32,
                                  MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.SparePartSaleDetail_Error_NullId;
                else if (model.ErrorNo == 1)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
            }
            catch(Exception ex)
            {
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected;
            }
            finally
            {
                CloseConnection();
            }
        }

        public SparePartSaleDetailDetailModel GetSparePartSaleDetail(UserInfo user, SparePartSaleDetailDetailModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE_DETAIL");
                db.AddInParameter(cmd, "SPARE_PART_SALE_DETAIL_ID", DbType.Int64, MakeDbNull(filter.SparePartSaleDetailId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.SparePartSaleDetailId = reader["SPARE_PART_SALE_DETAIL_ID"].GetValue<long>();
                    filter.PartSaleId = reader["ID_PART_SALE"].GetValue<int>();
                    filter.SparePartId = reader["ID_PART"].GetValue<int>();
                    filter.SparePartCode = reader["PART_CODE"].GetValue<string>();
                    filter.SparePartName = reader["PART_NAME"].GetValue<string>();
                    filter.CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>();
                    filter.ListPrice = reader["LIST_PRICE"].GetValue<decimal>();
                    filter.DealerPrice = reader["DEALER_PRICE"].GetValue<decimal?>();
                    filter.DiscountPrice = reader["DISCOUNT_PRICE"].GetValue<decimal>();
                    filter.DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.VatRatio = reader["VAT_RATIO"].GetValue<decimal>();
                    filter.StatusId = reader["STATUS_ID"].GetValue<int>();
                    filter.StatusName = reader["STATUS_NAME"].GetValue<string>();
                    filter.PlanQuantity = reader["PLAN_QUANTITY"].GetValue<decimal>();
                    filter.ReturnReasonText = reader["RETURN_REASON_TEXT"].GetValue<string>();
                    filter.DeliverySeqNo = reader["DELIVERY_SEQ_NO"].GetValue<long>();
                    filter.IsPriceFixed = reader["IS_PRICE_FIXED"].GetValue<bool>();
                    filter.PickQuantity = reader["PICK_PLANNED_QUANTITY"].GetValue<decimal>();
                    filter.PickedQuantity = reader["PICKED_QUANTITY"].GetValue<decimal>();
                    filter.ReturnedQuantity = reader["RETURNED_QUANTITY"].GetValue<decimal>();
                    filter.SoDetSeqNo = reader["SO_DET_SEQ_NO"].GetValue<string>();
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

        public List<SparePartSaleDetailListModel> ListSparePartSaleDetails(UserInfo user, SparePartSaleDetailListModel filter, out int totalCnt)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var result = new List<SparePartSaleDetailListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PARTS_SALE_DETAILS");
                db.AddInParameter(cmd, "ID_SALE_PART", DbType.Int32, MakeDbNull(filter.PartSaleId));
                db.AddInParameter(cmd, "PART_ID", DbType.String, MakeDbNull(filter.SparePartId));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.SparePartName));
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Double, MakeDbNull(filter.DiscountRatio));
                db.AddInParameter(cmd, "PLAN_QUANTITY", DbType.Int32, MakeDbNull(filter.PlanQuantity));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String,
                                  MakeDbNull(user.LanguageCode));
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
                        var listModel = new SparePartSaleDetailListModel
                        {
                            SparePartSaleDetailId = reader["SPARE_PART_SALE_DETAIL_ID"].GetValue<int>(),
                            SparePartId = reader["ID_PART"].GetValue<long>(),
                            ChangedPartId = reader["CHANGED_PART_ID"].GetValue<long?>(),
                            CurrencyCode = reader["CURRENCY_CODE"].GetValue<string>(),
                            ListPrice = reader["LIST_PRICE"].GetValue<decimal>(),
                            ListPriceWithCurrency = String.Format("{0:N}", reader["LIST_PRICE"].GetValue<decimal>()) + " " + currencyCode,
                            DealerPrice = reader["DEALER_PRICE"].GetValue<decimal>(),
                            DealerPriceWithCurrency = String.Format("{0:N}", reader["DEALER_PRICE"].GetValue<decimal>()) + " " + currencyCode,
                            DiscountPrice = reader["DISCOUNT_PRICE"].GetValue<decimal>(),
                            DiscountPriceWithCurrency = String.Format("{0:N}", reader["DISCOUNT_PRICE"].GetValue<decimal>()) + " " + currencyCode,
                            PartSaleId = reader["ID_PART_SALE"].GetValue<int>(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>(),
                            PlanQuantity = reader["PLAN_QUANTITY"].GetValue<decimal>(),
                            ReturnReasonText = reader["RETURN_REASON_TEXT"].GetValue<string>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            SparePartCode = reader["PART_CODE"].GetValue<string>(),
                            SparePartName = reader["PART_NAME"].GetValue<string>(),
                            VatRatio = reader["VAT_RATIO"].GetValue<decimal>(),
                            PickQuantity = reader["PICK_PLANNED_QUANTITY"].GetValue<decimal>(),
                            PickedQuantity = reader["PICKED_QUANTITY"].GetValue<decimal>(),
                            ReturnedQuantity = reader["RETURNED_QUANTITY"].GetValue<decimal>(),
                            SoDetSeqNo = reader["SO_DET_SEQ_NO"].GetValue<string>(),
                            IsPriceFixed = reader["IS_PRICE_FIXED"].GetValue<bool>(),
                            SoNumber = reader["SO_NUMBER"].GetValue<string>(),

                            CalculatedPrice = reader["PLAN_QUANTITY"].GetValue<decimal>() * reader["DISCOUNT_PRICE"].GetValue<decimal>(),
                            CalculatedPriceWithCurrency = String.Format("{0:N}", (reader["PLAN_QUANTITY"].GetValue<decimal>() * reader["DISCOUNT_PRICE"].GetValue<decimal>()).GetValue<decimal>()) + " " + currencyCode,
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

        public List<OSparePartSaleDetailListModel> ListOtokarSparePartSaleDetail(UserInfo user,OSparePartSaleDetailListModel filter, out int totalCnt)
        {
            var listModel = new List<OSparePartSaleDetailListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_OTOKAR_SPARE_PART_SALE");

                db.AddInParameter(cmd, "SPARE_PART_SALE_ID", DbType.Int64, MakeDbNull(filter.SparePartSaleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new OSparePartSaleDetailListModel()
                        {
                            SparePartSaleDetailId = dr["SPARE_PART_SALE_DETAIL_ID"].GetValue<int>(),
                            PartsNameCode = dr["PART_NAME_CODE"].GetValue<string>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            DeliverBillyNo = dr["VAYBILL_NO"].GetValue<string>(),
                            PlanQuantity = dr["PLAN_QUANTITY"].GetValue<string>(),
                            SparePartSaleId = dr["ID_PART_SALE"].GetValue<int>(),
                            DiscountPrice = dr["DISCOUNT_PRICE"].GetValue<decimal>(),
                            ReasonText = dr["RETURN_REASON_TEXT"].GetValue<string>(),
                            StatusId = dr["STATUS_LOOKVAL"].GetValue<int>(),
                            Status = dr["STATUS"].GetValue<string>()
                        };

                        listModel.Add(model);
                    }
                    dr.Close();
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

            return listModel;
        }

        public List<OSparePartSaleDevlieryListModel> ListOtokarSparePartDelivery(UserInfo user, OSparePartSaleDevlieryListModel filter, out int totalCnt)
        {
            var listModel = new List<OSparePartSaleDevlieryListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_OTOKAR_SPARE_PART_SALE_DELIVERY");

                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(filter.PartId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int64, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, filter.PageSize);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new OSparePartSaleDevlieryListModel()
                        {
                            DeliveryId = dr["ID_DELIVERY"].GetValue<Int64>(),
                            Quantity = dr["RECEIVED_QUANT"].GetValue<string>(),
                            Price = dr["INVOICE_PRICE"].GetValue<string>()
                        };
                        listModel.Add(model);
                    }
                    dr.Close();
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
            return listModel;
        }

        public void DMLOtokarSparePartSaleDetail(UserInfo user, OSparePartSaleDetailViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_OTOKAR_SPARE_PART_SALE_DETAIL");

                db.AddInParameter(cmd, "SPARE_PART_SALE_DETAIL_ID", DbType.Int64, model.SparePartSaleDetailId);
                db.AddInParameter(cmd, "RETURN_REASON_TEXT", DbType.String, model.ReturnReasonText);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "PRICE", DbType.Decimal, model.Price);
                db.AddInParameter(cmd, "DISCOUNT_PRICE", DbType.Decimal, model.DiscountPrice);
                db.AddInParameter(cmd, "QUANTITY", DbType.Int64, model.PlanQuantity);
                db.AddInParameter(cmd, "SPARE_PART_SALE_ID", DbType.Int64, model.SparePartSaleId);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, user.UserId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, user.GetUserDealerId());
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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

        public void GetOtokarSparePartSaleDetail(UserInfo user, OSparePartSaleDetailViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_OTOKAR_SPARE_PART_SALE_DETAIL");

                db.AddInParameter(cmd, "SPARE_PART_SALE_DETAIL_ID", DbType.Int64, model.SparePartSaleDetailId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.Price = dr["PRICE"].GetValue<decimal>();
                        model.PlanQuantity = dr["QUANTITY"].GetValue<int>();
                        model.ReturnReasonText = dr["RETURN_REASON_TEXT"].GetValue<string>();
                        model.PartNameCode = dr["PART_NAME_CODE"].GetValue<string>();
                        model.MaxQuantity = dr["RECEIVED_QUANT"].GetValue<int>();
                        model.PartId = dr["PART_ID"].GetValue<int>();
                        model.SparePartSaleId = dr["PART_SALE_ID"].GetValue<int>();
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
        }
    }
}
