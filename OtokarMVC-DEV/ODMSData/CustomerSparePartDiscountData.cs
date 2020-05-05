using ODMSCommon.Security;
using ODMSModel.CustomerDiscount;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using System.Web.Mvc;
using ODMSModel.CustomerSparePartDiscount;

namespace ODMSData
{
    public class CustomerSparePartDiscountData : DataAccessBase
    {
        public CustomerSparePartDiscountViewModel GetById(long id, string languageCode)
        {
            CustomerSparePartDiscountViewModel retVal = new CustomerSparePartDiscountViewModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_CRM_CUSTOMER_SPAREPART_DISCOUNT");
                db.AddInParameter(cmd, "ID_CRM_CUSTOMER_SPAREPART_DISCOUNT", DbType.Int64, MakeDbNull(id));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(languageCode));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new CustomerSparePartDiscountViewModel
                        {
                            CustomerSparePartDiscountId = reader["CustomerSparePartDiscountId"].GetValue<long>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            CustomerId = reader["CustomerId"].GetValue<int>(),
                            PartId = reader["PartId"].GetValue<int>(),
                            DiscountRatio = reader["DiscountRatio"].GetValue<int>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            CustomerName = reader["CustomerName"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            OrgTypeId = reader["OrgTypeId"].GetValue<int?>(),
                            OrgTypeName = reader["OrgTypeName"].GetValue<string>(),
                            SparePartClassCode = reader["SparePartClassCode"].GetValue<string>(),
                            IsApplicableToWorkOrder = reader["IsApplicableToWorkOrder"].GetValue<bool>(),
                            IsApplicableToWorkOrderName = reader["IsApplicableToWorkOrderName"].GetValue<string>()
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

            return retVal;
        }
        public List<CustomerSparePartDiscountListModel> List(UserInfo user, CustomerSparePartDiscountListModel filter,out int totalCnt)
        {
            var retVal = new List<CustomerSparePartDiscountListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CRM_CUSTOMER_SPAREPART_DISCOUNT");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.DealerId);
                db.AddInParameter(cmd, "PART_NAME", DbType.String, filter.PartName);
                db.AddInParameter(cmd, "PART_CODE", DbType.String, filter.PartCode);
                db.AddInParameter(cmd, "PART_CLASS_LIST", DbType.String, filter.PartClassList);
                db.AddInParameter(cmd, "CUSTOMER_ID_LIST", DbType.String, filter.CustomerIdList);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                //db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, filter.SortColumn);
                //db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, filter.SortDirection);
                //db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                //db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, filter.PageSize);
                //db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                //db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 400);
                AddPagingParameters(cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var customerDiscountListModel = new CustomerSparePartDiscountListModel
                        {
                            CustomerSparePartDiscountId = reader["CustomerSparePartDiscountId"].GetValue<Int64>(),
                            DealerId = reader["DealerId"].GetValue<int>(),
                            CustomerId = reader["CustomerId"].GetValue<int>(),
                            PartId = reader["PartId"].GetValue<int>(),
                            DiscountRatio = reader["DiscountRatio"].GetValue<decimal>(),
                            DealerName = reader["DealerName"].GetValue<string>(),
                            CustomerName = reader["CustomerName"].GetValue<string>(),
                            PartName = reader["PartName"].GetValue<string>(),
                            PartCode = reader["PartCode"].GetValue<string>(),
                            OrgTypeName = reader["OrgTypeName"].GetValue<string>(),
                            SparePartClassCode = reader["SparePartClassCode"].GetValue<string>(),
                            IsApplicableToWorkOrderName = reader["IsApplicableToWorkOrderName"].GetValue<string>()
                        };

                        retVal.Add(customerDiscountListModel);
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

            return retVal;
        }

        public List<SelectListItem> ListDealers()
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CRM_CUSTOMER_SPAREPART_DISCOUNT_DEALERS_COMBO");

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["[Value]"].GetValue<string>(),
                            Text = reader["[Text]"].GetValue<string>()
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

        public List<SelectListItem> ListDealersForCreate(UserInfo user)
        {
            List<SelectListItem> retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_CRM_CUSTOMER_SPAREPART_DISCOUNT_DEALERS_COMBO_FOR_CREATE");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, user.GetUserDealerId());

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                        {
                            Value = reader["[Value]"].GetValue<string>(),
                            Text = reader["[Text]"].GetValue<string>()
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

        public void DMLCustomerSparePartDiscount(UserInfo user, CustomerSparePartDiscountViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_CRM_CUSTOMER_SPAREPART_DISCOUNT");
                db.AddInParameter(cmd, "ID_CRM_CUSTOMER_SPAREPART_DISCOUNT", DbType.Int64, model.CustomerSparePartDiscountId);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, user.GetUserDealerId());
                db.AddInParameter(cmd, "ID_CUSTOMER", DbType.Int64, model.CustomerId);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, model.PartId);
                db.AddInParameter(cmd, "SPARE_PART_CLASS_CODE", DbType.String, model.SparePartClassCode);
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Decimal, model.DiscountRatio);
                db.AddInParameter(cmd, "ORGTYPE_LOOKVAL", DbType.String, model.OrgTypeId);
                db.AddInParameter(cmd, "IS_APPLICABLE_TO_WORKORDER", DbType.Int16, model.IsApplicableToWorkOrder);
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
    }
}
