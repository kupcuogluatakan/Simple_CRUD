using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace ODMSModel.ServiceCallSchedule
{
    public class ServiceCallLogModel : ModelBase
    {
        public ServiceCallLogModel()
        {

        }

        public ServiceCallLogModel(string serviceName)
        {
            this.ServiceName = serviceName;
        }

        public Int64 LogId { get; set; }
        public Dictionary<string,string> ReqResDic { get; set; }
        public XElement LogXml { get; set; }
        public string ServiceName { get; set; }
        public string LogType { get; set; }
        public bool IsManuel { get; set; }

        public string LogErrorDesc { get; set; }

        public bool IsSuccess { get; set; }
        
        public XElement LogErrorXml {
            get
            {
                var rVal = new XElement("ErrorLogList");
                if (ErrorModel!=null)
                {
                    foreach (var model in ErrorModel)
                    {
                        rVal.Add(new XElement("Error",
                            new XElement("ActionId",model.Action),
                            new XElement("Description",model.Error)));
                    }
                }
                return rVal;

            }
        }
        public List<ServiceCallScheduleErrorListModel> ErrorModel { get; set; }


    }
}
