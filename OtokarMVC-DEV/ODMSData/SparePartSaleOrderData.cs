using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.SparePartSaleOrder;
using ODMSModel;
using ODMSModel.SparePartSaleOrderDetail;
using ODMSModel.SaleOrder;
using ODMSData.Utility;

namespace ODMSData
{
    public class SparePartSaleOrderData : DataAccessBase
    {
        private readonly DbHelper _dbHelper;
        public SparePartSaleOrderData()
        {
            _dbHelper = new DbHelper();
        }
        public List<SparePartSaleOrderListModel> ListSparePartSaleOrders(UserInfo user, SparePartSaleOrderListModel filter, out int totalCount)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new List<SparePartSaleOrderListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SALE_ORDERS");
                db.AddInParameter(cmd, "SO_NUMBER", DbType.Int64, MakeDbNull(filter.SoNumber));
                db.AddInParameter(cmd, "ID_FIRM_TYPE", DbType.Int32, MakeDbNull(filter.FirmTypeId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(filter.CustomerId));
                db.AddInParameter(cmd, "ID_SO_TYPE", DbType.Int32, MakeDbNull(filter.SoTypeId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.StockTypeId));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.PartName));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "STATUS_LOOKVAL", DbType.Int32, filter.StatusId);
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
                        var sparePartListModel = new SparePartSaleOrderListModel
                        {
                            SoNumber = reader["SO_NUMBER"].GetValue<string>(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int?>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            DealerId = reader["DEALER_ID"].GetValue<int?>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            SoTypeId = reader["PO_TYPE_ID"].GetValue<int?>(),
                            SoTypeName = reader["PO_TYPE_NAME"].GetValue<string>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int?>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>(),
                            IsProposal = reader["IS_PROPOSAL"].GetValue<bool?>(),
                            IsProposalName = reader["IS_PROPOSAL_NAME"].GetValue<string>(),
                            IsFixedPrice = reader["IS_PRICE_FIXED"].GetValue<bool?>(),
                            IsFixedPriceName = reader["IS_PRICE_FIXED_NAME"].GetValue<string>(),
                            OrderDate = reader["ORDER_DATE"].GetValue<DateTime?>(),
                            StatusId = reader["STATUS_ID"].GetValue<int?>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>(),
                            DetailCount = reader["DETAIL_COUNT"].GetValue<int>(),
                            PODDetailCount = reader["POD_DETAIL_COUNT"].GetValue<int>(),
                            PlannedDetailCount = reader["PLANNED_DETAIL_COUNT"].GetValue<int>(),
                            ApprovedCount = reader["APPROVED_COUNT"].GetValue<int>(),
                            SaleDate = reader["UPDATE_DATE"].GetValue<DateTime?>() ?? reader["CREATE_DATE"].GetValue<DateTime>()
                        };
                        retVal.Add(sparePartListModel);
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

        public void DMLSparePartSaleOrder(UserInfo user, SparePartSaleOrderViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SALE_ORDER");
                db.AddParameter(cmd, "SO_NUMBER", DbType.Int32, ParameterDirection.InputOutput, "SO_NUMBER", DataRowVersion.Default, model.SoNumber);
                db.AddInParameter(cmd, "ID_DEALER ", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_CUSTOMER ", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ID_SO_TYPE ", DbType.Int32, MakeDbNull(model.SoTypeId));
                db.AddInParameter(cmd, "ID_STOCK_TYPE ", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "IS_PROPOSAL ", DbType.Boolean, model.IsProposal);
                db.AddInParameter(cmd, "IS_PRICE_FIXED ", DbType.Boolean, model.IsFixedPrice);
                db.AddInParameter(cmd, "ORDER_DATE ", DbType.DateTime, MakeDbNull(model.OrderDate));
                db.AddInParameter(cmd, "STATUS_ID ", DbType.Int32, model.StatusId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SoNumber = db.GetParameterValue(cmd, "SO_NUMBER").GetValue<string>();
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

        public SparePartSaleOrderViewModel GetSparePartSaleOrder(UserInfo user, string soNumber)
        {
            DealerData dData = new DealerData();
            DealerViewModel dModel = dData.GetDealer(user, user.GetUserDealerId());
            string currencyCode = dData.GetCountryCurrencyCode(dModel.Country);

            var retVal = new SparePartSaleOrderViewModel { SoNumber = soNumber };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SALE_ORDER");
                db.AddInParameter(cmd, "SO_NUMBER", DbType.Int32, MakeDbNull(soNumber));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        retVal.SoNumber = reader["SO_NUMBER"].GetValue<string>();
                        retVal.CustomerId = reader["CUSTOMER_ID"].GetValue<int?>();
                        retVal.CustomerName = reader["CUSTOMER_NAME"].GetValue<string>();
                        retVal.DealerId = reader["DEALER_ID"].GetValue<int?>();
                        retVal.DealerName = reader["DEALER_NAME"].GetValue<string>();
                        retVal.SoTypeId = reader["PO_TYPE_ID"].GetValue<int>();
                        retVal.SoTypeName = reader["PO_TYPE_NAME"].GetValue<string>();
                        retVal.StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>();
                        retVal.StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>();
                        retVal.IsProposal = reader["IS_PROPOSAL"].GetValue<bool>();
                        retVal.IsProposalName = reader["IS_PROPOSAL_NAME"].GetValue<string>();
                        retVal.IsFixedPrice = reader["IS_PRICE_FIXED"].GetValue<bool>();
                        retVal.IsFixedPriceName = reader["IS_PRICE_FIXED_NAME"].GetValue<string>();
                        retVal.OrderDate = reader["ORDER_DATE"].GetValue<DateTime?>();
                        retVal.StatusId = reader["STATUS_ID"].GetValue<int?>();
                        retVal.StatusName = reader["STATUS_NAME"].GetValue<string>();
                        retVal.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    }
                    reader.Close();
                }

                retVal.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                retVal.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());

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

        public string GetSparePartSaleOrderLatestStatus(string soNumber)
        {
            var value = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SALE_ORDER_MST_LATEST_STATUS");
                db.AddInParameter(cmd, "SO_NUMBER", DbType.String, MakeDbNull(soNumber));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        value = reader["VALUE"].GetValue<string>();

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
            return value;
        }

        public DataTable CreateDataTableFromList(List<SparePartSaleOrderDetailListModel> list)
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
                row[0] = item.SparePartSaleOrderDetailId;
                row[1] = item.PlannedQuantity;
                table.Rows.Add(row);
            }

            return table;
        }

        public ModelBase CreateSaleOrder(UserInfo user,List<SparePartSaleOrderDetailListModel> list)
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
    }
}
