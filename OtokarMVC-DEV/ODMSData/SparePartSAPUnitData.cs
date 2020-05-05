using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Web.Mvc;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.SparePartSAPUnit;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class SparePartSAPUnitData : DataAccessBase
    {
        public void Delete(UserInfo user, SparePartSAPUnitViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SAP_UNIT");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "UNIT", DbType.String, MakeDbNull(model.UnitId));
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.String, MakeDbNull(model.ShipQuantity));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
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

        public SparePartSAPUnitViewModel Get(UserInfo user, SparePartSAPUnitViewModel model)
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
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_SAP_UNIT");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(lang));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    model.PartId = reader["ID_PART"].GetValue<int>();
                    model.SparePartCode = reader["PART_CODE"].GetValue<string>();
                    model.SparePartName = reader["PART_NAME"].GetValue<string>();
                    model.UnitId = reader["UNIT"].GetValue<int>();
                    model.UnitName = reader["UNIT_NAME"].GetValue<string>();
                    model.IsActive = reader["IS_ACTIVE"].GetValue<bool>();
                    model.StateName = reader["IS_ACTIVE_NAME"].GetValue<string>();
                    model.ShipQuantity = reader["SHIP_QUANT"].GetValue<decimal>();
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

        public void Insert(UserInfo user, SparePartSAPUnitViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SAP_UNIT");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "UNIT", DbType.String, model.UnitId);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.String, MakeDbNull(model.ShipQuantity));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
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

        public List<SparePartSAPUnitListModel> List(UserInfo user, SparePartSAPUnitListModel filter, out int totalCnt)
        {
            var result = new List<SparePartSAPUnitListModel>();
            totalCnt = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SAP_UNIT");
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
                db.AddInParameter(cmd, "PART_NAME", DbType.String, MakeDbNull(filter.SparePartCode));
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
                        var listModel = new SparePartSAPUnitListModel
                        {
                            PartId = reader["ID_PART"].GetValue<int>(),
                            SparePartCode = reader["PART_CODE"].GetValue<string>(),
                            SparePartName = reader["PART_NAME"].GetValue<string>(),
                            UnitId = reader["UNIT"].GetValue<int>(),
                            UnitName = reader["UNIT_NAME"].GetValue<string>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<int>(),
                            StateName = reader["IS_ACTIVE_NAME"].GetValue<string>(),
                            ShipQuantity = reader["SHIP_QUANT"].GetValue<decimal>()
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

        public void Update(UserInfo user, SparePartSAPUnitViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_SAP_UNIT");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(model.PartId));
                db.AddInParameter(cmd, "UNIT", DbType.String, model.UnitId);
                db.AddInParameter(cmd, "SHIP_QUANT", DbType.String, MakeDbNull(model.ShipQuantity));
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, MakeDbNull(model.IsActive));
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

