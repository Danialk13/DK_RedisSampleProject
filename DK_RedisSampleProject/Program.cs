using StackExchange.Redis;
using System.Reflection;

using (ConnectionMultiplexer connection = ConnectionMultiplexer.Connect("localhost,allowAdmin=true"))
{
    // This code is for flushing the database for having an empty db to test
    var server = connection.GetServer(connection.GetEndPoints().FirstOrDefault());
    server.FlushDatabase();

    // Get a reference to the Redis database
    IDatabase db = connection.GetDatabase();


    // *********************************************
    Console.WriteLine("Key-Value Pair:");

    // Set a key-value pair
    db.StringSet("DK_Hello", "Hello, This is DK Redis sample project!");

    // Retrieve a value by key
    string value = db.StringGet(key: "DK_Hello")!;

    Console.WriteLine(value);
    Console.WriteLine();
    // *********************************************


    // *********************************************
    Console.WriteLine("Redis Hash:");

    // Create a Redis Hash
    HashEntry[] hashEntries = new HashEntry[]
    {
        new HashEntry("field1", "value1"),
        new HashEntry("field2", "value2")
    };

    db.HashSet("myhash", hashEntries);

    // Retrieve a specific field from the Hash
    string fieldValue = db.HashGet("myhash", "field1")!;

    Console.WriteLine(fieldValue);
    Console.WriteLine();
    // *********************************************


    // *********************************************
    Console.WriteLine("Redis List:");

    // Add items to a Redis List
    db.ListLeftPush("mylist", "item1");
    db.ListLeftPush("mylist", "item2");
    db.ListRightPush("mylist", "item0");

    // Retrieve all items from the List
    RedisValue[] listItems = db.ListRange("mylist");

    foreach (var item in listItems)
        Console.WriteLine(item.ToString());
    Console.WriteLine();
    // *********************************************


    // Disconnect from Redis
    connection.Close();
}

Console.ReadKey();