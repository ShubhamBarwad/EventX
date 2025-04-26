
namespace EventX.EventXException
{
    public class HandlerAlreadyRegisteredException : EventXException
    {
        public string EventName { get; set; }
        public HandlerAlreadyRegisteredException(string eventName)
            : base($"Event name '{eventName}' already exists")
        {
            EventName = eventName;
        }
    }
}
