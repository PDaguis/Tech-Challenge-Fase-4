namespace Fase4.Contato.Core.Interfaces;

public interface IContatoRepository
{
    Task<Entities.Contato> GetByIdAsync(Guid id);
    Task<IEnumerable<Entities.Contato>> GetAllAsync();
    Task AddAsync(Entities.Contato contato);
    Task UpdateAsync(Entities.Contato contato);
    Task DeleteAsync(Guid id);
}