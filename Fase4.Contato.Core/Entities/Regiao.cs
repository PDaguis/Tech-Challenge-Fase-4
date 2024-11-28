namespace Fase4.Contato.Core.Entities;

public class Regiao : EntityBase
{
    public Regiao(string nome, string ddd)
    {
        Nome = nome;
        DDD = ddd;
        
        ValidateEntity();
    }

    public string Nome { get; set; }
    public string DDD { get; set; }
    
    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome cannot be null or empty");
        AssertionConcern.AssertArgumentNotEmpty(DDD, "DDD cannot be null or empty");
        AssertionConcern.AssertDDDIsValid(DDD, "DDD incorrect");
    }
}