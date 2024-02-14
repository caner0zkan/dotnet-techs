﻿using StackExchange.Redis;

ConnectionMultiplexer conn = await ConnectionMultiplexer.ConnectAsync("localhost:1453");

ISubscriber subscriber = conn.GetSubscriber();

while (true) {
    Console.Write("Message: ");
    string message = Console.ReadLine();
    await subscriber.PublishAsync("myChannel", message);
}
