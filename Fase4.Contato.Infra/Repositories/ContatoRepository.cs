using Fase4.Contato.Core.Interfaces;
using Fase4.Contato.Shared.Configs;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Fase4.Contato.Infra.Repositories;

public class ContatoRepository : IContatoRepository
{
    private readonly IMongoCollection<Core.Entities.Contato> _contatoCollection;

    public ContatoRepository(IMongoClient database, IOptions<MongoConfiguration> options)
    {
        _contatoCollection = database.GetDatabase(options.Value.DatabaseName)
            .GetCollection<Core.Entities.Contato>("contatos");
    }
    
    public async Task<Core.Entities.Contato> GetByIdAsync(Guid id)
    {
        try
        {
            return await _contatoCollection
                .Find(Builders<Core.Entities.Contato>.Filter.Eq(c => c.Id, id))
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<Core.Entities.Contato>> GetAllAsync()
    {
        try
        {
            return await _contatoCollection.Find(_ => true)
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task AddAsync(Core.Entities.Contato contato)
    {
        try
        {
            await _contatoCollection.InsertOneAsync(contato);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task UpdateAsync(Core.Entities.Contato contato)
    {
        try
        {
            await _contatoCollection
                .ReplaceOneAsync(filter: g => g.Id == contato.Id, replacement: contato);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task DeleteAsync(Guid id)
    {
        try
        {
            await _contatoCollection.DeleteOneAsync(Builders<Core.Entities.Contato>.Filter.Eq(c => c.Id, id));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}