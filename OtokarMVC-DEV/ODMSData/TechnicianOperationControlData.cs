using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ODMSCommon;
using ODMSData.DataContracts;
using ODMSModel.TechnicianOperationControl;
using ODMSCommon.Security;

namespace ODMSData
{
    public class TechnicianOperationControlData : DataAccessBase, ITechnicianOperationControl<TechnicianOperationViewModel>
    {
        public TechnicianOperationViewModel Get(UserInfo user, TechnicianOperationViewModel model)
        {
            var result = new TechnicianOperationViewModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_TECHNICIAN_OPERATION_CONTROL");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, model.UserId);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        result.CheckInDate = reader["CHECKIN_DATE"].GetValue<DateTime?>();
                        result.CheckOutDate = reader["CHECKOUT_DATE"].GetValue<DateTime?>();

                        if (result.CheckInDate.HasValue && result.CheckOutDate.HasValue)
                            result.ProcessType = ProcessType.CheckIn;
                        else if (result.CheckInDate.HasValue)
                            result.ProcessType = ProcessType.CheckOut;
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

        public IEnumerable<TechnicianOperationListModel> List(UserInfo user, TechnicianOperationListModel model, out int totalCnt)
        {
            var result = new List<TechnicianOperationListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_TECHNICIAN_OPERATION_CONTROL");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, model.UserId);
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
                        var listModel = new TechnicianOperationListModel
                            {
                                CheckInDate = reader["CHECKIN_DATE"].GetValue<DateTime>(),
                                CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                                CheckOutDate = reader["CHECKOUT_DATE"].GetValue<DateTime?>().HasValue
                                                   ? reader["CHECKOUT_DATE"].GetValue<DateTime?>()
                                                   : null
                            };

                        result.Add(listModel);
                    }
                    
                    
                    totalCnt = db.GetParameterValue(cmd, "TOTAL_COUNT").GetValue<int>();
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

        public void Insert(UserInfo user, TechnicianOperationViewModel model)
        {
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_TECHNICIAN_OPERATION_CONTROL");
                db.AddInParameter(cmd, "USER_ID", DbType.Int32, MakeDbNull(model.UserId));
                db.AddInParameter(cmd, "DEALER_ID", DbType.Int32, MakeDbNull(model.DealerId));
                db.AddInParameter(cmd, "CHECKIN_DATE", DbType.DateTime, MakeDbNull(model.CheckInDate));
                db.AddInParameter(cmd, "CHECKOUT_DATE", DbType.DateTime, MakeDbNull(model.CheckOutDate));
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                {
                    model.ErrorMessage = model.ErrorMessage;
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
        }
    }
}
