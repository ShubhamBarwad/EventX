
namespace EventX.EventXException
{
    public class EventManagerNotInitializedException : EventXException
    {
        public EventManagerNotInitializedException()
            : base("The Eventmanager has not been properly initialized") { }
    }
}
