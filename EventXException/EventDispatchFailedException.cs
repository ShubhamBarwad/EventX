
namespace EventX.EventXException
{
    public class EventDispatchFailedException : EventXException
    {
        public string EventName { get; set; }

        public EventDispatchFailedException(string eventName, string message)
            : base(message)
        {
            EventName = eventName;
        }
    }
}
