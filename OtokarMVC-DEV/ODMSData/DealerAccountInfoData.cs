using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.DealerAccountInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class DealerAccountInfoData : DataAccessBase
    {
        public List<DealerAccountListModel> List(UserInfo user, int? dealerAccountInfoId)
        {
            var result = new List<DealerAccountListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_ACCOUNT_INFO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ID", DbType.Int32, MakeDbNull(dealerAccountInfoId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new DealerAccountListModel
                        {
                            BankId = reader["ID_BANK"].GetValue<int>(),
                            BankName = reader["BANK_NAME"].GetValue<string>(),
                            Branch = reader["BRANCH"].GetValue<string>(),
                            Iban = reader["IBAN"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            Id = reader["ID"].GetValue<int>()
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

        public void DMLDealerAccountInfo(UserInfo user, DealerAccountListModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_ACCOUNT_INFO");
                db.AddParameter(cmd, "ID", DbType.Int32, ParameterDirection.InputOutput, "ID", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ID_BANK", DbType.Int32, MakeDbNull(model.BankId));
                db.AddInParameter(cmd, "BRANCH", DbType.String, MakeDbNull(model.Branch));
                db.AddInParameter(cmd, "IBAN", DbType.String, MakeDbNull(model.Iban));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "ID").GetValue<int>();
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
    }
}
