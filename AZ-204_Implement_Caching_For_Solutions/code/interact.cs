using StackExchange.Redis;
using System;

class Program
{
    static void Main()
    {
        // Connect to the Redis cache
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("<your-redis-connection-string>");

        // Get a reference to the cache database
        IDatabase cache = redis.GetDatabase();

        // Store a value in the cache
        cache.StringSet("mykey", "myvalue");

        // Retrieve the value from the cache
        string value = cache.StringGet("mykey");

        Console.WriteLine("Value from cache: " + value);

        // Remove the value from the cache
        cache.KeyDelete("mykey");

        // Close the connection to the Redis cache
        redis.Close();
    }
}