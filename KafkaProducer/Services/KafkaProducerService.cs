using System.Threading.Tasks;
using Confluent.Kafka;
using KafkaProducer.Models;
using Microsoft.Extensions.Logging;

namespace KafkaProducer.Services;

public class KafkaProducerService : IKafkaProducerService
{
    private readonly ILogger<KafkaProducerService> _logger;
    private readonly IProducer<string, string> _producer;

    public KafkaProducerService(ILogger<KafkaProducerService> logger, IProducer<string, string> producer)
    {
        _logger = logger;
        _producer = producer;
    }

    public void SendMessage(KafkaMessage message)
    {
        Task.Run(() =>
        {
            _producer.ProduceAsync("mytopic", new Message<string, string>
            {
                Key = message.Id ?? "",
                Value = message.ToString()
            });
        });
    }
}