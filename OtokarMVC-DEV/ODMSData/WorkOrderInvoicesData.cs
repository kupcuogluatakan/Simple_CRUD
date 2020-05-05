using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.WorkOrderCard;
using ODMSModel.WorkOrderInvoice;

namespace ODMSData
{
    public class WorkOrderInvoicesData : DataAccessBase
    {
        public WorkOrderInvoiceDTO GetWorkOrderInvoiceAmount(long workOrderId, int customerId, long invoiceId, string workOrderDetailIds)
        {
            var retValue = new WorkOrderInvoiceDTO();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_INVOICE_AMOUNT");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, workOrderId);
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, workOrderDetailIds);
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(customerId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_INVOICE", DbType.Int64, MakeDbNull(invoiceId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 0);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retValue.CurrencyCode = dr["CURRENCY_CODE"].GetValue<string>();
                        retValue.IsFromProposalWitholding = dr["IS_FROM_PROPOSAL_WITHOLDING"].GetValue<bool>();
                        retValue.HasWitholding = dr["HAS_WITHOLDING"].GetValue<bool>();
                        retValue.InvoiceAmount = dr["INVOICE_AMOUNT"].GetValue<decimal>();
                        retValue.InvoiceAmountWithVat = dr["INVOICE_AMOUNT_WITH_VAT"].GetValue<decimal>();
                        retValue.InvoiceRatio = dr["INVOICE_RATIO"].GetValue<decimal>();
                        retValue.WitholdId = dr["ID_WITHOLDING"].GetValue<int>();
                        retValue.WitholdingMinAmount = dr["WITHOLDING_MIN_AMOUNT"].GetValue<decimal>();
                        retValue.VatRatio = dr["VAT_RATIO"].GetValue<decimal>();
                        retValue.WorkOrderInvoiceId = dr["ID_WORK_ORDER_INV"].GetValue<long>();
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
            return retValue;
        }

        public void DMLWorkOrderInvoices(UserInfo user, WorkOrderInvoicesViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_WORK_ORDER_INVOICE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(model.WorkOrderId));
                db.AddParameter(cmd, "ID_WORK_ORDER_INVOICE", DbType.Int64, ParameterDirection.InputOutput, "ID_WORK_ORDER_INVOICE", DataRowVersion.Default, model.WorkOrderInvoiceId);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "ID_ADDRESS", DbType.Int32, MakeDbNull(model.AddressId));
                db.AddInParameter(cmd, "VAT_RATIO", DbType.Decimal, MakeDbNull(model.VatRatio));
                db.AddInParameter(cmd, "INVOICE_SERIAL_NO", DbType.String, MakeDbNull(model.InvoiceSerialNo));
                db.AddInParameter(cmd, "INVOICE_NO", DbType.String, MakeDbNull(model.InvoiceNo));
                db.AddInParameter(cmd, "INVOICE_DATE", DbType.DateTime, MakeDbNull(model.InvoiceDate));
                db.AddInParameter(cmd, "INVOICE_RATIO", DbType.Decimal, MakeDbNull(model.InvoiceRatio));
                db.AddInParameter(cmd, "INVOICE_AMOUNT", DbType.Decimal, MakeDbNull(model.InvoiceAmount));
                db.AddInParameter(cmd, "INVOICE_VAT_AMOUNT", DbType.Decimal, MakeDbNull(model.InvoiceVatAmount));
                db.AddInParameter(cmd, "DUE_DURATION", DbType.Int32, MakeDbNull(model.DueDuration));
                db.AddInParameter(cmd, "ID_WITHOLDING", DbType.String, MakeDbNull(model.WitholdId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, MakeDbNull(model.WorkOrderIds));

                db.AddInParameter(cmd, "INVOICE_TYPE_LOOKKEY", DbType.String, MakeDbNull(WorkOrderInvoicesViewModel.InvoiceTypeLookKey));
                db.AddInParameter(cmd, "INVOICE_TYPE_LOOKVAL", DbType.String, MakeDbNull(model.InvoiceTypeId));
                db.AddInParameter(cmd, "SPECIAL_INVOICE_DESCRIPTION", DbType.String, MakeDbNull(model.SpecialInvoiceDescription));
                db.AddInParameter(cmd, "SPECIAL_INVOICE_AMOUNT", DbType.Decimal, MakeDbNull(model.SpecialInvoiceAmount));
                db.AddInParameter(cmd, "SPECIAL_INVOICE_VAT_AMOUNT", DbType.Decimal, MakeDbNull(model.SpecialInvoiceVatAmount));

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                model.WorkOrderInvoiceId = db.GetParameterValue(cmd, "ID_WORK_ORDER_INVOICE").GetValue<long>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                else
                {
                    model.ErrorMessage = string.Format(MessageResource.WorkOrderInvoice_Message_SuccessMessage,
                        model.WorkOrderInvoiceId);
                }
                cmd.Dispose();

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

        public List<WorkOrderInvoicesListModel> ListWorkOrderInvoices(UserInfo user,WorkOrderInvoicesListModel model, out int totalCnt)
        {
            var list = new List<WorkOrderInvoicesListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_WORKORDER_INVOICE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, model.WorkOrderId);
                AddPagingParametersWithLanguage(user,cmd, model);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var workOrderListModel = new WorkOrderInvoicesListModel
                        {
                            WorkOrderInvoiceId = dr["ID_WORK_ORDER_INV"].GetValue<long>(),
                            CustomerName = dr["CUSTOMER_FULL_NAME"].GetValue<string>(),
                            InvoiceSerialNo = dr["INVOICE_SERIAL_NO"].GetValue<string>(),
                            InvoiceAmount = dr["INVOICE_AMOUNT"].GetValue<decimal>(),
                            InvoiceDate = dr["INVOICE_DATE"].GetValue<DateTime>(),
                            InvoiceNo = dr["INVOICE_NO"].GetValue<string>(),
                            InvoiceRatio = dr["INVOICE_RATIO"].GetValue<decimal>()
                        };

                        list.Add(workOrderListModel);
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

            return list;
        }

