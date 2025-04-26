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

## Dependency Injection (DI) Support
**EventX** also supports Dependency Injection (DI) and provides a fluent builder pattern to register initial event listeners.

You can easily set it up with .NET's built-in DI system using Host.CreateDefaultBuilder:

```C#
using EventX.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddEventManager(builder =>
        {
            builder
                .AddListener("myevent:customevent1", args => Console.WriteLine("Dispatched customevent1"))
                .AddListener("myevent:customevent2", args => Console.WriteLine("Dispatched customevent2"));
        });
        services.AddScoped<MyCustomService>();
    })
    .Build();
```
This allows you to **pre-register** event listeners at application startup, making it easier to maintain and extend your event-driven architecture.

## License
This project is licensed under the MIT License. See the LICENSE file for details.

## Contribution
Contributions are welcome! Please feel free to open an issue or submit a pull request.
## Contact
For questions or suggestions, please open an issue in the GitHub repository.

### Happy coding with EventX!
