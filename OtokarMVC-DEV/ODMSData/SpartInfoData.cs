using ODMSCommon;
using ODMSCommon.Resources;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SpartInfo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class SpartInfoData : DataAccessBase
    {
        public List<SpartInfoXMLViewModel> GetSpartInfoListForXML()
        {
            var listModel = new List<SpartInfoXMLViewModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SPART_INFO_XML");

                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new SpartInfoXMLViewModel
                        {
                            Spart = dr["SPARE_PART_CLASS_CODE"].GetValue<string>(),
                            Text = dr["ADMIN_DESC"].GetValue<string>()
                        };
                        listModel.Add(model);
                    }
                    dr.Close();
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
            return listModel;
        }
        public ServiceCallLogModel XMLtoDBSparePart(List<SpartInfoXMLViewModel> listModel)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPART_INFO_XML");

                CreateConnection(cmd);
                foreach (var model in listModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "SPART", DbType.String, MakeDbNull(model.Spart));
                    db.AddInParameter(cmd, "TEXT", DbType.String, MakeDbNull(model.Text));
                    db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                    if (model.ErrorNo > 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.Spart + " - " + MessageResource.SparePart_Display_PartCode,
                            Error = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>())
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

        public ServiceCallLogModel XMLtoDBVehicleCodeLang(List<SpartInfoXMLViewModel> listLangModel)
        {
            var rModel = new ServiceCallLogModel() { IsSuccess = true };
            var listError = new List<ServiceCallScheduleErrorListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_DML_SPART_INFO_LANG_XML");

                CreateConnection(cmd);
                foreach (var model in listLangModel)
                {
                    cmd.Parameters.Clear();
                    db.AddInParameter(cmd, "SPART", DbType.String, MakeDbNull(model.Spart));
                    db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(model.Language));
                    db.AddInParameter(cmd, "TEXT", DbType.String, MakeDbNull(model.Text));
                    db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                    db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                    cmd.ExecuteNonQuery();

                    if (model.ErrorNo > 0)
                    {
                        listError.Add(new ServiceCallScheduleErrorListModel()
                        {
                            Action = model.Spart + " - " + MessageResource.SparePart_Display_PartCode,
                            Error = ResolveDatabaseErrorXml(db.GetParameterValue(cmd, "ERROR_DESC").GetValue<string>())
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
    }
}