        public WorkOrderInvoicesViewModel GetWorkOrderInvoice(UserInfo user, long workOrderInvoiceId)
        {
            DbDataReader dr = null;
            var model = new WorkOrderInvoicesViewModel { WorkOrderInvoiceId = workOrderInvoiceId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_INVOICE");
                db.AddInParameter(cmd, "ID_WORK_ORDER_INVOICE", DbType.Int32, MakeDbNull(workOrderInvoiceId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    model.WorkOrderId = dr["ID_WORK_ORDER"].GetValue<long>();
                    model.CustomerId = dr["ID_CUSTOMER"].GetValue<int>();
                    model.CustomerName = dr["CUSTOMER_NAME"].GetValue<String>();
                    model.Address = dr["ADDRESS_DESC"].GetValue<String>();
                    model.VatRatio = dr["VAT_RATIO"].GetValue<decimal>();
                    model.InvoiceRatio = dr["INVOICE_RATIO"].GetValue<decimal>();
                    model.InvoiceNo = dr["INVOICE_NO"].GetValue<String>();
                    model.InvoiceAmount = dr["INVOICE_AMOUNT"].GetValue<decimal>();
                    model.InvoiceDate = dr["INVOICE_DATE"].GetValue<DateTime>();
                    model.InvoiceVatAmount = dr["INVOICE_VAT_AMOUNT"].GetValue<decimal>();
                    model.InvoiceSerialNo = dr["INVOICE_SERIAL_NO"].GetValue<String>();
                    model.Currrency = dr["CURRENCY_CODE"].GetValue<String>();
                    model.DueDuration = dr["DUE_DURATION"].GetValue<int>();
                    model.InvoiceTypeId = dr["INVOICE_TYPE_LOOKVAL"].GetValue<int>();
                    model.HasWitholding = dr["HASWITHOLD"].GetValue<bool>();
                    model.WorkOrderIds = dr["WORK_ORDER_IDS"].ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
                CloseConnection();
            }
            return model;
        }

        public List<SelectListItem> GetWitholdingListForDealer(int dealerId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WITHOLDINGS_COMBO_FOR_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                        {
                            Value = reader["VALUE"].GetValue<string>(),
                            Text = reader["TEXT"].GetValue<string>()
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

        public List<WorkOrderInvoiceItem> ListWorkOrderInvoiceItems(UserInfo user, long workOrderId, string workOrderDetailds)
        {

            var retVal = new List<WorkOrderInvoiceItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_DETAIL_INVOICES");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, MakeDbNull(workOrderDetailds));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "CURRENCY", DbType.String, 3);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new WorkOrderInvoiceItem
                        {
                            Indicator = reader["DESCRIPTION"].GetValue<string>(),
                            Price = reader["PRICE"].GetValue<decimal>(),
                            WorkOrderDetailId = reader["ID_WORK_ORDER_DETAIL"].GetValue<long>(),
                            WorkOrderInvoiceId = reader["ID_WORK_ORDER_INV"].GetValue<long>(),
                            InvoiceCancel = reader["INVOICE_CANCEL"].GetValue<bool>(),
                            WorkOrderId = reader["WORK_ORDER_ID"].GetValue<long>()
                        };
                        retVal.Add(listItem);
                    }
                    reader.Close();
                }
                var currency = db.GetParameterValue(cmd, "CURRENCY").ToString();
                retVal.ForEach(c => c.PriceString = c.Price.ToString("N2") + " " + currency);
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

        public List<WorkOrderInvoicesListModel> ListInvoices(UserInfo user, long workOrderId, string workOrderDetailIds)
        {

            var retVal = new List<WorkOrderInvoicesListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_INVOICES_TAB");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, MakeDbNull(workOrderDetailIds));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "CURRENCY", DbType.String, 3);
                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var workOrderListModel = new WorkOrderInvoicesListModel
                        {
                            WorkOrderInvoiceId = dr["ID_WORK_ORDER_INV"].GetValue<long>(),
                            CustomerName = dr["CUSTOMER_FULL_NAME"].GetValue<string>(),
                            InvoiceSerialNo = dr["INVOICE_SERIAL_NO"].GetValue<string>(),
                            InvoiceAmount = dr["INVOICE_AMOUNT"].GetValue<decimal>(),
                            InvoiceDate = dr["INVOICE_DATE"].GetValue<DateTime>(),
                            InvoiceNo = dr["INVOICE_NO"].GetValue<string>(),
                            InvoiceRatio = dr["INVOICE_RATIO"].GetValue<decimal>()
                        };

                        retVal.Add(workOrderListModel);
                    }
                    dr.Close();
                }
                var currency = db.GetParameterValue(cmd, "CURRENCY").ToString();
                retVal.ForEach(c => c.PriceString = c.InvoiceAmount.ToString("N2") + " " + currency);
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


        public ODMSModel.ModelBase UpdateInvoiceIds(UserInfo user, long workOrderId, long invoiceId, string worOrderDetailIds, string commandType, out string invType)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_INVOICE_IDS");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "ID_WORK_ORDER_INV", DbType.Int64, MakeDbNull(invoiceId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, MakeDbNull(worOrderDetailIds));
                db.AddOutParameter(cmd, "INVOICE_TYPE", DbType.String, 20);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, commandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                invType = db.GetParameterValue(cmd, "INVOICE_TYPE").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);

                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return model;
        }

