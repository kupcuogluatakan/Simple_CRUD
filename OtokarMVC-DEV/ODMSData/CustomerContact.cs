using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSModel.CustomerContact;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class CustomerContactData : DataAccessBase
    {
        public List<CustomerContactListModel> ListCustomerContacts(UserInfo user,CustomerContactListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerContactListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CUSTOMER_CONTACTS");
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(filter.CustomerId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerContactListModel = new CustomerContactListModel
                        {
                            ContactId = reader["CONTACT_ID"].GetValue<int>(),
                            ContactTypeName = reader["CONTACT_TYPE_NAME"].ToString(),
                            ContactTypeValue = reader["CONTACT_TYPE_VALUE"].ToString(),
                            ContactTypeId = reader["CONTACT_TYPE_ID"].GetValue<int>(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            Name = reader["NAME"].ToString(),
                            Surname = reader["SURNAME"].ToString()
                        };
                        retVal.Add(customerContactListModel);
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

        public void DMLCustomerContact(UserInfo user, CustomerContactIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CUSTOMER_CONTACT_MAIN");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddParameter(cmd, "CONTACT_ID", DbType.Int32, ParameterDirection.InputOutput, "CONTACT_ID", DataRowVersion.Default, model.ContactId);
                db.AddInParameter(cmd, "CUSTOMER_ID", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "CONTACT_TYPE_ID", DbType.Int32, MakeDbNull(model.ContactTypeId));
                db.AddInParameter(cmd, "NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "SURNAME", DbType.String, MakeDbNull(model.Surname));
                db.AddInParameter(cmd, "CONTACT_TYPE_VALUE", DbType.String, MakeDbNull(model.ContactTypeValue));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ContactId = db.GetParameterValue(cmd, "CONTACT_ID").GetValue<int>();
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

        public CustomerContactIndexViewModel GetCustomerContact(UserInfo user, CustomerContactIndexViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CUSTOMER_CONTACT");
                db.AddInParameter(cmd, "ID_CUST_CONTACT", DbType.Int32, MakeDbNull(filter.ContactId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.CustomerId = dReader["CUSTOMER_ID"].GetValue<int>();
                    filter.CustomerName = dReader["CUSTOMER_NAME"].ToString();
                    filter.ContactTypeId = dReader["CONTACT_TYPE_ID"].GetValue<int?>();
                    filter.ContactTypeName = dReader["CONTACT_TYPE_NAME"].ToString();
                    filter.ContactTypeValue = dReader["CONTACT_TYPE_VALUE"].ToString();
                    filter.Name = dReader["NAME"].ToString();
                    filter.Surname = dReader["SURNAME"].ToString();
                }
                dReader.Close();
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
    }
}
