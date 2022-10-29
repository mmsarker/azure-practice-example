using Azure.Core;
using Azure.Storage.Queues;
using Azure.Storage;
using Azure.Storage.Queues.Models;

public class Program
{
    public static async Task Main()
    {
        string connectionString = "";
        string queueName = "";

        QueueClient  queueClient = new QueueClient(connectionString, queueName);
        queueClient.CreateIfNotExists();

        if(queueClient.Exists())
        {
            string message = "this is a test message";
            await queueClient.SendMessageAsync(message);
        }        

    }

}