using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using System.Web.Mvc;
using ODMSModel.ClaimPartListApprove;
using System.Linq;
using ODMSModel;
using System;

namespace ODMSData
{
    public class ClaimPeriodPartListApproveData : DataAccessBase
    {
        public List<SelectListItem> GetClaimPeriods()
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_PERIOD_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["Text"].GetValue<string>(),
                            Value = reader["Value"].GetValue<int>().ToString(),
                            Selected = reader["Selected"].GetValue<bool>()
                        };
                        list.Add(item);
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

            return list;
        }
        public List<ClaimPartListApproveListModel> ListClaimRecallPeriodPart(UserInfo user, int id)
        {
            List<ClaimPartListApproveListModel> retVal = new List<ClaimPartListApproveListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_PARTS_LIST");
                db.AddInParameter(cmd, "ID_CLAIM_RECALL_PERIOD", DbType.String, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimRecallPeriodPartListModel = new ClaimPartListApproveListModel
                        {
                            Type = reader["Type"].GetValue<sbyte>(),
                            PartId = reader["PartId"].GetValue<int>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Quantity = reader["Quantity"].GetValue<decimal>()
                        };
                        retVal.Add(claimRecallPeriodPartListModel);
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
            var list = new List<ClaimPartListApproveListModel>();
            var existingItems = new List<ClaimPartListApproveListModel>();
            foreach (var item in retVal.Where(c => c.Type == 1))
            {
                var listPart = retVal.SingleOrDefault(c => c.Type == 2 && c.PartId == item.PartId);
                if (listPart != null)
                {
                    item.IsSelected = true;
                    item.Status = true;
                    existingItems.Add(listPart);
                    list.Add(item);
                }
                else
                {
                    item.IsSelected = false;
                    item.Status = false;
                    list.Add(item);
                }
            }
            foreach (var item in existingItems)
            {
                var listpart = retVal.SingleOrDefault(c => c.Type == 2 && c.PartId == item.PartId);
                retVal.Remove(listpart);
            }

            foreach (var item in retVal.Where(c => c.Type == 2))
            {
                item.IsSelected = true;
                item.Status = true;
                list.Add(item);
            }
            return list;
        }
        public List<ClaimDismantledPartInfoModel> ListDismantledParts(UserInfo user, int claimPeriodId, long partId)
        {
            List<ClaimDismantledPartInfoModel> retVal = new List<ClaimDismantledPartInfoModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CLAIM_DISMANTLED_PART_INFO");
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.String, MakeDbNull(claimPeriodId));
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(partId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var claimDismantledPartInfoModel = new ClaimDismantledPartInfoModel
                        {
                            WorkOrderId = reader["WorkOrderId"].GetValue<long>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            Quantity = reader["Quantity"].GetValue<decimal>(),
                            DismantledPartSerialNo = reader["DismantledPartSerialNo"].GetValue<string>(),
                            DealerShortName = reader["DealerShortName"].GetValue<string>()
                        };
                        retVal.Add(claimDismantledPartInfoModel);
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
        public ModelBase Save(UserInfo user, int claimPeriodId, List<long> selectedParts, List<long> notSelectedParts)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_APROVE_CLAIM_RECALL_PART_LIST");
                db.AddInParameter(cmd, "CLAIM_RECALL_PERIOD_ID", DbType.Int32, claimPeriodId);
                db.AddInParameter(cmd, "SELECTED_PART_IDS", DbType.String, (selectedParts == null ? string.Empty : string.Join(",", selectedParts)));
                db.AddInParameter(cmd, "NOT_SELECTED_PART_IDS", DbType.String, (notSelectedParts == null ? string.Empty : string.Join(",", notSelectedParts)));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
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

            return model;

        }
    }
}
