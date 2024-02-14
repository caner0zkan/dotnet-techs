using StackExchange.Redis;

ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = conn.GetSubscriber();

await subscriber.SubscribeAsync("myChannel", (channel, message) => {
    Console.WriteLine(message);
});

Console.Read();