        public Tuple<string, string, decimal> GetSuggestedInvoiceData(long workOrderId, string workOrderDetailIds)
        {
            Tuple<string, string, decimal> retVal = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SUGGESTED_INVOICE_DATA");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int64, MakeDbNull(workOrderId));
                db.AddInParameter(cmd, "WORK_ORDER_DETAIL_IDS", DbType.String, MakeDbNull(workOrderDetailIds));
                CreateConnection(cmd);

                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        retVal = new Tuple<string, string, decimal>(dr["SUGGESTED_INVOICE_SERIAL_NO"].ToString(),
                            dr["SUGGESTED_INVOICE_NO"].ToString(), dr["SUGGESTED_VAT_RATIO"].GetValue<decimal>());
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

            return retVal;
        }


        public ModelBase SetBillingStatus(UserInfo user, long workOrderDetailId, bool invoiceCancel)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SET_WORK_ORDER_DETAIL_INVOICE_CANCEL");
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int64, MakeDbNull(workOrderDetailId));
                db.AddInParameter(cmd, "INVOICE_CANCEL", DbType.Boolean, invoiceCancel);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();

                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                cmd.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return model;
        }

        public ModelBase WorkOrderDetailInvoiceDelete(int workOrderDetailId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SET_WORK_ORDER_DETAIL_INVOICE_DELETE");
                db.AddInParameter(cmd, "ID_WORK_ORDER_DETAIL", DbType.Int32, workOrderDetailId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return model;
        }


        public int GetLastWorkOrderInvoiceId(int workOrderId)
        {
            var id = 0;

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_WORK_ORDER_INVOICE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, workOrderId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader["ID_WORK_ORDER_INV"].GetValue<int>();
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

            return id;
        }
    }
}
