using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.SaleOrder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class SaleOrderData : DataAccessBase
    {
        private readonly DbHelper _dbHelper;
        public SaleOrderData()
        {
            _dbHelper = new DbHelper();
        }

        public List<SelectListItem> ListSaleOrderCustomers(UserInfo user,int? dealerId = null)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SALE_ORDER_CUSTOMERS_COMBO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
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
                if (!dealerId.HasValue)
                    dealerId = user.GetUserDealerId();
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

        public List<SelectListItem> ListPurchaseOrderTypes(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SALE_ORDER_TYPES_COMBO");
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

        public List<SaleOrderRemainingListItem> ListSaleOrderRemaining(UserInfo user,SaleOrderRemainingFilter filter, out int totalCnt)
        {
            var dto = new List<SaleOrderRemainingListItem>();
            totalCnt = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SALE_ORDER_REMAINING");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "ID_PURCHASE_ORDER_TYPE", DbType.Int32, MakeDbNull(filter.PurchaseOrderType));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "IS_ORIGINAL", DbType.Int32, MakeDbNull(filter.PartType));
                db.AddInParameter(cmd, "BEGIN_DATE", DbType.DateTime, MakeDbNull(filter.BeginDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "MAX_ROW_COUNT", DbType.Int32, MakeDbNull(filter.MaxRecordCount));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddInParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddInParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddInParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var saleOrderRemainingListItem = new SaleOrderRemainingListItem
                        {

                            IsSelected = reader["IS_SELECTED"].GetValue<bool>(),
                            SaleOrderNumber = reader["SO_NUMBER"].GetValue<long>(),
                            SaleOrderCreateDate = reader["SO_CREATE_DATE"].GetValue<DateTime>(),
                            SaleOrderType = reader["SO_TYPE"].GetValue<string>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PlannedQuantity = reader["PLANNED_QUANTITY"].GetValue<decimal>(),
                            OnOrderQuantity = reader["ON_ORDER_QUANTITY"].GetValue<decimal>(),
                            TotalOnOrderQuantity = reader["TOTAL_ON_ORDER_QUANTITY"].GetValue<decimal>(),
                            ExistingStockQuantity = reader["EXISTING_STOCK_QUANTITY"].GetValue<decimal>(),
                            StockType = reader["MAINT_NAME"].GetValue<string>(),
                            IsOriginal = reader["IS_ORIGINAL"].GetValue<string>(),
                            SoDetSeqNo = reader["SO_DET_SEQ_NO"].GetValue<long>(),
                            PartId = reader["ID_PART"].GetValue<long>(),
                            ChangedPartId = reader["ID_CHANGE_PART"].GetValue<long>(),
                            ListPrice = reader["LIST_PRICE"].GetValue<decimal>(),
                            OrderPrice = reader["ORDER_PRICE"].GetValue<decimal>(),
                            ConfirmPrice = reader["CONFIRM_PRICE"].GetValue<decimal>(),
                            ListDiscountRatio = reader["LIST_DISCOUNT_RATIO"].GetValue<decimal>(),
                            AppliedDiscountRatio = reader["APPLIED_DISCOUNT_RATIO"].GetValue<decimal>()

                        };
                        dto.Add(saleOrderRemainingListItem);
                    }
                    reader.Close();
                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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
            return dto;
        }

        public List<SaleOrderPartStockInfo> ListSelectedSaleOrderPartsStockQuants(string argument)
        {
            var retVal = new List<SaleOrderPartStockInfo>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SET_SELECTED_SALE_ORDER_PARTS_STOCK_QTYS");
                db.AddInParameter(cmd, "@SO_DET_SEQ_NOS", DbType.String, argument);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var saleOrderPartStockInfo = new SaleOrderPartStockInfo
                        {

                            PartId = reader["ID_PART"].GetValue<long>(),
                            SoDetSeqNo = reader["SO_DET_SEQ_NO"].GetValue<long>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>()

                        };
                        retVal.Add(saleOrderPartStockInfo);
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

        public ModelBase CreateSaleOrderDocument(UserInfo user,List<SaleOrderRemainingListItem> list)
        {
            var retVal = new ModelBase();
            var dt = CreateDataTableFromList(list);
            try
            {
                _dbHelper.ExecuteNonQuery("P_COMPLETE_SALE_ORDER_REMAING", dt, user.UserId, null, null);

                retVal.ErrorNo = int.Parse(_dbHelper.GetOutputValue("ERROR_NO").ToString());
                if (retVal.ErrorNo > 0)
                {
                    retVal.ErrorMessage = ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_NO").ToString());
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

        private DataTable CreateDataTableFromList(List<SaleOrderRemainingListItem> list)
        {
            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("SALE_ORDER_DET");
            DataColumn col2 = new DataColumn("PLANNED_QUANTITY");

            col1.DataType = System.Type.GetType("System.Int64");
            col2.DataType = System.Type.GetType("System.Decimal");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                row[0] = item.SoDetSeqNo;
                row[1] = item.PlannedQuantity;
                table.Rows.Add(row);
            }

            return table;
        }


    }
}
