﻿using ODMSModel.PDIGOSApproveGroupDealers;
using System.Collections.Generic;
using ODMSCommon;
using System.Data;
using System;
using ODMSCommon.Security;

namespace ODMSData
{
    public class PDIGOSApproveGroupDealersData : DataAccessBase
    {
        //Left - All dealers without selected dealers.
        public List<PDIGOSApproveGroupDealersListModel> ListPDIGOSApproveGroupDealersNotInclude(PDIGOSApproveGroupDealersListModel filter)
        {
            var retVal = new List<PDIGOSApproveGroupDealersListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP_DEALERS_NOT_INCLUDE");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, filter.IdGroup);

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guaranteeAuthorityGroupDealersListModel = new PDIGOSApproveGroupDealersListModel
                        {
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>()
                        };

                        retVal.Add(guaranteeAuthorityGroupDealersListModel);
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


        public List<PDIGOSApproveGroupDealersListModel> ListPDIGOSApproveGroupDealers(PDIGOSApproveGroupDealersListModel filter)
        {
            var retVal = new List<PDIGOSApproveGroupDealersListModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_PDI_GOS_APPROVE_GROUP_DEALERS");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, filter.IdGroup);

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var guaranteeAuthorityGroupDealersListModel = new PDIGOSApproveGroupDealersListModel
                        {
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>()
                        };

                        retVal.Add(guaranteeAuthorityGroupDealersListModel);
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

        public void SavePDIGOSApproveGroupDealers(UserInfo user, PDIGOSApproveGroupDealersSaveModel model)
        {
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_SAVE_PDI_GOS_APPROVE_GROUP_DEALERS");
                db.AddInParameter(cmd, "ID_GROUP", DbType.Int32, model.id);
                db.AddInParameter(cmd, "DEALER_IDS", DbType.String, model.SerializedDealerIds);
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