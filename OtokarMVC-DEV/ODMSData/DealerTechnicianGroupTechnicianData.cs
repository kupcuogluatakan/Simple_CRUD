using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerTechnicianGroupTechnicianData : DataAccessBase
    {
        public List<SelectListItem> ListDealerTechnicianGroupsAsSelectListItem(UserInfo user)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_TECHNICIAN_GROUP_COMBO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["TECHNICIAN_GROUP_NAME"].GetValue<string>(),
                            Value = reader["ID_DEALER_TECHNICIAN_GROUP"].GetValue<int>().ToString(),
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
        public List<SelectListItem> ListDealerTechnicianGroupsExcluded(UserInfo user, int dealerTechnicanGroupId)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_TECHNICIAN_GROUP_TECHNICIAN_EXCLUDED");
                db.AddInParameter(cmd, "ID_DEALER_TECHNICIAN_GROUP", DbType.Int32, MakeDbNull(dealerTechnicanGroupId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["TECHNICAIAN_NAME"].GetValue<string>(),
                            Value = reader["ID_DMS_USER"].GetValue<int>().ToString(),
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
        public List<SelectListItem> ListDealerTechnicianGroupsIncluded(UserInfo user, int dealerTechnicanGroupId)
        {
            var list = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_TECHNICIAN_GROUP_TECHNICIAN_INCLUDED");
                db.AddInParameter(cmd, "ID_DEALER_TECHNICIAN_GROUP", DbType.Int32, MakeDbNull(dealerTechnicanGroupId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = reader["TECHNICAIAN_NAME"].GetValue<string>(),
                            Value = reader["ID_DMS_USER"].GetValue<int>().ToString(),
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

        public ODMSModel.ModelBase SaveTechnicianGroupTechnicians(UserInfo user, int dealerTechnicianGroupId, List<int> TechinicanIds)
        {
            Func<List<int>, string> getSerializedString = c =>
            {
                if (c == null)
                    return string.Empty;
                var sb = new StringBuilder();
                c.ForEach(x => sb.Append(x).Append(","));
                var result = sb.ToString();
                return result.Length > 0 ? result.Substring(0, result.Length - 1) : string.Empty;
            };
            var model = new ODMSModel.ModelBase();
            //if (TechinicanIds==null || !TechinicanIds.Any())
            //    return model;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_DEALER_TECHNICIAN_GROUP_TECHNICIANs");
                db.AddInParameter(cmd, "ID_DEALER_TECHNICIAN_GROUP", DbType.Int32, MakeDbNull(dealerTechnicianGroupId));
                db.AddInParameter(cmd, "TECHNICIAN_IDS", DbType.String, MakeDbNull(getSerializedString(TechinicanIds)));
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
