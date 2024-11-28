using Fase4.Contato.Shared.Configs;
using MongoDB.Driver;

namespace Fase4.Contato.API.Extensions;

public static class DatabaseExtensions
{
    public static void AddDatabaseExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MongoConfiguration>(configuration.GetSection("MongoConfiguration"));
        
        var mongoConnection = configuration.GetSection("MongoConfiguration:ConnectionString").Value;
        services.AddScoped<IMongoClient>(sp => new MongoClient(mongoConnection));
    }
}