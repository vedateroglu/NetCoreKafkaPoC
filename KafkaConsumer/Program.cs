using Confluent.Kafka;
using KafkaConsumer.Listeners;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context,services) =>
    {
        services.AddSingleton(s => new ConsumerConfig
        {
            BootstrapServers = context.Configuration["Kafka:Brokers"],
            GroupId = context.Configuration["Kafka:GroupId"],
            AutoOffsetReset = AutoOffsetReset.Latest
        });
        services.AddHostedService<KafkaMessageListener>();
    })
    .Build();
await host.RunAsync();