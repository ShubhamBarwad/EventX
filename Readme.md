# EventX

**EventX** is a lightweight, flexible event manager for C# that allows you to easily handle events, dispatch them, and register listeners. This library is perfect for implementing an event-driven architecture in your C# projects.
**EventX** follows the JavaScript-style event listener pattern, where you can register event listeners for specific events, dispatch events, and remove listeners dynamically.

## Installation

To install via NuGet:

```bash
dotnet add package EventX --version 1.0.0
```
Or you can manually add the package through the NuGet Package Manager in Visual Studio.
Usage

## Add an Event Listener
You can add an event listener to handle a specific event. Event handlers are triggered when the event is dispatched.
using EventX.Core;

```C#
EventManager.AddEventListener("eventName", (args) =>
{
    // Handle the event
    Console.WriteLine("Event received with args: " + string.Join(", ", args));
});
```

## Dispatch an Event
You can dispatch an event and pass arguments to the listeners.

```C#
using EventX.Core;

EventManager.Dispatch("eventName", "arg1", 42);
```

## Remove an Event Listener
If you want to remove a previously registered event listener, use the Off method.
using EventX.Core;

```C#
EventManager.Off("eventName", handler);
```

## Dispatch an Event Asynchronously
You can also dispatch events asynchronously.

```C#
using EventX.Core;

await EventManager.DispatchAsync("eventName", "asyncArg", 123);
```

## Add a Once-Only Event Listener
To add a listener that will only be triggered once, use the Once method.

```C#
using EventX.Core;

EventManager.AddEventListener("eventName", (args) =>
{
    Console.WriteLine("This will be triggered only once.");
}, IsOnce: true);
```

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contribution
Contributions are welcome! Please feel free to open an issue or submit a pull request.
## Contact
For questions or suggestions, please open an issue in the GitHub repository.

### Happy coding with EventX!
