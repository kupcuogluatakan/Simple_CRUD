using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ClaimSupplierPart;
using System.Data.Common;

namespace ODMSData
{
    public class ClaimSupplierPartData : DataAccessBase
    {
        public List<SelectListItem> ListClaimSupplierCodesAsSelectListItem(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_SUPPLIER_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["SUPPLIER_CODE"].GetValue<string>(),
                            Text = reader["SUPPLIER_NAME"].GetValue<string>()
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

        public List<ClaimSupplierPartListModel> ListClaimSupplierPart(UserInfo user,ClaimSupplierPartListModel filter, out int totalCount)
        {
            var retVal = new List<ClaimSupplierPartListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_SUPPLIER_PART");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, filter.SupplierCode);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimSupplierPartListModel = new ClaimSupplierPartListModel
                        {
                            SupplierCode = reader["SUPPLIER_CODE"].GetValue<string>(),
                            SupplierName = reader["SUPPLIER_NAME"].GetValue<string>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>()
                        };
                        retVal.Add(claimSupplierPartListModel);
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

        public bool DeleteAllClaimSupplierPart(string supplierCode)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DELETE_ALL_CLAIM_SUPPLIER_PART");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, supplierCode);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                int errorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (errorNo > 0)
                {
                    return false;
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
            return true;
        }

        public void DMLClaimSupplierPart(UserInfo user, ClaimSupplierPartViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CLAIM_SUPPLIER_PART");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, model.SupplierCode);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, model.PartId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SupplierCode = db.GetParameterValue(cmd, "SUPPLIER_CODE").GetValue<string>();
                model.PartId = db.GetParameterValue(cmd, "PART_ID").GetValue<int>();
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

        public ClaimSupplierPartViewModel GetClaimSupplierPart(UserInfo user, long partId, string supplierCode)
        {
            ClaimSupplierPartViewModel viewModel = new ClaimSupplierPartViewModel();

            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CLAIM_SUPPLIER_PART");
                db.AddInParameter(cmd, "SUPPLIER_CODE", DbType.String, MakeDbNull(supplierCode));
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    viewModel.PartId = dReader["PartId"].GetValue<int>();
                    viewModel.PartCode = dReader["PartCode"].GetValue<string>();
                    viewModel.PartName = dReader["PartName"].GetValue<string>();
                    viewModel.IsActive = dReader["IsActive"].GetValue<bool>();
                    viewModel.UpdateDate = dReader["UpdateDate"].GetValue<DateTime>();
                    viewModel.SupplierCode = dReader["SupplierCode"].GetValue<string>();
                    viewModel.SupplierName = dReader["SupplierName"].GetValue<string>();
                }

                return viewModel;
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
        }
    }
}
