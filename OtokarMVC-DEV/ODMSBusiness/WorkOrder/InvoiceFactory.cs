
namespace ODMSBusiness.WorkOrder
{
    internal static class InvoiceFactory
    {
        private static readonly object _lock= new object();
        public static InvoiceBase Create(InvoiceType invocType,long invoiceId,bool hasWitholding=false)
        {
            lock (_lock)
            {
                switch (invocType)
                {
                    case InvoiceType.Dokumlu:
                        return new DokumluFatura(invoiceId,hasWitholding);
                    case InvoiceType.Ozel:
                        return new OzelFatura(invoiceId);
                    case InvoiceType.UcKirilimli:
                        return new UcKirilimliFatura(invoiceId);
                    default :
                        return null;

                }
            }
        }

    }
}
