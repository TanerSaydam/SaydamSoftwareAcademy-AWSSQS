using Amazon;
using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using System.Text.Json;


string accessKey = string.Empty;
string secretKey = string.Empty;
var region = RegionEndpoint.EUWest3;

var sqsClient = new AmazonSQSClient();

var queueUrlResponse = await sqsClient.GetQueueUrlAsync("customers");

var customer = new Customer()
{
    Name = "Taner Saydam",
    Address = "Kayseri"
};

var sendMessageRequest = new SendMessageRequest
{
    QueueUrl = queueUrlResponse.QueueUrl,
    MessageBody = JsonSerializer.Serialize(customer),
    MessageAttributes = new Dictionary<string, MessageAttributeValue>
    {
        {
            "MessageType", new MessageAttributeValue
            {
                DataType = "String",
                StringValue = nameof(Customer)
            }
        }
    }
};

var response = await sqsClient.SendMessageAsync(sendMessageRequest);

Console.WriteLine(response.HttpStatusCode);

Console.ReadLine();


public class Customer
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
}