using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class ContractData : DataAccessBase
    {
        public List<ContractListModel> ListContract(UserInfo user, ContractListModel filter, out int totalCount)
        {
            var retVal = new List<ContractListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CONTRACT");
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, MakeDbNull(filter.IdContract));
                db.AddInParameter(cmd, "CONTRACT_NAME", DbType.String, MakeDbNull(filter.ContractName));
                db.AddInParameter(cmd, "DURATION", DbType.String, MakeDbNull(filter.Duration));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                //AddPagingParameters(cmd, filter);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contractListModel = new ContractListModel
                        {
                            IdContract = reader["ID_CONTRACT"].GetValue<int>(),
                            ContractName = reader["CONTRACT_NAME"].GetValue<string>(),
                            DocId = reader["DOC_ID"].GetValue<int>(),
                            DocName = reader["DOC_NAME"].GetValue<string>(),
                            //DocumentDesc = reader["DOCUMENT_DESC"].GetValue<string>(),
                            StartDate = reader["START_DATE"].GetValue<DateTime>(),
                            EndDate = reader["END_DATE"].GetValue<DateTime>(),
                            Duration = reader["DURATION"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                        };

                        retVal.Add(contractListModel);
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

        public void DMLContract(UserInfo user, ContractViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CONTRACT");
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, model.IdContract);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "CONTRACT_NAME", DbType.String, MakeDbNull(model.ContractName));
                db.AddInParameter(cmd, "DURATION", DbType.Int32, MakeDbNull(model.Duration));
                db.AddInParameter(cmd, "DOCUMENT_ID", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(model.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(model.EndDate));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                //model.IdContract =Convert.ToInt32(db.GetParameterValue(cmd, "ID_CONTRACT").ToString());
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

        public ContractViewModel GetContract(UserInfo user, ContractViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CONTRACT");
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.String, MakeDbNull(filter.IdContract));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdContract = dReader["ID_CONTRACT"].GetValue<int>();
                    filter.DocId = dReader["DOC_ID"].GetValue<int>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.ContractName = dReader["CONTRACT_NAME"].GetValue<string>();
                    filter.StartDate = dReader["START_DATE"].GetValue<DateTime>();
                    filter.EndDate = dReader["END_DATE"].GetValue<DateTime>();
                    filter.Duration = dReader["DURATION"].GetValue<int>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
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

        public List<SelectListItem> ListContractAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CONTRACT_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_CONTRACT"].GetValue<string>(),
                            Text = reader["CONTRACT_NAME"].GetValue<string>()
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
