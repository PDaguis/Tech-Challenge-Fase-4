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
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] ContatoPost contato)
        {
            try
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
                
                await channel.QueueDeclareAsync(
                        queue: "mq-contato-cadastrar",
                        durable: false,
                        exclusive: false,
                        autoDelete: true,
                        arguments: null);
                    
                    var message = JsonSerializer.Serialize(contato);
                    var body = Encoding.UTF8.GetBytes(message);
                    
                    await channel.BasicPublishAsync(
                        exchange: string.Empty, 
                        routingKey: "",
                        body: body);
                
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest(e.Message);
            }
        }
    }
}
