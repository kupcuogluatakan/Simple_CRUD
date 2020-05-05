using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;
using ODMSCommon;
using ODMSModel.LabourType;
using ODMSModel.ViewModel;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class LabourTypeData : DataAccessBase
    {
        public List<LabourTypeListModel> ListLabourTypes(UserInfo user,LabourTypeListModel referenceModel, out int totalCnt)
        {
            var result = new List<LabourTypeListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_TYPES");
                db.AddInParameter(cmd, "LABOUR_TYPE_NAME", DbType.String, MakeDbNull(referenceModel.Name));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(referenceModel.SearchIsActive));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new LabourTypeListModel
                        {
                            Id = reader["ID_LABOUR_TYPE"].GetValue<int>(),
                            Name = reader["LABOUR_TYPE_DESC"].GetValue<string>(),
                            Description = reader["ADMIN_DESC"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
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

            return result;
        }

        public LabourTypeDetailModel GetLabourType(UserInfo user,LabourTypeDetailModel referenceModel)
        {
            System.Data.Common.DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LABOUR_TYPE");
                db.AddInParameter(cmd, "ID_LABOUR_TYPE", DbType.Int32, MakeDbNull(referenceModel.Id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    referenceModel.Name = reader["LABOUR_TYPE_DESC"].GetValue<string>();
                    referenceModel.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                    referenceModel.Description = reader["ADMIN_DESC"].GetValue<string>();
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    referenceModel.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "LABOUR_TYPE_DESC");
                    referenceModel.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(referenceModel.MultiLanguageName);
                    referenceModel.MultiLanguageName.MultiLanguageContentAsText = referenceModel.MultiLanguageContentAsText;
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
            return referenceModel;
        }

        public void DMLLabourType(UserInfo user,LabourTypeDetailModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_LABOUR_TYPE");
                db.AddParameter(cmd, "LABOUR_TYPE_ID", DbType.Int32, ParameterDirection.InputOutput, "LABOUR_TYPE_ID", DataRowVersion.Default, model.Id);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "ADMIN_DESC", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.Id = db.GetParameterValue(cmd, "LABOUR_TYPE_ID").GetValue<int>();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.LabourType_Error_NullId;
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

        public List<LabourTypeVatRatioModel> GetVatRatioList()
        {
            var result = new List<LabourTypeVatRatioModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VAT_RATIOS");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, null); // Bu parametre sp'de kullanilmiyor cunku tablo multi-lang degil (sp'den kaldirilmali)
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, null);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, null);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, 0);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, null);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new LabourTypeVatRatioModel {VatRatio = reader["VAT_RATIO"].GetValue<double>()};
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

        //Combobox için ekledim => Taner
        public List<SelectListItem> ListLabourTypesAsSelectListItems(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_LABOUR_TYPE_SHORT");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem { Value = reader["ID_LABOUR_TYPE"].GetValue<string>(), Text = reader["LABOUR_TYPE_DESC"].GetValue<string>() };
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
    }
}
