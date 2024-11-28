using System.Text;
using System.Text.Json;
using Fase4.Contato.Core.DTO;
using Fase4.Contato.Core.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Fase4.Contato.Consumer.Cadastrar;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IConfiguration _configuration;

    public Worker(ILogger<Worker> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                UserName = "pdaguis",
                Password = "p12345",
                Port = 5672
            };
            using var connection = await factory.CreateConnectionAsync();
            using var channel = await connection.CreateChannelAsync();
            
            channel.QueueDeclareAsync(
                    queue: "mq-contato-cadastrar",
                    durable: false,
                    exclusive: false,
                    autoDelete: true,
                    arguments: null);

                var consumer = new AsyncEventingBasicConsumer(channel);

                consumer.ReceivedAsync += (sender, eventArgs) =>
                {
                    var body = eventArgs.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var response = JsonSerializer.Deserialize<ContatoPost>(message);

                    var contato = new Core.Entities.Contato()
                    {
                        Nome = response.Nome,
                        Email = response.Email,
                        Telefone = response.Telefone,
                        RegiaoId = response.RegiaoId
                    };
                    Console.WriteLine("Contato cadastrado com sucesso!");
                    return Task.CompletedTask;
                };

                await channel.BasicConsumeAsync(
                    queue: "mq-contato-cadastrar",
                    autoAck: true,
                    consumer: consumer);
            

            await Task.Delay(2000, stoppingToken);
        }
    }
}