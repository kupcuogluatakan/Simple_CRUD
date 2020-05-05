
namespace ODMSBusiness.Reports
{
    public interface IReport
    {
        byte[] FetchBytes(params object[] parameters);

        byte[] FetchBytesAsync(params object[] parameters);
    }
}
