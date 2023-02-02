using HotChocolate.Execution;
using HotChocolate.Subscriptions;
using Subscription.Gateway;

public class Subscriptions
{
    [SubscribeAndResolve]
    public ValueTask<ISourceStream<Test>> TestSubscription([Service] ITopicEventReceiver receiver, CancellationToken ct)
    {
        return receiver.SubscribeAsync<string, Test>("test", ct);
    }
}