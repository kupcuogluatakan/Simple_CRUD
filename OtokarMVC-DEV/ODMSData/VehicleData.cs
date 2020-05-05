using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web.Mvc;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ListModel;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using ODMSData.Utility;
using ODMSModel;
using ODMSModel.Announcement;

namespace ODMSData
{
    public class VehicleData : DataAccessBase
    {
        private readonly DbHelper _dbHelper;
        private readonly AppErrorsData appError = new AppErrorsData();

        public VehicleData()
        {
            _dbHelper = new DbHelper();
        }
        public List<VehicleHistoryListModel> ListVehicleHistory(UserInfo user, VehicleHistoryListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleHistoryListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleHistoryListModel
                        {
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            VehicleHistoryId = reader["VEHICLE_HISTORY_ID"].GetValue<int>(),
                            ProcessType = reader["PROCESS_TYPE_NAME"].ToString(),
                            IndicatorType = reader["INDICATOR_TYPE_NAME"].ToString(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            WorkOrderDetailId = reader["WORK_ORDER_DETAIL_ID"].GetValue<int>(),
                            IndicatorDate = reader["INDICATOR_DATE"].GetValue<DateTime>(),
                            VehicleKM = reader["VEHICLE_KM"].GetValue<long>(),
                            WorkOrderDate = reader["WORK_ORDER_DATE"].GetValue<DateTime>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            AppIndicCode = reader["APP_INDIC_CODE"].GetValue<string>(),
                            AppIndicName = reader["APP_INDIC_NAME"].GetValue<string>(),
                            CampaignNameCode = reader["CAMPAIGN_CODE_NAME"].GetValue<string>()
                        };
                        retVal.Add(vehicleListModel);
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
        private DataTable CreateDataTableFromList(List<VehicleIndexViewModel> list)
        {
            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("VIN_NO");
            DataColumn col2 = new DataColumn("SPECIAL_CONDITIONS");
            DataColumn col3 = new DataColumn("OUT_OF_WARRANTY_DESC");

            col1.DataType = System.Type.GetType("System.String");
            col2.DataType = System.Type.GetType("System.String");
            col3.DataType = Type.GetType("System.String");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                row[0] = item.VinNo;
                row[1] = item.SpecialConditions;
                row[2] = item.OutOfWarrantyDescription;
                table.Rows.Add(row);
            }

            return table;
        }
        public ModelBase UpdateNotesForVehicle(UserInfo user, List<VehicleIndexViewModel> model)
        {
            var retVal = new ModelBase();
            var dt2 = CreateDataTableFromList(model);
            _dbHelper.ExecuteNonQuery("P_UPDATE_VEHICLE_NOTES", dt2, user.UserId, null, null);

            retVal.ErrorNo = int.Parse(_dbHelper.GetOutputValue("ERROR_NO").ToString());
            if (retVal.ErrorNo > 0)
            {
                retVal.ErrorMessage = ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_NO").ToString());
            }
            return retVal;
        }
        

        /// <summary>
        /// Emre (10.05.2019) Araç Geçmişi Detay Exceli (2.Excel Butonu)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<VehicleHistoryDetailListModel> ListVehicleHistoryAllDetails(UserInfo user, VehicleHistoryDetailListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleHistoryDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY_ALL_DETAILS");
                db.AddInParameter(cmd, "@DEALER_ID_ACTIVE", DbType.Int32, MakeDbNull(user.DealerID));

                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "CUSTOMER_IDS", DbType.String, MakeDbNull(filter.CustomerIds));
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(filter.DealerName));
                db.AddInParameter(cmd, "PROCESS_TYPE_CODE", DbType.String, MakeDbNull(filter.ProcessType));
                db.AddInParameter(cmd, "INDICATOR_TYPE_NAME", DbType.String, MakeDbNull(filter.IndicatorType));


                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleHistoryDetailListModel
                        {
                            DealerName = reader["DEALER_NAME"].ToString(),
                            VehicleHistoryId = reader["VEHICLE_HISTORY_ID"].GetValue<int>(),
                            VehicleHistoryDetailId = reader["VEHICLE_HISTORY_DETAIL_ID"].GetValue<int>(),
                            IsPart = reader["IS_PART"].GetValue<bool>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourName = reader["LABOUR_NAME"].ToString(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].ToString(),
                            DetailCode = reader["DETAIL_CODE"].ToString(),
                            DetailDescription = reader["DETAIL_DESCRIPTION"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<string>(),
                            WarratyRatio = reader["W_RATIO"].GetValue<string>(),
                            WarrantyPrice = reader["W_PRICE"].GetValue<string>(),
                            ListPrice = reader["PRICE"].GetValue<string>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>()

                        };
                        retVal.Add(vehicleListModel);
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

        public List<VehicleHistoryDetailListModel> ListVehicleHistoryDetails(UserInfo user, VehicleHistoryDetailListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleHistoryDetailListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY_DETAILS");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.DealerID));
                db.AddInParameter(cmd, "VEHICLE_HISTORY_ID", DbType.Int32, MakeDbNull(filter.VehicleHistoryId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleHistoryDetailListModel
                        {
                            DealerName = reader["DEALER_NAME"].ToString(),
                            VehicleHistoryId = reader["VEHICLE_HISTORY_ID"].GetValue<int>(),
                            VehicleHistoryDetailId = reader["VEHICLE_HISTORY_DETAIL_ID"].GetValue<int>(),
                            IsPart = reader["IS_PART"].GetValue<bool>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourName = reader["LABOUR_NAME"].ToString(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].ToString(),
                            DetailCode = reader["DETAIL_CODE"].ToString(),
                            DetailDescription = reader["DETAIL_DESCRIPTION"].ToString(),
                            Quantity = reader["QUANTITY"].GetValue<string>(),
                            WarratyRatio = reader["W_RATIO"].GetValue<string>(),
                            WarrantyPrice = reader["W_PRICE"].GetValue<string>(),
                            ListPrice = reader["PRICE"].GetValue<string>(),

                        };
                        retVal.Add(vehicleListModel);
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

        /// <summary>
        /// Detaylı Araç Geçmişi -Emre (10.05.2019)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="filter"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public List<VehicleHistoryListWithDetailsModel> ListVehicleHistoryListWithDetails(UserInfo user, VehicleHistoryListWithDetailsModel filter, out int totalCount)
        {
            var retVal = new List<VehicleHistoryListWithDetailsModel>();
            totalCount = 0;
            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY_WITH_DETAILS");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(filter.VehicleId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.DealerID));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleHistoryListWithDetailsModel
                        {
                            VehicleHistoryDetailId = reader["VEHICLE_HISTORY_DETAIL_ID"].GetValue<int>(),
                            VehicleHistoryId = reader["VEHICLE_HISTORY_ID"].GetValue<int>(),
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            ProcessType = reader["PROCESS_TYPE_NAME"].ToString(),
                            IndicatorType = reader["INDICATOR_TYPE_NAME"].ToString(),
                            DealerId = reader["DEALER_ID"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].ToString(),
                            WorkOrderDetailId = reader["WORK_ORDER_DETAIL_ID"].GetValue<int>(),
                            IndicatorDate = reader["INDICATOR_DATE"].GetValue<DateTime>(),
                            VehicleKM = reader["VEHICLE_KM"].GetValue<Int64>(),
                            CampaignNameCode = reader["CAMPAIGN_CODE_NAME"].ToString(),
                            AppIndicCode = reader["APP_INDIC_CODE"].ToString(),
                            AppIndicName = reader["APP_INDIC_NAME"].ToString(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<Int64>(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            WorkOrderDate = reader["WORK_ORDER_DATE"].GetValue<DateTime>(),
                            IsPart = reader["IS_PART"].GetValue<bool>(),
                            LabourId = reader["LABOUR_ID"].GetValue<int>(),
                            LabourName = reader["LABOUR_NAME"].ToString(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartName = reader["PART_NAME"].ToString(),
                            DetailCode = reader["DETAIL_CODE"].ToString(),
                            DetailDescription = reader["DETAIL_DESCRIPTION"].ToString(),
                            WarratyRatio = reader["W_RATIO"].ToString(),
                            ListPrice = reader["PRICE"].ToString(),
                            WarrantyPrice = reader["W_PRICE"].ToString(),
                            Quantity = reader["QUANTITY"].ToString()

                        };
                        retVal.Add(vehicleListModel);
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


        public List<VehicleListModel> ListVehicles(UserInfo user, VehicleListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLES");
                db.AddInParameter(cmd, "V_CODE_KOD", DbType.String, MakeDbNull(filter.VehicleCode));
                db.AddInParameter(cmd, "VEHICLE_MODEL", DbType.String, MakeDbNull(filter.VehicleModel));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(filter.CustomerName));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "WARRANTY_START_DATE", DbType.Date, MakeDbNull(filter.WarrantyStartDate));
                db.AddInParameter(cmd, "WARRANTY_END_DATE", DbType.Date, MakeDbNull(filter.WarrantyEndDate));
                db.AddInParameter(cmd, "MODEL_YEAR", DbType.String, MakeDbNull(filter.ModelYear));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "VIN_NO_LIST", DbType.String, MakeDbNull(filter.VinCodeList));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                db.AddInParameter(cmd, "ENGINE_NO", DbType.String, MakeDbNull(filter.EngineNo));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                AddPagingParametersWithLanguage(user, cmd, filter);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleListModel
                        {
                            VehicleId = reader["VEHICLE_ID"].GetValue<int>(),
                            VehicleCode = reader["VEHICLE_CODE"].ToString(),
                            VehicleCodeDesc = reader["VEHICLE_CODE_DESC"].ToString(),
                            CustomerId = reader["CUSTOMER_ID"].GetValue<int>(),
                            CustomerName = reader["CUSTOMER_NAME"].ToString(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int?>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].ToString(),
                            VinNo = reader["VIN_NO"].ToString(),
                            VehicleGroup = reader["VEHICLE_GROUP_NAME"].ToString(),
                            VehicleModel = reader["VEHICLE_MODEL"].ToString(),
                            VehicleType = reader["VEHICLE_TYPE_NAME"].ToString(),
                            EngineNo = reader["ENGINE_NO"].ToString(),
                            EngineType = reader["ENGINE_TYPE"].ToString(),
                            Plate = reader["PLATE"].GetValue<string>(),
                            ModelYear = reader["MODEL_YEAR"].GetValue<int>(),
                            Description = reader["DESCRIPTION"].ToString(),
                            IsHourMaint = reader["IS_HOUR_MAINT"].GetValue<bool>(),
                            IsHourMaintName = reader["IS_HOUR_MAINT_NAME"].ToString(),
                            Hour = reader["HOUR"].GetValue<int>(),
                            Location = reader["LOCATION"].ToString(),
                            ResponsiblePerson = reader["RESPONSIBLE_PERSON"].ToString(),
                            ResponsiblePersonPhone = reader["RESPONSIBLE_PERSON_PHONE"].ToString(),
                            FactoryProductionDate = reader["FACT_PROD_DATE"].GetValue<DateTime?>(),
                            FactoryShipmentDate = reader["FACT_SHIP_DATE"].GetValue<DateTime?>(),
                            PaintWarrantyEndDate = reader["PAINT_WARRANTY_END_DATE"].GetValue<DateTime?>(),
                            CorrosionWarrantyEndDate = reader["CORROSION_WARRANTY_END_DATE"].GetValue<DateTime?>(),
                            //End 
                            WarrantyEndKilometer = reader["WARRANTY_END_KM"].GetValue<long?>(),
                            //Plate
                            Color = reader["COLOR"].GetValue<string>(),
                            ContractNo = reader["CONTRACT_NO"].GetValue<string>(),
                            WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime?>(),                           
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime?>(),                       
                            FactoryQualityControlDate = reader["FACT_QCNTRL_DATE"].GetValue<DateTime?>(),
                            VehicleKilometer = reader["VEHICLE_KM"].GetValue<long?>(),
                            SpecialConditions = reader["SPECIAL_CONDITIONS"].GetValue<string>(),
                            VatExcludeTypeName = reader["VAT_EXCLUDE_NAME"].GetValue<string>(),
                            OutOfWarrantyDescription = reader["OUTOF_WARRANTY_DESC"].GetValue<string>(),
                            SSIDPriceList = reader["SSID_PRICE_LIST"].GetValue<string>(),
                            //IsHourMaintName
                            //WarrantyEndKilometer
                            Notes = reader["NOTES"].GetValue<string>(),
                            WarrantyStatusName = reader["WARRANTY_STAT"].GetValue<string>()
                        };
                        retVal.Add(vehicleListModel);
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

        public List<VehicleListModel> ListVehicles()
        {
            var retVal = new List<VehicleListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE");
                CreateConnection(cmd);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new VehicleListModel
                        {
                            VehicleId = reader["ID_VEHICLE"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].ToString(),
                            CustomerName = reader["CUSTOMER_NAME_SURNAME"].ToString(),
                            ModelCode = reader["MODEL_KOD"].ToString(),
                            ModelName = reader["MODEL_NAME"].ToString(),
                            ModelYear = reader["MODEL_YEAR"].GetValue<int>(),
                            GroupCode = reader["VEHICLE_GROUP_SSID"].ToString(),
                            GroupName = reader["GROUP_NAME"].ToString(),
                            TypeCode = reader["TYPE_SSID"].ToString(),
                            TypeName = reader["TYPE_NAME"].ToString(),
                        };
                        retVal.Add(vehicleListModel);
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

        public List<VehicleWorkOrderMaintModelcs> ListVehicleWorkOrderMaint(string vinNo, string languageCode)
        {
            var retVal = new List<VehicleWorkOrderMaintModelcs>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_WORK_ORDER_MAINT");
                db.AddInParameter(cmd, "VIN_NO", DbType.String, vinNo);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, languageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new VehicleWorkOrderMaintModelcs
                        {
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            WarrantStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            WarrantyEndDate = reader["WARRANTY_END_DATE"].GetValue<DateTime>(),
                            WorkOrderCreateDate = reader["WORK_ORDER_CREATE_DATE"].GetValue<DateTime>(),
                            VehicleLeaveDate = reader["VEHICLE_LEAVE_DATE"].GetValue<DateTime>(),
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            MaintName = reader["MAINT_NAME"].GetValue<string>()
                        };
                        retVal.Add(dealerListModel);
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

        public VehicleListModel ListVehicleLastVersion()
        {
            var retVal = new VehicleListModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_LAST_VERSION");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new VehicleListModel
                        {
                            VehicleLastUpdate = reader["VEHICLE_LAST_UPDATE"].GetValue<int>(),
                            VehicleModelLastUpdate = reader["VEHICLE_MODEL_LAST_UPDATE"].GetValue<int>()
                        };
                        retVal = listModel;
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

        public List<SelectListItem> ListVehicleEngineTypesAsSelectListItem(UserInfo user, int? typeId)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_ENGINE_TYPES_COMBO");
                db.AddInParameter(cmd, "ID_TYPE", DbType.Int32, MakeDbNull(typeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ENGINE_TYPE"].ToString(),
                            Text = reader["ENGINE_TYPE"].ToString()
                        };
                        retVal.Add(lookupItem);
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
        public List<SelectListItem> ListVehicleCodeAsSelectListItem(UserInfo user, string vehicleType)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_CODES_COMBO");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(vehicleType));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["V_CODE_KOD"].GetValue<string>(),
                            Text = reader["V_CODE_KOD"] + CommonValues.MinusWithSpace +
                                reader["CODE_NAME"].GetValue<string>() + CommonValues.MinusWithSpace +
                                reader["ENGINE_TYPE"].GetValue<string>()
                        };
                        retVal.Add(lookupItem);
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

        public void DMLVehicle(UserInfo user, VehicleIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_VEHICLE_MAIN");
                db.AddParameter(cmd, "VEHICLE_ID", DbType.Int32, ParameterDirection.InputOutput, "VEHICLE_ID", DataRowVersion.Default, model.VehicleId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "V_CODE_KOD", DbType.String, MakeDbNull(model.VehicleCode));
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int32, MakeDbNull(model.CustomerId));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(model.VinNo));
                db.AddInParameter(cmd, "ENGINE_NO", DbType.String, MakeDbNull(model.EngineNo));
                db.AddInParameter(cmd, "MODEL_YEAR", DbType.Int32, MakeDbNull(model.ModelYear));
                db.AddInParameter(cmd, "FACT_PROD_DATE", DbType.DateTime, MakeDbNull(model.FactoryProductionDate));
                db.AddInParameter(cmd, "FACT_QCNTRL_DATE", DbType.DateTime, MakeDbNull(model.FactoryQualityControlDate));
                db.AddInParameter(cmd, "FACT_SHIP_DATE", DbType.DateTime, MakeDbNull(model.FactoryShipmentDate));
                db.AddInParameter(cmd, "VAT_EXCLUDE", DbType.Boolean, model.VatExcludeType);
                db.AddInParameter(cmd, "CONTRACT_NO", DbType.String, MakeDbNull(model.ContractNo));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(string.IsNullOrEmpty(model.Plate) ? string.Empty : model.Plate.ToUpper().Replace(" ", "")));
                db.AddInParameter(cmd, "COLOR", DbType.String, MakeDbNull(model.Color));
                db.AddInParameter(cmd, "WARRANTY_START_DATE", DbType.DateTime, MakeDbNull(model.WarrantyStartDate));
                db.AddInParameter(cmd, "WARRANTY_END_DATE", DbType.DateTime, MakeDbNull(model.WarrantyEndDate));
                db.AddInParameter(cmd, "PAINT_WARRANTY_END_DATE", DbType.DateTime, MakeDbNull(model.PaintWarrantyEndDate));
                db.AddInParameter(cmd, "WARRANTY_END_KM", DbType.Int64, MakeDbNull(model.WarrantyEndKilometer));
                db.AddInParameter(cmd, "CORROSION_WARRANTY_END_DATE", DbType.DateTime, MakeDbNull(model.CorrosionWarrantyEndDate));
                db.AddInParameter(cmd, "VEHICLE_KM", DbType.Int64, MakeDbNull(model.VehicleKilometer));
                db.AddInParameter(cmd, "SPECIAL_CONDITIONS", DbType.String, MakeDbNull(model.SpecialConditions));
                db.AddInParameter(cmd, "NOTES", DbType.String, MakeDbNull(model.Notes));
                db.AddInParameter(cmd, "WARRANTY_STAT", DbType.Int32, model.WarrantyStatus);
                db.AddInParameter(cmd, "OUTOF_WARRANTY_DESC", DbType.String, MakeDbNull(model.OutOfWarrantyDescription));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, model.IsActive);
                db.AddInParameter(cmd, "ID_PRICE_LIST", DbType.Int32, model.IdPriceList);
                db.AddInParameter(cmd, "IS_HOUR_MAINT", DbType.Int16, model.IsHourMaint);
                db.AddInParameter(cmd, "HOUR", DbType.Int32, model.Hour);
                db.AddInParameter(cmd, "LOCATION", DbType.String, MakeDbNull(model.Location));
                db.AddInParameter(cmd, "RESPONSIBLE_PERSON", DbType.String, MakeDbNull(model.ResponsiblePerson));
                db.AddInParameter(cmd, "RESPONSIBLE_PERSON_PHONE", DbType.String, MakeDbNull(model.ResponsiblePersonPhone));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.VehicleId = db.GetParameterValue(cmd, "VEHICLE_ID").GetValue<int>();
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

        public List<VehicleIndexViewModel> CheckVehicle(UserInfo user, string vinNoList)
        {
            List<VehicleIndexViewModel> existsVehicleList = new List<VehicleIndexViewModel>();
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_VEHICLE");
                db.AddInParameter(cmd, "VIN_NO_LIST", DbType.String, vinNoList);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    VehicleIndexViewModel vehicleModel = new VehicleIndexViewModel
                    {
                        VehicleId = dReader["VEHICLE_ID"].GetValue<int>(),
                        VinNo = dReader["VIN_NO"].ToString()
                    };
                    existsVehicleList.Add(vehicleModel);
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
            return existsVehicleList;
        }

        public VehicleIndexViewModel GetVehicle(UserInfo user, VehicleIndexViewModel vehicleModel)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(vehicleModel.VehicleId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    vehicleModel.Color = dReader["COLOR"].ToString();
                    vehicleModel.ContractNo = dReader["CONTRACT_NO"].ToString();
                    vehicleModel.CorrosionWarrantyEndDate = dReader["CORROSION_WARRANTY_END_DATE"].GetValue<DateTime?>();
                    vehicleModel.CustomerId = dReader["CUSTOMER_ID"].GetValue<int?>();
                    vehicleModel.CustomerName = dReader["CUSTOMER_NAME"].ToString();
                    vehicleModel.EngineNo = dReader["ENGINE_NO"].ToString();
                    vehicleModel.FactoryProductionDate = dReader["FACT_PROD_DATE"].GetValue<DateTime?>();
                    vehicleModel.FactoryQualityControlDate = dReader["FACT_QCNTRL_DATE"].GetValue<DateTime?>();
                    vehicleModel.FactoryShipmentDate = dReader["FACT_SHIP_DATE"].GetValue<DateTime?>();
                    vehicleModel.ModelYear = dReader["MODEL_YEAR"].GetValue<int>();
                    vehicleModel.Notes = dReader["NOTES"].ToString();
                    vehicleModel.OutOfWarrantyDescription = dReader["OUTOF_WARRANTY_DESC"].ToString();
                    vehicleModel.PaintWarrantyEndDate = dReader["PAINT_WARRANTY_END_DATE"].GetValue<DateTime?>();
                    vehicleModel.Plate = dReader["PLATE"].ToString();
                    vehicleModel.SpecialConditions = dReader["SPECIAL_CONDITIONS"].ToString();
                    vehicleModel.VatExcludeType = dReader["VAT_EXCLUDE"].GetValue<int?>();
                    vehicleModel.VatExcludeTypeName = dReader["VAT_EXCLUDE_NAME"].ToString();
                    vehicleModel.VehicleCode = dReader["VEHICLE_CODE"].ToString();
                    vehicleModel.VehicleId = dReader["VEHICLE_ID"].GetValue<int>();
                    vehicleModel.VehicleKilometer = dReader["VEHICLE_KM"].GetValue<long>();
                    vehicleModel.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    vehicleModel.IsActiveName = dReader["IS_ACTIVE_NAME"].ToString();
                    vehicleModel.IdPriceList = dReader["ID_PRICE_LIST"].GetValue<int>();
                    vehicleModel.SSIDPriceList = dReader["SSID_PRICE_LIST"].ToString();
                    vehicleModel.VinNo = dReader["VIN_NO"].ToString();
                    vehicleModel.WarrantyEndDate = dReader["WARRANTY_END_DATE"].GetValue<DateTime?>();
                    vehicleModel.WarrantyEndKilometer = dReader["WARRANTY_END_KM"].GetValue<long>();
                    vehicleModel.WarrantyStartDate = dReader["WARRANTY_START_DATE"].GetValue<DateTime?>();
                    vehicleModel.WarrantyStatus = dReader["WARRANTY_STAT"].GetValue<int?>();
                    vehicleModel.IsHourMaint = dReader["IS_HOUR_MAINT"].GetValue<bool>();
                    vehicleModel.IsHourMaintName = dReader["IS_HOUR_MAINT_NAME"].ToString();
                    vehicleModel.Hour = dReader["HOUR"].GetValue<int>();
                    vehicleModel.Location = dReader["LOCATION"].ToString();
                    vehicleModel.ResponsiblePerson = dReader["RESPONSIBLE_PERSON"].ToString();
                    vehicleModel.ResponsiblePersonPhone = dReader["RESPONSIBLE_PERSON_PHONE"].ToString();
                    vehicleModel.VehicleType = dReader["VEHICLE_TYPE"].ToString();
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
            return vehicleModel;
        }
        public VehicleIndexViewModel GetVehicleByVinNo(UserInfo user, VehicleIndexViewModel vehicleModel)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_VEHICLE_BY_VIN_NO");
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(vehicleModel.VinNo));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    vehicleModel.VehicleId = dReader["VEHICLE_ID"].GetValue<int>();
                    vehicleModel.VinNo = dReader["VIN_NO"].ToString();
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
            return vehicleModel;
        }
        public ServiceCallLogModel XMLtoDBVehicle(List<VehicleXMLModel> listModel)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_XML");

                CreateConnection(cmd);
                foreach (var model in listModel)
                {


                    cmd.Parameters.Clear();

                    SetCustomerType(model);

                    db.AddInParameter(cmd, "CODE_SSID", DbType.String, MakeDbNull(model.CodeSSID));
                    db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(model.VinNo));
                    db.AddInParameter(cmd, "ENGINE_NO", DbType.String, MakeDbNull(model.EngineNo));
                    db.AddInParameter(cmd, "MODEL_YEAR", DbType.String, MakeDbNull(model.ModelYear));
                    db.AddInParameter(cmd, "COLOR", DbType.String, MakeDbNull(model.Color));
                    db.AddInParameter(cmd, "FACT_PROD_DATE", DbType.String, MakeDbNull(model.FactProdDate));
                    db.AddInParameter(cmd, "FACT_QCNTRL_DATE", DbType.String, MakeDbNull(model.FactQcntrlDate));
                    db.AddInParameter(cmd, "FACT_SHIP_DATE", DbType.String, MakeDbNull(model.FactShipDate));
                    db.AddInParameter(cmd, "VAT_EXCLUDE", DbType.String, MakeDbNull(model.VatExclude));
                    db.AddInParameter(cmd, "SAP_CUSTOMER_NO", DbType.String, MakeDbNull(model.CustomerNo));
                    db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(model.CustomerName));
                    db.AddInParameter(cmd, "CUST_TYPE", DbType.String, MakeDbNull(model.CustType));
                    db.AddInParameter(cmd, "TC_NO", DbType.String, MakeDbNull(model.TCNo));
                    db.AddInParameter(cmd, "VAT_NO", DbType.String, MakeDbNull(model.VatNo));
                    db.AddInParameter(cmd, "VAT_OFFICE", DbType.String, MakeDbNull(model.VatOffice));
                    db.AddInParameter(cmd, "ADDRESS", DbType.String, MakeDbNull(model.Address));
                    db.AddInParameter(cmd, "COUNTRY_SHORT_CODE", DbType.String, MakeDbNull(model.CountryShortCode));
                    db.AddInParameter(cmd, "PLATE_CODE", DbType.String, MakeDbNull(model.PlateCode));
                    db.AddInParameter(cmd, "CITY", DbType.String, MakeDbNull(model.City));
                    db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                    db.AddInParameter(cmd, "MOBILE_PHONE", DbType.String, MakeDbNull(model.MobilePhone));
                    db.AddInParameter(cmd, "EMAIL", DbType.String, MakeDbNull(model.Email));
                    db.AddInParameter(cmd, "FAX_NO", DbType.String, MakeDbNull(model.FaxNo));
                    db.AddInParameter(cmd, "KAMU", DbType.String, MakeDbNull(model.Kamu));
                    db.AddInParameter(cmd, "LIST_PRICE", DbType.String, MakeDbNull(model.PriceList));
                    db.AddInParameter(cmd, "BSTKD", DbType.String, MakeDbNull(model.BSTKD));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd);
                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.VinNo + " - Şasi",
                            Error = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>()
                        });

