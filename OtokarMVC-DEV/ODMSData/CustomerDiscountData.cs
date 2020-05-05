using ODMSCommon.Security;
using ODMSModel.CustomerDiscount;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System.Web.Mvc;

namespace ODMSData
{
    public class CustomerDiscountData : DataAccessBase
    {
        public List<CustomerDiscountListModel> ListCustomerDiscount(UserInfo user,CustomerDiscountListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerDiscountListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_DISCOUNT");
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int64, filter.IdCustomer);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, filter.CustomerName);
                db.AddInParameter(cmd, "PART_DISCOUNT_RATIO", DbType.Decimal, filter.PartDiscountRatio);
                db.AddInParameter(cmd, "LABOUR_DISCOUNT_RATIO", DbType.Decimal, filter.LabourDiscountRatio);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerDiscountListModel = new CustomerDiscountListModel
                        {
                            IdCustomer = reader["ID_CUSTOMER"].GetValue<Int64>(),
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            PartDiscountRatio = reader["PART_DISCOUNT_RATIO"].GetValue<decimal>(),
                            LabourDiscountRatio = reader["LABOUR_DISCOUNT_RATIO"].GetValue<decimal>()
                        };

                        retVal.Add(customerDiscountListModel);
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

        public CustomerDiscountIndexViewModel GetCustomerDiscount(UserInfo user, CustomerDiscountIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CUSTOMER_DISCOUNT");
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int64, MakeDbNull(filter.IdCustomer));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(filter.CustomerName));
                db.AddInParameter(cmd, "DEALER_NAME", DbType.String, MakeDbNull(filter.DealerName));
                db.AddInParameter(cmd, "PART_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(filter.PartDiscountRatio));
                db.AddInParameter(cmd, "LABOUR_DISCOUNT_RATIO", DbType.Decimal, MakeDbNull(filter.LabourDiscountRatio));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdCustomer = dReader["ID_CUSTOMER"].GetValue<Int64>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.CustomerName = dReader["CUSTOMER_NAME"].GetValue<string>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.PartDiscountRatio = dReader["PART_DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.LabourDiscountRatio = dReader["LABOUR_DISCOUNT_RATIO"].GetValue<decimal>();
                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
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

        public void DMLCustomerDiscount(UserInfo user, CustomerDiscountIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CUSTOMER_DISCOUNT");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, model.IdCustomer);
                db.AddInParameter(cmd, "PART_DISCOUNT_RATIO", DbType.Decimal, model.PartDiscountRatio);
                db.AddInParameter(cmd, "LABOUR_DISCOUNT_RATIO", DbType.Decimal, model.LabourDiscountRatio);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
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

        public List<SelectListItem> ListDealerAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
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
