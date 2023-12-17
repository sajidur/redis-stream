// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Hello, World!");
// Connection to the Redis server
var redisConnection = ConnectionMultiplexer.Connect("localhost");

// Get a reference to the Redis database
var redisDatabase = redisConnection.GetDatabase();

// Name of the Redis Stream
var streamName = "mystream";

// Add messages to the stream
await AddMessagesToStream(redisDatabase, streamName);

async Task AddMessagesToStream(IDatabase redisDatabase, string streamName)
{
    // Add messages to the stream
    await redisDatabase.StreamAddAsync(streamName, new RedisValue("Message1"),new RedisValue("Message2"));

}

// Read messages from the stream
await ReadMessagesFromStream(redisDatabase, streamName);

async Task ReadMessagesFromStream(IDatabase redisDatabase, string streamName)
{
    // Read messages from the stream
    var entries = await redisDatabase.StreamReadAsync(streamName, "0", count: 10);

    // Process the entries
    foreach (var entry in entries)
    {
        Console.WriteLine($"Message: {entry.Values.First(v => v.Name == "message").Value}");
    }
}

// Close the connection
redisConnection.Close();
