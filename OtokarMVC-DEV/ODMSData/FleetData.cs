using ODMSCommon.Security;
using ODMSModel.Fleet;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Resources;

namespace ODMSData
{
    public class FleetData : DataAccessBase
    {
        public List<FleetListModel> ListFleet(UserInfo user, FleetListModel filter, out int totalCount)
        {
            var retVal = new List<FleetListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_FLEET");
                db.AddInParameter(cmd, "FLEET_NAME", DbType.String, MakeDbNull(filter.FleetName));
                db.AddInParameter(cmd, "FLEET_CODE", DbType.String, MakeDbNull(filter.FleetCode));
                db.AddInParameter(cmd, "IS_PART_CONSTRICTED", DbType.Int32, filter.IsConstricted);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
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
                        var fleetListModel = new FleetListModel
                        {
                            IdFleet = reader["FLEET_ID"].GetValue<int>(),
                            FleetName = reader["FLEET_NAME"].GetValue<string>(),
                            FleetCode = reader["FLEET_CODE"].GetValue<string>(),
                            OtokarPartDiscount = reader["OTOKAR_PART_DISCOUNT_RATE"].GetValue<decimal>(),
                            OtokarLabourDiscount = reader["OTOKAR_LABOUR_DISCOUNT_RATE"].GetValue<decimal>(),
                            DealerPartDiscount = reader["DEALER_PART_DISCOUNT_RATE"].GetValue<decimal>(),
                            DealerLabourDiscount = reader["DEALER_LABOUR_DISCOUNT_RATE"].GetValue<decimal>(),
                            IsConstrictedName = reader["IS_CONSTRICTED_NAME"].GetValue<string>(),
                            StartDateValid = reader["VALIDITY_START_DATE"].GetValue<DateTime>(),
                            EndDateValid = reader["VALIDITY_END_DATE"].GetValue<DateTime>(),
                            IsVinControl = reader["VIN_CONTROL"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No,
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>()
                        };

                        retVal.Add(fleetListModel);
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

        public FleetViewModel GetFleet(UserInfo user, FleetViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_FLEET");
                db.AddInParameter(cmd, "ID_FLEET", DbType.Int32, MakeDbNull(filter.IdFleet));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdFleet = dReader["FLEET_ID"].GetValue<int>();
                    filter.FleetName = dReader["FLEET_NAME"].GetValue<string>();
                    filter.FleetCode = dReader["FLEET_CODE"].GetValue<string>();
                    filter.IsConstricted = dReader["IS_PART_CONSTRICTED"].GetValue<int>();
                    filter.OtokarPartDiscount = dReader["OTOKAR_PART_DISCOUNT_RATE"].GetValue<decimal>();
                    filter.OtokarLabourDiscount = dReader["OTOKAR_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                    filter.DealerPartDiscount = dReader["DEALER_PART_DISCOUNT_RATE"].GetValue<decimal>();
                    filter.DealerLabourDiscount = dReader["DEALER_LABOUR_DISCOUNT_RATE"].GetValue<decimal>();
                    filter.StartDateValid = dReader["VALIDITY_START_DATE"].GetValue<DateTime>();
                    filter.EndDateValid = dReader["VALIDITY_END_DATE"].GetValue<DateTime>();
                    filter.IsConstrictedName = dReader["IS_CONSTRICTED_NAME"].GetValue<string>();
                    filter.IsVinControl = dReader["VIN_CONTROL"].GetValue<bool>();
                    filter.HasContent = dReader["HAS_CONTENT"].GetValue<int>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
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


            return filter;
        }

        //TODO : Id set edilmeli
        public void DMLFleet(UserInfo user, FleetViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_FLEET");
                db.AddInParameter(cmd, "ID_FLEET", DbType.Int32, model.IdFleet);
                db.AddInParameter(cmd, "FLEET_NAME", DbType.String, model.FleetName);
                db.AddInParameter(cmd, "FLEET_CODE", DbType.String, model.FleetCode);
                db.AddInParameter(cmd, "VIN_CONTROL", DbType.Boolean, model.IsVinControl);
                db.AddInParameter(cmd, "IS_PART_CONSTRICTED", DbType.Int32, model.IsConstricted);
                db.AddInParameter(cmd, "OTOKAR_PART_DISCOUNT_RATE", DbType.Decimal, model.OtokarPartDiscount);
                db.AddInParameter(cmd, "OTOKAR_LABOUR_DISCOUNT_RATE", DbType.Decimal, model.OtokarLabourDiscount);
                db.AddInParameter(cmd, "VALID_START_DATE", DbType.DateTime, MakeDbNull(model.StartDateValid));
                db.AddInParameter(cmd, "VALID_END_DATE", DbType.DateTime, MakeDbNull(model.EndDateValid));
                db.AddInParameter(cmd, "DEALER_PART_DISCOUNT_RATE", DbType.Decimal, model.DealerPartDiscount);
                db.AddInParameter(cmd, "DEALER_LABOUR_DISCOUNT_RATE", DbType.Decimal, model.DealerLabourDiscount);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);
                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                cmd.ExecuteNonQuery();

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = MessageResource.Global_Error_FK;

                if (model.ErrorNo == 1)
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
