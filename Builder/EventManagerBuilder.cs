using EventX.Core;
using EventX.Interface;

namespace EventX.Builder
{
    public class EventManagerBuilder : IEventManagerBuilder
    {
        private readonly List<(string eventName, Action<object[]> ahndler, bool IsOnce)> _listeners = new();
        public IEventManagerBuilder AddListener(string eventName, Action<object[]> handler, bool IsOnce = false)
        {
            _listeners.Add((eventName, handler, IsOnce));
            return this;
        }

        public IEventManager Build()
        {
            var manager = new EventManager();

            foreach (var (eventName, handler, IsOnce) in _listeners)
            {
                manager.AddEventListener(eventName, handler, IsOnce);
            }
            
            return manager;
        }
    }
}
