using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.PurchaseOrderType;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class PurchaseOrderTypeData : DataAccessBase
    {
        public void Delete(UserInfo user,PurchaseOrderTypeViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_TYPE");
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurchaseOrderTypeId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, MakeDbNull(model.ProposalType));
                db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int32, MakeDbNull(model.DeliveryPriority));
                db.AddInParameter(cmd, "ORD_REASON", DbType.String, MakeDbNull(model.OrderReason));
                db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, MakeDbNull(model.ItemCategory));
                db.AddInParameter(cmd, "IS_VEHICLE_SELECTION_MUST", DbType.Boolean, MakeDbNull(model.IsVehicleSelectionMust));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
                db.AddInParameter(cmd, "IS_CURRENCY_SELECT_ALLOW", DbType.Boolean, model.IsCurrencySelectAllow);
                db.AddInParameter(cmd, "IS_MANUEL_PRICE_ALLOW", DbType.Boolean, model.ManuelPriceAllow);
                db.AddInParameter(cmd, "IS_SUPPLIER_ORDER_ALLOW", DbType.Boolean, model.IsSupplierOrderAllow);
                db.AddInParameter(cmd, "IS_FIRM_ORDER_ALLOW", DbType.Boolean, model.IsFirmOrderAllow);
                db.AddInParameter(cmd, "SALE_ORDER_ALLOW", DbType.Boolean, model.IsSaleOrderAllow);
                db.AddInParameter(cmd, "IS_DEALER_ORDER_ALLOW", DbType.Boolean, model.IsDealerOrderAllow);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Boolean, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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
        }

        public PurchaseOrderTypeViewModel Get(UserInfo user,PurchaseOrderTypeViewModel model)
        {
            DbDataReader reader = null;

            int? dealerId = null;
            string lang = user.LanguageCode;
            try
            {
                if (user != null)
                    dealerId = user.GetUserDealerId();
                else
                {
                    lang = "TR";
                }
            }
            catch
            {
                lang = "TR";
                dealerId = null;
            }

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_TYPE");
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurchaseOrderTypeId));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(lang));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    model.PurchaseOrderTypeId = reader["ID_PO_TYPE"].GetValue<int>();
                    model.Description = reader["ADMIN_DESC"].GetValue<string>();
                    model.ProposalType = reader["PROPOSAL_TYPE"].GetValue<string>();
                    model.DeliveryPriority = reader["DELIVERY_PRIORITY"].GetValue<int>();
                    model.SalesOrganization = reader["SALES_ORGANIZATION"].GetValue<string>();
                    model.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                    model.PurchaseOrderTypeName = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>();
                    model.DistrChan = reader["DISTR_CHAN"].GetValue<string>();
                    model.Division = reader["DIVISION"].GetValue<string>();
                    model.OrderReason = reader["ORD_REASON"].GetValue<string>();
                    model.ItemCategory = reader["ITEM_CATEG"].GetValue<string>();
                    model.IsVehicleSelectionMust = reader["IS_VEHICLE_SELECTION_MUST"].GetValue<bool>();
                    model.ManuelPriceAllow = reader["MANUEL_PRICE_ALLOW"].GetValue<bool>();
                    model.DealerBranchSSID = reader["DEALER_BRANCH_SSID"].GetValue<string>();
                    model.IsCurrencySelectAllow = reader["IS_CURRENCY_SELECT_ALLOW"].GetValue<bool>();
                    model.IsFirmOrderAllow = reader["IS_FIRM_ORDER_ALLOW"].GetValue<bool>();
                    model.IsDealerOrderAllow = reader["IS_DEALER_ORDER_ALLOW"].GetValue<bool>();
                    model.IsSupplierOrderAllow = reader["IS_SUPPLIER_ORDER_ALLOW"].GetValue<bool>();
                    model.StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int>();
                    model.StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>();
                }

                if (reader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(reader);
                    model.MultiLanguageContentAsText = GetLanguageContentFromDataSet(table, "PURCHASE_ORDER_TYPE_NAME");
                    model.MultiLanguageName = (MultiLanguageModel)CommonUtility.DeepClone(model.MultiLanguageName);
                    model.MultiLanguageName.MultiLanguageContentAsText = model.MultiLanguageContentAsText;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return model;
        }

        public void Insert(UserInfo user,PurchaseOrderTypeViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_TYPE");
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurchaseOrderTypeId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, MakeDbNull(model.ProposalType));
                db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int32, MakeDbNull(model.DeliveryPriority));
                db.AddInParameter(cmd, "ORD_REASON", DbType.String, MakeDbNull(model.OrderReason));
                db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, MakeDbNull(model.ItemCategory));
                db.AddInParameter(cmd, "IS_CURRENCY_SELECT_ALLOW", DbType.Boolean, model.IsCurrencySelectAllow);
                db.AddInParameter(cmd, "IS_VEHICLE_SELECTION_MUST", DbType.Boolean, model.IsVehicleSelectionMust);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "IS_MANUEL_PRICE_ALLOW", DbType.Boolean, model.ManuelPriceAllow);
                db.AddInParameter(cmd, "IS_FIRM_ORDER_ALLOW", DbType.Boolean, model.IsFirmOrderAllow);
                db.AddInParameter(cmd, "IS_DEALER_ORDER_ALLOW", DbType.Boolean, model.IsDealerOrderAllow);
                db.AddInParameter(cmd, "IS_SUPPLIER_ORDER_ALLOW", DbType.Boolean, model.IsSupplierOrderAllow);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "SALE_ORDER_ALLOW", DbType.Boolean, model.IsSaleOrderAllow);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
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
        }

        public List<PurchaseOrderTypeListModel> List(UserInfo user,PurchaseOrderTypeListModel filter, out int totalCnt)
        {
            var result = new List<PurchaseOrderTypeListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_TYPE");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, filter.StockTypeId);
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
                        var listModel = new PurchaseOrderTypeListModel
                        {
                            PurchaseOrderTypeId = reader["ID_PO_TYPE"].GetValue<int>(),
                            Description = reader["ADMIN_DESC"].GetValue<string>(),
                            DeliveryPriority = reader["DELIVERY_PRIORITY"].GetValue<int>(),
                            ProposalType = reader["PROPOSAL_TYPE"].GetValue<string>(),
                            StateName = reader["IS_ACTIVE"].GetValue<string>(),
                            PurchaseOrderTypeName = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>(),
                            IsCurrencySelectAllow = reader["IS_CURRENCY_SELECT_ALLOW"].GetValue<bool>(),
                            IsDealerOrderAllow = reader["IS_DEALER_ORDER_ALLOW"].GetValue<bool>(),
                            IsFirmOrderAllow = reader["IS_FIRM_ORDER_ALLOW"].GetValue<bool>(),
                            IsSupplierOrderAllow = reader["IS_SUPPLIER_ORDER_ALLOW"].GetValue<bool>(),
                            StockTypeId = reader["STOCK_TYPE_ID"].GetValue<int?>(),
                            StockTypeName = reader["STOCK_TYPE_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
                    }
                    reader.Close();
                }
                totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void Update(UserInfo user,PurchaseOrderTypeViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_TYPE");
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurchaseOrderTypeId));
                db.AddInParameter(cmd, "ML_CONTENT", DbType.String, model.MultiLanguageContentAsText);
                db.AddInParameter(cmd, "DESCRIPTION", DbType.String, MakeDbNull(model.Description));
                db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, MakeDbNull(model.ProposalType));
                db.AddInParameter(cmd, "DELIVERY_PRIORITY", DbType.Int32, MakeDbNull(model.DeliveryPriority));
                db.AddInParameter(cmd, "ORD_REASON", DbType.String, MakeDbNull(model.OrderReason));
                db.AddInParameter(cmd, "ITEM_CATEG", DbType.String, MakeDbNull(model.ItemCategory));
                db.AddInParameter(cmd, "IS_VEHICLE_SELECTION_MUST", DbType.Boolean, model.IsVehicleSelectionMust);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "IS_CURRENCY_SELECT_ALLOW", DbType.Boolean, model.IsCurrencySelectAllow);
                db.AddInParameter(cmd, "IS_MANUEL_PRICE_ALLOW", DbType.Boolean, model.ManuelPriceAllow);
                db.AddInParameter(cmd, "IS_DEALER_ORDER_ALLOW", DbType.Boolean, model.IsDealerOrderAllow);
                db.AddInParameter(cmd, "IS_FIRM_ORDER_ALLOW", DbType.Boolean, model.IsFirmOrderAllow);
                db.AddInParameter(cmd, "IS_SUPPLIER_ORDER_ALLOW", DbType.Boolean, model.IsSupplierOrderAllow);
                db.AddInParameter(cmd, "STOCK_TYPE_ID", DbType.Int32, MakeDbNull(model.StockTypeId));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "SALE_ORDER_ALLOW", DbType.Boolean, model.IsSaleOrderAllow);
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
        }
        [Obsolete("Use overload")]
        public List<SelectListItem> ListPurchaseOrderTypeAsSelectListItem(UserInfo user,int? dealerId)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_TYPE_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Selected = false,
                            Value = reader["ID_PO_TYPE"].GetValue<string>(),
                            Text = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
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

            return result;
        }
        public List<SelectListItem> ListPurchaseOrderTypeAsSelectListItem(UserInfo user,int? dealerId, bool? dealerOrderAllow, bool? supplierOrderAllow, bool? firmOrderAllow, bool? saleOrderAllow)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_TYPE_COMBO");
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(dealerId));
                db.AddInParameter(cmd, "DEALER_ORDER_ALLOW", DbType.Int32, MakeDbNull(dealerOrderAllow));
                db.AddInParameter(cmd, "SUPPLIER_ORDER_ALLOW", DbType.Int32, MakeDbNull(supplierOrderAllow));
                db.AddInParameter(cmd, "FIRM_ORDER_ALLOW", DbType.Int32, MakeDbNull(firmOrderAllow));
                db.AddInParameter(cmd, "SALE_ORDER_ALLOW", DbType.Int32, MakeDbNull(saleOrderAllow));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Selected = false,
                            Value = reader["ID_PO_TYPE"].GetValue<string>(),
                            Text = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
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

            return result;
        }


        public List<SelectListItem> PurchaseOrderTypeList(UserInfo user)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_TYPE_COMBO_");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Selected = false,
                            Value = reader["ID_PO_TYPE"].GetValue<string>(),
                            Text = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>()
                        };
                        result.Add(listModel);
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

            return result;
        }

        public PurchaseOrderTypeViewModel GetPurchaseOrderTypeDetails(string purchaseOrderGroupId, string salesOrganization,
            string distrChan, string division, string proposalType, string ordReason)
        {
            PurchaseOrderTypeViewModel model = new PurchaseOrderTypeViewModel();

            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_TYPE_DETAILS");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(purchaseOrderGroupId));
                db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, MakeDbNull(salesOrganization));
                db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, MakeDbNull(distrChan));
                db.AddInParameter(cmd, "DIVISION", DbType.String, MakeDbNull(division));
                db.AddInParameter(cmd, "PROPOSAL_TYPE", DbType.String, MakeDbNull(proposalType));
                db.AddInParameter(cmd, "ORD_REASON", DbType.String, MakeDbNull(ordReason));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    model.PurchaseOrderTypeId = reader["ID_PO_TYPE"].GetValue<int>();
                    model.StockTypeId = reader["ID_STOCK_TYPE"].GetValue<int>();
                    model.DeliveryPriority = reader["DELIVERY_PRIORITY"].GetValue<int>();
                    model.ItemCategory = reader["ITEM_CATEG"].GetValue<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null && !reader.IsClosed)
                {
                    reader.Close();
                    reader.Dispose();
                }
                CloseConnection();
            }
            return model;
        }
    }
}

