using ODMSCommon;
using ODMSData;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.SpartInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSBusiness.Business
{
    public class SpartInfoBL : BaseBusiness
    {
        private readonly SpartInfoData data = new SpartInfoData();

        public ServiceCallLogModel XMLtoDBSPartInfo(List<SpartInfoXMLViewModel> listModel, List<SpartInfoXMLViewModel> listLangModel)
        {
            var model = new ServiceCallLogModel();
            try
            {
                var listOldModel = data.GetSpartInfoListForXML();

                foreach (var item in listModel)
                {
                    //update
                    if (listOldModel.Any(x => x.Spart == item.Spart))
                    {
                        item.CommandType = CommonValues.DMLType.Update;
                    }
                    //insert
                    else
                    {
                        item.CommandType = CommonValues.DMLType.Insert;
                    }
                }

                model = data.XMLtoDBSparePart(listModel);

                if (model.IsSuccess)
                    data.XMLtoDBVehicleCodeLang(listLangModel);
            }
            catch (Exception ex)
            {
                AppErrorsBL.Add(ex.Message, System.Reflection.MethodBase.GetCurrentMethod(), data.ExecuteSql);
            }

            return model;
        }
    }
}
