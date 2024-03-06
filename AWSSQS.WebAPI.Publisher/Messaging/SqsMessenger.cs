using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;

namespace AWSSQS.WebAPI.Publisher.Messaging;

public sealed class SqsMessenger(
    IAmazonSQS sqs) : ISqsMessenger
{
    public async Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        var queueUrlResponse = await sqs.GetQueueUrlAsync("customers",cancellationToken);

        var sendMessageRequest = new SendMessageRequest
        {
            QueueUrl = queueUrlResponse.QueueUrl,
            MessageBody = JsonSerializer.Serialize(message),
            MessageAttributes = new Dictionary<string, MessageAttributeValue>
            {
                {
                    "MessageType", new MessageAttributeValue
                    {
                        DataType = "String",
                        StringValue = typeof(T).Name
                    }
                }
            }
        };

        return await sqs.SendMessageAsync(sendMessageRequest, cancellationToken);   
    }
}
