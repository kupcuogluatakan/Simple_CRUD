using System;

namespace ODMSBusiness.WorkOrder
{
    [Flags]
    public enum InvoiceType
    {
        Dokumlu = 1 << 0,
        UcKirilimli = 1 << 1,
        Ozel = 1 << 2
    }
    [Flags]
    public enum InvoicePrintType
    {
        Printed = 1 << 0,
        Transcript = 1 << 1,
        Proforma = 1 << 2,
        ProformaExcel=1<<3,
        WorkOrderAndProforma=1<<4
    }

}
