using System.Data.Common;
using ODMSCommon.Resources;
using ODMSCommon.Security;
using ODMSModel.SparePartAssemble;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.SparePartSplitting;
using ODMSModel.ViewModel;

namespace ODMSData
{
    public class SparePartAssembleData : DataAccessBase
    {
        public List<SparePartAssembleListModel> ListSparePartAssemble(UserInfo user,SparePartAssembleListModel filter, out int totalCount)
        {
            var retVal = new List<SparePartAssembleListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_ASSEMBLE");

                db.AddInParameter(cmd, "PART_CODE", DbType.String, filter.PartCode);
                db.AddInParameter(cmd, "PART_NAME", DbType.String, filter.PartName);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, filter.IdPart);
                db.AddInParameter(cmd, "ASSEMBLE_ID_PART", DbType.Int64, filter.IdPartAssemble);
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
                        var sparePartAssembleListModel = new SparePartAssembleListModel
                        {
                            IdPart = reader["ID_PART"].GetValue<Int64>(),
                            IdPartAssemble = reader["ASSEMBLE_ID_PART"].GetValue<Int64>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartCodeAssemble = reader["PART_CODE_ASSEMBLE"].GetValue<string>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].GetValue<string>()
                        };


                        retVal.Add(sparePartAssembleListModel);
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

        public SparePartAssembleIndexViewModel GetSparePartAssemble(UserInfo user,SparePartAssembleIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPARE_PART_ASSEMBLE");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "ASSEMBLE_ID_PART", DbType.Int32, MakeDbNull(filter.IdPartAssemble));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.PartCode = dReader["PART_CODE"].GetValue<string>();
                    filter.PartCodeAssemble = dReader["PART_CODE_ASSEMBLE"].GetValue<string>();
                    filter.Quantity = dReader["QUANTITY"].GetValue<decimal>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();

                }
                if (dReader.NextResult())
                {
                    var table = new DataTable();
                    table.Load(dReader);
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

        public void DMLSparePartAssemble(UserInfo user,SparePartAssembleIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_SPARE_PART_ASSEMBLE");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, model.IdPart);
                db.AddInParameter(cmd, "ASSEMBLE_ID_PART", DbType.Int64, model.IdPartAssemble);
                db.AddInParameter(cmd, "QUANTITY", DbType.Decimal, model.Quantity);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, model.IsActive);

                db.AddInParameter(cmd, "COMMAND_TYPE", DbType.String, model.CommandType);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.Int32, MakeDbNull(user.UserId));
                db.AddInParameter(cmd, "OPERATING_DATE", DbType.Date, MakeDbNull(DateTime.Now));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
                model.IdPart = Int64.Parse(db.GetParameterValue(cmd, "ID_PART").ToString());
                model.IdPartAssemble = Int64.Parse(db.GetParameterValue(cmd, "ASSEMBLE_ID_PART").ToString());
                model.Quantity = decimal.Parse(db.GetParameterValue(cmd, "QUANTITY").ToString());
                model.IsActive = bool.Parse(db.GetParameterValue(cmd, "IS_ACTIVE").ToString());

                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                else
                {
                    var modelAS = new AutocompleteSearchViewModel
                    {
                        DefaultText = model.PartCode,
                        DefaultValue = model.IdPart.ToString()
                    };

                    model.PartSearch = modelAS;
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

        public List<SparePartSplittingListModel> ListSparePartSplitting(UserInfo user,SparePartSplittingListModel filter, out int totalCount)
        {
            var listModel = new List<SparePartSplittingListModel>();
            try
            {
                CreateDatabase();
                DbCommand cmd = db.GetStoredProcCommand("P_LIST_SPARE_PART_SPLITTING");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, filter.PartCode);
                db.AddInParameter(cmd, "PART_NAME", DbType.String, filter.PartName);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddInParameter(cmd, "SORT_COLUMN", DbType.String, MakeDbNull(filter.SortColumn));
                db.AddInParameter(cmd, "SORT_DIRECTION", DbType.String, MakeDbNull(filter.SortDirection));
                db.AddInParameter(cmd, "OFFSET", DbType.Int32, filter.Offset);
                db.AddInParameter(cmd, "PAGE_SIZE", DbType.Int32, MakeDbNull(filter.PageSize));
                db.AddOutParameter(cmd, "TOTAL_COUNT", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var model = new SparePartSplittingListModel()
                        {
                            GroupId = dr["GROUP_ID"].GetValue<string>(),
                            RankNo = dr["RANK_NUMBER"].GetValue<string>(),
                            OldPartId = dr["FIRST_PART_ID"].GetValue<string>(),
                            OldPartCode = dr["FIRST_PART_CODE"].GetValue<string>(),
                            OldPartName = dr["OLD_PART_NAME"].GetValue<string>(),
                            CounterNo = dr["COUNTER_NO"].GetValue<string>(),
                            NewPartId = dr["PART_ID"].GetValue<string>(),
                            NewPartCode = dr["NEW_PART_CODE"].GetValue<string>(),
                            NewPartName = dr["NEW_PART_NAME"].GetValue<string>(),
                            Quantity = dr["QUANTITY"].GetValue<string>(),
                            Usable = dr["USABLE"].GetValue<bool>() ? MessageResource.Global_Display_Yes : MessageResource.Global_Display_No,
                            Status = dr["STATUS"].GetValue<string>(),
                            CreateUser = dr["CREATE_USER"].GetValue<string>(),
                            CreateDate = dr["CREATE_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy"),
                            WarrantyUsable = dr["WARRANTY_USABLE"].GetValue<bool>(),
                            WarrantyUsableChange = dr["ACTION_DATE"].GetValue<DateTime?>() == null ? "Kayıt yok!" :
                                $"{dr["CHANGE_USER"]} tarafından {dr["ACTION_DATE"].GetValue<DateTime>().ToString("dd/MM/yyyy hh:mm:ss")} tarihinde değiştirilmiştir."
                        };

                        listModel.Add(model);
                    }
                    dr.Close();
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
            return listModel;
        }

        public void ChangeSplitPartUsage(UserInfo user,long partId, bool usable)
        {
            try
            {

                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHANGE_SPLIT_PART_USAGE");
                db.AddInParameter(cmd, "ID_PART", DbType.Int32, MakeDbNull(partId));
                db.AddInParameter(cmd, "USABLE", DbType.Int32, MakeDbNull(usable));
                db.AddInParameter(cmd, "OPERATING_USER", DbType.String, user.UserId);
                CreateConnection(cmd);
                cmd.ExecuteNonQuery();
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
