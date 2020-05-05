namespace ODMSCommon.Exception
{
    public class ODMSException : System.Exception
    {
        public ODMSException()
        {
        }

        public ODMSException(string message)
            : base(message)
        {
        }
    }
}

