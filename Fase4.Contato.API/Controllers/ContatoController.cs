using System.Text;
using System.Text.Json;
using Fase4.Contato.Core.DTO;
using Microsoft.AspNetCore.Mvc;
using RabbitMQ.Client;

namespace Fase4.Contato.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {
        private ILogger<ContatoController> _logger;

        public ContatoController(ILogger<ContatoController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ContatoPost contato)
        {
            try
            {
                _logger.LogInformation("Contato recebido!");
                var factory = new ConnectionFactory()
                {
                    HostName = "rabbitmq",
                    UserName = "pdaguis",
                    Password = "pdaguis",
                    Port = 5672
                };
                using var connection = await factory.CreateConnectionAsync();
                using var channel = await connection.CreateChannelAsync();

                await channel.QueueDeclareAsync(
                        queue: "mq-contato-cadastrar",
                        durable: false,
                        exclusive: false,
                        autoDelete: true,
                        arguments: null);

                var message = JsonSerializer.Serialize(contato);
                var body = Encoding.UTF8.GetBytes(message);

                var props = new BasicProperties
                {
                    Persistent = true
                };

                await channel.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: "mq-contato-cadastrar",
                    body: body,
                    basicProperties: props,
                    mandatory: true);

                _logger.LogInformation("Contato enviado para a fila!");

                return Ok("Enviado para a fila!");
            }
            catch (Exception e)
            {
                _logger.LogError($"ERRO: {e.Message}");
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
