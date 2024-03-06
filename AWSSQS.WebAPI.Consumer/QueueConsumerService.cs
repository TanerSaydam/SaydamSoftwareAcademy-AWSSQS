
using Amazon.SQS;
using Amazon.SQS.Model;

namespace AWSSQS.WebAPI.Consumer;

public class QueueConsumerService(
    IAmazonSQS sqs) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var queueUrlResponse = await sqs.GetQueueUrlAsync("customers", stoppingToken);

        var receiveMessageRequest = new ReceiveMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
        };

        while(!stoppingToken.IsCancellationRequested)
        {
            var response = await sqs.ReceiveMessageAsync(receiveMessageRequest);

            foreach (var message in response.Messages)
            {
                Console.WriteLine($"Message Body: {message.Body}");

                await sqs.DeleteMessageAsync(queueUrlResponse.QueueUrl, message.ReceiptHandle);
            }

            await Task.Delay(100, stoppingToken);            
        }

    }
}
