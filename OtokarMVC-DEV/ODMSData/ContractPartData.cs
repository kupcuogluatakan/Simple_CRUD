using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.ContractPart;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class ContractPartData : DataAccessBase
    {
        public ContractPartData()
        { }

        public List<ContractPartListModel> ListContractPart(UserInfo user, ContractPartListModel filter, out int totalCount)
        {
            var retVal = new List<ContractPartListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CONTRACT_PART");
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, MakeDbNull(filter.IdContract));
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(filter.PartCode));
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(filter.AdminDesc));
                AddPagingParameters(cmd, filter);
                //AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var contractPartListModel = new ContractPartListModel
                        {
                            IdContract = reader["ID_CONTRACT"].GetValue<int>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            AdminDesc = reader["ADMIN_DESC"].GetValue<string>(),
                            IdPart = reader["ID_PART"].GetValue<int>(),
                            IdContractPart = reader["ID_CONTRACT_PART"].GetValue<int>()
                        };

                        retVal.Add(contractPartListModel);
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

        public void DMLContractPart(UserInfo user, ContractPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CONTRACT_PART");
                db.AddInParameter(cmd, "ID_CONTRACT_PART", DbType.Int32, model.IdContractPart);
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, model.IdContract);
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, model.IdPart);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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

        public ContractPartViewModel GetContractPart(UserInfo user, ContractPartViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CONTRACT_PART");
                db.AddInParameter(cmd, "ID_CONTRACT_PART", DbType.String, MakeDbNull(filter.IdContract));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdContract = dReader["ID_CONTRACT"].GetValue<int>();
                    filter.IdContractPart = dReader["ID_CONTRACT_PART"].GetValue<int>();
                    filter.IdPart = dReader["ID_PART"].GetValue<int>();
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
                    filter.AdminDesc = dReader["ADMIN_DESC"].GetValue<string>();
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
