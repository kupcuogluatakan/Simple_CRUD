using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.ObjectSearch;
using ODMSModel.PurchaseOrderDetail;

namespace ODMSData
{
    public class ObjectSearchData : DataAccessBase
    {
        public List<CustomerSearchListModel> SearchCustomer(UserInfo user, CustomerSearchListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerSearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_CUSTOMER");
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, (user.IsDealer) ? MakeDbNull(user.GetUserDealerId()) : null);
                db.AddInParameter(cmd, "CUSTOMER_FULL_NAME", DbType.String, MakeDbNull(filter.CustomerFullName));
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(filter.TCIdentityNo));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(filter.TaxNo));
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(filter.PassportNo));
                db.AddInParameter(cmd, "MOBILE_NO", DbType.String, MakeDbNull(filter.MobileNo));
                db.AddInParameter(cmd, "COUNTRY_ID", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "CUSTOMER_TYPE_ID", DbType.Int32, MakeDbNull(filter.CustomerTypeId));
                db.AddInParameter(cmd, "WITHOLDING_STATUS", DbType.Int32, MakeDbNull(filter.WitholdingStatus));
                db.AddInParameter(cmd, "ORG_TYPE_ID", DbType.Int32, MakeDbNull(filter.OrgTypeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
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
                        var customerListModel = new CustomerSearchListModel();
                        customerListModel.CustomerId = reader["CUSTOMER_ID"].GetValue<int>();
                        customerListModel.CustomerFullName = reader["CUSTOMER_FULL_NAME"].GetValue<string>();
                        customerListModel.SAPCustomerSSID = reader["SAP_CUSTOMER_SSID"].GetValue<string>();
                        customerListModel.BOSCustomerSSID = reader["BOS_CUSTOMER_SSID"].GetValue<string>();
                        customerListModel.CountryName = reader["COUNTRY_NAME"].GetValue<string>();
                        customerListModel.CompanyTypeName = reader["COMPANY_TYPE_NAME"].GetValue<string>();
                        customerListModel.CustomerTypeName = reader["CUSTOMER_TYPE_NAME"].GetValue<string>();
                        customerListModel.GovernmentTypeName = reader["GOVERNMENT_TYPE_NAME"].GetValue<string>();
                        customerListModel.TaxOffice = reader["TAX_OFFICE"].GetValue<string>();
                        customerListModel.TaxNo = reader["TAX_NO"].GetValue<string>();
                        customerListModel.TCIdentityNo = reader["TC_IDENTITY_NO"].GetValue<string>();
                        customerListModel.PassportNo = reader["PASSPORT_NO"].GetValue<string>();
                        retVal.Add(customerListModel);
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
        public List<CustomerSearchListModel> SearchDealer(UserInfo user, CustomerSearchListModel filter, out int totalCount)
        {
            var retVal = new List<CustomerSearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_DEALER");
                db.AddInParameter(cmd, "CUSTOMER_FULL_NAME", DbType.String, MakeDbNull(filter.CustomerFullName));
                db.AddInParameter(cmd, "TC_IDENTITY_NO", DbType.String, MakeDbNull(filter.TCIdentityNo));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(filter.TaxNo));
                db.AddInParameter(cmd, "PASSPORT_NO", DbType.String, MakeDbNull(filter.PassportNo));
                db.AddInParameter(cmd, "ORG_TYPE_ID", DbType.Int32, MakeDbNull(filter.OrgTypeId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
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
                        var customerListModel = new CustomerSearchListModel();
                        customerListModel.CustomerId = reader["CUSTOMER_ID"].GetValue<int>();
                        customerListModel.CustomerFullName = reader["CUSTOMER_FULL_NAME"].GetValue<string>();
                        customerListModel.SAPCustomerSSID = reader["SAP_CUSTOMER_SSID"].GetValue<string>();
                        customerListModel.BOSCustomerSSID = reader["BOS_CUSTOMER_SSID"].GetValue<string>();
                        customerListModel.CountryName = reader["COUNTRY_NAME"].GetValue<string>();
                        customerListModel.CompanyTypeName = reader["COMPANY_TYPE_NAME"].GetValue<string>();
                        customerListModel.CustomerTypeName = reader["CUSTOMER_TYPE_NAME"].GetValue<string>();
                        customerListModel.GovernmentTypeName = reader["GOVERNMENT_TYPE_NAME"].GetValue<string>();
                        customerListModel.TaxOffice = reader["TAX_OFFICE"].GetValue<string>();
                        customerListModel.TaxNo = reader["TAX_NO"].GetValue<string>();
                        customerListModel.TCIdentityNo = reader["TC_IDENTITY_NO"].GetValue<string>();
                        customerListModel.PassportNo = reader["PASSPORT_NO"].GetValue<string>();
                        retVal.Add(customerListModel);
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
        public List<AppointmentIndicatorSubCategorySearchListModel> SearchAppointmentIndicatorSubCategory(UserInfo user, AppointmentIndicatorSubCategorySearchListModel filter, out int totalCount)
        {
            var retVal = new List<AppointmentIndicatorSubCategorySearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_APPOINTMENT_INDICATOR_SUB_CATEGORY");
                db.AddInParameter(cmd, "MAIN_CATEGORY_CODE", DbType.String, MakeDbNull(filter.MainCategoryCode));
                db.AddInParameter(cmd, "MAIN_CATEGORY_NAME", DbType.String, MakeDbNull(filter.MainCategoryName));
                db.AddInParameter(cmd, "CATEGORY_CODE", DbType.String, MakeDbNull(filter.CategoryCode));
                db.AddInParameter(cmd, "CATEGORY_NAME", DbType.String, MakeDbNull(filter.CategoryName));
                db.AddInParameter(cmd, "SUB_CATEGORY_CODE", DbType.String, MakeDbNull(filter.SubCategoryCode));
                db.AddInParameter(cmd, "SUB_CATEGORY_NAME", DbType.String, MakeDbNull(filter.SubCategoryName));
                db.AddInParameter(cmd, "INDICATOR_TYPE_CODE", DbType.String, MakeDbNull(filter.IndicatorTypeCode));
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
                        var customerListModel = new AppointmentIndicatorSubCategorySearchListModel();
                        customerListModel.MainCategoryId = reader["MAIN_CATEGORY_ID"].GetValue<int>();
                        customerListModel.MainCategoryCode = reader["MAIN_CATEGORY_CODE"].GetValue<string>();
                        customerListModel.MainCategoryName = reader["MAIN_CATEGORY_NAME"].GetValue<string>();
                        customerListModel.CategoryId = reader["CATEGORY_ID"].GetValue<int>();
                        customerListModel.CategoryCode = reader["CATEGORY_CODE"].GetValue<string>();
                        customerListModel.CategoryName = reader["CATEGORY_NAME"].GetValue<string>();
                        customerListModel.SubCategoryId = reader["SUB_CATEGORY_ID"].GetValue<int>();
                        customerListModel.SubCategoryCode = reader["SUB_CATEGORY_CODE"].GetValue<string>();
                        customerListModel.SubCategoryName = reader["SUB_CATEGORY_NAME"].GetValue<string>();
                        customerListModel.IndicatorTypeCode = reader["INDICATOR_TYPE_CODE"].GetValue<string>();
                        retVal.Add(customerListModel);
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


        public List<VehicleSearchListModel> SearchVehicle(UserInfo user, VehicleSearchListModel filter, out int totalCount)
        {
            var retVal = new List<VehicleSearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SEARCH_VEHICLE");
                db.AddInParameter(cmd, "DEALER_ID", DbType.String, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "CUSTOMER_FULL_NAME", DbType.String, MakeDbNull(filter.CustomerFullName));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VinNo));
                db.AddInParameter(cmd, "WARRANTY_START_DATE", DbType.Date, MakeDbNull(filter.WarrantyStartDate));
                db.AddInParameter(cmd, "ENGINE_NO", DbType.String, MakeDbNull(filter.EngineNo));
                db.AddInParameter(cmd, "MODEL_YEAR", DbType.Int16, MakeDbNull(filter.ModelYear));
                db.AddInParameter(cmd, "PLATE", DbType.String, MakeDbNull(filter.Plate));
                db.AddInParameter(cmd, "VEHICLE_TYPE", DbType.String, MakeDbNull(filter.VehicleType));
                db.AddInParameter(cmd, "VEHICLE_MODEL", DbType.String, MakeDbNull(filter.VehicleModel));
                db.AddInParameter(cmd, "BODYWORK_DETAIL_REQUIRED", DbType.Boolean, filter.BodyworkDetailRequired);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
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
                        var vehicleListModel = new VehicleSearchListModel();
                        vehicleListModel.VehicleId = reader["VEHICLE_ID"].GetValue<int>();
                        vehicleListModel.VehicleCodeDesc = reader["VEHICLE_CODE_DESC"].GetValue<string>();
                        vehicleListModel.CustomerFullName = reader["CUSTOMER_FULL_NAME"].GetValue<string>();
                        vehicleListModel.VinNo = reader["VIN_NO"].GetValue<string>();
                        vehicleListModel.EngineNo = reader["ENGINE_NO"].GetValue<string>();
                        vehicleListModel.ModelYear = reader["MODEL_YEAR"].GetValue<int>();
                        vehicleListModel.FactoryProductionDate = reader["FACT_PROD_DATE"].GetValue<DateTime?>();
                        vehicleListModel.Plate = reader["PLATE"].GetValue<string>();
                        vehicleListModel.WarrantyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>();
                        vehicleListModel.Mobile = reader["MOBILE"].GetValue<string>();
                        vehicleListModel.Color = reader["COLOR"].GetValue<string>();
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

        public string GetObjectText(UserInfo user, CommonValues.ObjectSearchType filter, long objectId)
        {
            if (filter == CommonValues.ObjectSearchType.PurchaseOrder)
                return objectId.ToString();

            var retVal = "-";
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_OBJECT_TEXT");
                db.AddInParameter(cmd, "OBJECT_ID", DbType.Int64, MakeDbNull(objectId));
                db.AddInParameter(cmd, "OBJECT_TYPE_ID", DbType.String, MakeDbNull((int)filter));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                object text = cmd.ExecuteScalar();
                if (text != null)
                {
                    retVal = text.GetValue<string>();
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


        public List<AppointmentSearchListModel> SearchAppointment(UserInfo user, AppointmentSearchListModel filter, out int totalCount)
        {
            var retVal = new List<AppointmentSearchListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var ui = user;
                var cmd = db.GetStoredProcCommand("P_SEARCH_APPOINTMENT");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, ui.GetUserDealerId());
                db.AddInParameter(cmd, "CUSTOMER_NAME", DbType.String, MakeDbNull(filter.CustomerFullName));
                db.AddInParameter(cmd, "CONTACT_NAME", DbType.String, MakeDbNull(filter.ContactName));
                db.AddInParameter(cmd, "VEHICLE_PLATE", DbType.String, MakeDbNull(filter.VehiclePlate));
                db.AddInParameter(cmd, "START_DATE", DbType.DateTime, MakeDbNull(filter.StartDate));
                db.AddInParameter(cmd, "END_DATE", DbType.DateTime, MakeDbNull(filter.EndDate));
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
                        var item = new AppointmentSearchListModel();
                        item.AppointmentId = reader["APPOINTMENT_ID"].GetValue<int>();
                        item.CustomerFullName = reader["CUSTOMER_FULL_NAME"].GetValue<string>();
                        item.AppointmentDate = reader["APPOINTMENT_DATE"].GetValue<DateTime>();
                        item.ContactName = reader["CONTACT_NAME"].GetValue<string>();
                        item.ContactSurname = reader["CONTACT_SURNAME"].GetValue<string>();
                        item.ContactAddress = reader["CONTACT_ADDRESS"].GetValue<string>();
                        item.ContactPhone = reader["CONTACT_PHONE"].GetValue<string>();
                        item.VehiclePlate = reader["VEHICLE_PLATE"].GetValue<string>();
                        item.VehicleColor = reader["VEHICLE_COLOR"].GetValue<string>();
                        item.AppointmentType = reader["APPOINTMENT_TYPE"].GetValue<string>();
                        item.VehicleModel = reader["MODEL_NAME"].GetValue<string>();
                        retVal.Add(item);
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

        public List<FleetSearchListModel> SearchFleet(UserInfo user, FleetSearchListModel filter)
        {
            var retVal = new List<FleetSearchListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET");
                db.AddInParameter(cmd, "FLEET_NAME", DbType.String, MakeDbNull(filter.FleetName));
                db.AddInParameter(cmd, "FLEET_CODE", DbType.Int32, MakeDbNull(filter.FleetCode));
                db.AddInParameter(cmd, "IS_PART_CONSTRICTED", DbType.Int32, MakeDbNull(filter.IsPartConstricted));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, true);
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
                        var fleetListModel = new FleetSearchListModel();
                        fleetListModel.FleetId = reader["FLEET_ID"].GetValue<int>();
                        fleetListModel.FleetName = reader["FLEET_NAME"].GetValue<string>();
                        fleetListModel.FleetCode = reader["FLEET_CODE"].GetValue<string>();
                        fleetListModel.OtokarPartDiscountRate = reader["OTOKAR_PART_DISCOUNT_RATE"].GetValue<decimal>();
                        fleetListModel.OtokarLabourDiscountRate = reader["OTOKAR_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                        fleetListModel.DealerPartDiscountRate = reader["DEALER_PART_DISCOUNT_RATE"].GetValue<decimal>();
                        fleetListModel.DealerLabourDiscountRate = reader["DEALER_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                        fleetListModel.IsConstrictedName = reader["IS_CONSTRICTED_NAME"].GetValue<string>();
                        fleetListModel.StartDateTime = reader["VALIDITY_START_DATE"].GetValue<DateTime>();
                        fleetListModel.EndDateTime = reader["VALIDITY_END_DATE"].GetValue<DateTime>();

                        retVal.Add(fleetListModel);
                    }
                    reader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<PurchaseOrderSearchListModel> SearchPurchaseOrder(UserInfo user, PurchaseOrderSearchListModel filter)
        {
            var retVal = new List<PurchaseOrderSearchListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_OBJECTSEARCH");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int64, MakeDbNull(filter.PoNumber));
                db.AddInParameter(cmd, "ID_STOCK_TYPE", DbType.Int32, MakeDbNull(filter.IdStockType));
                db.AddInParameter(cmd, "DESIRED_SHIP_DATE", DbType.DateTime, MakeDbNull(filter.DesiredShipDate));
                db.AddInParameter(cmd, "STATUS", DbType.Int32, MakeDbNull(filter.Status));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "PO_TYPE", DbType.Int32, MakeDbNull(filter.PoType));
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int32, MakeDbNull(filter.SupplierId));

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
                        var purchaseOrderListModel = new PurchaseOrderSearchListModel();
                        purchaseOrderListModel.PoNumber = reader["PO_NUMBER"].GetValue<Int64>();
                        purchaseOrderListModel.PoTypeName = reader["POTYPE_NAME"].GetValue<string>();

                        purchaseOrderListModel.DesiredShipDate = reader["PO_DESIRED_SHIP_DATE"].GetValue<DateTime?>();
                        purchaseOrderListModel.VinNo = reader["VIN_NO"].GetValue<string>();
                        purchaseOrderListModel.Plate = reader["PLATE"].GetValue<string>();
                        purchaseOrderListModel.StatusName = reader["STATUS_NAME"].GetValue<string>();
                        purchaseOrderListModel.StatusName = reader["STATUS_NAME"].GetValue<string>();
                        purchaseOrderListModel.Status = reader["STATUS"].GetValue<int?>();

                        retVal.Add(purchaseOrderListModel);
                    }
                    reader.Close();
                }
                filter.TotalCount = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public List<PurchaseOrderDetailListModel> ListPurchaseOrderDetails(UserInfo user, PurchaseOrderDetailListModel filter, out int total)
        {
            var retVal = new List<PurchaseOrderDetailListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_DET_NOTADDED");
                db.AddInParameter(cmd, "PO_NUMBER", DbType.Int32, MakeDbNull(filter.PurchaseOrderNumber));
                db.AddInParameter(cmd, "PART_ID", DbType.Int64, MakeDbNull(filter.PartId));
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
                        var purchaseOrderDetailListModel = new PurchaseOrderDetailListModel
                        {
                            DesireQuantity = reader["DESIRE_QUANTITY"].GetValue<decimal>(),
                            OrderPrice = reader["ORDER_PRICE"].GetValue<decimal>(),
                            OrderQuantity = reader["ORDER_QUANTITY"].GetValue<decimal>(),
                            PackageQuantity = reader["PACKAGE_QUANTITY"].GetValue<decimal>(),
                            PartId = reader["PART_ID"].GetValue<int>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            PurchaseOrderDetailSeqNo = reader["PO_DET_SEQ_NO"].GetValue<int>(),
                            ShipmentQuantity = reader["SHIP_QUANTITY"].GetValue<decimal>(),
                            PurchaseOrderNumber = reader["PO_NUMBER"].GetValue<int>(),
                            StatusId = reader["STATUS_ID"].GetValue<int>(),
                            StatusName = reader["STATUS_NAME"].GetValue<string>()
                        };
                        retVal.Add(purchaseOrderDetailListModel);
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
    }
}
