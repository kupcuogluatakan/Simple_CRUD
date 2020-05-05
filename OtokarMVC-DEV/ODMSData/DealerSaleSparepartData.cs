using ODMSCommon.Security;
using ODMSModel.DealerSaleSparepart;
using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSModel.ViewModel;
using ODMSData.Utility;

namespace ODMSData
{
    public class DealerSaleSparepartData : DataAccessBase
    {

        private readonly DbHelper _dbHelper;

        public DealerSaleSparepartData()
        {
            _dbHelper = new DbHelper();
        }



        public List<DealerSaleSparepartListModel> ListDealerSaleSparepart(UserInfo user,DealerSaleSparepartListModel filter, out int totalCount)
        {
            var retVal = new List<DealerSaleSparepartListModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_SALE_SPAREPART");
                // *** DİKKAT! *** olmayabilir
                db.AddInParameter(cmd, "PART_CODE", DbType.String, filter.PartCode);
                db.AddInParameter(cmd, "PART_NAME", DbType.String, filter.PartName);
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, filter.IdDealer);
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Decimal, filter.DiscountRatio);
                db.AddInParameter(cmd, "LIST_PRICE", DbType.Decimal, filter.ListPrice);
                db.AddInParameter(cmd, "IS_ACTIVE", DbType.Boolean, filter.IsActive);
                AddPagingParametersWithLanguage(user, cmd, filter);
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var dealerSaleSparepartListModel = new DealerSaleSparepartListModel
                        {
                            IdPart = reader["ID_PART"].GetValue<Int64>(),
                            IdDealer = reader["ID_DEALER"].GetValue<int>(),
                            DiscountRatio = reader["DISCOUNT_RATIO"].GetValue<decimal>(),
                            ListPrice = reader["LIST_PRICE"].GetValue<decimal>(),
                            StockQuantity = reader["STOCK_QUANTITY"].GetValue<decimal>(),
                            DiscountPrice = reader["DISCOUNT_PRICE"].GetValue<decimal>(),
                            PartName = reader["PART_NAME"].ToString(),
                            PartCode = reader["PART_CODE"].ToString(),
                            CreateDate = reader["CREATE_DATE"].GetValue<DateTime>(),
                            IsActive = reader["IS_ACTIVE"].GetValue<bool>(),
                            IsActiveName = reader["IS_ACTIVE_NAME"].ToString(),
                            ShipQty = reader["SHIP_QUANT"].GetValue<decimal>(),
                            Unit = reader["UNIT"].GetValue<string>()
                        };

                        retVal.Add(dealerSaleSparepartListModel);
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

