using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.DealerSaleChannelDiscountRatio;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerSaleChannelDiscountRatioData : DataAccessBase
    {
        public List<DealerSaleChannelDiscountRatioListModel> ListDealerSaleChannelDiscountRatios(UserInfo user, DealerSaleChannelDiscountRatioListModel filter, out int totalCount)
        {
            System.Data.Common.DbDataReader dbDataReader = null;
            var retVal = new List<DealerSaleChannelDiscountRatioListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_SALE_CHANNEL_DISCOUNT_RATIOS");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(filter.DealerClassCode));
                db.AddInParameter(cmd, "CHANNEL_CODE", DbType.String, MakeDbNull(filter.ChannelCode));
                db.AddInParameter(cmd, "SPARE_PART_CLASS_CODE", DbType.String, MakeDbNull(filter.SparePartClassCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dbDataReader = cmd.ExecuteReader())
                {
                    while (dbDataReader.Read())
                    {
                        var dealerSaleChannelDiscountRatiosListModel = new DealerSaleChannelDiscountRatioListModel
                        {
                            ChannelCode = dbDataReader["CHANNEL_CODE"].GetValue<string>(),
                            ChannelName = dbDataReader["CHANNEL_NAME"].GetValue<string>(),
                            DealerClassCode = dbDataReader["DEALER_CLASS_CODE"].GetValue<string>(),
                            DealerClassName = dbDataReader["DEALER_CLASS_NAME"].GetValue<string>(),
                            SparePartClassCode = dbDataReader["SPARE_PART_CLASS_CODE"].GetValue<string>(),
                            TseInvalidDiscountRatio = dbDataReader["TSE_INVALID_DISCOUNT_RATIO"].GetValue<decimal>(),
                            TseValidDiscountRatio = dbDataReader["TSE_VALID_DISCOUNT_RATIO"].GetValue<decimal>()
                        };
                        retVal.Add(dealerSaleChannelDiscountRatiosListModel);
                    }
                    dbDataReader.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dbDataReader != null && !dbDataReader.IsClosed)
                    dbDataReader.Close();
                CloseConnection();
            }

            return retVal;
        }

        public void DMLDealerSaleChannelDiscountRatio(UserInfo user, DealerSaleChannelDiscountRatioViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_SALE_CHANNEL_DISCOUNT_RATIO");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, model.DealerClassCode);
                db.AddInParameter(cmd, "CHANNEL_CODE", DbType.String, model.ChannelCode);
                db.AddInParameter(cmd, "SPARE_PART_CLASS_CODE", DbType.String, model.SparePartClassCode);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "TSE_VALID_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.TseValidDiscountRatio));
                db.AddInParameter(cmd, "TSE_INVALID_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(model.TseInvalidDiscountRatio));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.DealerSaleChannelDiscountRatio_Error_NullId;
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

        public DealerSaleChannelDiscountRatioViewModel GetDealerSaleChannelDiscountRatio(UserInfo user, string dealerClassCode, string channelCode, string sparePartClassCode)
        {
            System.Data.Common.DbDataReader dReader = null;
            var dealerSaleChannelDiscountRatiosModel = new DealerSaleChannelDiscountRatioViewModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_SALE_CHANNEL_DISCOUNT_RATIO");
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(dealerClassCode));
                db.AddInParameter(cmd, "CHANNEL_CODE", DbType.String, MakeDbNull(channelCode));
                db.AddInParameter(cmd, "SPARE_PART_CLASS_CODE", DbType.String, MakeDbNull(sparePartClassCode));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (dReader = cmd.ExecuteReader())
                {
                    while (dReader.Read())
                    {
                        dealerSaleChannelDiscountRatiosModel.ChannelCode = dReader["CHANNEL_CODE"].GetValue<string>();
                        dealerSaleChannelDiscountRatiosModel.ChannelName = dReader["CHANNEL_NAME"].GetValue<string>();
                        dealerSaleChannelDiscountRatiosModel.DealerClassCode = dReader["DEALER_CLASS_CODE"].GetValue<string>();
                        dealerSaleChannelDiscountRatiosModel.DealerClassName = dReader["DEALER_CLASS_NAME"].GetValue<string>();
                        dealerSaleChannelDiscountRatiosModel.SparePartClassCode = dReader["SPARE_PART_CLASS_CODE"].GetValue<string>();
                        dealerSaleChannelDiscountRatiosModel.TseInvalidDiscountRatio = dReader["TSE_INVALID_DISCOUNT_RATIO"].GetValue<decimal>();
                        dealerSaleChannelDiscountRatiosModel.TseValidDiscountRatio = dReader["TSE_VALID_DISCOUNT_RATIO"].GetValue<decimal>();
                    }
                    dReader.Close();
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


            return dealerSaleChannelDiscountRatiosModel;
        }
    }
}
