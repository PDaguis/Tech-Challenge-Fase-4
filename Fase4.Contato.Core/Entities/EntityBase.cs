namespace Fase4.Contato.Core.Entities;

public abstract class EntityBase
{
    public Guid Id { get; }
    public DateTime CadastradoEm { get; }
    
    public static DateTime CurrentDataTime => DateTime.Now;

    public EntityBase()
    {
        Id = Guid.NewGuid();
        CadastradoEm = CurrentDataTime;
    }
}