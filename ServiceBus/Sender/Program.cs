using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
public partial class Program
{
    static string _connectionString = "Endpoint=sb://test-service-bus-namespace-11.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GJ66J0fRQg0aCV0uWGHsmbmW2X9pGMAw253EnVfJ2hk=";
    static string _queueName = "test-queue-name-11";
    static ServiceBusClient _client;
    static ServiceBusSender _sender;
    private const int _numberOfMessages = 3;


    static async Task Main()
    {
        _client = new ServiceBusClient(_connectionString);
        _sender = _client.CreateSender(_queueName);

        using ServiceBusMessageBatch messageBatch = await _sender.CreateMessageBatchAsync();

        for(int i = 1;i<=_numberOfMessages;i++)
        {
            if(!messageBatch.TryAddMessage(new ServiceBusMessage($"Message{i}")))
            {
                throw new Exception($"Exception {i} has occured. ");
            }            
        }

        try
        {            
            await   _sender.SendMessagesAsync(messageBatch);
            Console.WriteLine($"Messages sent: {_numberOfMessages}");
        } 
        finally
        {
            await _sender.DisposeAsync();
            await _client.DisposeAsync();
        }


        Console.Write("This is test");
        Console.ReadKey();
    }
}
