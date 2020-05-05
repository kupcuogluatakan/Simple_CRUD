using ODMSCommon;
using ODMSModel.PartCostPriceService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSData
{
    public class PartCostData : DataAccessBase
    {
        public List<PartCostPriceXMLModel> GetGuaranteeDetPart(int partID,out int totalCount)
        {
            var retVal = new List<PartCostPriceXMLModel>();
            totalCount = 0;
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_GUARANTEE_DET_PART");
                db.AddInParameter(cmd, "PART_ID", DbType.Int32, MakeDbNull(partID));
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var vehicleListModel = new PartCostPriceXMLModel
                        {
                            PartCode = reader["PART_CODE"].ToString(),
                            Date = reader["APPROVE_DATE"].GetValue<DateTime>(),
                            StockType = reader["STOCK_TYPE"].ToString()                           
                        };
                        retVal.Add(vehicleListModel);
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
      
    }
}