                        rModel.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                rModel.IsSuccess = false;
                rModel.LogErrorDesc = ex.Message;
            }
            finally
            {
                if (!rModel.IsSuccess)
                {
                    rModel.ErrorModel = listError;
                }
                CloseConnection();
            }
            return rModel;
        }

        private bool SetCustomerType(VehicleXMLModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.Kamu))
                {
                    if (model.VatNo.Length > 0 && !string.IsNullOrEmpty(model.VatOffice))
                        model.CustType = "1";//tüzel
                    else if (!string.IsNullOrEmpty(model.TCNo))
                        model.CustType = "2";//gerçek
                }
                else
                    model.CustType = "3";//kamu
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                return false;
            }
            return !string.IsNullOrEmpty(model.CustType);
        }

        public ServiceCallLogModel XMLtoDBVehicleCustomer(List<VehicleCustomerXMLModel> listCustModel)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_VEHICLE_CUSTOMER_XML");

                CreateConnection(cmd);
                string errMsg = string.Empty;

                foreach (var model in listCustModel)
                {
                    cmd.Parameters.Clear();
                    errMsg = CheckVehicleCustomerInfo(model);
                    if (!errMsg.Equals(string.Empty))
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.VinNo + " - Şasi",
                            Error = errMsg
                        });

                        rModel.IsSuccess = false;
                    }

                    db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(model.VinNo));
                    db.AddInParameter(cmd, "ADDRESS", DbType.String, MakeDbNull(model.Address));
                    db.AddInParameter(cmd, "CUSTOMER_SSID", DbType.String, MakeDbNull(model.CustomerSSID));
                    db.AddInParameter(cmd, "COUNTRY_CODE", DbType.String, MakeDbNull(model.CountryCode));
                    db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(model.CustomerName));
                    db.AddInParameter(cmd, "ENGINE_NO", DbType.String, MakeDbNull(model.EngineNo));
                    db.AddInParameter(cmd, "DESC", DbType.String, MakeDbNull(model.Desc));
                    db.AddInParameter(cmd, "MOBILE_PHONE", DbType.String, MakeDbNull(model.MobilePhone));
                    db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                    db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(model.Plate));
                    db.AddInParameter(cmd, "CITY_PLATE_CODE", DbType.String, MakeDbNull(model.CityPlateCode));
                    db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(model.TCIdentity));
                    db.AddInParameter(cmd, "VAT_NO", DbType.String, MakeDbNull(model.VatNo));
                    db.AddInParameter(cmd, "VAT_OFFICE", DbType.String, MakeDbNull(model.VatOffice));
                    db.AddInParameter(cmd, "IS_PUBLIC", DbType.Boolean, !(string.IsNullOrEmpty(model.IsPublic)));
                    db.AddInParameter(cmd, "CUST_TYPE_LOOKVAL", DbType.String, MakeDbNull(model.CustType));
                    db.AddInParameter(cmd, "EMAIL", DbType.String, MakeDbNull(model.Email));
                    db.AddInParameter(cmd, "FAX", DbType.String, MakeDbNull(model.Fax));
                    db.AddInParameter(cmd, "CITY_NAME", DbType.String, MakeDbNull(model.CityName));
                    db.AddInParameter(cmd, "WARRANTY_START_DATE", DbType.DateTime,
                        MakeDbNull(model.WarrantyStartDate));
                    db.AddInParameter(cmd, "WARRANTY_END_DATE", DbType.DateTime, MakeDbNull(model.WarrantyEndDate));
                    db.AddInParameter(cmd, "WARRANTY_COLOR_END_DATE", DbType.DateTime,
                        MakeDbNull(model.WarrantyColorEndDate));
                    db.AddInParameter(cmd, "WARRANTY_CORRUSION_END_DATE", DbType.DateTime,
                        MakeDbNull(model.WarrantyCorrosionEndDate));
                    db.AddInParameter(cmd, "WARRANTY_KM", DbType.String, MakeDbNull(model.WarrantyKm));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    db.ExecuteNonQuery(cmd);

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.VinNo + " - Şasi",
                            Error = db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>()
                        });

                        rModel.IsSuccess = false;
                    }
                }
            }
            catch (Exception ex)
            {
                rModel.IsSuccess = false;
                rModel.LogErrorDesc = ex.Message;
            }
            finally
            {

                if (!rModel.IsSuccess)
                {
                    rModel.ErrorModel = listError;
                }
                CloseConnection();
            }
            return rModel;
        }

        private string CheckVehicleCustomerInfo(VehicleCustomerXMLModel model)
        {
            string errorMessage = string.Empty;
            try
            {
                long taxNo = 0;
                long tcIdentityNo = 0;
                if (model != null)
                {
                    //vat no must be integer
                    if (!string.IsNullOrEmpty(model.VatNo))
                        if (!long.TryParse(model.VatNo.Replace(" ", "").Replace(".", string.Empty), out taxNo) || taxNo == 0)
                            errorMessage = string.Format("Vat No bilgisinin uzunluğu hatalı ya da formatı yanlış {0}", model.VatNo);

                    if (!string.IsNullOrEmpty(model.TCIdentity))
                        if (!long.TryParse(model.TCIdentity.Replace(" ", "").Replace(".", string.Empty), out tcIdentityNo) || tcIdentityNo == 0)
                            errorMessage += string.Format(" TC Kimlik No bilgisinin uzunluğu hatalı ya da formatı yanlış {0}", model.TCIdentity);

                    if (string.IsNullOrEmpty(model.CountryCode))
                        errorMessage += string.Format(" Ülke kodu boş {0}", model.CountryCode);

                    if (string.IsNullOrEmpty(model.IsPublic))
                    {
                        if (tcIdentityNo > 0)
                            model.CustType = "2";//gerçek
                        else if (taxNo > 0 && !string.IsNullOrEmpty(model.VatOffice))
                            model.CustType = "1";//tüzel
                        else if (taxNo == 0 && tcIdentityNo == 0 && model.CountryCode != "TR")
                            model.CustType = "4"; //yurtdışı
                    }
                    else
                        model.CustType = "3";//kamu                    
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                if (!errorMessage.Equals(string.Empty))
                    model.CustType = null;
            }
            return errorMessage;
        }

        public VehicleContactInfoModel GetVehicleContactInfo(int vehicleId)
        {
            var model = new VehicleContactInfoModel();

            try
            {
                CreateDatabase();
                var mapper =
                    MapBuilder<VehicleContactInfoModel>.MapAllProperties()
                        .DoNotMap(c => c.ErrorNo)
                        .DoNotMap(c => c.ErrorDesc)
                        .DoNotMap(c => c.VehicleId)
                        .Build();
                model = db.ExecuteSprocAccessor("P_GET_VEHICLE_CONTACT_INFO", mapper, vehicleId).SingleOrDefault();

                model.VehicleId = vehicleId;
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

        public void UpdateVehicleContactInfo(UserInfo user, VehicleContactInfoModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_VEHICLE_CONTACT_INFO", model.VehicleId,
                    MakeDbNull(model.Location), MakeDbNull(model.ResponsiblePerson),
                    MakeDbNull(model.ResponsiblePersonPhone), user.UserId, null, null);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                if (model.ErrorNo > 0)
                    model.ErrorDesc = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").ToString());
            }
            finally
            {
                CloseConnection();
            }
        }

        public DataSet ListVehicleHistoryForService(string vinNo)
        {
            var dtVehicleCustomer = new DataTable();
            var dtWorkOrderPartsLabours = new DataTable();
            var ds = new DataSet();

            dtVehicleCustomer.TableName = "tblVehicleCustomerInfo";
            dtWorkOrderPartsLabours.TableName = "tblWorkOrderPartsLaboursInfo";

            ds.Tables.Add(dtVehicleCustomer);
            ds.Tables.Add(dtWorkOrderPartsLabours);
            try
            {

                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_INFO_WEB_SERVICE");
                db.AddInParameter(cmd, "VIN_NO", DbType.String, vinNo);

                CreateConnection(cmd);
                var drs = (IDataReader)cmd.ExecuteReader();
                ds.Load(drs, LoadOption.OverwriteChanges, dtVehicleCustomer, dtWorkOrderPartsLabours);
                drs.Close();
                drs.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }

        public List<VehicleHistoryTotalPriceListModel> ListVehicleHistoryTotalPrice(UserInfo user, VehicleHistoryTotalPriceListModel hModel, out int totalCount)
        {
            var listModel = new List<VehicleHistoryTotalPriceListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_VEHICLE_HISTORY_TOTAL_PRICE_FOR_OTOKAR");
                db.AddInParameter(cmd, "VEHICLE_ID", DbType.Int32, MakeDbNull(hModel.VehicleId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(user.DealerID));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new VehicleHistoryTotalPriceListModel()
                        {
                            CurrencyCode = dr["CURRENCY_CODE"].GetValue<string>(),
                            ProcessTypeCode = dr["PROCESS_TYPE_CODE"].GetValue<string>(),
                            ProcessTypeName = dr["PROCESS_TYPE_NAME"].GetValue<string>(),
                            IndicatorTypeCode = dr["INDICATOR_TYPE_CODE"].GetValue<string>(),
                            IndicatorTypeName = dr["INDICATOR_TYPE_NAME"].GetValue<string>(),
                            CustomerPrice = dr["CUSTOMER_PRICE"].GetValue<decimal>(),
                            OtokarPrice = dr["OTOKAR_PRICE"].GetValue<decimal>(),
                            CustomerTotalPrice = dr["CUSTOMER_TOTAL_PRICE"].GetValue<decimal>(),
                            OtokarTotalPrice = dr["OTOKAR_TOTAL_PRICE"].GetValue<decimal>()
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
        public void UpdateWarrantyEndStatuses()
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_UPDATE_VEHICLE_WARRANTY");
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

        public bool CheckVehicleByVinNoOrPlate(string vinNo, string plate)
        {
            bool isExists = false;
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_EXISTS_VEHICLE_BY_PLATE_OR_VIN_NO", plate, vinNo);
                CreateConnection(cmd);
                isExists = cmd.ExecuteScalar().GetValue<bool>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                CloseConnection();
            }

            return isExists;
        }

        /// <summary>
        /// Aracın son km bilgisini verir.
        /// </summary>
        /// <param name="vehicleId">Araç Id</param>
        public int GetLastVehicleKm(int vehicleId)
        {
            var km = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_LAST_VEHICLE_KM");
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, vehicleId);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        km = reader["VEHICLE_KM"].GetValue<int>();
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
            return km;
        }


    }
}
