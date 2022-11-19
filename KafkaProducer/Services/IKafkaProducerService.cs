using KafkaProducer.Models;

namespace KafkaProducer.Services;

public interface IKafkaProducerService
{
     void SendMessage(KafkaMessage message);
}