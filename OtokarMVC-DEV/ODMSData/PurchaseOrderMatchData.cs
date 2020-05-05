using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.PurchaseOrderMatch;
using System;

namespace ODMSData
{
    public class PurchaseOrderMatchData : DataAccessBase
    {
        public void Delete(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_MATCH");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(model.PurhcaseOrderGroupId));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurhcaseOrderTypeId));
                db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, MakeDbNull(model.SalesOrganization));
                db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, MakeDbNull(model.DistrChan));
                db.AddInParameter(cmd, "DIVISION", DbType.String, MakeDbNull(model.Division));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.String, MakeDbNull(model.IsActive));
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

        public PurchaseOrderMatchViewModel Get(UserInfo user,PurchaseOrderMatchViewModel filter)
        {
            DbDataReader reader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PURCHASE_ORDER_MATCH");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(filter.PurhcaseOrderGroupId));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(filter.PurhcaseOrderTypeId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    filter.PurhcaseOrderGroupId = reader["ID_PO_GROUP"].GetValue<int>();
                    filter.PurhcaseOrderTypeId = reader["ID_PO_TYPE"].GetValue<int>();
                    filter.PurhcaseOrderGroupName = reader["GROUP_NAME"].GetValue<string>();
                    filter.PurhcaseOrderTypeName = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>();
                    filter.SalesOrganization = reader["SALES_ORGANIZATION"].GetValue<string>();
                    filter.DistrChan = reader["DISTR_CHAN"].GetValue<string>();
                    filter.Division = reader["DIVISION"].GetValue<string>();
                    filter.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
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
            return filter;
        }

        public void Insert(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_MATCH");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(model.PurhcaseOrderGroupId));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurhcaseOrderTypeId));
                db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, MakeDbNull(model.SalesOrganization));
                db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, MakeDbNull(model.DistrChan));
                db.AddInParameter(cmd, "DIVISION", DbType.String, MakeDbNull(model.Division));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.String, MakeDbNull(model.IsActive));
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

        public List<PurchaseOrderMatchListModel> List(UserInfo user,PurchaseOrderMatchListModel model, out int totalCnt)
        {
            var result = new List<PurchaseOrderMatchListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_MATCH");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(model.PurhcaseOrderGroupId));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurhcaseOrderTypeId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
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
                        var listModel = new PurchaseOrderMatchListModel
                        {
                            PurhcaseOrderGroupId = reader["ID_PO_GROUP"].GetValue<int>(),
                            PurhcaseOrderTypeId = reader["ID_PO_TYPE"].GetValue<int>(),
                            PurhcaseOrderGroupName = reader["GROUP_NAME"].GetValue<string>(),
                            PurhcaseOrderTypeName = reader["PURCHASE_ORDER_TYPE_NAME"].GetValue<string>(),
                            SalesOrganization = reader["SALES_ORGANIZATION"].GetValue<string>(),
                            DistrChan = reader["DISTR_CHAN"].GetValue<string>(),
                            Division = reader["DIVISION"].GetValue<string>(),
                            StateName = reader["IS_ACTIVE"].GetValue<string>(),
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

        public void Update(UserInfo user,PurchaseOrderMatchViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_MATCH");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, MakeDbNull(model.PurhcaseOrderGroupId));
                db.AddInParameter(cmd, "ID_PO_TYPE", DbType.Int32, MakeDbNull(model.PurhcaseOrderTypeId));
                db.AddInParameter(cmd, "SALES_ORGANIZATION", DbType.String, MakeDbNull(model.SalesOrganization));
                db.AddInParameter(cmd, "DISTR_CHAN", DbType.String, MakeDbNull(model.DistrChan));
                db.AddInParameter(cmd, "DIVISION", DbType.String, MakeDbNull(model.Division));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.String, MakeDbNull(model.IsActive));
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
