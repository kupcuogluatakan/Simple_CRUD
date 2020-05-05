using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ODMSCommon;
using ODMSCommon.Resources;
using ODMSBusiness.OtokarService;
namespace ODMSBusiness.Terminal.Common
{
    public static class Utility
    {
        internal static string ResolveDatabaseErrorXml(string xmlError)
        {
            var errCode = string.Empty;
            var errParamList = new List<string>();

            if (string.IsNullOrWhiteSpace(xmlError) || xmlError.IndexOf("<Error>") < 0)
            {
                return MessageResource.Err_Generic_Unexpected;
            }

            var startIdx = xmlError.IndexOf("<Error>");
            var endIdx = xmlError.IndexOf("</Error>") + "</Error>".Length;

            var xmlDoc = new XmlDocument
            {
                InnerXml =
                    xmlError.Substring(startIdx, endIdx - startIdx)
                            .Replace("&", "&amp;")
                            .Replace("'", "&apos;")
                            .Replace("\"", "&quot;")
            };
            XmlElement root = xmlDoc.DocumentElement;
            XmlNodeList nodeList = root.GetElementsByTagName("ErrorMessage");
            if (nodeList.Count > 0)
            {
                errCode = nodeList[0].InnerText;
            }

            nodeList = root.GetElementsByTagName("ErrParam1");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            nodeList = root.GetElementsByTagName("ErrParam2");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            nodeList = root.GetElementsByTagName("ErrParam3");
            if (nodeList.Count > 0 && !string.IsNullOrWhiteSpace(nodeList[0].InnerText))
            {
                errParamList.Add(nodeList[0].InnerText);
            }

            if (string.IsNullOrWhiteSpace(errCode))
            { return MessageResource.Err_Generic_Unexpected; }

            return string.Format(CommonUtility.GetResourceValue(errCode), errParamList.ToArray());

        }

        internal static dynamic GetClient()
        {
            if (ConfigurationManager.AppSettings.GetValues("ServiceReferance")[0] == "Test")
                return new ProjectServiceSoapClient();
            return new tr.com.otokar.service.ProjectServiceSoapClient();
        }
        internal static object MakeDbNull(object obj)
        {
            if (obj == null) return DBNull.Value;

            if (obj is String)
            {
                if (string.IsNullOrWhiteSpace(obj.ToString())) return DBNull.Value;
            }
            if (obj is Int16 || obj is Int32 || obj is Int64)
            {
                if (obj.ToString().Equals("0")) return DBNull.Value;
            }
            if (obj.GetType() == TypeCode.Double.GetType())
            {
                if (obj.GetValue<double>() == 0) return DBNull.Value;
            }
            if (obj is Decimal && (obj.GetValue<decimal>() == 0))
            {
                return DBNull.Value;
            }
            if (obj is DateTime)
            {
                if (((DateTime)obj).Equals(DateTime.MinValue))
                    return DBNull.Value;
            }

            return obj;
        }
    }
}
