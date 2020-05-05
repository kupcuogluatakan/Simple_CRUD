using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.Bank;
using ODMSCommon.Resources;
using System.Web.Mvc;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class BankData : DataAccessBase
    {
        public List<BankListModel> ListBanks(BankListModel filter, out int totalCount)
        {
            var retVal = new List<BankListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BANKS");
                db.AddInParameter(cmd, "BANK_CODE", DbType.String, MakeDbNull(filter.Code));
                db.AddInParameter(cmd, "BANK_NAME", DbType.String, MakeDbNull(filter.Name));
                AddPagingParameters(cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new BankListModel
                        {
                            Id = reader["ID_BANK"].GetValue<int>(),
                            Code = reader["BANK_CODE"].GetValue<string>(),
                            Name = reader["BANK_NAME"].GetValue<string>()
                        };
                        retVal.Add(listModel);
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

        public BankDetailModel GetBank(BankDetailModel filter)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_BANK");
                db.AddInParameter(cmd, "ID_BANK", DbType.Int32, MakeDbNull(filter.Id));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    filter.Code = reader["BANK_CODE"].GetValue<string>();
                    filter.Name = reader["BANK_NAME"].GetValue<string>();
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

        public void DMLBank(UserInfo user, BankDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_BANK");
                db.AddParameter(cmd, "ID_BANK", DbType.Int32, ParameterDirection.InputOutput, "ID_BANK", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "BANK_CODE", DbType.String, MakeDbNull(model.Code));
                db.AddInParameter(cmd, "BANK_NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "ID_BANK").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Bank_Error_NullBankId;
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

        public List<SelectListItem> ListBanksAsSelectList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BANKS_AS_SELECTLIST");
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

        public List<BankListModel> ListBanks(BankFilter filter)
        {
            var retVal = new List<BankListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_BANKS");
                db.AddInParameter(cmd, "BANK_CODE", DbType.String, filter.Code.Value);
                db.AddInParameter(cmd, "BANK_NAME", DbType.String, filter.Description.Value);
                AddPagingParameters(cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new BankListModel
                        {
                            Id = reader["ID_BANK"].GetValue<int>(),
                            Code = reader["BANK_CODE"].GetValue<string>(),
                            Name = reader["BANK_NAME"].GetValue<string>()
                        };
                        retVal.Add(listModel);
                    }
                    reader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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
