using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ReportService.Aspects;
using System.Dynamic;
using KingAOP;

namespace ReportService
{
    public static class ReportUtility
    {
        public static bool checkConsumer(UserCredentials consumer)
        {
            if (GetUserInfo(consumer))
                return true;
            else
                return false;
        }

        private static bool GetUserInfo(UserCredentials consumer)
        {
            bool result = false;

            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["Otokar_DMSConnectionString"].ConnectionString);
            SqlCommand cmd = new SqlCommand("P_CHECK_USER_VALIDATION", connection);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("ID_DMS_USER", consumer.userId);

            connection.Open();

            try
            {
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        if (consumer.userId == Convert.ToInt32(reader["UserId"]) && DecryptSymmetric(consumer.password) == reader["Password"].ToString())
                        {
                            result = true;
                        }
                    }
                }
            }
            finally
            {
                connection.Close();
                connection.Dispose();
                cmd.Dispose();
            }

            return result;
        }

        private static string DecryptSymmetric(string text)
        {
            try
            {
                string initVector = ConfigurationManager.AppSettings.Get("initVector");
                var initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                var cipherTextBytes = Convert.FromBase64String(text);
                var password = new PasswordDeriveBytes(ConfigurationManager.AppSettings.Get("passwordKey"), null);
                var keyBytes = password.GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged { Mode = CipherMode.CBC };
                var decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                var plainTextBytes = new byte[cipherTextBytes.Length];
                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch (System.Exception)
            {
                return string.Empty;
            }
        }

        public static void SetDBLogonForReport(CrystalDecisions.Shared.ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {

            Tables tables = reportDocument.Database.Tables;
            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
        }

        public static void SetDBLogonForSubreports(CrystalDecisions.Shared.ConnectionInfo connectionInfo, ReportDocument reportDocument)
        {
            Sections sections = reportDocument.ReportDefinition.Sections;
            foreach (Section section in sections)
            {
                ReportObjects reportObjects = section.ReportObjects;
                foreach (ReportObject reportObject in reportObjects)
                {
                    if (reportObject.Kind == ReportObjectKind.SubreportObject)
                    {
                        SubreportObject subreportObject = (SubreportObject)reportObject;
                        ReportDocument subReportDocument = subreportObject.OpenSubreport(subreportObject.SubreportName);
                        SetDBLogonForReport(connectionInfo, subReportDocument);
                    }
                }
            }
        }

        public static ConnectionInfo GetConnectionInfo()
        {
            CrystalDecisions.Shared.ConnectionInfo connectionInfo = new CrystalDecisions.Shared.ConnectionInfo
            {
                ServerName = ConfigurationManager.AppSettings.Get("ServerName"),
                DatabaseName = ConfigurationManager.AppSettings.Get("DatabaseName"),
                UserID = ConfigurationManager.AppSettings.Get("UserID"),
                Password = ConfigurationManager.AppSettings.Get("Password")
            };
            return connectionInfo;
        }      
    }
}