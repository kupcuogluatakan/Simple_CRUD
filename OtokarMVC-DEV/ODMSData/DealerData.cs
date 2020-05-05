using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Dealer;
using ODMSModel.DealerVehicleGroup;

namespace ODMSData
{
    public class DealerData : DataAccessBase
    {
        public List<SelectListItem> ListDealerAsSelectListItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
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

        public List<SelectListItem> ListDealerSSIdAsSelectItem()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_SSID");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_SSID"].GetValue<string>()
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
        public List<DealerListModel> ListDealers(UserInfo user,DealerListModel filter, out int total)
        {
            var retVal = new List<DealerListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALERS");
                db.AddInParameter(cmd, "DEALER_SSID", DbType.String, MakeDbNull(filter.SSID));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "ID_CITY", DbType.Int32, MakeDbNull(filter.CityId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.SearchIsActive);
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(filter.PoGroupId));
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new DealerListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            ShortName = reader["DEALER_SHRT_NAME"].GetValue<string>(),
                            Name = reader["DEALER_NAME"].GetValue<string>(),
                            SSID = reader["DEALER_SSID"].GetValue<string>()
                        };
                        retVal.Add(dealerListModel);
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

        public List<DealerListModel> ListDealers(string languageCode)
        {
            var retVal = new List<DealerListModel>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, languageCode);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new DealerListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            Name = reader["DEALER_NAME"].GetValue<string>(),
                            Address = reader["ADDRESS"].GetValue<string>(),
                            CountryId = reader["ID_COUNTRY"].GetValue<int>(),
                            Country = reader["COUNTRY_NAME"].GetValue<string>(),
                            CityId = reader["ID_CITY"].GetValue<int>(),
                            City = reader["CITY_NAME"].GetValue<string>(),
                            TownId = reader["ID_TOWN"].GetValue<int>(),
                            Town = reader["TOWN_NAME"].GetValue<string>(),
                            GroupName = reader["GROUP_NAME"].GetValue<string>(),
                            Latitude = reader["LATITUDE"].GetValue<decimal>(),
                            Longitute = reader["LONGITUDE"].GetValue<decimal>(),
                            IsSaleDealer = reader["IS_SALE_DEALER"].GetValue<bool>()
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

        public DealerListModel ListDealerLastVersion()
        {
            var retVal = new DealerListModel();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_LAST_VERSION");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new DealerListModel
                        {
                            DealerLastUpdate = reader["DEALER_LAST_UPDATE"].GetValue<int>(),
                            CountryLastUpdate = reader["COUNTRY_LAST_UPDATE"].GetValue<int>(),
                            CityLastUpdate = reader["CITY_LAST_UPDATE"].GetValue<int>(),
                            TownLastUpdate = reader["TOWN_LAST_UPDATE"].GetValue<int>()
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

        public List<DealerListModel> ListDealersGrid(UserInfo user,DealerListModel filter, out int total)
        {
            var retVal = new List<DealerListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALERS_GRID");
                db.AddInParameter(cmd, "DEALER_SSID", DbType.String, MakeDbNull(filter.SSID));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(filter.DealerRegionId));
                db.AddInParameter(cmd, "IS_ELECTRONIC_INVOICE_ENABLED", DbType.Int32, MakeDbNull(filter.IsElectronicInvoiceEnabled));
                db.AddInParameter(cmd, "ACCEPT_ORDER_PROPOSAL", DbType.Int32, MakeDbNull(filter.AcceptOrderProposal));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(filter.CountryId));
                db.AddInParameter(cmd, "ID_CITY", DbType.Int32, MakeDbNull(filter.CityId));
                db.AddInParameter(cmd, "IS_SALE_DEALER", DbType.Int32, MakeDbNull(filter.IsSaleDealer));
                db.AddInParameter(cmd, "ID_PURCHASE_GROUP", DbType.Int32, MakeDbNull(filter.PurchaseOrderGroupId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.SearchIsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerListModel = new DealerListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            SSID = reader["SSID"].GetValue<string>(),
                            BranchSSID = reader["DEALER_BRANCH_SSID"].GetValue<string>(),
                            City = reader["CITY_NAME"].GetValue<string>(),
                            ShortName = reader["DEALER_SHRT_NAME"].GetValue<string>(),
                            Country = reader["COUNTRY_NAME"].GetValue<string>(),
                            ContactFullName = reader["CNTCT_NAME_SURNAME"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            Name = reader["DEALER_NAME"].GetValue<string>(),
                            RegionName = reader["DEALER_REGION_NAME"].GetValue<string>(),
                            IsSaleDealer = reader["IS_SALE_DEALER"].GetValue<bool>(),
                            IsSaleDealerString = reader["IS_SALE_DEALER_STRING"].GetValue<string>(),
                            PurchaseOrderGroupId = reader["PURCHASE_ORDER_GROUP_ID"].GetValue<string>(),
                            PurchaseOrderGroupName = reader["PURCHASE_ORDER_GROUP_NAME"].GetValue<string>()
                        };
                        retVal.Add(dealerListModel);
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

        public List<SelectListItem> ListDealerRegionsAsSelectListItem()
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_REGION_SHORT");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                        {
                            Value = reader["ID_DEALER_REGION"].GetValue<string>(),
                            Text = reader["DEALER_REGION_NAME"].GetValue<string>()
                        };
                        retVal.Add(listItem);
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

        public void DMLDealer(UserInfo user, DealerViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_MAIN");
                db.AddParameter(cmd, "ID_DEALER", DbType.Int32, ParameterDirection.InputOutput, "ID_DEALER", DataRowVersion.Default, model.DealerId);
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(model.PurchaseOrderGroupId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "DEALER_SSID", DbType.String, MakeDbNull(model.SSID));
                db.AddInParameter(cmd, "DEALER_BRANCH_SSID", DbType.String, MakeDbNull(model.BranchSSID));
                db.AddInParameter(cmd, "DEALER_SHRT_NAME", DbType.String, MakeDbNull(model.ShortName));
                db.AddInParameter(cmd, "DEALER_NAME", DbType.String, MakeDbNull(model.Name));
                db.AddInParameter(cmd, "ID_DEALER_REGION", DbType.Int32, MakeDbNull(model.DealerRegionId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(model.Country));
                db.AddInParameter(cmd, "ID_CITY", DbType.Int32, MakeDbNull(model.City));
                db.AddInParameter(cmd, "FOREIGN_CITY", DbType.String, MakeDbNull(model.ForeignCity));
                db.AddInParameter(cmd, "ID_TOWN", DbType.Int32, MakeDbNull(model.Town));
                db.AddInParameter(cmd, "FOREIGN_TOWN", DbType.String, MakeDbNull(model.ForeignTown));
                db.AddInParameter(cmd, "ADDRESS1", DbType.String, MakeDbNull(model.Address1));
                db.AddInParameter(cmd, "ADDRESS2", DbType.String, MakeDbNull(model.Address2));
                db.AddInParameter(cmd, "TAX_OFFICE", DbType.String, MakeDbNull(model.TaxOffice));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(model.TaxNo));
                db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                db.AddInParameter(cmd, "MOBILE_PHONE", DbType.String, MakeDbNull(model.MobilePhone));
                db.AddInParameter(cmd, "FAX", DbType.String, MakeDbNull(model.Fax));
                db.AddInParameter(cmd, "CNTCT_NAME_SURNAME", DbType.String, MakeDbNull(model.ContactNameSurname));
                db.AddInParameter(cmd, "DEALER_CLASS_CODE", DbType.String, MakeDbNull(model.DealerClassCode));
                db.AddInParameter(cmd, "TS12047_CERTIFICATE_CHCK", DbType.Boolean, MakeDbNull(model.HasTs12047Certificate));
                db.AddInParameter(cmd, "TS12047_VALID_DATE", DbType.DateTime, MakeDbNull(model.Ts12047CertificateDate));
                db.AddInParameter(cmd, "CNTCT_EMAIL", DbType.String, MakeDbNull(model.ContactEmail));
                db.AddInParameter(cmd, "SERVICE_RESP_INSRNCE_CHCK", DbType.Boolean, MakeDbNull(model.HasServiceResponsibilityInsurance));
                db.AddInParameter(cmd, "CUSTOMER_GRP_LOOK_UP_CODE", DbType.String, MakeDbNull(DealerViewModel.CustomerGroupLookUpCode));
                db.AddInParameter(cmd, "CUSTOMER_GRP_LOOKVAL", DbType.String, MakeDbNull(model.CustomerGroupLookVal));
                db.AddInParameter(cmd, "CLAIM_RATIO", DbType.Decimal, MakeDbNull(model.ClaimRatio));
                db.AddInParameter(cmd, "WORKSHOP_PLAN_TYPE", DbType.Boolean, MakeDbNull(model.WorkshopPlanType == DealerViewModel.WorkshopPlan.Advanced));//Basic=0 || Advanced=1
                db.AddInParameter(cmd, "IS_ELECTRONIC_INVOICE_ENABLED", DbType.Boolean, MakeDbNull(model.IsElectronicInvoiceEnabled));
                db.AddInParameter(cmd, "ACCEPT_ORDER_PROPOSAL", DbType.Boolean, MakeDbNull(model.AcceptOrderProposal));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "AUTO_MRP", DbType.Boolean, MakeDbNull(model.AutoMrp));
                db.AddInParameter(cmd, "LATITUDE", DbType.Decimal, MakeDbNull(model.Latitude == null ? string.Empty : model.Latitude.Replace(".", ",")));
                db.AddInParameter(cmd, "LONGITUDE", DbType.Decimal, MakeDbNull(model.Longitude == null ? string.Empty : model.Longitude.Replace(".", ",")));
                db.AddInParameter(cmd, "CHANNEL_CODE", DbType.String, MakeDbNull(model.SaleChannelCode));
                db.AddInParameter(cmd, "IS_SALE_DEALER", DbType.Boolean, MakeDbNull(model.IsSaleDealer));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.DealerId = db.GetParameterValue(cmd, "ID_DEALER").GetValue<int>();
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
        public DealerViewModel GetDealer(UserInfo user, int dealerId)
        {
            DbDataReader dReader = null;
            var dealer = new DealerViewModel { DealerId = dealerId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    dealer.DealerId = dealerId;
                    dealer.SSID = dReader["DEALER_SSID"].GetValue<string>();
                    dealer.BranchSSID = dReader["DEALER_BRANCH_SSID"].GetValue<string>();
                    dealer.ShortName = dReader["DEALER_SHRT_NAME"].GetValue<string>();
                    dealer.Name = dReader["DEALER_NAME"].GetValue<string>();
                    dealer.DealerRegionId = dReader["ID_DEALER_REGION"].GetValue<int>();
                    dealer.RegionName = dReader["DEALER_REGION_NAME"].GetValue<string>();
                    dealer.Country = dReader["ID_COUNTRY"].GetValue<int>();
                    dealer.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();
                    dealer.City = dReader["ID_CITY"].GetValue<int>();
                    dealer.CityName = dReader["CITY_NAME"].GetValue<string>();
                    dealer.ForeignCity = dReader["FOREIGN_CITY"].GetValue<string>();
                    dealer.Town = dReader["ID_TOWN"].GetValue<String>();
                    dealer.TownName = dReader["TOWN_NAME"].GetValue<string>();
                    dealer.ForeignTown = dReader["FOREIGN_TOWN"].GetValue<string>();
                    dealer.Address1 = dReader["ADDRESS1"].GetValue<string>();
                    dealer.Address2 = dReader["ADDRESS2"].GetValue<string>();
                    dealer.TaxOffice = dReader["TAX_OFFICE"].GetValue<string>();
                    dealer.TaxNo = dReader["TAX_NO"].GetValue<string>();
                    dealer.Phone = dReader["PHONE"].GetValue<string>();
                    dealer.MobilePhone = dReader["MOBILE_PHONE"].GetValue<string>();
                    dealer.Fax = dReader["FAX"].GetValue<string>();
                    dealer.ContactNameSurname = dReader["CNTCT_NAME_SURNAME"].GetValue<string>();
                    dealer.ContactEmail = dReader["CNTCT_EMAIL"].GetValue<string>();
                    dealer.DealerClassCode = dReader["DEALER_CLASS_CODE"].GetValue<string>();
                    dealer.DealerClassName = dReader["DEALER_CLASS_NAME"].GetValue<string>();
                    dealer.HasTs12047Certificate = dReader["TS12047_CERTIFICATE_CHCK"].GetValue<bool>();
                    dealer.Ts12047CertificateDate = dReader["TS12047_VALID_DATE"].GetValue<DateTime?>();
                    dealer.HasServiceResponsibilityInsurance = dReader["SERVICE_RESP_INSRNCE_CHCK"].GetValue<bool>();
                    dealer.CustomerGroupLookKey = dReader["CUSTOMER_GRP_LOOKKEY"].GetValue<int>();
                    dealer.CustomerGroupLookVal = dReader["CUSTOMER_GRP_LOOKVAL"].GetValue<string>();
                    dealer.CustomerGroup = dReader["CUSTOMER_GROUP"].GetValue<string>();
                    dealer.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    dealer.CurrencyName = dReader["CURRENCY_NAME"].GetValue<string>();
                    dealer.WorkshopPlanType = dReader["WORKSHOP_PLAN_TYPE"].GetValue<bool>()
                        ? DealerViewModel.WorkshopPlan.Advanced
                        : DealerViewModel.WorkshopPlan.Basic;
                    dealer.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    dealer.SaleChannelCode = dReader["CHANNEL_CODE"].GetValue<string>();
                    dealer.SaleChannelName = dReader["CHANNEL_NAME"].GetValue<string>();
                    dealer.ClaimRatio = dReader["CLAIM_RATIO"].GetValue<decimal>();
                    dealer.AutoMrp = dReader["AUTO_MRP"].GetValue<bool>();
                    dealer.LastMrpDate = dReader["LAST_MRP_DATE"].GetValue<DateTime?>().HasValue
                                             ? dReader["LAST_MRP_DATE"].GetValue<DateTime?>().Value.ToString("dd.MM.yyyy H:mm")
                                             : string.Empty;
                    dealer.PurchaseOrderGroupName = dReader["GROUP_NAME"].GetValue<string>();
                    dealer.PurchaseOrderGroupId = dReader["ID_PO_GROUP"].GetValue<string>();
                    dealer.IsElectronicInvoiceEnabled = dReader["IS_ELECTRONIC_INVOICE_ENABLED"].GetValue<bool>();
                    dealer.AcceptOrderProposal = dReader["ACCEPT_ORDER_PROPOSAL"].GetValue<bool>();
                    dealer.Latitude = dReader["LATITUDE"].GetValue<string>();
                    dealer.Longitude = dReader["LONGITUDE"].GetValue<string>();
                    dealer.IsSaleDealer = dReader["IS_SALE_DEALER"].GetValue<bool>();
                    dealer.RegionResponsibleEmail = dReader["REGION_RESPONSIBLE_EMAIL"].GetValue<string>();
                    dealer.RegionResponsibleUserId = dReader["REGION_RESPONSIBLE_USER_ID"].GetValue<int>();
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
            return dealer;
        }


        public DealerViewModel GetDealerBySSID(UserInfo user, string ssid)
        {
            DbDataReader dReader = null;
            var dealer = new DealerViewModel { SSID = ssid };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_BY_SSID");
                db.AddInParameter(cmd, "DEALER_SSID", DbType.String, MakeDbNull(ssid));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    dealer.DealerId = dReader["ID_DEALER"].GetValue<int>();
                    dealer.SSID = dReader["DEALER_SSID"].GetValue<string>();
                    dealer.BranchSSID = dReader["DEALER_BRANCH_SSID"].GetValue<string>();
                    dealer.ShortName = dReader["DEALER_SHRT_NAME"].GetValue<string>();
                    dealer.Name = dReader["DEALER_NAME"].GetValue<string>();
                    dealer.DealerRegionId = dReader["ID_DEALER_REGION"].GetValue<int>();
                    dealer.RegionName = dReader["DEALER_REGION_NAME"].GetValue<string>();
                    dealer.Country = dReader["ID_COUNTRY"].GetValue<int>();
                    dealer.CountryName = dReader["COUNTRY_NAME"].GetValue<string>();
                    dealer.City = dReader["ID_CITY"].GetValue<int>();
                    dealer.CityName = dReader["CITY_NAME"].GetValue<string>();
                    dealer.Town = dReader["ID_TOWN"].GetValue<String>();
                    dealer.TownName = dReader["TOWN_NAME"].GetValue<string>();
                    dealer.Address1 = dReader["ADDRESS1"].GetValue<string>();
                    dealer.Address2 = dReader["ADDRESS2"].GetValue<string>();
                    dealer.TaxOffice = dReader["TAX_OFFICE"].GetValue<string>();
                    dealer.TaxNo = dReader["TAX_NO"].GetValue<string>();
                    dealer.Phone = dReader["PHONE"].GetValue<string>();
                    dealer.MobilePhone = dReader["MOBILE_PHONE"].GetValue<string>();
                    dealer.Fax = dReader["FAX"].GetValue<string>();
                    dealer.ContactNameSurname = dReader["CNTCT_NAME_SURNAME"].GetValue<string>();
                    dealer.ContactEmail = dReader["CNTCT_EMAIL"].GetValue<string>();
                    dealer.DealerClassCode = dReader["DEALER_CLASS_CODE"].GetValue<string>();
                    dealer.DealerClassName = dReader["DEALER_CLASS_NAME"].GetValue<string>();
                    dealer.HasTs12047Certificate = dReader["TS12047_CERTIFICATE_CHCK"].GetValue<bool>();
                    dealer.Ts12047CertificateDate = dReader["TS12047_VALID_DATE"].GetValue<DateTime?>();
                    dealer.HasServiceResponsibilityInsurance = dReader["SERVICE_RESP_INSRNCE_CHCK"].GetValue<bool>();
                    dealer.CustomerGroupLookKey = dReader["CUSTOMER_GRP_LOOKKEY"].GetValue<int>();
                    dealer.CustomerGroupLookVal = dReader["CUSTOMER_GRP_LOOKVAL"].GetValue<string>();
                    dealer.CustomerGroup = dReader["CUSTOMER_GROUP"].GetValue<string>();
                    dealer.CurrencyCode = dReader["CURRENCY_CODE"].GetValue<string>();
                    dealer.CurrencyName = dReader["CURRENCY_NAME"].GetValue<string>();
                    dealer.WorkshopPlanType = dReader["WORKSHOP_PLAN_TYPE"].GetValue<bool>()
                        ? DealerViewModel.WorkshopPlan.Advanced
                        : DealerViewModel.WorkshopPlan.Basic;
                    dealer.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    dealer.SaleChannelCode = dReader["CHANNEL_CODE"].GetValue<string>();
                    dealer.SaleChannelName = dReader["CHANNEL_NAME"].GetValue<string>();
                    dealer.ClaimRatio = dReader["CLAIM_RATIO"].GetValue<decimal>();
                    dealer.AutoMrp = dReader["AUTO_MRP"].GetValue<bool>();
                    dealer.LastMrpDate = dReader["LAST_MRP_DATE"].GetValue<DateTime?>().HasValue
                                             ? dReader["LAST_MRP_DATE"].GetValue<DateTime?>().Value.ToString("dd.MM.yyyy H:mm")
                                             : string.Empty;
                    dealer.PurchaseOrderGroupName = dReader["GROUP_NAME"].GetValue<string>();
                    dealer.PurchaseOrderGroupId = dReader["ID_PO_GROUP"].GetValue<string>();
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
            return dealer;
        }

        public List<DealerVehicleGroupsListModel> ListDealerVehicleGroups(UserInfo user,DealerVehicleGroupsListModel filter, out int total)
        {
            var retVal = new List<DealerVehicleGroupsListModel>();
            total = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_VEHICLE_GROUPS_GRID");
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, filter.DealerId);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var item = new DealerVehicleGroupsListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            VehicleGroupId = reader["ID_VEHICLE_GROUP"].GetValue<int>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            VehicleModelName = reader["MODEL_NAME"].GetValue<string>(),
                            VehicleModelCode = reader["MODEL_KOD"].GetValue<string>(),
                            VehicleGroupName = reader["VEHICLE_GROUP_NAME"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>()
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

        //TODO : Id set edilmeli
        public void DMLDealerVehicleGroups(UserInfo user, DealerVehicleGroupViewModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_DEALER_VHCL_GRP_RELATION");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "MODEL_CODE", DbType.String, MakeDbNull(model.VehicleModelCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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

        public DealerVehicleGroupViewModel GetDealerVehicleGroup(UserInfo user, int idDealer, int idVehicleGroup)
        {
            DbDataReader dReader = null;
            var model = new DealerVehicleGroupViewModel { DealerId = idDealer, VehicleGroupId = idVehicleGroup };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_VHCL_GRP_RELATION");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(idDealer));
                db.AddInParameter(cmd, "ID_VEHICLE_GROUP", DbType.Int32, MakeDbNull(idVehicleGroup));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    model.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    model.VehicleGroupName = dReader["VHCL_GRP_NAME"].GetValue<string>();
                    model.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    model.IsActiveString = dReader["IS_ACTIVE_STRING"].GetValue<string>();
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
            return model;
        }

        public List<SelectListItem> GetDealerUsersAsSelectListItem(int dealerId)
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_USERS_COMBO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listItem = new SelectListItem
                        {
                            Value = reader["DMS_USER_CODE"].GetValue<string>(),
                            Text = reader["USER_NAME"].GetValue<string>()
                        };
                        retVal.Add(listItem);
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

        public int GetCountryDefaultPriceList(int? countryId)
        {
            int result = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_COUNTRY_DEFAULT_PRICE_LIST");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(countryId));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<int>();
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
            return result;
        }
        public string GetCountryCurrencyCode(int? countryId)
        {
            string result = string.Empty;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_COUNTRY_CURRENCY_CODE");
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(countryId));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<string>();
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
            return result;
        }
        public bool ExistsDealerByDealerSSID(string ssid)
        {
            bool result = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_EXISTS_DEALER_BY_DEALER_SSID");
                db.AddInParameter(cmd, "DEALER_SSID", DbType.String, MakeDbNull(ssid));
                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        result = dr[0].GetValue<bool>();
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
            return result;
        }
    }

}

