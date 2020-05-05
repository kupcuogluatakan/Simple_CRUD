namespace ODMSCommon.Exception
{
    public class ODMSDatabaseConnectionException:ODMSException
    {
        public new string Message { get; set; }

        public ODMSDatabaseConnectionException(string message):base(message)
        {
            Message = message;
        }
    }
}
