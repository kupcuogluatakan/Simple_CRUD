using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODMSModel.Proposal
{
    [Flags]
    public enum ProposalInvoiceType
    {
        Dokumlu = 1 << 0,
        UcKirilimli = 1 << 1,
        Ozel = 1 << 2
    }
    [Flags]
    public enum ProposalInvoicePrintType
    {
        Printed = 1 << 0,
        Transcript = 1 << 1,
        Proforma = 1 << 2,
        ProformaExcel = 1 << 3,
        ProposalAndProforma = 1 << 4
    }
}
