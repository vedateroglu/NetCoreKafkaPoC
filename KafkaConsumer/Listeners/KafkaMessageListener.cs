using Confluent.Kafka;
using KafkaConsumer.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace KafkaConsumer.Listeners;

public class KafkaMessageListener : BackgroundService
{
    private readonly ILogger<KafkaMessageListener> _logger;

    private readonly ConsumerConfig _consumerConfig;
    private String _topicName;

    public KafkaMessageListener(ConsumerConfig consumerConfig, ILogger<KafkaMessageListener> logger, IConfiguration configuration)
    {
        _consumerConfig = consumerConfig;
        _logger = logger;
        _topicName = configuration["Kafka:Topic"];
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await Task.Delay(1, stoppingToken);
        _logger.LogInformation($"{nameof(KafkaMessageListener)} Started!");


        using (var consumer = new ConsumerBuilder<string, string>(_consumerConfig)
                   .SetErrorHandler((_, e) => _logger.LogError($"Error: {e.Reason}"))
                   .SetStatisticsHandler((_, json) => _logger.LogInformation($"Statistics: {json}"))
                   .SetPartitionsAssignedHandler((c, partitions) =>
                   {
                       _logger.LogInformation($"Assigned partitions: [{string.Join(", ", partitions)}]");
                   })
                   .SetPartitionsRevokedHandler((c, partitions) =>
                   {
                       _logger.LogInformation($"Revoking assignment: [{string.Join(", ", partitions)}]");
                   })
                   .Build())
        {
            consumer.Subscribe(new[] {_topicName});

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    ConsumeResult<string, string> consumeResult = consumer.Consume(stoppingToken);
                    try
                    {
                        _logger.LogInformation(
                            $"Offset: {consumeResult.Offset.Value} - Key: {consumeResult.Message.Key} - Message: {consumeResult.Message.Value}");
                        // var message =
                        //     JsonConvert.DeserializeObject<KafkaMessage>(consumeResult.Message.Value);
                    }
                    catch (Exception exception)
                    {
                        _logger.LogError($"Error: {exception} - Message: {consumeResult?.Message?.Value}");
                    }
                }
                catch (Exception exception)
                {
                    _logger.LogError(exception.ToString());
                }
            }

            consumer.Close();
        }
    }
}