
namespace EventX.Interface
{
    /// <summary>
    /// Defines methods to manage events, allowing adding, removing, dispatching, and listing listeners.
    /// </summary>
    public interface IEventManager
    {
        /// <summary>
        /// Adds an event listener for a specified event.
        /// </summary>
        /// <param name="eventName">The name of the event to listen to.</param>
        /// <param name="handler">The handler that will be invoked when the event is triggered.</param>
        /// <param name="IsOnce">Whether the handler should be invoked only once.</param>
        void AddEventListener(string eventName, Action<object[]> handler, bool IsOnce = false);

        /// <summary>
        /// Removes a specific event listener.
        /// </summary>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="handler">The handler to remove.</param>
        void Off(string eventName, Action<object[]> handler);

        /// <summary>
        /// Dispatches an event with optional parameters.
        /// </summary>
        /// <param name="eventName">The name of the event to dispatch.</param>
        /// <param name="args">Optional parameters to pass to the event handlers.</param>
        void Dispatch(string eventName, params object[] args);

        /// <summary>
        /// Asynchronously dispatches an event with optional parameters.
        /// </summary>
        /// <param name="eventName">The name of the event to dispatch.</param>
        /// <param name="args">Optional parameters to pass to the event handlers.</param>
        Task DispatchAsync(string eventName, params object[] args);

        /// <summary>
        /// Clears all events or a specific event by name.
        /// </summary>
        /// <param name="eventName">The name of the event to clear. If null, clears all events.</param>
        void Clear(string? eventName = null);

        /// <summary>
        /// Lists all currently registered event names.
        /// </summary>
        /// <returns>An enumerable of event names.</returns>
        IEnumerable<string> Listeners();
    }
}
