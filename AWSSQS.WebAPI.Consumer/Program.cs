using Amazon.SQS;
using AWSSQS.WebAPI.Consumer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IAmazonSQS, AmazonSQSClient>();

builder.Services.AddHostedService<QueueConsumerService>();

var app = builder.Build();

app.UseHttpsRedirection();

app.Run();
