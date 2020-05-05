using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.PaymentType;
using ODMSModel.WorkorderInvoicePayments;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class WorkorderInvoicePaymentsData : DataAccessBase
    {
        public WorkorderInvoicePaymentsIndexModel GetWorkorderInvoicePaymentsIndexModel(UserInfo user, int workorderInvoiceId, int workorderId)
        {
            return new WorkorderInvoicePaymentsIndexModel
            {
                WorkorderInvoiceId = workorderInvoiceId,
                WorkorderId = workorderId,
                BankList = GetBankList(),
                PaymentTypeList = GetPaymentTypeList(user)
            };
        }

        public List<PaymentTypeListModel> GetPaymentTypeList(UserInfo user)
        {
            var result = new List<PaymentTypeListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PAYMENT_TYPES_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new PaymentTypeListModel
                        {
                            Id = reader["ID_PAYMENT_TYPE"].GetValue<int>(),
                            BankRequired = reader["BANK_REQUIRED"].GetValue<bool>(),
                            TransmitNoRequired = reader["TRANSMIT_NO_REQUIRED"].GetValue<bool>(),
                            InstalmentRequired = reader["INSTALMENT_REQUIRED"].GetValue<bool>(),
                            PaymentTypeName = reader["PAYMENT_TYPE_DESC"].GetValue<string>(),
                            DefermentRequired = reader["DEFERMENT_REQUIRED"].GetValue<bool>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public List<SelectListItem> GetBankList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BANKS_COMBO");
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_BANK"].GetValue<string>(),
                            Text = reader["BANK_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
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
            return result;
        }

        public void DMLWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_WORKORDER_INVOICE_PAYMENT");
                db.AddParameter(cmd, "ID_WORK_ORDER_INVO_PAYMNT", DbType.Int32, ParameterDirection.InputOutput, "ID_WORK_ORDER_INVO_PAYMNT", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "ID_WORK_ORDER_INV", DbType.Int32, model.WorkorderInvoiceId);
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, MakeDbNull(model.WorkorderId));
                db.AddInParameter(cmd, "ID_PAYMENT_TYPE", DbType.Int32, MakeDbNull(model.PaymentTypeId));
                db.AddInParameter(cmd, "ID_BANK", DbType.Int32, MakeDbNull(model.BankId));
                db.AddInParameter(cmd, "INSTALMENT_NUMBER", DbType.Int16, MakeDbNull(model.InstalmentNumber));
                db.AddInParameter(cmd, "PAY_AMOUNT", DbType.Double, MakeDbNull(model.PayAmount));
                db.AddInParameter(cmd, "TRANSMIT_NO", DbType.String, MakeDbNull(model.TransmitNumber));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "ID_WORK_ORDER_INVO_PAYMNT").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.WorkorderInvoicePayment_Error_NullId;
                else if (model.ErrorNo == 1)
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

        public WorkorderInvoicePaymentsDetailModel GetWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsDetailModel referenceModel)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_INVOICE_PAYMENT");
                db.AddInParameter(cmd, "ID_WORK_ORDER_INVO_PAYMNT", DbType.Int32, MakeDbNull(referenceModel.Id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    referenceModel.Id = reader["ID_WORK_ORDER_INVO_PAYMNT"].GetValue<int>();
                    referenceModel.WorkorderInvoiceId = reader["ID_WORK_ORDER_INV"].GetValue<int>();
                    referenceModel.WorkorderId = reader["ID_WORK_ORDER"].GetValue<int>();
                    referenceModel.PaymentTypeId = reader["ID_PAYMENT_TYPE"].GetValue<int?>();
                    referenceModel.BankId = reader["ID_BANK"].GetValue<int?>();
                    referenceModel.InstalmentNumber = reader["INSTALMENT_NUMBER"].GetValue<int?>();
                    referenceModel.PayAmount = reader["PAY_AMOUNT"].GetValue<double>();
                    referenceModel.TransmitNumber = reader["TRANSMIT_NO"].GetValue<string>();
                    referenceModel.PaymentTypeName = reader["PAYMENT_TYPE_DESC"].GetValue<string>();
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

            referenceModel.BankName = GetBankName(referenceModel.BankId);

            return referenceModel;
        }

        public List<WorkorderInvoicePaymentsListModel> ListWorkorderInvoicePayments(UserInfo user, WorkorderInvoicePaymentsListModel filter, out int totalCnt)
        {
            var result = new List<WorkorderInvoicePaymentsListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_WORK_ORDER_INVOICE_PAYMENTS");
                db.AddInParameter(cmd, "ID_PAYMENT_TYPE", DbType.Int32, MakeDbNull(filter.PaymentTypeId));
                db.AddInParameter(cmd, "ID_BANK", DbType.Int32, MakeDbNull(filter.BankId));
                db.AddInParameter(cmd, "PAY_AMOUNT", DbType.Double, MakeDbNull(filter.PayAmount));
                db.AddInParameter(cmd, "INSTALMENT_NUMBER", DbType.Int32, MakeDbNull(filter.SearchIsActive));
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
                        var model = new WorkorderInvoicePaymentsListModel
                        {
                            Id = reader["ID_WORK_ORDER_INVO_PAYMNT"].GetValue<int>(),
                            WorkorderInvoiceId = reader["ID_WORK_ORDER_INV"].GetValue<int>(),
                            WorkorderId = reader["ID_WORK_ORDER"].GetValue<int>(),
                            PaymentTypeId = reader["ID_PAYMENT_TYPE"].GetValue<int?>(),
                            BankId = reader["ID_BANK"].GetValue<int?>(),
                            InstalmentNumber = reader["INSTALMENT_NUMBER"].GetValue<int?>(),
                            PayAmount = reader["PAY_AMOUNT"].GetValue<double>(),
                            TransmitNumber = reader["TRANSMIT_NO"].GetValue<string>(),
                            PaymentTypeName = reader["PAYMENT_TYPE_DESC"].GetValue<string>()
                        };
                        result.Add(model);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
                result.ForEach(x => x.BankName = GetBankName(x.BankId));
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

        private string GetBankName(int? id)
        {
            string result = null;
            if (id != null)
            {
                System.Data.Common.DbDataReader reader = null;
                try
                {
                    CreateDatabase();
                    var cmd = db.GetStoredProcCommand("P_GET_BANK");
                    db.AddInParameter(cmd, "ID_BANK", DbType.Int32, MakeDbNull(id));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                    CreateConnection(cmd);

                    reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        result = reader["BANK_NAME"].GetValue<string>();
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
            }
            return result;
        }
    }
}
