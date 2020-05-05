using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using ODMSCommon;
using ODMSModel.DealerTechnicianGroup;
using ODMSCommon.Resources;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class DealerTechnicianGroupData : DataAccessBase
    {
        public List<DealerTechnicianGroupListModel> ListDealerTechnicianGroups(UserInfo user,DealerTechnicianGroupListModel filter, out int total)
        {
            var retVal = new List<DealerTechnicianGroupListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_TECHNICIAN_GROUP");
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "VEHICLE_MODEL_KOD", DbType.String, MakeDbNull(filter.VehicleModelKod));
                db.AddInParameter(cmd, "WORKSHOP_TYPE_ID", DbType.Int32, MakeDbNull(filter.WorkshopTypeId));
                db.AddInParameter(cmd, "TECHNICIAN_GROUP_NAME", DbType.String, MakeDbNull(filter.TechnicianGroupName));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new DealerTechnicianGroupListModel
                        {
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            DealerTechnicianGroupId = reader["DEALER_TECHNICIAN_GROUP_ID"].GetValue<int>(),
                            Description = reader["DESCRIPTION"].ToString(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].ToString(),
                            TechnicianGroupName = reader["TECHNICIAN_GROUP_NAME"].ToString(),
                            VehicleModelKod = reader["VEHICLE_MODEL_KOD"].ToString(),
                            WorkshopTypeId = reader["WORKSHOP_TYPE_ID"].GetValue<int>(),
                            WorkshopTypeName = reader["WORKSHOP_TYPE_NAME"].ToString()
                        };
                        retVal.Add(item);
                    }
                    reader.Close();
                }
                total = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void DMLDealerTechnicianGroup(UserInfo user, DealerTechnicianGroupViewModel model)
        {
            string vehicleGroupIdList = null;
            if (model.VehicleModelKodList != null)
            {
                for (int i = 0; i < model.VehicleModelKodList.Count(); i++)
                {
                    if (i == 0) vehicleGroupIdList = model.VehicleModelKodList[i].ToString();
                    else
                    {
                        vehicleGroupIdList = vehicleGroupIdList + "," + model.VehicleModelKodList[i].ToString();
                    }
                }
            }
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_DEALER_TECHNICIAN_GROUP");
                db.AddInParameter(cmd, "DEALER_TECHNICIAN_GROUP_ID", DbType.Int32, MakeDbNull(model.DealerTechnicianGroupId));
                db.AddInParameter(cmd, "WORKSHOP_TYPE_ID", DbType.Int32, MakeDbNull(model.WorkshopTypeId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "VEHICLE_MODEL_KOD_LIST", DbType.String, vehicleGroupIdList);
                db.AddInParameter(cmd, "VEHICLE_MODEL_KOD_OLD", DbType.Int32, MakeDbNull(model.VehicleModelKodOld));//For Update 
                db.AddInParameter(cmd, "TECHNICIAN_GROUP_NAME", DbType.String, MakeDbNull(model.TechnicianGroupName));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.DealerTechnicianGroup_Error_NullId;
                else if (model.ErrorNo > 0)
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

        public DealerTechnicianGroupViewModel GetDealerTechnicianGroup(UserInfo user, DealerTechnicianGroupViewModel filter)
        {
            var list = new List<DealerTechnicianGroupViewModel>();

            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_TECHNICIAN_GROUP");
                db.AddInParameter(cmd, "DEALER_TECHNICIAN_GROUP_ID", DbType.Int32, MakeDbNull(filter.DealerTechnicianGroupId));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    var entity = new DealerTechnicianGroupViewModel();
                    entity.DealerId = dReader["DEALER_ID"].GetValue<int>();
                    entity.DealerName = dReader["DEALER_NAME"].ToString();
                    entity.DealerTechnicianGroupId = dReader["DEALER_TECHNICIAN_GROUP_ID"].GetValue<int>();
                    entity.Description = dReader["DESCRIPTION"].ToString();
                    entity.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    entity.IsActiveName = dReader["IS_ACTIVE_NAME"].ToString();
                    entity.VehicleModelKod = dReader["VEHICLE_MODEL_KOD"].ToString();
                    entity.TechnicianGroupName = dReader["TECHNICIAN_GROUP_NAME"].ToString();
                    entity.VehicleModelKod = dReader["VEHICLE_MODEL_KOD"].ToString();
                    entity.WorkshopTypeId = dReader["WORKSHOP_TYPE_ID"].GetValue<int>();
                    entity.WorkshopTypeName = dReader["WORKSHOP_TYPE_NAME"].ToString();

                    list.Add(entity);
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

            var firstOrDefault = (from item in list
                                  group item by new { item.DealerId, item.DealerName, item.DealerTechnicianGroupId, item.Description, item.IsActive, item.IsActiveName, item.TechnicianGroupName, item.WorkshopTypeId, item.WorkshopTypeName } into g
                                  select new DealerTechnicianGroupViewModel()
                                  {
                                      DealerId = g.Key.DealerId,
                                      DealerName = g.Key.DealerName,
                                      DealerTechnicianGroupId = g.Key.DealerTechnicianGroupId,
                                      Description = g.Key.Description,
                                      IsActive = g.Key.IsActive,
                                      IsActiveName = g.Key.IsActiveName,
                                      TechnicianGroupName = g.Key.TechnicianGroupName,
                                      WorkshopTypeId = g.Key.WorkshopTypeId,
                                      WorkshopTypeName = g.Key.WorkshopTypeName,
                                      VehicleModelKodList = g.Select(x => x.VehicleModelKod).ToList()
                                  }).FirstOrDefault();
            return firstOrDefault;
        }

        public int GetDealerVehicleGroupRelationCount(int dealerId, int vehicleGroupId)
        {
            int count = 0;
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_VEHICLE_GRP_REL_COUNT");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "VEHICLE_GROUP_ID", DbType.Int32, MakeDbNull(vehicleGroupId));
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    count = dReader["VALUE"].GetValue<int>();
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
            return count;
        }
    }
}
