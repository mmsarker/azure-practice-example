using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;

public partial class Program{
    static string _connectionString = "Endpoint=sb://test-service-bus-namespace-11.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=GJ66J0fRQg0aCV0uWGHsmbmW2X9pGMAw253EnVfJ2hk=";
    static string _queueName = "test-queue-name-11";

    static ServiceBusClient _serviceBusClient;
    static ServiceBusProcessor _serviceBusProcessor;
    
    public static async Task Main()
    {
        try
        {
            _serviceBusClient = new ServiceBusClient(_connectionString);
            _serviceBusProcessor = _serviceBusClient.CreateProcessor(_queueName, new ServiceBusProcessorOptions());
            _serviceBusProcessor.ProcessMessageAsync += MessageHandler;
            _serviceBusProcessor.ProcessErrorAsync += ErrorHandler;
            await _serviceBusProcessor.StartProcessingAsync();
            Console.WriteLine("Message is processing. press any key to stop.");
            Console.ReadKey();

            Console.WriteLine("Stopping to receive messages.");
            await _serviceBusProcessor.StopProcessingAsync();
        }
        catch(Exception ex)   
        {
            await _serviceBusProcessor.DisposeAsync();
            await _serviceBusClient.DisposeAsync();
        }

    }

    static async Task MessageHandler(ProcessMessageEventArgs args)
    {
        string messageBody = args.Message.Body.ToString();
        Console.WriteLine($"Message received: {messageBody}");
        await args.CompleteMessageAsync(args.Message);        
    }

    static Task ErrorHandler(ProcessErrorEventArgs args)
    {
        Console.WriteLine(args.Exception.ToString());
        return Task.CompletedTask;
    }
}
