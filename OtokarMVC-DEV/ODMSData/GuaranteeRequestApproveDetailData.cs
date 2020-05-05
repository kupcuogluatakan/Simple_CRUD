using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.GuaranteeRequestApproveDetail;

namespace ODMSData
{
    public class GuaranteeRequestApproveDetailData : DataAccessBase
    {
        public void UpdatePricesOnOpen(UserInfo user, long guaranteeId, int guaranteeSeq)
        {
            try
            {
                var mModel = new PeriodicMaintInfo();

                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_UPDATE_WARRANTY_AND_WO_PART_PRICES");
                cmd.CommandTimeout = 14440;

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(guaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(guaranteeSeq));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, user.UserId);

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
        public void GetGuaranteeInfo(UserInfo user, GRADMstViewModel filter)
        {
            try
            {
                var gifModel = new GifModel();
                var mModel = new PeriodicMaintInfo();

                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_REQUEST_APPROVE_DETAIL_INFO");

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(filter.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(filter.GuaranteeSeq));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        filter.WorkOrderId = dr["ID_WORK_ORDER"].GetValue<Int64>();
                        filter.HasPdiVehicle = dr["HAS_PDI_VEHICLE"].GetValue<int>();
                        filter.IsShowCategory = dr["SHOW_GIF_CATEGORY"].GetValue<bool>();
                        filter.WarrantyStatus = dr["WARRANTY_STATUS"].GetValue<int>();
                        filter.ConfirmDesc = dr["CONFIRM_DESC"].GetValue<string>();
                        filter.CurrencySymbol = dr["CURRENCY_CODE"].GetValue<string>();
                        filter.IsInvoiced = dr["ID_WORK_ORDER_INV"].GetValue<bool>();
                        filter.GuaranteeDealer = dr["GUARANTEE_DEALER"].ToString();
                        filter.DealerId = dr["ID_DEALER"].GetValue<int>();
                        filter.GuaranteeCustomer = dr["GUARANTEE_CUSTOMER"].ToString();
                        filter.TechnicalDesc = dr["DESCRIPTION"].ToString();
                        filter.CustomerDesc = dr["REQUEST_DESC"].ToString();
                        filter.FailureCodeAndDescription = dr["FAILURE_CODE_AND_DESCRIPTION"].GetValue<string>();

                        #region gifModel
                        gifModel = new GifModel()
                        {
                            BrokenDate = dr["BREAKDOWN_DATE"].GetValue<DateTime>() == DateTime.MinValue ? null : dr["BREAKDOWN_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy"),
                            CampaignName = dr["CAMPAIGN_NAME"].GetValue<string>(),
                            SpecialNotes = dr["SPECIAL_CONDITIONS"].GetValue<string>(),
                            Notes = dr["NOTES"].GetValue<string>(),
                            GifNo = dr["SSID_GUARANTEE"].GetValue<string>(),
                            IndicatorName = dr["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            ProcessName = dr["PROCESS_TYPE_NAME"].GetValue<string>(),
                            VehicleKm = dr["VEHICLE_KM"].GetValue<string>(),
                            VehicleLeaveDate = dr["VEHICLE_LEAVE_DATE"].GetValue<DateTime>() == DateTime.MinValue ? null : dr["VEHICLE_LEAVE_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy"),
                            WorkOrderDetId = dr["ID_WORK_ORDER_DETAIL"].GetValue<Int64>(),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<Int64>(),
                            IsPerm = dr["IS_PERM"].GetValue<bool>(),
                            TxNote = dr["TX_NOTES"].ToString()

                        };
                        #endregion

                        #region PeriodicMaint
                        mModel = new PeriodicMaintInfo()
                        {
                            Date = dr["PERIODIC_MAINT_DATE"].GetValue<DateTime>() == DateTime.MinValue ? null : dr["PERIODIC_MAINT_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy"),
                            Km = dr["PERIODIC_MAINT_ACTUAL_KM"].GetValue<string>(),
                            ServiceName = dr["DEALER_NAME"].GetValue<string>(),
                            PeriodicMaintDesc = dr["ADMIN_DESC"].GetValue<string>()
                        };
                        #endregion

                        #region category
                        if (!dr["CATEGORY"].GetValue<string>().Equals(string.Empty))
                        {
                            switch (dr["CATEGORY"].GetValue<string>())
                            {
                                case GRADMstViewModel.GIFCategory_Other:
                                    filter.CategoryId = GRADMstViewModel.Category.Other;
                                    break;
                                case GRADMstViewModel.GIFCategory_Campaign:
                                    filter.CategoryId = GRADMstViewModel.Category.Campaign;
                                    break;
                                case GRADMstViewModel.GIFCategory_Commercial:
                                    filter.CategoryId = GRADMstViewModel.Category.Commercial;
                                    break;
                                case GRADMstViewModel.GIFCategory_Contract:
                                    filter.CategoryId = GRADMstViewModel.Category.Contract;
                                    break;
                                case GRADMstViewModel.GIFCategory_Corrosion:
                                    filter.CategoryId = GRADMstViewModel.Category.Corrosion;
                                    break;
                                case GRADMstViewModel.GIFCategory_Design:
                                    filter.CategoryId = GRADMstViewModel.Category.Design;
                                    break;
                                case GRADMstViewModel.GIFCategory_Production:
                                    filter.CategoryId = GRADMstViewModel.Category.Production;
                                    break;
                                case GRADMstViewModel.GIFCategory_Service:
                                    filter.CategoryId = GRADMstViewModel.Category.Service;
                                    break;
                                case GRADMstViewModel.GIFCategory_Supplier:
                                    filter.CategoryId = GRADMstViewModel.Category.Supplier;
                                    break;
                                case GRADMstViewModel.GIFCategory_PDI:
                                    filter.CategoryId = GRADMstViewModel.Category.PDI;
                                    break;
                                case GRADMstViewModel.GIFCategory_SPGuarantee:
                                    filter.CategoryId = GRADMstViewModel.Category.SPGuarantee;
                                    break;
                                case GRADMstViewModel.GIFCategory_Coupon:
                                    filter.CategoryId = GRADMstViewModel.Category.Coupon;
                                    break;
                                case GRADMstViewModel.GIFCategory_Water:
                                    filter.CategoryId = GRADMstViewModel.Category.Water;
                                    break;
                            }

                            filter.CategoryName = dr["CATEGORY_NAME"].GetValue<string>();
                        }
                        #endregion

                        #region vehicle
                        filter.VehicleId = dr["ID_VEHICLE"].ToString();
                        filter.VehicleNoteCount = dr["VEHICLE_NOTE_COUNT"].GetValue<int>();
                        #endregion



                    }
                }
                filter.PeriodicMaintInfo = mModel;
                filter.GifInfo = gifModel;
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

        public List<GuaranteePartsListModel> ListGuaranteeParts(UserInfo user, GuaranteePartsListModel filter, out int totalCount)
        {
            var listModel = new List<GuaranteePartsListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE_DETAIL_PARTS");

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(filter.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(filter.GuaranteeSeq));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new GuaranteePartsListModel()
                        {
                            Id = dr["ID_GUARANTEE_DET_PARTS"].GetValue<Int64>(),
                            Text = string.IsNullOrEmpty(dr["DIS_PART_CODE"].GetValue<string>()) ? "" : dr["DIS_PART_CODE"].GetValue<string>() + " / " + dr["DIS_PART_NAME"].GetValue<string>(),
                            DisSerialNo = dr["DIS_SERIAL_NO"].GetValue<string>(),
                            Value = dr["DIS_PART_ID"].GetValue<string>(),
                            PartCodeName = string.IsNullOrEmpty(dr["PART_CODE"].GetValue<string>()) ? "" : dr["PART_CODE"].GetValue<string>() + " / " + dr["PART_NAME"].GetValue<string>(),
                            SerialNo = dr["SERIAL_NO"].GetValue<string>(),
                            PartId = dr["PART_ID"].GetValue<int>(),
                            Quantity = dr["QUANTITY"].GetValue<decimal>(),
                            StockType = dr["STOCK_TYPE"].GetValue<int>() == 1 ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No,
                            Ratio = dr["WARRANTY_RATIO"].GetValue<decimal?>() == null ? -1 : dr["WARRANTY_RATIO"].GetValue<decimal>(),
                            WarrantyTotal = dr["WARRANTY_TOTAL"].GetValue<decimal>(),
                            WarrantyPrice = dr["WARRANTY_PRICE"].GetValue<decimal>(),
                            PartCount = dr["PART_COUNT"].GetValue<int>()
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
        public List<GuaranteeDescriptionHistoryListModel> ListGuaranteeDescriptionHistory(GuaranteeDescriptionHistoryListModel filter, out int totalCount)
        {
            var listModel = new List<GuaranteeDescriptionHistoryListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_DESCRIPTION_HISTORY");

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(filter.GuaranteeId));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new GuaranteeDescriptionHistoryListModel()
                        {
                            Description = dr["DESCRIPTION"].ToString(),
                            RequestDate = dr["REQUEST_DATE"].GetValue<DateTime>(),
                            RequestUser = dr["REQUEST_USER"].ToString(),
                            ApproveDate = dr["APPROVE_DATE"].GetValue<DateTime>(),
                            ApproveUser = dr["APPROVE_USER"].ToString(),
                            RequestWarrantyStatus = dr["REQUEST_WARRANTY_STATUS"].GetValue<short>(),
                            ApproveWarrantyStatus = dr["APPROVE_WARRANTY_STATUS"].GetValue<short>(),
                            GuaranteeId = dr["GUARANTEE_ID"].GetValue<Int64>(),
                            SeqNo = dr["GUARANTEE_SEQ_ID"].GetValue<short>(),
                            Type = dr["TYPE"].GetValue<string>()
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


        public List<GuaranteeLaboursListModel> ListGuaranteeLabours(UserInfo user, GuaranteeLaboursListModel filter, out int totalCount)
        {
            var listModel = new List<GuaranteeLaboursListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE_DETAIL_LABOURS");

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(filter.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(filter.GuaranteeSeq));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new GuaranteeLaboursListModel()
                        {
                            Id = dr["ID_GUARANTEE_DET_LABOURS"].GetValue<Int64>(),
                            Value = dr["APPROVED_LABOUR_CODE"].GetValue<string>(),
                            Text = dr["APPROVED_LABOUR_CODE"].GetValue<string>(),
                            Desc = dr["APPROVED_LABOUR_DESC"].GetValue<string>(),
                            Duration = dr["DURATION"].GetValue<int>(),
                            Quantity = dr["QUANTITY"].GetValue<int>(),
                            IsDurationCheck = dr["DEALER_DURATION_CHCK"].GetValue<bool>(),
                            Ratio = dr["WARRANTY_RATIO"].GetValue<decimal?>() == null ? -1 : dr["WARRANTY_RATIO"].GetValue<decimal>(),
                            WarrantyTotal = dr["WARRANTY_TOTAL"].GetValue<decimal>(),
                            WarrantyPrice = dr["WARRANTY_PRICE"].GetValue<decimal>()
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

        public List<System.Web.Mvc.SelectListItem> ListRemovalPart(UserInfo user, int partId)
        {
            var listItem = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_REMOVABLE_PARTS_COMBO");

                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(partId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var item = new SelectListItem()
                        {
                            Text = dr["Text"].GetValue<string>(),
                            Value = dr["Value"].GetValue<string>()
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

        public void DMLSaveGuaranteeParts(UserInfo user, GuaranteePartsLabourViewModel model)
        {
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_APPROVE_PARTS");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in model.ListModelParts)
                {
                    model.CommandType = CommonValues.DMLType.Update;

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "ID", DbType.Int64, item.Id);
                    db.AddInParameter(cmd, "DIS_PART_SERIAL_NO", DbType.String, MakeDbNull(item.DisSerialNo));
                    db.AddInParameter(cmd, "PART_SERIAL_NO", DbType.String, MakeDbNull(item.SerialNo));
                    db.AddInParameter(cmd, "DIS_PART_ID", DbType.Int32, MakeDbNull(item.Value));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "RATIO", DbType.Decimal, MakeDbNull(item.Ratio));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
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

        public void DMLSaveGuaranteeLabour(UserInfo user, GuaranteePartsLabourViewModel model)
        {
            bool isSuccess = true;
            CreateDatabase();
            DbCommand cmd = db.GetStoredProcCommand("P_DML_GUARANTEE_APPROVE_LABOURS");
            CreateConnection(cmd);
            DbTransaction transaction = connection.BeginTransaction();
            try
            {
                foreach (var item in model.ListModelLabour)
                {
                    model.CommandType = CommonValues.DMLType.Update;

                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "ID", DbType.Int64, item.Id);
                    db.AddInParameter(cmd, "LABOUR_CODE", DbType.String, MakeDbNull(item.Value));
                    db.AddInParameter(cmd, "DURATION", DbType.Int32, MakeDbNull(item.Duration));
                    db.AddInParameter(cmd, "RATIO", DbType.Decimal, MakeDbNull(item.Ratio));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, MakeDbNull(model.CommandType));
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

        private string ReturnCategoryId(GRADMstViewModel.Category? categoryId)
        {
            string category = string.Empty;
            switch (categoryId)
            {
                case GRADMstViewModel.Category.Other:
                    category = GRADMstViewModel.GIFCategory_Other;
                    break;
                case GRADMstViewModel.Category.Supplier:
                    category = GRADMstViewModel.GIFCategory_Supplier;
                    break;
                case GRADMstViewModel.Category.Service:
                    category = GRADMstViewModel.GIFCategory_Service;
                    break;
                case GRADMstViewModel.Category.Production:
                    category = GRADMstViewModel.GIFCategory_Production;
                    break;
                case GRADMstViewModel.Category.Design:
                    category = GRADMstViewModel.GIFCategory_Design;
                    break;
                case GRADMstViewModel.Category.Corrosion:
                    category = GRADMstViewModel.GIFCategory_Corrosion;
                    break;
                case GRADMstViewModel.Category.Contract:
                    category = GRADMstViewModel.GIFCategory_Contract;
                    break;
                case GRADMstViewModel.Category.Commercial:
                    category = GRADMstViewModel.GIFCategory_Commercial;
                    break;
                case GRADMstViewModel.Category.Campaign:
                    category = GRADMstViewModel.GIFCategory_Campaign;
                    break;
                case GRADMstViewModel.Category.PDI:
                    category = GRADMstViewModel.GIFCategory_PDI;
                    break;
                case GRADMstViewModel.Category.SPGuarantee:
                    category = GRADMstViewModel.GIFCategory_SPGuarantee;
                    break;
                case GRADMstViewModel.Category.Coupon:
                    category = GRADMstViewModel.GIFCategory_Coupon;
                    break;
                case GRADMstViewModel.Category.Water:
                    category = GRADMstViewModel.GIFCategory_Water;
                    break;

            }
            return category;
        }

        public void CompleteGuaranteeApprove(UserInfo user, GRADMstViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_COMPLETE_GUARANTEE_DETAIL");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(model.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int16, MakeDbNull(model.GuaranteeSeq));
                db.AddInParameter(cmd, "WARRANTY_TYPE", DbType.Int16, MakeDbNull(model.WarrantyStatus));
                db.AddInParameter(cmd, "CATEGORY", DbType.String, MakeDbNull(ReturnCategoryId(model.CategoryId)));
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.ConfirmDesc));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int64, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage =
                        ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());

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

        public void GuaranteeUpdateDescription(UserInfo user, GRADMstViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_UPDATE_CONFIRM_DESC_GUARANTEE_MST");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(model.GuaranteeId));
                db.AddInParameter(cmd, "CONFIRM_DESC", DbType.String, MakeDbNull(model.ConfirmDesc));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int64, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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

        public void CompleteGuaranteeCancel(UserInfo user, GRADMstViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_COMPLETE_GUARANTEE_CANCEL");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(model.GuaranteeId));
                db.AddInParameter(cmd, "WARRANTY_TYPE", DbType.Int16, model.WarrantyStatus);
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int16, model.GuaranteeSeq);
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int64, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorMessage =
                        ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>());
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

        public List<GRADGifHistoryModel> ListGRADGifHistory(UserInfo user, GRADGifHistoryModel filter, out int totalCount)
        {
            var listModel = new List<GRADGifHistoryModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE_DETAIL_GIF_HISTORY");

                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(filter.GuaranteeId));
                db.AddInParameter(cmd, "GUARANTEE_SEQ", DbType.Int32, MakeDbNull(filter.GuaranteeSeq));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, filter.SortColumn);
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, filter.SortDirection);
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, filter.PageSize);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new GRADGifHistoryModel()
                        {
                            GuaranteeId = filter.GuaranteeId,
                            GuaranteeSeq = filter.GuaranteeSeq,
                            ApprovedUserCode = dr["DMS_USER_CODE"].GetValue<string>(),
                            ApproveduserEmail = dr["EMAIL"].GetValue<string>(),
                            CampaignName = dr["CAMPAIGN_NAME"].GetValue<string>(),
                            ConfirmDesc = dr["CONFIRM_DESC"].GetValue<string>(),
                            DealerName = dr["DEALER_NAME"].GetValue<string>(),
                            IndicatorCode = dr["INDICATOR_TYPE_CODE"].GetValue<string>(),
                            IndicatorName = dr["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            ProcessCode = dr["PROCESS_TYPE_CODE"].GetValue<string>(),
                            ProcessName = dr["PROCESS_TYPE_NAME"].GetValue<string>(),
                            VehicleKm = dr["VEHICLE_KM"].GetValue<string>(),
                            VehicleLeaveDate = dr["VEHICLE_LEAVE_DATE"].GetValue<DateTime>() == DateTime.MinValue ? null : dr["VEHICLE_LEAVE_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy"),
                            VinNo = dr["VEHICLE_VIN_NO"].GetValue<string>(),
                            WarrantyStatusName = dr["STATUS_NAME"].GetValue<string>(),
                            WarrantyStatus = dr["WARRANTY_STATUS"].GetValue<int>(),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<Int64>(),
                            WorkOrderDetId = dr["ID_WORK_ORDER_DETAIL"].GetValue<int>()
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

        public List<GRADGifHistoryDetModel> ListGRADGifHistoryDet(UserInfo user, long guaranteeId, out int totalCount)
        {
            var listModel = new List<GRADGifHistoryDetModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_GUARANTEE_REQUEST_APPROVE_DETAIL_GIF_HISTORY_DET");
                db.AddInParameter(cmd, "GUARANTEE_ID", DbType.Int64, MakeDbNull(guaranteeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new GRADGifHistoryDetModel()
                        {
                            Type = dr["TYPE"].GetValue<string>(),
                            Code = dr["CODE"].GetValue<string>(),
                            Name = dr["NAME"].GetValue<string>(),
                            Ratio = dr["WARRANTY_RATIO"].GetValue<string>()
                        };

                        listModel.Add(model);
                    }
                }
                totalCount = listModel.Count;
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

        public List<Part_Infos> GetPartInfos(long id)
        {
            var returnModel = new List<Part_Infos>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_REQUEST_APPROVE_DETAIL_PART_INFOS");
                db.AddInParameter(cmd, "GUARANTEE_DET_PART_ID", DbType.Int64, MakeDbNull(id));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        returnModel.Add(new Part_Infos
                        {
                            PartName = dr["PART_NAME"].GetValue<string>(),
                            Quantity = dr["QUANTITY"].GetValue<decimal>(),
                            VinNo = dr["VEHICLE_VIN_NO"].GetValue<string>(),
                            Km = dr["VEHICLE_KM"].GetValue<string>(),
                            CustomerName = string.Format("{0} {1}", dr["CRM_CUSTOMER_CUSTOMER_NAME"].GetValue<string>(), dr["CRM_CUSTOMER_CUSTOMER_LAST_NAME"].GetValue<string>()),
                            WorkOrderId = dr["ID_WORK_ORDER"].GetValue<string>(),
                            WorkOrderDetId = dr["ID_WORK_ORDER_DETAIL"].GetValue<string>(),
                            GuaranteeId = dr["ID_GUARANTEE"].GetValue<string>(),
                            GuaranteeDesc = dr["DESCRIPTION"].GetValue<string>()
                        });

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
            return returnModel;
        }
    }
}
