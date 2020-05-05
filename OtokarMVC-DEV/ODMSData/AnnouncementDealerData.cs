using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSData
{
    public class AnnouncementDealerData : DataAccessBase
    {
        public List<SelectListItem> ListAnnouncementDealersIncluded(int announcementId)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ANNOUNCEMENT_DEALER_INCLUDED");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int64, announcementId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_SHRT_NAME"].GetValue<string>()
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
        public List<SelectListItem> ListAnnouncementDealersExcluded(int announcementId, int customerGroupId, string vehicleModelId)
        {
            var list = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_ANNOUNCEMENT_DEALER_EXCLUDED");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int64, announcementId);
                db.AddInParameter(cmd, "ID_CUSTOMER_GROUP", DbType.Int32, MakeDbNull(customerGroupId));
                db.AddInParameter(cmd, "MODEL_KOD", DbType.String, MakeDbNull(vehicleModelId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_SHRT_NAME"].GetValue<string>()
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

        public void Save(UserInfo user, ODMSModel.AnnouncementDealer.AnnouncementDealerModel model)
        {
            Func<List<int>, string> getSerializedString = c =>
            {
                if (c == null || c.Count == 0) return String.Empty;
                var sb = new StringBuilder();
                c.ForEach(x => sb.Append(x).Append(","));
                var result = sb.ToString();
                return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
            };

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_ANNOUNCEMENT_DEALERS");
                db.AddInParameter(cmd, "ID_ANNOUNCEMENT", DbType.Int32, MakeDbNull(model.AnnouncementId));
                db.AddInParameter(cmd, "DEALER_IDS", DbType.String, MakeDbNull(getSerializedString(model.DealerList)));
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
        }
    }
}
