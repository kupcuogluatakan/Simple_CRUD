using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Supplier;
using System;

namespace ODMSData
{
    public class SupplierData : DataAccessBase
    {
        public List<SupplierListModel> ListSuppliers(UserInfo user,SupplierListModel model, out int total)
        {
            var retVal = new List<SupplierListModel>();
            total = 0;
            try
            {


                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SUPPLIER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.String, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "SUPPLIER_NAME", DbType.String, MakeDbNull(model.SupplierName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.SearchIsActive);
                db.AddInParameter(cmd, "MOBILE_PHONE", DbType.String, MakeDbNull(model.MobilePhone));
                db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                db.AddInParameter(cmd, "TAX_OFFICE", DbType.String, MakeDbNull(model.TaxOffice));
                db.AddInParameter(cmd, "CONTACT_PERSON", DbType.String, model.ContactPerson);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(model.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(model.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, model.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(model.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var supplier = new SupplierListModel
                        {
                            SupplierId = reader["ID_SUPPLIER"].GetValue<int>(),
                            Ssid = reader["SUPPLIER_SSID"].GetValue<string>(),
                            SupplierName = reader["SUPPLIER_NAME"].GetValue<string>(),
                            TaxOffice = reader["TAX_OFFICE"].GetValue<string>(),
                            TaxNo = reader["TAX_NO"].GetValue<string>(),
                            ContactPerson = reader["CONTACT_PERSON"].GetValue<string>(),
                            Email = reader["E_MAIL"].GetValue<string>(),
                            Phone = reader["PHONE"].GetValue<string>(),
                            Fax = reader["FAX"].GetValue<string>(),
                            MobilePhone = reader["MOBILE_PHONE"].GetValue<string>(),
                            ChamberOfCommerce = reader["CHAMBER_OF_COMMERCE"].GetValue<string>(),
                            TradeRegistryNo = reader["TRADE_REGISTRY_NO"].GetValue<string>(),
                            CountryName = reader["COUNTRY_NAME"].GetValue<string>(),
                            CityName = reader["CITY_NAME"].GetValue<string>(),
                            TownName = reader["TOWN_NAME"].GetValue<string>(),
                            ZipCode = reader["ZIP_CODE"].GetValue<string>(),
                            IsActiveString = reader["IS_ACTIVE_STRING"].GetValue<string>()
                        };

                        retVal.Add(supplier);
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

        public void DLMSupplier(UserInfo user,SupplierViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SUPPLIER");
                db.AddParameter(cmd, "ID_SUPPLIER", DbType.Int32, ParameterDirection.InputOutput, "ID_SUPPLIER", DataRowVersion.Default, model.SupplierId);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "SUPPLIER_SSID", DbType.String, MakeDbNull(model.Ssid));
                db.AddInParameter(cmd, "SUPPLIER_NAME", DbType.String, MakeDbNull(model.SupplierName));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "ID_COUNTRY", DbType.Int32, MakeDbNull(model.CountryId));
                db.AddInParameter(cmd, "ID_CITY", DbType.Int32, MakeDbNull(model.CityId));
                db.AddInParameter(cmd, "ID_TOWN", DbType.Int32, MakeDbNull(model.TownId));
                db.AddInParameter(cmd, "ADDRESS", DbType.String, MakeDbNull(model.Address));
                db.AddInParameter(cmd, "TAX_OFFICE", DbType.String, MakeDbNull(model.TaxOffice));
                db.AddInParameter(cmd, "TAX_NO", DbType.String, MakeDbNull(model.TaxNo));
                db.AddInParameter(cmd, "PHONE", DbType.String, MakeDbNull(model.Phone));
                db.AddInParameter(cmd, "FAX", DbType.String, MakeDbNull(model.Fax));
                db.AddInParameter(cmd, "MOBILE_PHONE", DbType.String, MakeDbNull(model.MobilePhone));
                db.AddInParameter(cmd, "CONTACT_PERSON", DbType.String, MakeDbNull(model.ContactPerson));
                db.AddInParameter(cmd, "CHAMBER_OF_COMMERCE", DbType.String, MakeDbNull(model.ChamberOfCommerce));
                db.AddInParameter(cmd, "TRADE_REGISTRY_NO", DbType.String, MakeDbNull(model.TradeRegistryNo));
                db.AddInParameter(cmd, "TX_URL", DbType.String, MakeDbNull(model.Url));
                db.AddInParameter(cmd, "E_MAIL", DbType.String, MakeDbNull(model.Email));
                db.AddInParameter(cmd, "ZIP_CODE", DbType.String, MakeDbNull(model.ZipCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.SupplierId = db.GetParameterValue(cmd, "ID_SUPPLIER").GetValue<int>();
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

        public SupplierViewModel GetSupplier(UserInfo user,int supplierId)
        {
            DbDataReader dReader = null;
            var supplier = new SupplierViewModel { SupplierId = supplierId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SUPPLIER");
                db.AddInParameter(cmd, "ID_SUPPLIER", DbType.Int32, MakeDbNull(supplierId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    supplier.CountryId = dReader["ID_COUNTRY"].GetValue<int>();
                    supplier.CityId = dReader["ID_CITY"].GetValue<int>();
                    supplier.TownId = dReader["ID_TOWN"].GetValue<int>();
                    supplier.DealerId = dReader["ID_DEALER"].GetValue<int>();
                    supplier.Url = dReader["TX_URL"].GetValue<string>();
                    supplier.Address = dReader["ADDRESS"].GetValue<string>();
                    supplier.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    supplier.Ssid = dReader["SUPPLIER_SSID"] != null
                        ? dReader["SUPPLIER_SSID"].ToString()
                        : string.Empty;
                    supplier.SupplierName = dReader["SUPPLIER_NAME"] != null
                        ? dReader["SUPPLIER_NAME"].ToString()
                        : string.Empty;
                    supplier.TaxOffice = dReader["TAX_OFFICE"] != null
                        ? dReader["TAX_OFFICE"].ToString()
                        : string.Empty;
                    supplier.TaxNo = dReader["TAX_NO"] != null
                        ? dReader["TAX_NO"].ToString()
                        : string.Empty;
                    supplier.ContactPerson = dReader["CONTACT_PERSON"] != null
                        ? dReader["CONTACT_PERSON"].ToString()
                        : string.Empty;
                    supplier.Email = dReader["E_MAIL"] != null
                        ? dReader["E_MAIL"].ToString()
                        : string.Empty;
                    supplier.Phone = dReader["PHONE"] != null
                        ? dReader["PHONE"].ToString()
                        : string.Empty;

                    supplier.MobilePhone = dReader["MOBILE_PHONE"].GetValue<string>();

                    supplier.Fax = dReader["FAX"] != null
                        ? dReader["FAX"].ToString()
                        : string.Empty;
                    supplier.ChamberOfCommerce = dReader["CHAMBER_OF_COMMERCE"] != null
                        ? dReader["CHAMBER_OF_COMMERCE"].ToString()
                        : string.Empty;
                    supplier.TradeRegistryNo = dReader["TRADE_REGISTRY_NO"] != null
                        ? dReader["TRADE_REGISTRY_NO"].ToString()
                        : string.Empty;
                    supplier.CountryName = dReader["COUNTRY_NAME"] != null
                        ? dReader["COUNTRY_NAME"].ToString()
                        : string.Empty;
                    supplier.CityName = dReader["CITY_NAME"] != null
                        ? dReader["CITY_NAME"].ToString()
                        : string.Empty;
                    supplier.TownName = dReader["TOWN_NAME"] != null
                        ? dReader["TOWN_NAME"].ToString()
                        : string.Empty;
                    supplier.ZipCode = dReader["ZIP_CODE"] != null
                        ? dReader["ZIP_CODE"].ToString()
                        : string.Empty;
                    supplier.IsActiveString = dReader["IS_ACTIVE_STRING"] != null
                        ? dReader["IS_ACTIVE_STRING"].ToString()
                        : string.Empty;
                    supplier.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();

                    if (supplier.ErrorNo > 0)
                    {
                        supplier.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                        supplier.ErrorMessage = ResolveDatabaseErrorXml(supplier.ErrorMessage);
                    }
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
            return supplier;
        }

        public List<SelectListItem> ListSupplierComboAsSelectListItem(UserInfo user,bool addTaxNo=false)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SUPPLIER_COMBO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ADD_TAX_NO", DbType.Boolean, addTaxNo);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_SUPPLIER"].GetValue<string>(),
                            Text = reader["SUPPLIER_NAME"].GetValue<string>()
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

        public List<SelectListItem> ListSupplierComboAsSelectListItemPO(UserInfo user,bool? acceptOrderProposal)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SUPPLIER_COMBO_PO");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ACCEPT_ORDER_PROPOSAL", DbType.Int32, MakeDbNull(acceptOrderProposal));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_SUPPLIER"].GetValue<string>(),
                            Text = reader["SUPPLIER_NAME"].GetValue<string>()
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
        public List<SelectListItem> ListSupplierComboAsSelectListItemPONotInDealer(UserInfo user,bool? acceptOrderProposal)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SUPPLIER_COMBO_PO_NOT_IN_DEALER");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                db.AddInParameter(cmd, "ACCEPT_ORDER_PROPOSAL", DbType.Int32, MakeDbNull(acceptOrderProposal));
                db.AddInParameter(cmd, "IN_DEALER", DbType.Int32, MakeDbNull(user.GetUserDealerId()));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["ID_SUPPLIER"].GetValue<string>(),
                            Text = reader["SUPPLIER_NAME"].GetValue<string>()
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
    }
}

