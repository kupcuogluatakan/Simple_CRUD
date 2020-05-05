using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.DeliveryGoodsPlacement;
using ODMSCommon.Resources;
using ODMSModel.DeliveryListPart;

namespace ODMSData
{
    public class DeliveryGoodsPlacementData : DataAccessBase
    {
        public List<DeliveryGoodsPlacementListModel> ListDeliveryGoodsPlacement(UserInfo user,DeliveryGoodsPlacementListModel filter, out int totalCount)
        {
            var listModel = new List<DeliveryGoodsPlacementListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_GOODS_PLACEMENT");

                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "STATUS_LOOKVAL", DbType.Int32, filter.StatusId);
                db.AddInParameter(cmd, "IS_PLACED", DbType.Boolean, MakeDbNull(filter.IsPlaced));
                db.AddInParameter(cmd, "WAYBILL_NO", DbType.String, MakeDbNull(filter.WayBillNo));
                db.AddInParameter(cmd, "WAYBILL_DATE", DbType.DateTime, MakeDbNull(filter.WayBillDate));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new DeliveryGoodsPlacementListModel();
                        model.IsPlacedS = dr["IS_PLACED"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No;
                        model.IsPlaced = dr["IS_PLACED"].GetValue<bool>();
                        model.DeliveryId = dr["ID_DELIVERY"].GetValue<Int64>();
                        model.AcceptedByUser = dr["ACCEPTED_BY_USER"].ToString();
                        model.SapDeliveryNo = dr["SAP_DELIVERY_NO"].ToString();
                        model.Status = dr["STATUS"].ToString();
                        model.StatusId = dr["STATUS_LOOKVAL"].GetValue<int>();
                        model.WayBillDate = dr["WAYBILL_DATE"].GetValue<DateTime>();
                        model.WayBillNo = dr["VAYBILL_NO"].ToString();

                        listModel.Add(model);
                    }
                    dr.Close();
                }
                totalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                CloseConnection();
            }

            return listModel;
        }

        public List<PartsPlacementListModel> ListPartsPlacement(UserInfo user, PartsPlacementListModel filter, out int totalCount)
        {
            var listModel = new List<PartsPlacementListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_DELIVERY_GOODS_PLACEMENT_PARTS");

                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, filter.DeliverySeqNo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, null);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, null);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, null);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, null);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new PartsPlacementListModel()
                        {
                            DeliverySeqNo = dr["DELIVERY_SEQ_NO"].GetValue<Int64>(),
                            PlacementId = dr["ID_DELIVERY_PLACEMENT"].GetValue<Int64>(),
                            Quantity = dr["QTY"].GetValue<decimal>(),
                            Text = dr["RACK_WAREHOUSE"].ToString(),
                            Value = dr["ID_RACK"].ToString()
                        };
                        listModel.Add(model);
                    }
                    dr.Close();
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
            return listModel;
        }

        public List<SelectListItem> ListRackWarehouseByDetId(UserInfo user, int id)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_RACK_AND_WAREHOUSE_BY_DELIVERYSEQNO_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, id);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem
                        {
                            Text = dr["RACK_WAREHOUSE"].ToString(),
                            Value = dr["ID_RACK"].ToString()
                        };

                        listItem.Add(item);
                    }
                    dr.Close();
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

            return listItem;
        }

        public void DeletePartsPlacement(UserInfo user, PartsPlacementViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_DELIVERY_GOODS_PLACEMENT_PARTS");

                db.AddInParameter(cmd, "PLACEMENT_ID", DbType.Int64, model.PlacementId);
                db.AddInParameter(cmd, "QUANTITY", DbType.Int32, null);
                db.AddInParameter(cmd, "RACK_ID", DbType.Int32, null);
                db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, null);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();


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

        public void DMLPartsPlacement(UserInfo user, PartsPlacementViewModel model)
        {
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_DELIVERY_GOODS_PLACEMENT_PARTS");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var submodel in model.ListModel)
                {
                    //Set Command Type 'Insert' or 'Update' by PlacementId
                    model.CommandType = submodel.PlacementId != -1 ? CommonValues.DMLType.Update : CommonValues.DMLType.Insert;

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "PLACEMENT_ID", DbType.Int64, submodel.PlacementId);
                    db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, submodel.Quantity);
                    db.AddInParameter(cmd, "RACK_ID", DbType.Int64, submodel.Value);
                    db.AddInParameter(cmd, "DELIVERY_SEQ_NO", DbType.Int64, submodel.DeliverySeqNo);
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);
                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (model.ErrorNo > 0)
                    {
                        isSuccess = false;
                        model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }

                }

            }
            catch (Exception Ex)
            {
                isSuccess = false;
                model.ErrorNo = 1;
                model.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
            }
            finally
            {
                if (isSuccess)
                {
                    transaction.Commit();
                }
                else
                {
                    transaction.Rollback();
                }
                CloseConnection();
            }

        }

        public void CompleteDeliveryGoodsPlacement(UserInfo user, DeliveryListPartSubViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_COMPLETE_DELIVERY_GOODS_PLACEMENT");
                db.AddInParameter(cmd, "DELIVERY_ID", DbType.Int64, model.DeliveryId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 1)
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

        //TODO : Id set edilmeli
        public void DMLPartsPlacementDefault(UserInfo user, DeliveryListPartSubViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_DELIVERY_GOODS_PLACEMENT_DEFAULT_RACK");
                db.AddInParameter(cmd, "DELIVERY_ID", DbType.Int64, model.DeliveryId);
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "DEFAULT_TYPE", DbType.String, model.DefaultType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());


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

        public void DoReverseTransaction(UserInfo user, DeliveryListPartSubViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DELIVERY_GOODS_PLACEMENT_PARTS_REVERSE_TRANSACTION");
                CreateConnection(cmd);

                db.AddInParameter(cmd, "ID_DELIVERY", DbType.Int64, model.DeliveryId);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                db.ExecuteNonQuery(cmd);

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();

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
