using EventX.Builder;
using EventX.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace EventX.Extension
{
    public static class EventManagerServiceCollectionExtension
    {
        public static IServiceCollection AddEventManager(this IServiceCollection services, Action<IEventManagerBuilder>? configure = null)
        {
            var builder = new EventManagerBuilder();
            configure?.Invoke(builder);

            var manager = builder.Build();

            return services.AddSingleton<IEventManager>(manager);
        }
    }
}
