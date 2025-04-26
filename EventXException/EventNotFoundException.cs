
namespace EventX.EventXException
{
    public class EventNotFoundException : EventXException
    {
        public string EventName {  get; set; }
        public EventNotFoundException(string eventName)
            : base($"Event '{eventName}' was not found of has no listners.")
        {
            EventName = eventName;
        }
    }
}
