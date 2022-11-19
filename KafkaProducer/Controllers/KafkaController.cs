using KafkaProducer.Models;
using KafkaProducer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KafkaProducer.Controllers;

[ApiController]
[Route("kafka")]
public class KafkaController : ControllerBase
{
    private readonly ILogger<KafkaController> _logger;

    private readonly IKafkaProducerService _kafkaProducerService;

    public KafkaController(ILogger<KafkaController> logger, IKafkaProducerService kafkaProducerService)
    {
        _logger = logger;
        _kafkaProducerService = kafkaProducerService;
    }

    [HttpPost("sendMessage")]
    [Consumes("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult SendMessage(KafkaMessage message)
    {
         
        _kafkaProducerService.SendMessage(message);
        return Ok();
    }
}