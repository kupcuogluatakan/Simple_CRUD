using ODMSCommon;
using ODMSCommon.Security;
using ODMSModel.Defect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ODMSData
{
    public class DefectData : DataAccessBase
    {
        public DefectData()
        {
        }

        public List<DefectListModel> ListDefect(UserInfo user, DefectListModel filter, out int totalCount)
        {
            var retVal = new List<DefectListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEFECT");
                db.AddInParameter(cmd, "ID_DEFECT", DbType.Int32, MakeDbNull(filter.IdDefect));
                db.AddInParameter(cmd, "DEFECT_NO", DbType.String, MakeDbNull(filter.DefectNo));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));
                db.AddInParameter(cmd, "VIN_NO", DbType.String, MakeDbNull(filter.VehicleVinNo));
                db.AddInParameter(cmd, "DECLARATION_DATE", DbType.DateTime, MakeDbNull(filter.DeclarationDate));
                db.AddInParameter(cmd, "DEALER_DECLARATION_DATE", DbType.DateTime, MakeDbNull(filter.DealerDeclarationDate));
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, MakeDbNull(filter.IdContract));
                db.AddInParameter(cmd, "DOCUMENT_NAME", DbType.String, MakeDbNull(filter.DocName));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, filter.IsActive);
                //AddPagingParameters(cmd, filter);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var defectListModel = new DefectListModel
                        {
                            IdDefect = reader["ID_DEFECT"].GetValue<int>(),
                            DefectNo = reader["DEFECT_NO"].GetValue<string>(),
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            IdVehicle = reader["ID_VEHICLE"].GetValue<int>(),
                            DeclarationDate = reader["DECLARATION_DATE"].GetValue<DateTime>(),
                            DealerDeclarationDate = reader["DEALER_DECLARATION_DATE"].GetValue<DateTime>(),
                            IdContract = reader["ID_CONTRACT"].GetValue<int>(),
                            DocId = reader["DOC_ID"].GetValue<int>(),
                            DocName = reader["DOC_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            ContractName = reader["CONTRACT_NAME"].GetValue<string>(),
                            VehicleVinNo = reader["VIN_NO"].GetValue<string>()
                        };

                        retVal.Add(defectListModel);
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

        public void DMLDefect(UserInfo user, DefectViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEFECT");
                db.AddInParameter(cmd, "ID_DEFECT", DbType.Int32, model.IdDefect);
                db.AddInParameter(cmd, "DEFECT_NO", DbType.String, model.DefectNo);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, model.VehicleId);
                db.AddInParameter(cmd, "DECLARATION_DATE", DbType.DateTime, MakeDbNull(model.DeclarationDate));
                db.AddInParameter(cmd, "DEALER_DECLARATION_DATE", DbType.DateTime, MakeDbNull(model.DealerDeclarationDate));
                db.AddInParameter(cmd, "ID_CONTRACT", DbType.Int32, model.IdContract);
                db.AddInParameter(cmd, "ID_DOCUMENT", DbType.Int32, MakeDbNull(model.DocId));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Int32, MakeDbNull(model.IsActive));              
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                //db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                //model.IdDefect =Convert.ToInt32(db.GetParameterValue(cmd, "ID_DEFECT").ToString());
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

        public DefectViewModel GetDefect(UserInfo user, DefectViewModel filter)
        {
            DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEFECT");
                db.AddInParameter(cmd, "ID_DEFECT", DbType.String, MakeDbNull(filter.IdDefect));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdDefect = dReader["ID_DEFECT"].GetValue<int>();
                    filter.DocId = dReader["DOC_ID"].GetValue<int>();
                    filter.DocName = dReader["DOC_NAME"].GetValue<string>();
                    filter.DefectNo = dReader["DEFECT_NO"].GetValue<string>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.VehicleId = dReader["ID_VEHICLE"].GetValue<int>();
                    filter.DeclarationDate = dReader["DECLARATION_DATE"].GetValue<DateTime>();
                    filter.DealerDeclarationDate = dReader["DEALER_DECLARATION_DATE"].GetValue<DateTime>();
                    filter.IdContract = dReader["ID_CONTRACT"].GetValue<int>();
                    filter.DealerName = dReader["DEALER_NAME"].GetValue<string>();
                    filter.VehicleVinNo = dReader["VIN_NO"].GetValue<string>();
                    filter.ContractName = dReader["CONTRACT_NAME"].GetValue<string>();
                }

                dReader.Close();
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
            return filter;
        }

        public List<SelectListItem> GetDealerList()
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DEALER"].GetValue<string>(),
                            Text = reader["DEALER_NAME"].GetValue<string>()
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

        public List<SelectListItem> GetDefectComboList(int? idVehicle)
        {
            var result = new List<SelectListItem>();
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEFECT_COMBO");
                db.AddInParameter(cmd, "ID_VEHICLE", DbType.Int32, MakeDbNull(idVehicle));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var listModel = new SelectListItem
                        {
                            Value = reader["ID_DEFECT"].GetValue<string>(),
                            Text = reader["DEFECT_NO"].GetValue<string>()
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
    }
}
