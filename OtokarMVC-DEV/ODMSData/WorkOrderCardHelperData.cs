using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel;
using ODMSModel.CampaignRequest;
using ODMSModel.WorkOrderCard;
using System.Data.Common;
using ODMSModel.WorkOrderCard.CampaignDetail;

namespace ODMSData
{
    public class WorkOrderCardHelperData : DataAccessBase
    {
        public List<SelectListItem> ListDetailProcessTypes(UserInfo user, string indicatorTypeCode, long workOrderId)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PROCESS_TYPE_BY_INDICATOR_TYPE_COMBO", rowMapper, MakeDbNull(workOrderId), MakeDbNull(indicatorTypeCode), MakeDbNull(user.LanguageCode)).ToList();

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

        public List<SelectListItem> ListVehicleHourMaints(UserInfo user, int vehicleId)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_VEHICLE_HOUR_MAINTS_COMBO", rowMapper, MakeDbNull(vehicleId), MakeDbNull(user.LanguageCode)).ToList();
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


        public List<SelectListItem> ListDetailProcessTypes(UserInfo user)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_WORK_ORDER_INDICATOR_TYPES", rowMapper, MakeDbNull(user.LanguageCode)).ToList();

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

        public PartReservationDTO GetPartReservationData(UserInfo user, long workOrderDetailId)
        {
            var dto = new PartReservationDTO();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_DETAIL_PART_RESERVATION_DATA",
                    workOrderDetailId,
                   null, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dto.ProcessTypeList.Add(new SelectListItem
                        {
                            Selected = reader.GetBoolean(2),
                            Text = reader.GetString(1),
                            Value = reader.GetString(0)
                        });
                    }
                    reader.Close();
                }
                dto.CurrentProcessType = db.GetParameterValue(cmd, "CURRENT_PROCESS_TYPE").ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return dto;
        }

        public ModelBase ReservDetailParts(UserInfo user, long workOrderDetailId, string processType, out List<PartReservationInfo> reservationInfos)
        {
            var model = new ModelBase();
            var list = new List<PartReservationInfo>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_ALL_PARTS_RESERVATION",
                    workOrderDetailId, processType, MakeDbNull(user.LanguageCode), user.UserId, null, null);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    int errNo;
                    model.ErrorNo = int.TryParse(db.GetParameterValue(cmd, "ERROR_NO").ToString(), out errNo) ? errNo : 0;
                    if (model.ErrorNo > 0)
                    {
                        model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            list.Add(new PartReservationInfo
                            {
                                RequiredQuantity = reader.GetDecimal(0),
                                ReservedQuantity = reader.GetDecimal(1),
                                PartName = reader.GetString(2),
                                PartCode = reader.GetString(3)
                            });
                        }
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
            reservationInfos = list;
            return model;
        }

        public ModelBase CancelVehicleLeave(UserInfo user, long workOrderId, string reason)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CANCEL_WORK_ORDER_VEHICLE_LEAVE",
                    workOrderId, reason, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;

        }

        public ModelBase PickDetailParts(UserInfo user, long id, long workOrderDetailId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_PICK_WORK_ORDER_DETAIL_PARTS",
                    id, workOrderDetailId, MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }


        public string GetTechicalDescription(long workOrderDetailId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_DETAIL_DESCRIPTION",
                    workOrderDetailId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }

        public ModelBase UpdateTechicalDescription(UserInfo user, long workOrderDetailId, string description)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_DESCRIPTION",
                    workOrderDetailId, description, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }


        public List<PartReturnModel> ListDetailPartReturnItems(UserInfo user, long workOrderId)
        {
            List<PartReturnModel> list;
            try
            {
                CreateDatabase();
                var rowMapper =
                    MapBuilder<PartReturnModel>.MapAllProperties()
                        .DoNotMap(c => c.RequiredQuantity)
                        .DoNotMap(c => c.ReservedQuantity)
                        .Build();
                list = db.ExecuteSprocAccessor("P_LIST_WORK_ORDER_CARD_DETAIL_PARTS_RETURN", rowMapper, workOrderId, MakeDbNull(user.LanguageCode)).ToList();

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

        //
        //
        public List<ModelBase> ReturnDetailParts(UserInfo user, List<PartReturnModel> detailList, long workOrderDetailId)
        {
            var list = new List<ModelBase>(detailList.Count);
            CreateDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                long masterId = 0;
                try
                {

                    foreach (var partReturnModel in detailList.Where(c => c.ReturnedQuantity > 0))
                    {
                        var cmd = db.GetStoredProcCommand("P_RETURN_WORK_ORDER_DETAIL_PARTS", workOrderDetailId,
                            partReturnModel.PartId, partReturnModel.ReturnedQuantity, MakeDbNull(masterId),
                            MakeDbNull(user.UserId), null, null
                            );
                        db.ExecuteNonQuery(cmd, transaction);
                        var modelBase = new ModelBase();

                        modelBase.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                        if (modelBase.ErrorNo > 0)
                        {
                            modelBase.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                            modelBase.ErrorMessage = ResolveDatabaseErrorXml(modelBase.ErrorMessage);
                            list.Add(modelBase);
                        }
                        else
                              if (masterId == 0)
                            masterId = long.Parse(db.GetParameterValue(cmd, "ID_WORK_ORDER_PICKING_MST").ToString());

                    }
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }



            return list;

        }

        public string GetWorkOrderContactInfo(long workOrderId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CONTACT_INFO", workOrderId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }

        public ModelBase UpdateWorkOrderContactInfo(UserInfo user, long workOrderId, string note)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CONTACT_INFO",
                    workOrderId, note, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }

        public List<SelectListItem> ListWorkOrderStats(UserInfo user)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_WORK_ORDER_STAT_COMBO", rowMapper, MakeDbNull(user.LanguageCode)).ToList();
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

        public List<PartModel> ListCampaignRequestDetails(UserInfo user, long workOrderDetailId, CampaignRequestViewModel model)
        {
            List<PartModel> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PartModel>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_WORK_ORDER_CARD_CAMPAIGN_REQUEST_DETAILS", rowMapper, workOrderDetailId, MakeDbNull(user.LanguageCode)).ToList();
                model = new CampaignRequestData().GetCampaignRequest(user, model);
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



        public string GetWorkOrderCampaignDenyReason(long workOrderId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CAMPAIGN_DENY_REASON",
                    workOrderId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }
        public string GetWorkOrderCampaignDenyDealerReason(long workOrderId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CAMPAIGN_DENY_DEALER_REASON",
                    workOrderId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }

        
        public ModelBase CancelCampaignRejections(UserInfo user, long workOrderId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CANCEL_CAMPAIGN_REJECTIONS", workOrderId, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }

        /// <summary>
        /// İş Kartında kampanya penceresinden eğer bir kampanya eklemesi yapılmış ise reddedilenler arasından silinme işlemini yapar.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="workOrderId"></param>
        /// <param name="deniedCamps">String tipinde tek bir kampanya kodudur.</param>
        /// <returns></returns>
        public ModelBase UpdateDeniedCampaigns(UserInfo user, long workOrderId, string deniedCamps)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_REMOVE_DENIED_CAMPAIGN", workOrderId, MakeDbNull(user.UserId), null, null, deniedCamps);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }

        public ModelBase UpdateWorkOrderCampaignDenyReason(UserInfo user, long workOrderId, string denyReason,string deniedCamps)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CAMPAIGN_DENY_REASON", workOrderId, denyReason, MakeDbNull(user.UserId), null, null, deniedCamps);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }
        public ModelBase UpdateWorkOrderCampaignDenyDealerReason(UserInfo user, long workOrderId, string denyReason,string deniedCampaigns)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CAMPAIGN_DENY_DEALER_REASON",
                    workOrderId, denyReason, MakeDbNull(user.UserId), null, null, deniedCampaigns);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }


        public ModelBase CheckForOtherCampaigns(long id)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CHECK_WORK_ORDER_CARD_CAMPAIGNS_FOR_DETAIL_ADDITION",
                    id, null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = bool.Parse(db.GetParameterValue(cmd, "RESULT").ToString()) ? 1 : 0;
                if (model.ErrorNo == 0)
                {
                    model.ErrorNo = 1;
                    model.ErrorMessage = db.GetParameterValue(cmd, "REASON").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else
                    model.ErrorNo = 0;

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

        public ModelBase CreateCampaignRequest(UserInfo user, long workOrderDetailId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CREATE_WORK_ORDER_DETAIL_CAMPAIGN_REQUEST",
                    workOrderDetailId, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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

            return model;
        }
        public bool? CheckPartLastLevel(long workOrderDetailId, string csPartIds)
        {
            bool? result;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_CHECK_WORK_ORDER_CARD_PART_LAST_LEVEL", workOrderDetailId, csPartIds, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                result = db.GetParameterValue(cmd, "RESULT").GetValue<bool?>();

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

        public long GetLastLevelPartId(long partId)
        {
            long newPartId = 0;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_LAST_LEVEL_PART_ID", partId);
                CreateConnection(cmd);
                newPartId = cmd.ExecuteScalar().GetValue<long>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return newPartId;
        }

        public string GetPartsAsCsv(string id, string type)
        {
            string csvParts;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PARTS_AS_STRING",
                    id, type, null);
                CreateConnection(cmd);
                csvParts = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return csvParts;
        }

        public ChangeProcessTypeModel GetProcessTypeData(UserInfo user, long workOrderDetailId)
        {
            var model = new ChangeProcessTypeModel();
            var dto = GetPartReservationData(user, workOrderDetailId);
            model.CurrentProcessType = dto.CurrentProcessType;
            model.ProcessTypeList = dto.ProcessTypeList;
            return model;
        }


        public ModelBase UpdateProcessType(UserInfo user, long workOrderDetailId, string processType, bool confirmed)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_DETAIL_PROCESS_TYPE",
                    workOrderDetailId, processType, confirmed, MakeDbNull(user.UserId), null, null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());


                if (model.ErrorNo.In(1, 2, 4))
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                }
                else if (model.ErrorNo == 3)
                {
                    string desc = db.GetParameterValue(cmd, "DESC").ToString();
                    if (!string.IsNullOrEmpty(desc))
                    {
                        var items = desc.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                        var parts = new StringBuilder();
                        foreach (var item in items)
                        {
                            var arr = item.Split(new[] { "@" }, StringSplitOptions.RemoveEmptyEntries);
                            var partCode = arr[0];
                            bool hasPickingOrder = arr[1] == "1";
                            if (hasPickingOrder)
                            {
                                parts.Append(partCode).Append(",");
                            }
                        }
                        if (parts.Length > 0)
                        {
                            model.ErrorMessage =
                                string.Format(
                                    CommonUtility.GetResourceValue("WorkOrderCard_ProcessTypeWithPickOrders"),
                                    parts.ToString(0, parts.Length - 1));
                        }

                    }
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
            return model;
        }

        public DateTime? GetVehicleLeaveDate(long workOrderId)
        {
            DateTime? vehicleLeaveDate;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_VEHICLE_LEAVE_DATE", workOrderId);
                CreateConnection(cmd);
                vehicleLeaveDate = cmd.ExecuteScalar().GetValue<DateTime?>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return vehicleLeaveDate;
        }

        public List<SelectListItem> ListRemovableParts(UserInfo user, long partId)
        {
            List<SelectListItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<SelectListItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_REMOVABLE_PARTS_COMBO", rowMapper, partId, MakeDbNull(user.LanguageCode)).ToList();
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


        public PartRemovalDto GetPartRemovalDto(UserInfo user, long workOrderDetailId, long partId)
        {
            var dto = new PartRemovalDto();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_DETAIL_PART_REMOVAL_DATA",
                    workOrderDetailId,
                    partId, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dto.DismentledPartId = reader["DismentledPartId"].GetValue<long?>();
                        dto.DismentledPartName = reader["DismentledPartName"].ToString();
                        dto.DismentledPartSerialNo = reader["DismentledPartSerialNo"].ToString();
                        dto.PartId = reader["PartId"].GetValue<long>();
                        dto.PartName = reader["PartName"].ToString();
                        dto.PartCode = reader["PartCode"].ToString();
                        dto.PartSerialNo = reader["PartSerialNo"].ToString();
                    }
                    reader.Close();
                }
                dto.WorkOrderDetailId = workOrderDetailId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return dto;
        }

        public ModelBase UpdateRemovalInfo(UserInfo user, PartRemovalDto dto)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CARD_DETAIL_PART_REMOVAL_DATA",
                    dto.WorkOrderDetailId, dto.PartId, dto.DismentledPartId, dto.DismentledPartSerialNo, dto.PartSerialNo, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }


        public ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult CheckVehicleLeaveMandatoryFields(UserInfo user, long workOrderId)
        {
            var result = 0;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_VEHICLE_LEAVE_STATUS",
                    workOrderId, MakeDbNull(user.UserId));
                CreateConnection(cmd);
                result = cmd.ExecuteScalar().GetValue<int>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            switch (result)
            {
                default:
                case 0:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.Success;
                case 1:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.ProcessTypeNotSet;
                case 2:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.DismentledPartsNotSet;
                case 3:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.IncompletedLaboursExists;
                case 4:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.NotPickedPartsExists;
                case 5:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.PdiResultsNotSet;
                case 6:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.PickingIsNotFinished;
                case 7:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.EmptyWorkOrderDeatail;
                case 8:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.EmptyTechDesc;
                case 9:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.NoDetailExists;
                case 10:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.WaitingPreApproval;
                case 11:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.NullKm;
                case 12:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.RequiredPartForGuarantee;
                case 13:
                    return ODMSCommon.CommonValues.WorkOrderCardVehicleLeaveResult.CouponMaintenanceKmControl;

            }
        }

        public List<MandatoryRemovalPart> ListMandatoryRemovalPart(UserInfo user, long workOrderId)
        {
            List<MandatoryRemovalPart> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<MandatoryRemovalPart>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_GET_WORK_ORDER_VEHICLE_LEAVE_PARTS", rowMapper, workOrderId, MakeDbNull(user.LanguageCode)).ToList();
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

        public ModelBase SendToWarrantyApproval(UserInfo user, long workOrderDetailId, string requestDescription, int warrantyStatus)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_MST", workOrderDetailId, MakeDbNull(requestDescription), warrantyStatus, MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());

                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public bool CheckWorkOrderDealer(long workOrderId, int dealerId, long invoiceid = 0)
        {
            bool result;
            try
            {
                CreateDatabase();
                CreateConnection();
                result = base.ExecSqlFunction<bool>("FN_WORK_ORDER_DEALER_CHECK", workOrderId, 0, dealerId, invoiceid);
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

        public void AddPdiPackage(UserInfo user, AddPdiPackageModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_ADD_WORK_ORDER_CARD_PDI_PACKAGE",
                    model.WorkOrderId,
                    model.TransmissionSerialNo,
                    model.DifferencialSerialNo,
                    MakeDbNull(model.PdiCheckNote),
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }


        public Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>> GetPdiResultData(UserInfo user, long workOrderId, string controlCode)
        {
            string controlName = string.Empty;
            var partList = new List<SelectListItem>();
            var breakDownList = new List<SelectListItem>();
            var resultList = new List<SelectListItem>();
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_RESULT_DATA", workOrderId, controlCode, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        controlName = reader.GetString(0);
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            partList.Add(new SelectListItem
                            {
                                Text = reader["PART_NAME"].ToString(),
                                Value = reader["PDI_PART_CODE"].ToString()
                            });
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            breakDownList.Add(new SelectListItem
                            {
                                Text = reader["BREAKDOWN_NAME"].ToString(),
                                Value = reader["PDI_BREAKDOWN_CODE"].ToString()
                            });
                        }
                    }
                    if (reader.NextResult())
                    {
                        while (reader.Read())
                        {
                            resultList.Add(new SelectListItem
                            {
                                Text = reader["RESULT_NAME"].ToString(),
                                Value = reader["PDI_RESULT_CODE"].ToString()
                            });
                        }
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


            var tupple = new Tuple<string, string, List<SelectListItem>, List<SelectListItem>, List<SelectListItem>>(controlCode, controlName, partList, breakDownList, resultList);
            return tupple;
        }

        public List<PdiResultItem> ListPdiResultItems(UserInfo user, long workOrderId)
        {
            List<PdiResultItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PdiResultItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_PDI_VEHICLE_RESULT_DET", rowMapper, workOrderId, MakeDbNull(user.LanguageCode)).ToList();

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

        public ModelBase SavePdiResult(UserInfo user, PdiResultModel dto, string type)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_PDI_VEHICLE_RESULT_DET",
                    dto.WorkOrderId, type, dto.ControlCode, dto.PartCode, dto.BreakDownCode, dto.ResultCode,
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public AddPdiPackageModel GetPdiPackageData(long workOrderId)
        {
            var model = new AddPdiPackageModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_MST_UPDATE_DATA", workOrderId);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.PdiCheckNote = reader["PdiCheckNote"].ToString();
                        model.TransmissionSerialNo = reader["TransmissionSerialNo"].ToString();
                        model.DifferencialSerialNo = reader["DifferencialSerialNo"].ToString();
                        model.ApprovalNote = reader["ApprovalNote"].ToString();
                        model.WorkOrderId = reader["WorkOrderId"].GetValue<long>();
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

            return model;
        }

        public void UpdatePdiPackage(UserInfo user, AddPdiPackageModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_UPDATE_WORK_ORDER_CARD_PDI_PACKAGE",
                    model.WorkOrderId,
                    model.TransmissionSerialNo,
                    model.DifferencialSerialNo,
                    MakeDbNull(model.PdiCheckNote),
                    MakeDbNull(user.UserId), MakeDbNull(user.LanguageCode), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
        }

        public ModelBase PdiSendToApproval(UserInfo user, long id)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEND_PDI_VEHICLE_RESULT_MST_APPROVAL", id, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public PdiPrintModel GetPdiPackageDetails(UserInfo user, long workOrderId)
        {
            var model = new PdiPrintModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PDI_PACKAGE_VEHICLE_DETAILS", workOrderId, user.LanguageCode);
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.Dealer = reader["DEALER_SHRT_NAME"].ToString();
                        model.Sender = reader["SENDER"].ToString();
                        model.Approver = reader["APPROVER"].ToString();
                        model.VinNo = reader["VIN_NO"].ToString();
                        model.EngineNo = reader["ENGINE_NO"].ToString();
                        model.TransmissionNo = reader["TRANSMISSION_SERIALNO"].ToString();
                        model.Status = reader["STATUS"].ToString();
                        model.DifferentialNo = reader["DIFFERENTAIL_SERIALNO"].ToString();
                        model.SenderNote = reader["PDI_CHECK_NOTE"].ToString();
                        model.ApproverNote = reader["APPROVAL_NOTE"].ToString();
                        model.PdiCreateDate = reader["CREATE_DATE"].GetValue<DateTime>();
                        model.IsControlled = reader["IS_CONTROLLED"].GetValue<bool>();

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

            model.Items = ListPdiResultItems(user,workOrderId);

            return model;
        }


        public ModelBase PrintLabels(UserInfo user, long workOrderDetailId, bool printAll)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_WORK_ORDR_CARD_DETAIL_PRINT_LABELS",
                    workOrderDetailId, printAll,
                    MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public ModelBase DeleteDetailItem(UserInfo user, long workOrderId, long workOrderDetailId, string type, long itemId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DELETE_WORK_ORDER_DETAIL_ITEM",
                    workOrderDetailId, itemId, type,
                    MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public VehicleHistoryModel GetGetVehicleHistoryLastItem(int vehicleId)
        {
            VehicleHistoryModel model = null;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_WORK_ORDER_CARD_LAST_VEHICLE_HISTORY_DATA", vehicleId);

                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model = new VehicleHistoryModel
                        {
                            DealerName = reader["DEALER_NAME"].ToString(),
                            HistoryDate = reader["HISTORY_DATE"].GetValue<DateTime>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<long>()
                        };
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
            return model;
        }

        public string GetGuaranteeRequestDescription(long workOrderDetailId)
        {
            string desc;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_REQUEST_DESCRIPTION",
                    workOrderDetailId);
                CreateConnection(cmd);
                desc = cmd.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return desc;
        }


        public ModelBase CompletePdiVehicleResult(UserInfo user, long workOrderId)
        {
            var model = new ModelBase();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_COMPLETE_PDI_VEHICLE_RESULT",
                    workOrderId, MakeDbNull(user.UserId), null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = int.Parse(db.GetParameterValue(cmd, "ERROR_NO").ToString());
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
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
            return model;
        }

        public bool GetPdiVehicleIsControlled(long workOrderId)
        {
            bool isControlled;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_GET_PDI_VEHICLE_RESULT_IS_CONTROLLED",
                    workOrderId);
                CreateConnection(cmd);
                isControlled = cmd.ExecuteScalar().GetValue<bool>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return isControlled;
        }

        public List<PickingCancellationItem> ListPickingsForCancellation(UserInfo user, long workOrderId)
        {
            List<PickingCancellationItem> list;
            try
            {
                CreateDatabase();
                var rowMapper = MapBuilder<PickingCancellationItem>.BuildAllProperties();
                list = db.ExecuteSprocAccessor("P_LIST_WORK_ORDER_PICKING_MST_FOR_CANCELLATION", rowMapper, workOrderId, MakeDbNull(user.LanguageCode)).ToList();
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


        public void CancelPicking(UserInfo user, long? workOrderId, long pickingId)
        {
            CreateDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {


                    var cmd = db.GetStoredProcCommand("P_CANCEL_WORK_ORDER_PICKING_MST",
                                                      MakeDbNull(workOrderId), pickingId,
                                                      MakeDbNull(user.UserId));
                    CreateConnection(cmd);
                    db.ExecuteNonQuery(cmd, transaction);
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                finally
                {
                    CloseConnection();
                }
            }
        }




    }
}
