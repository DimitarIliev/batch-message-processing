using Azure.Messaging.ServiceBus;

var connectionString = "<NAMESPACE CONNECTION STRING>";
var topicName = "bulk-message";
var nmMessages = 3;

ServiceBusClient client;
ServiceBusSender sender;

client = new ServiceBusClient(connectionString);
sender = client.CreateSender(topicName);

using ServiceBusMessageBatch messageBatch = await sender.CreateMessageBatchAsync();

for (int i = 1; i <= nmMessages; i++)
{
    if (!messageBatch.TryAddMessage(new ServiceBusMessage($"Message {i}")))
    {
        throw new Exception($"The message {i} is too large to fit in the batch.");
    }
}

try
{
    await sender.SendMessagesAsync(messageBatch);
    Console.WriteLine($"A batch of {nmMessages} messages has been published to the topic: {topicName}.");
}
finally
{
    await sender.DisposeAsync();
    await client.DisposeAsync();
}

Console.WriteLine("Press any key exit...");
Console.ReadKey();
