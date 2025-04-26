using System.Collections.Concurrent;
using EventX.Interface;
using EventX.EventXException;

namespace EventX.Core
{
    /// <summary>
    /// Manages events, allowing adding, removing, dispatching, and listing event listeners.
    /// </summary>
    public class EventManager : IEventManager
    {
        private readonly ConcurrentDictionary<string, List<Delegate>> _events = new();

        /// <summary>
        /// Adds an event listener for the specified event.
        /// </summary>
        /// <param name="eventName">The name of the event to listen to.</param>
        /// <param name="handler">The handler to invoke when the event is dispatched.</param>
        /// <param name="IsOnce">If true, the handler will be invoked only once.</param>
        public void AddEventListener(string eventName, Action<object[]> handler, bool IsOnce = false)
        {
            ValidateEventName(eventName, handler);

            if (IsOnce)
            {
                Once(eventName, handler);
            }
            else
            {
                _events.AddOrUpdate(
                    eventName,
                    _ => new List<Delegate> { handler },
                    (_, list) =>
                    {
                        list.Add(handler);
                        return list;
                    }
                    );
            }
        }

        /// <summary>
        /// Clears all events or a specific event.
        /// </summary>
        /// <param name="eventName">The name of the event to clear. If null, clears all events.</param>
        public void Clear(string? eventName = null)
        {
            if (eventName == null)
                _events.Clear();
            else
            {
                IsEventExists(eventName);
                _events.TryRemove(eventName, out _);
            }
        }

        /// <summary>
        /// Dispatches an event with optional arguments to all registered listeners.
        /// </summary>
        /// <param name="eventName">The name of the event to dispatch.</param>
        /// <param name="args">Optional arguments to pass to the event handlers.</param>
        public void Dispatch(string eventName, params object[] args)
        {
            IsEventExists(eventName);

            if (_events.TryGetValue(eventName, out var handlers))
            {
                foreach (var handler in handlers.ToList())
                {
                    handler.DynamicInvoke(new object[] { args });
                }
            }
        }

        /// <summary>
        /// Asynchronously dispatches an event with optional arguments to all async listeners.
        /// </summary>
        /// <param name="eventName">The name of the event to dispatch.</param>
        /// <param name="args">Optional arguments to pass to the event handlers.</param>
        public async Task DispatchAsync(string eventName, params object[] args)
        {
            IsEventExists(eventName);

            if (_events.TryGetValue(eventName, out var handlers))
            {
                var tasks = handlers
                    .OfType<Func<object[], Task>>()
                    .Select(handler => handler(args));
                await Task.WhenAll(tasks);
            }
        }

        /// <summary>
        /// Lists all registered event names.
        /// </summary>
        /// <returns>An enumerable of event names.</returns>
        public IEnumerable<string> Listeners()
        {
            return _events.Keys;
        }

        /// <summary>
        /// Removes a specific event listener from an event.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="handler">The handler to remove.</param>
        public void Off(string eventName, Action<object[]> handler)
        {
            ValidateEventName(eventName, handler);

            if (_events.TryGetValue(eventName, out var handlers))
            {
                handlers.RemoveAll(h => h.Equals(handler));
                if (!handlers.Any())
                {
                    _events.TryRemove(eventName, out _);
                }
            }
        }

        /// <summary>
        /// Adds an event listener that will only be triggered once.
        /// </summary>
        /// <param name="eventName">The name of the event to listen to.</param>
        /// <param name="handler">The handler to invoke once.</param>
        private void Once(string eventName, Action<object[]> handler)
        {
            Action<object[]>? wrapper = null;
            wrapper = args =>
            {
                Off(eventName, wrapper!);
                handler(args);
            };
            AddEventListener(eventName, wrapper);
        }

        /// <summary>
        /// Validates the event name and handler before adding a listener.
        /// </summary>
        /// <param name="eventName">The event name to validate.</param>
        /// <param name="handler">The handler associated with the event.</param>
        /// <exception cref="InvalidEventNameException">Thrown if the event name is invalid.</exception>
        /// <exception cref="HandlerAlreadyRegisteredException">Thrown if the handler is already registered for the event.</exception>
        private void ValidateEventName(string eventName, Action<object[]> handler)
        {
            if (string.IsNullOrEmpty(eventName))
                throw new InvalidEventNameException(eventName);
            if (_events.ContainsKey(eventName) && _events[eventName].Contains(handler))
                throw new HandlerAlreadyRegisteredException(eventName);
        }

        /// <summary>
        /// Checks if an event exists in the event manager.
        /// </summary>
        /// <param name="eventName">The event name to check.</param>
        /// <exception cref="EventNotFoundException">Thrown if the event does not exist.</exception>
        private void IsEventExists(string eventName)
        {
            if (!_events.ContainsKey(eventName))
                throw new EventNotFoundException(eventName);
        }
    }
}
