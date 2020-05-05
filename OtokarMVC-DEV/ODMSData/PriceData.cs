using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Price;

namespace ODMSData
{
    public class PriceData : DataAccessBase
    {
        public List<PriceListModel> ListPrice(UserInfo user,PriceListModel filter, out int totalCount)
        {
            var listModel = new List<PriceListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PRICE");
                db.AddInParameter(cmd, "PRICE_LIST_ID", DbType.Int32, MakeDbNull(filter.PriceListId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "PART_CLASS_LIST", DbType.String, MakeDbNull(filter.PartClassList));
                db.AddInParameter(cmd, "PART_CODE_LIST", DbType.String, MakeDbNull(filter.PartCodeList));
                db.AddInParameter(cmd, "IS_VALID", DbType.Boolean, filter.IsValid);
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
                        var model = new PriceListModel()
                        {
                            PartCode = dr["PART_CODE"].GetValue<string>(),
                            PartId = dr["PART_ID"].GetValue<int>(),
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            Price = dr["LIST_PRICE"].GetValue<string>(),
                            PriceList = dr["SSID_PRICE_LIST"].GetValue<string>(),
                            PartClassCode = dr["SPARE_PART_CLASS_CODE"].GetValue<string>(),
                            ValidityStopDate = dr["VALIDITY_STOP_DATE"].GetValue<DateTime>(),
                            ValidityStopDateString = dr["VALIDITY_STOP_DATE"].GetValue<DateTime>().ToShortDateString(),
                            ValidityStartDate = dr["VALIDITY_START_DATE"].GetValue<DateTime>(),
                            ValidityStartDateString = dr["VALIDITY_START_DATE"].GetValue<DateTime>().ToShortDateString(),

                            ChangedPartCode = dr["CHANGED_PART_CODE"].GetValue<string>(),
                            ChangedPartId = dr["CHANGED_PART_ID"].GetValue<int>(),
                            ChangedPartName = dr["CHANGED_PART_NAME"].GetValue<string>(),
                            ChangedPartPrice = dr["CHANGED_PART_PRICE"].GetValue<string>()
                        };

                        listModel.Add(model);
                    }
                    dr.Close();
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
            return listModel;
        }

        public List<SelectListItem> PriceListCombo(UserInfo user)
        {
            var listModel = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DEALER_PRICE_LIST_COMBO");
                db.AddInParameter(cmd, "DealerId", DbType.Int32, MakeDbNull(user.GetUserDealerId()));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new SelectListItem()
                        {
                            Text = dr["PriceText"].GetValue<string>(),
                            Value = dr["PriceValue"].GetValue<string>()
                        };
                        listModel.Add(model);
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

            return listModel;
        }
    }
}
