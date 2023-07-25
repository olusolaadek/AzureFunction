using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;

string connectionString = "DefaultEndpointsProtocol=https;AccountName=olustor123;AccountKey=C+VbrUaOys4UBIutaYxUfeteG9MQDj3OKQOYjV3Grj6IC/2wsb7b+h/5abCDoO76pO4lzupfFJrN+ASt/T9uYg==;EndpointSuffix=core.windows.net";
string queueName = "appqueue";

SendMessage("Test message from VS 2022 ");
SendMessage("Another Test message from VS 2022 ");

// PeekMessages();
// ReceiveMessages();

Console.WriteLine("The number of messages in the queue is: "+ GetQueueLength());

int GetQueueLength()
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    if (queueClient.Exists())
    {
        QueueProperties properties = queueClient.GetProperties();

        return properties.ApproximateMessagesCount;

    }
    return 0;
}

void ReceiveMessages()
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    int maxMessages = 10;

    if (queueClient.Exists())
    {
        QueueMessage[] queueMessages = queueClient.ReceiveMessages(maxMessages);

        foreach (var message in queueMessages)
        {
            Console.WriteLine("{0}, Inserted on: {1}", message.Body, message.InsertedOn);
            // Delete the message
            queueClient.DeleteMessage(message.MessageId, message.PopReceipt);
        }

        if (queueMessages.Count() > 0)
        {
            Console.WriteLine("Messages are delete");
        }
        else
        {
            Console.WriteLine( "The queue is empty");
        }
        
    }
}

void PeekMessages()
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    int maxMessages = 10;

    if (queueClient.Exists())
    {
      PeekedMessage[] peekMessages =  queueClient.PeekMessagesAsync(maxMessages).Result;

        foreach (var message in peekMessages)
        {
            Console.WriteLine("{0}, Inserted on: {1}", message.Body, message.InsertedOn);
           
        }
    }
}
void SendMessage(string msg)
{
    QueueClient queueClient = new QueueClient(connectionString, queueName);

    if (queueClient.Exists())
    {
        queueClient.SendMessageAsync(msg).Wait();

        Console.WriteLine("Message sent successfully!");
    }
    else
    {
        queueClient.CreateAsync().Wait();
        Console.WriteLine("Queue created!");

        queueClient.SendMessageAsync(msg).Wait();

        Console.WriteLine("Message sent successfully!");
    }

}