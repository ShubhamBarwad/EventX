
namespace EventX.Interface
{
    public interface IEventManagerBuilder
    {
        IEventManagerBuilder AddListener(string eventName, Action<object[]> handler, bool IsOnce = false);
        IEventManager Build();
    }
}
