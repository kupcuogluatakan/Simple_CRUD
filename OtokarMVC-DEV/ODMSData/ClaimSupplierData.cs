using ODMSCommon.Security;
using ODMSModel.ClaimSupplier;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;

namespace ODMSData
{
    public class ClaimSupplierData : DataAccessBase
    {
        public List<ClaimSupplierListModel> ListClaimSupplier(UserInfo user,ClaimSupplierListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimSupplierListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_SUPPLIER");
                // *** DİKKAT! *** olmayabilir
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, filter.SupplierCode);
                db.AddInParameter(cmd, "SUPPLIER_NAME", DbType.String, filter.SupplierName);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimSupplierListModel = new ClaimSupplierListModel
                        {
                            SupplierCode = reader["SUPPLIER_CODE"].GetValue<string>(),
                            SupplierName = reader["SUPPLIER_NAME"].GetValue<string>(),
                            ClaimRackCode = reader["CLAIM_RACK_CODE"].GetValue<string>()
                        };

                        retVal.Add(claimSupplierListModel);
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

        public ClaimSupplierViewModel GetClaimSupplier(UserInfo user, ClaimSupplierViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_SUPPLIER");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, MakeDbNull(filter.SupplierCode));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    //claimSupplierModel.SupplierCode = dReader["SUPPLIER_CODE"].GetValue<string>();
                    filter.SupplierName = dReader["SUPPLIER_NAME"].GetValue<string>();
                    filter.ClaimRackCode = dReader["CLAIM_RACK_CODE"].GetValue<string>();
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

        public void DMLClaimSupplier(UserInfo user, ClaimSupplierViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_SUPPLIER");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, model.SupplierCode);
                db.AddInParameter(cmd, "SUPPLIER_NAME", DbType.String, model.SupplierName);
                db.AddInParameter(cmd, "CLAIM_RACK_CODE", DbType.String, model.ClaimRackCode);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                //db.AddInParameter(cmd, "ML_CONTENT", DbType.String, MakeDbNull(customerDiscountModel.MultiLanguageContentAsText));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SupplierCode = db.GetParameterValue(cmd, "SUPPLIER_CODE").ToString();
                model.SupplierName = db.GetParameterValue(cmd, "SUPPLIER_NAME").ToString();
                model.ClaimRackCode = db.GetParameterValue(cmd, "CLAIM_RACK_CODE").ToString();
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
