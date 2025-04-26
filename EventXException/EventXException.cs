
namespace EventX.EventXException
{
    public class EventXException : Exception
    {
        public int ErrorCode { get; set; }
        public EventXException() { }
        public EventXException(string message) : base(message) { }
        public EventXException(string message, Exception innerException) : base(message, innerException) { }
        public EventXException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
