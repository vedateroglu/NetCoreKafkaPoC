using System.Text.Json;

namespace KafkaConsumer.Models
{
    public class KafkaMessage
    {
        public string Id { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}