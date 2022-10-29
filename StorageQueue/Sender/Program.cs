using Azure.Core;
using Azure.Storage.Queues;
using Azure.Storage;
using Azure.Storage.Queues.Models;

public class Program
{
    public static async Task Main()
    {
        string connectionString = "DefaultEndpointsProtocol=https;AccountName=mizanqueuteststacc;AccountKey=WoXLZIiX0Ip1D/+FaRHEcKKiIxnqyHKTwalsmE9C72yZkdZJ0aa2PTVz7uIIxOkdE4mdzjEncJPM+AStZ6xAgw==;EndpointSuffix=core.windows.net";
        string queueName = "mizan-test-queue";

        QueueClient  queueClient = new QueueClient(connectionString, queueName);
        queueClient.CreateIfNotExists();

        if(queueClient.Exists())
        {
            string message = "this is a test message";
            await queueClient.SendMessageAsync(message);
        }        

    }

}