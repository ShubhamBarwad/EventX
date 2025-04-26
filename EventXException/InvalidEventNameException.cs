
namespace EventX.EventXException
{
    public class InvalidEventNameException : EventXException
    {
        public InvalidEventNameException(string eventName)
            : base($"The event name '{eventName}' is invalid.") { }
    }
}
