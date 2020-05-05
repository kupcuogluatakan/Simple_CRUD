using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.PurchaseOrderSuggestionDetail;

namespace ODMSData
{
    public class PurchaseOrderSuggestionDetailData : DataAccessBase
    {
        public List<POSuggestionDetailListModel> ListPOSuggestionDetail(UserInfo user,POSuggestionDetailListModel filter, out int totalCount)
        {
            var listModel = new List<POSuggestionDetailListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_SUGGESTION_DETAIL");

                db.AddInParameter(cmd, "MRP_ID", DbType.Int64, filter.MrpId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new POSuggestionDetailListModel()
                        {
                            MrpId = dr["ID_MRP"].GetValue<Int64>(),
                            PartId = dr["ID_PART"].GetValue<int>(),
                            CurrentQuantity = dr["CURR_QTY"].GetValue<string>(),
                            LeadTime = dr["LEAD_TIME"].GetValue<string>(),
                            IsChecked = dr["IS_CHECKED"].GetValue<bool>(),
                            MinStockQuantity = dr["MIN_STOCK_QTY"].GetValue<string>(),
                            ResultQuantity = dr["RESULT_QTY"].GetValue<decimal>(),
                            PropPoQuantity =  dr["PROP_PO_QTY"].GetValue<string>(),
                            OpenPoQuantity = dr["OPEN_PO_QTY"].GetValue<string>(),
                            OrderQuantity = dr["ORDER_QTY"].GetValue<decimal>(),
                            PackageQuantity = dr["PACKAGE_QTY"].GetValue<string>(),
                            PurchasePrice = dr["PURCHASE_PRICE"].GetValue<string>(),
                            Part = dr["PART"].GetValue<string>(),
                            ReserveQuantity = dr["RESERVE_QTY"].GetValue<string>(),
                            Unit = dr["UNIT"].GetValue<string>(),
                            IsDivided = dr["IS_DIVIDED"].GetValue<int>(),
                            IsChanged = dr["IS_CHANGED"].GetValue<int>(),
                            FromParts = dr["FROM_PARTS"].GetValue<string>()
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

        public void GetInitialInfoSuggestionDetail(POSuggestionDetailViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_SUGGESTION_DETAIL_INFO");

                db.AddInParameter(cmd, "MRP_ID", DbType.Int64, model.MrpId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        model.OrderPriceS = string.Format("{0:0.00}", dr["ORDER_PRICE"].GetValue<decimal>()).Replace(CommonValues.Comma, CommonValues.Dot);
                        model.SuggestionPrice = dr["SUGGESTION_PRICE"].GetValue<decimal>();
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
        }

        public void DMLPurchaseOrder(UserInfo user,POSuggestionDetailViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_MST_SUGGESTION");
                db.AddParameter(cmd, "PO_NUMBER", DbType.Int64, ParameterDirection.InputOutput, "PO_NUMBER", DataRowVersion.Default, MakeDbNull(model.PoNumber));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
                db.AddInParameter(cmd,"OPERATING_USER",DbType.Int32,MakeDbNull(user.UserId));
                db.AddInParameter(cmd,"MRP_ID",DbType.Int64,MakeDbNull(model.MrpId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>();
                else
                    model.PoNumber = db.GetParameterValue(cmd, "PO_NUMBER").GetValue<Int64>();


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

        public void DMLPurchaseOrderDetail(UserInfo user,POSuggestionDetailViewModel hModel)
        {
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_DET_SUGGESTION");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var model in hModel.ListModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd,"PO_NUMBER",DbType.Int64,MakeDbNull(hModel.PoNumber));
                    db.AddInParameter(cmd, "MRP_ID", DbType.Int64, MakeDbNull(hModel.MrpId));
                    db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(model.PartId));
                    db.AddInParameter(cmd, "ORDER_QTY", DbType.Decimal, model.OrderQuantity);
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, hModel.CommandType);
                    db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd, transaction);
                    hModel.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (hModel.ErrorNo > 0)
                    {
                        isSuccess = false;
                        hModel.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                isSuccess = false;
                hModel.ErrorNo = 1;
                hModel.ErrorMessage = MessageResource.Err_Generic_Unexpected + "System Message : (" + Ex.Message + ")";
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

        public void ControlMrp()
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("SP_AUTO_MRP_CNTRL");
                
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

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

        public void UpdatePurchaseOrderSuggestionDetail(UserInfo user,int mrpId)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("SP_UPD_MRP_DET");
                db.AddInParameter(cmd, "MRP_ID", DbType.Int64, MakeDbNull(mrpId));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, user.UserId.ToString(CultureInfo.InvariantCulture));

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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
