using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.SparePartGuaranteeAuthorityNeed;
using System;

namespace ODMSData
{
    public class SparePartGuaranteeAuthorityNeedData : DataAccessBase
    {
        public List<SparePartGuaranteeAuthorityNeedListModel> ListSparePartGuaranteeAuthorityNeeds(ODMSCommon.Security.UserInfo user, SparePartGuaranteeAuthorityNeedListModel referenceListModel, out int totalCount)
        {
            var retVal = new List<SparePartGuaranteeAuthorityNeedListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_GUARANTEE_AUTHORITY_NEED");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(referenceListModel.PartCode));
                db.AddInParameter(cmd, "GUARANTEE_NEED", DbType.Int32, referenceListModel.GuaranteeAuthorityNeedSearch == 2 ? -1 : referenceListModel.GuaranteeAuthorityNeedSearch);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(referenceListModel.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(referenceListModel.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, referenceListModel.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(referenceListModel.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var sparePartGuaranteeAuthorityNeedListModel = new SparePartGuaranteeAuthorityNeedListModel
                        {
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            GuaranteeAuthorityNeed = reader["GUARANTEE_AUTHORITY_NEED"].GetValue<bool>(),
                            GuaranteeAuthorityNeedName = reader["GUARANTEE_AUTHORITY_NEED_NAME"].GetValue<string>()
                        };
                        retVal.Add(sparePartGuaranteeAuthorityNeedListModel);
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

        public void DMLSparePartGuaranteeAuthorityNeed(ODMSCommon.Security.UserInfo user, SparePartGuaranteeAuthorityNeedViewModel sparePartGuaranteeAuthorityNeedModel)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_GUARANTEE_AUTHORITY_NEED");
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, sparePartGuaranteeAuthorityNeedModel.CommandType);
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(sparePartGuaranteeAuthorityNeedModel.PartId));
                db.AddInParameter(cmd, "GUARANTEE_AUTHORITY_NEED", DbType.Int32, MakeDbNull(sparePartGuaranteeAuthorityNeedModel.GuaranteeAuthorityNeed));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                sparePartGuaranteeAuthorityNeedModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                sparePartGuaranteeAuthorityNeedModel.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (sparePartGuaranteeAuthorityNeedModel.ErrorNo > 0)
                    sparePartGuaranteeAuthorityNeedModel.ErrorMessage = ResolveDatabaseErrorXml(sparePartGuaranteeAuthorityNeedModel.ErrorMessage);
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
