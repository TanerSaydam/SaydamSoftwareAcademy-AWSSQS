using Amazon.SQS.Model;

namespace AWSSQS.WebAPI.Publisher.Messaging;

public interface ISqsMessenger
{
    Task<SendMessageResponse> SendMessageAsync<T>(T message, CancellationToken cancellationToken = default);
}
