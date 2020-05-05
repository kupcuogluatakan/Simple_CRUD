namespace ODMSCommon.Exception
{
    public class ODMSAuthenticationException : ODMSException
    {
        public ODMSAuthenticationException()
        {
        }

        public ODMSAuthenticationException(string message)
            : base(message)
        {
        }
    }
}
