using System;
using System.Collections.Generic;
using System.Data;
using ODMSCommon;
using ODMSCommon.Security;
using ODMSData.DataContracts;
using ODMSModel.SparePartBarcode;

namespace ODMSData
{
    public class SparePartBarcodeData : DataAccessBase
    {
        public List<SparePartBarcodeIndexViewModel> List(UserInfo user,int workOrderId, bool isPrinted)
        {
            var list = new List<SparePartBarcodeIndexViewModel>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_PRINT_CLAIM_DISMANTLED_PART_BARCODE");
                db.AddInParameter(cmd, "ID_WORK_ORDER", DbType.Int32, workOrderId);
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, user.LanguageCode);
                db.AddInParameter(cmd, "IS_PRINTED", DbType.Int32, isPrinted);
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);

                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var model = new SparePartBarcodeIndexViewModel
                        {
                            ClaimDismantledPartId = reader["ID_CLAIM_DISMANTLED_PARTS"].GetValue<int>(),
                            WorkOrderDetail = reader["ID_WORK_ORDER_DETAIL"].GetValue<int>(),
                            Quantity = reader["QUANTITY"].GetValue<decimal>(),
                            WorkOrderId = reader["ID_WORK_ORDER"].GetValue<int>(),
                            VinNo = reader["VIN_NO"].GetValue<string>(),
                            WarrantlyStartDate = reader["WARRANTY_START_DATE"].GetValue<DateTime>(),
                            WorkOrderDate = reader["WO_DATE"].GetValue<DateTime>(),
                            PartCode = reader["PART_CODE"].GetValue<string>(),
                            PartName = reader["PART_NAME"].GetValue<string>(),
                            VehicleKm = reader["VEHICLE_KM"].GetValue<string>(),
                            DealerName = reader["DEALER_NAME"].GetValue<string>(),
                            ShipQuantity = reader["SHIP_QUANT"].GetValue<int>(),
                            GifNo = reader["SSID_GUARANTEE"].GetValue<string>()
                        };

                        list.Add(model);
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

            return list;
        }
    }
}
