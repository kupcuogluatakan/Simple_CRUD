using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSData.DataContracts;
using ODMSModel.PurchaseOrderGroupRelation;
using System;

namespace ODMSData
{
    public class PurchaseOrderGroupRelationData : DataAccessBase, IPurchaseOrderGroupRelationList<PurchaseOrderGroupRelationListModel>
    {
        public void Update(PurchaseGroupRelationSaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_DML_PURCHASE_ORDER_GROUP_DEALERS");
                db.AddInParameter(cmd, "ID_PO_GROUP", DbType.Int32, model.PurchaseOrderGroupId);
                db.AddInParameter(cmd, "DEALER_IDS_OF_INCLUEDED", DbType.String, model.SerializedDealerIdsOfIncluded);
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

        public List<PurchaseOrderGroupRelationListModel> ListOfInclueded(PurchaseOrderGroupRelationListModel model)
        {
            var retVal = new List<PurchaseOrderGroupRelationListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_GROUP_INCLUEDED_DEALERS");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, model.PurchaseOrderGroupId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var includedList = new PurchaseOrderGroupRelationListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            GroupName = reader["GROUP_NAME"].GetValue<string>()
                        };

                        retVal.Add(includedList);
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

        public List<PurchaseOrderGroupRelationListModel> ListOfNotInclueded(PurchaseOrderGroupRelationListModel model)
        {
            var retVal = new List<PurchaseOrderGroupRelationListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PURCHASE_ORDER_GROUP_NOT_INCLUEDED_DEALERS");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, model.PurchaseOrderGroupId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var includedList = new PurchaseOrderGroupRelationListModel
                        {
                            DealerId = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            GroupName = reader["GROUP_NAME"].GetValue<string>()
                        };

                        retVal.Add(includedList);
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
