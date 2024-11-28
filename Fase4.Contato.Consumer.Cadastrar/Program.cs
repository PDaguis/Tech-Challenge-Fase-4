using Fase4.Contato.Consumer.Cadastrar;
using Fase4.Contato.Core.Interfaces;
using Fase4.Contato.Infra.Repositories;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();