        public DealerSaleSparepartIndexViewModel GetDealerSaleSparepart(UserInfo user, DealerSaleSparepartIndexViewModel filter)
        {
            System.Data.Common.DbDataReader dReader = null;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_DEALER_SALE_SPAREPART");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(filter.IdPart));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, MakeDbNull(filter.IdDealer));

                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dReader = cmd.ExecuteReader();

                while (dReader.Read())
                {
                    filter.IdPart = dReader["ID_PART"].GetValue<Int64>();
                    filter.IdDealer = dReader["ID_DEALER"].GetValue<int>();
                    filter.DiscountRatio = dReader["DISCOUNT_RATIO"].GetValue<decimal>();
                    filter.DiscountPrice = dReader["DISCOUNT_PRICE"].GetValue<decimal>();
                    filter.SalePrice = dReader["DISCOUNT_PRICE"].GetValue<decimal>();
                    filter.PartName = dReader["PART_NAME"].ToString();
                    filter.PartCode = dReader["PART_CODE"].ToString();
                    filter.ListPrice = dReader["LIST_PRICE"].GetValue<decimal>();
                    filter.IsActive = dReader["IS_ACTIVE"].GetValue<bool>();
                    filter.IsActiveName = dReader["IS_ACTIVE_NAME"].ToString();
                    filter.CreateDate = dReader["CREATE_DATE"].GetValue<DateTime>();
                    filter.StockQuantity = dReader["STOCK_QUANTITY"].GetValue<decimal>();
                    filter.ShipQty = dReader["SHIP_QUANT"].GetValue<decimal>();
                    filter.Unit = dReader["UNIT"].GetValue<string>();
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

        public void DMLDealerSaleSparepart(UserInfo user, DealerSaleSparepartIndexViewModel model)
        {
            try
            {
                CreateDatabase();

                var cmd = db.GetStoredProcCommand("P_DML_DEALER_SALE_SPAREPART");
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int32, model.IdDealer);
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, model.IdPart);
                db.AddInParameter(cmd, "DISCOUNT_RATIO", DbType.Decimal, model.DiscountRatio);
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
                model.IdDealer = Int32.Parse(db.GetParameterValue(cmd, "ID_DEALER").ToString());
                model.DiscountRatio = decimal.Parse(db.GetParameterValue(cmd, "DISCOUNT_RATIO").ToString());
                model.ErrorNo = db.GetParameterValue(cmd, "ERROR_NO").GetValue<int>();
                model.ErrorMessage = db.GetParameterValue(cmd, "ERROR_DESC").ToString();
                if (model.ErrorNo == 2)
                    model.ErrorMessage = model.ErrorMessage;
                else if (model.ErrorNo > 0)
                    model.ErrorMessage = ResolveDatabaseErrorXml(model.ErrorMessage);
                else
                {
                    var modelAS = new AutocompleteSearchViewModel
                    {
                        DefaultText = model.PartName,
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
        private DataTable CreateDataTableFromList(List<DealerSaleSparepartIndexViewModel> list)
        {
            DataTable table = new DataTable();

            DataColumn col1 = new DataColumn("ID_DEALER");
            DataColumn col2 = new DataColumn("ID_PART");
            DataColumn col3 = new DataColumn("DISCOUNT_RATIO");
            DataColumn col4 = new DataColumn("IS_ACTIVE");


            col1.DataType = System.Type.GetType("System.Int64");
            col2.DataType = System.Type.GetType("System.Int64");
            col3.DataType = System.Type.GetType("System.Decimal");
            col4.DataType = System.Type.GetType("System.Boolean");

            table.Columns.Add(col1);
            table.Columns.Add(col2);
            table.Columns.Add(col3);
            table.Columns.Add(col4);

            foreach (var item in list)
            {
                DataRow row = table.NewRow();
                row[0] = item.IdDealer;
                row[1] = item.IdPart;
                row[2] = item.DiscountRatio;
                row[3] = item.IsActive;
                table.Rows.Add(row);
            }
            return table;
        }

        public ListDealerSaleSparePartReturnModel DMLDealerSaleSparepartList(UserInfo user, ListDealerSaleSparepartIndexViewModel model)
        {
            var dt = CreateDataTableFromList(model.ListModel);
            var returnList = new ListDealerSaleSparePartReturnModel();
            returnList.ListModel = _dbHelper.ExecuteListReader<DealerSaleSparePartReturnModel>("P_DML_DEALER_SALE_SPAREPART_LIST",
               dt,
               'I',
               MakeDbNull(user.LanguageCode),
               MakeDbNull(user.UserId),
               MakeDbNull(DateTime.Now),
               null,
               null
               );

            returnList.ErrorNo = int.Parse(_dbHelper.GetOutputValue("ERROR_NO").ToString());
            if (returnList.ErrorNo > 0)
            {
                returnList.ErrorMessage = ResolveDatabaseErrorXml(_dbHelper.GetOutputValue("ERROR_NO").ToString());
            }

            return returnList;
        }
        public DealerSaleSparepartIndexViewModel GetSparepartListPrice(UserInfo user, long? idPart)
        {
            var model = new DealerSaleSparepartIndexViewModel();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_SPAREPART_LIST_PRICE");
                db.AddInParameter(cmd, "ID_PART", DbType.Int64, MakeDbNull(idPart));
                db.AddInParameter(cmd, "ID_DEALER", DbType.Int64, MakeDbNull(user.GetUserDealerId()));

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ListPrice = reader["LIST_PRICE"].GetValue<decimal>();
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
            return model;
        }
        public bool CheckPart(string partCode)
        {
            var result = false;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_CHECK_PARTCODE_FOR_SPAREPART");
                db.AddInParameter(cmd, "PART_CODE", DbType.String, MakeDbNull(partCode));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var temp = reader["RESULT"].GetValue<int>();
                        if (temp == 0)
                        {
                            result = false;
                        }
                        else
                        {
                            result = true;
                        }
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
