using ODMSCommon;
using ODMSModel.ProposalInvoice;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ODMSCommon.Security;

namespace ODMSData
{
    public class ProposalInvoicesData:DataAccessBase
    {
        public ProposalInvoicesViewModel GetProposalInvoice(UserInfo user,long ProposalInvoiceId)
        {
            DbDataReader dr = null;
            var model = new ProposalInvoicesViewModel { ProposalInvoiceId = ProposalInvoiceId };
            try
            {
                CreateDatabase();
                var cmd = db.GetStoredProcCommand("P_GET_PROPOSAL_INVOICE");
                db.AddInParameter(cmd, "ID_PROPOSAL_INVOICE", DbType.Int32, MakeDbNull(ProposalInvoiceId));
                db.AddInParameter(cmd, "LANGUAGE_CODE", DbType.String, MakeDbNull(user.LanguageCode));
                db.AddOutParameter(cmd, "ERROR_NO", DbType.Int32, 10);
                db.AddOutParameter(cmd, "ERROR_DESC", DbType.String, 200);
                CreateConnection(cmd);

                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    model.ProposalId = dr["ID_PROPOSAL"].GetValue<long>();
                    model.CustomerId = dr["ID_CUSTOMER"].GetValue<int>();
                    model.CustomerName = dr["CUSTOMER_NAME"].GetValue<String>();
                    model.Address = dr["ADDRESS_DESC"].GetValue<String>();
                    model.VatRatio = dr["VAT_RATIO"].GetValue<decimal>();
                    model.InvoiceRatio = dr["INVOICE_RATIO"].GetValue<decimal>();
                    model.InvoiceNo = dr["INVOICE_NO"].GetValue<String>();
                    model.InvoiceAmount = dr["INVOICE_AMOUNT"].GetValue<decimal>();
                    model.InvoiceDate = dr["INVOICE_DATE"].GetValue<DateTime>();
                    model.InvoiceVatAmount = dr["INVOICE_VAT_AMOUNT"].GetValue<decimal>();
                    model.InvoiceSerialNo = dr["INVOICE_SERIAL_NO"].GetValue<String>();
                    model.Currrency = dr["CURRENCY_CODE"].GetValue<String>();
                    model.DueDuration = dr["DUE_DURATION"].GetValue<int>();
                    model.InvoiceTypeId = dr["INVOICE_TYPE_LOOKVAL"].GetValue<int>();
                    model.HasWitholding = dr["HASWITHOLD"].GetValue<bool>();
                    model.ProposalIds = dr["PROPOSAL_IDS"].ToString();
                }

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (dr != null && !dr.IsClosed)
                {
                    dr.Close();
                    dr.Dispose();
                }
                CloseConnection();
            }
            return model;
        }
    }
}
