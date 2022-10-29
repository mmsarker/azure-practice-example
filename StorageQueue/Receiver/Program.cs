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

        QueueClient queueClient = new QueueClient(connectionString, queueName);
        queueClient.CreateIfNotExists();

        if (queueClient.Exists())
        {
            await RetrieveMessages(queueClient);
            //PeekMessages(queueClient);
        }

    }

    private static async Task RetrieveMessages(QueueClient queueClient)
    {
        var response = await queueClient.ReceiveMessagesAsync(12);
        var queueMessages = response.Value;

        var properties = queueClient.GetProperties();        

        Console.WriteLine($"Number of message picked:{queueMessages.Count()} ");

        foreach (QueueMessage message in queueMessages)
        {
            string messageStr = message.Body.ToString();
            await queueClient.DeleteMessageAsync(message.MessageId, message.PopReceipt);
            Console.WriteLine("Messgae is here: ");
            Console.WriteLine(messageStr);
        }
    }

    private static async void PeekMessages(QueueClient queueClient)
    {
        PeekedMessage[] peekedMessages = await queueClient.PeekMessagesAsync(3);

        Console.WriteLine("Number of message picked: " + peekedMessages.Count());

        foreach (PeekedMessage message in peekedMessages)
        {
            string messageStr = message.Body.ToString();

            Console.WriteLine("Messgae is here: ");
            Console.WriteLine(messageStr);
        }
    }
}