using Confluent.Kafka;
using KafkaProducer.Services;
using System.ComponentModel.DataAnnotations;
// namespace KafkaProducer
// {
//     public class Program
//     {
//         public static void Main(string[] args)
//         {
//             var builder = WebApplication.CreateBuilder(args);
//             builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();
//             builder.Services.AddControllers();
//             builder.Services.AddEndpointsApiExplorer();
//             builder.Services.AddSwaggerGen();
//             builder.Services.AddSingleton(new ProducerBuilder<string, string>(new ProducerConfig
//                 {BootstrapServers = builder.Configuration["KafkaBrokers"]}).Build());
//             
//             var app = builder.Build();
//             app.UseSwagger();
//             app.UseSwaggerUI();
//             app.UseHttpsRedirection();
//             app.UseAuthorization();
//             app.MapControllers();
//             app.Run();
//         }
//     }
// }


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<IKafkaProducerService, KafkaProducerService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(new ProducerBuilder<string, string>(new ProducerConfig
    {BootstrapServers = builder.Configuration["KafkaBrokers"]}).Build());

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();