using ODMSBusiness;
using ODMSModel.ServiceCallSchedule;
using ODMSModel.Vehicle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;
using NUnit.Framework;
namespace ODMSServiceTests
{
    [TestFixture]
    public class ManualVehicleServiceTest
    {
        //[Test]
        //public void Work()
        //{
        //    var mainBL = new VehicleBL();
        //    var logModel = new ServiceCallLogModel();
        //    var rValue = new XmlDocument();
        //    rValue.Load(@"C:\Users\02482898\Desktop\ZDMS_VEHICLE.xml");
        //    XmlNodeList xmlList = rValue.SelectNodes("//tbl");
        //    var listModel = (from XmlNode xmlNode in xmlList
        //                     select new VehicleXMLModel
        //                     {
        //                         CodeSSID = xmlNode["CODE_SSID"].InnerText,
        //                         VinNo = xmlNode["VIN_NO"].InnerText,
        //                         EngineNo = xmlNode["ENGINE_NO"].InnerText,
        //                         ModelYear = xmlNode["MODEL_YEAR"].InnerText,
        //                         Color = xmlNode["COLOR"].InnerText,
        //                         FactProdDate = xmlNode["FACT_PROD_DATE"].InnerText,
        //                         FactQcntrlDate = xmlNode["FACT_QCNTRL_DATE"].InnerText,
        //                         FactShipDate = xmlNode["FACT_SHIP_DATE"].InnerText,
        //                         VatExclude = xmlNode["VAT_EXCLUDE"].InnerText,
        //                         CustomerNo = xmlNode["KUNAG"].InnerText,
        //                         CustomerName = xmlNode["NAME1"].InnerText,
        //                         TCNo = (xmlNode["COUNTRY"].InnerText == "TR" && xmlNode["STCD2"].InnerText.Length == 11) ? xmlNode["STCD2"].InnerText : string.Empty,
        //                         VatNo = string.IsNullOrEmpty((xmlNode["COUNTRY"].InnerText == "TR" && xmlNode["STCD2"].InnerText.Length == 11) ? xmlNode["STCD2"].InnerText : string.Empty) ? xmlNode["STCD2"].InnerText : string.Empty,
        //                         VatOffice = xmlNode["STCD1"].InnerText,
        //                         Address = xmlNode["ADRES"].InnerText,
        //                         CountryShortCode = xmlNode["COUNTRY"].InnerText,
        //                         PlateCode = xmlNode["REGION"].InnerText,
        //                         City = xmlNode["BEZEI"].InnerText,
        //                         Phone = xmlNode["TEL_NUMBER"].InnerText,
        //                         MobilePhone = xmlNode["MOB_NUMBER"].InnerText,
        //                         Email = xmlNode["SMTP_ADDR"].InnerText,
        //                         FaxNo = xmlNode["FAX_NUMBER"].InnerText,
        //                         Kamu = xmlNode["KAMU"].InnerText,
        //                         BSTKD = xmlNode["BSTKD"].InnerText,
        //                         PriceList = xmlNode["LISTEKODU"].InnerText
        //                     }).ToList();
          
        //       // logModel = mainBL.XMLtoDBVehicle(listModel);
        //    Assert.NotNull(listModel);

        //}
    }
}
