using System.Collections.Generic;
using System.Web.Mvc;
using ODMSCommon;
using System;

namespace ODMSData
{
    public class DealerSaleChannelData : DataAccessBase
    {
        public List<SelectListItem> ListDealerSaleChannelsAsSelectListItem()
        {
            var retVal = new List<SelectListItem>();

            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_LIST_DEALER_SALE_CHANNEL_COMBO");
                CreateConnection(cmd);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var lookupItem = new SelectListItem
                            {
                                Value = reader["CHANNEL_CODE"].GetValue<string>(),
                                Text = reader["CHANNEL_NAME"].GetValue<string>()
                            };
                        retVal.Add(lookupItem);
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
    }
}
