namespace Fase4.Contato.Core.Entities;

public class Contato : EntityBase
{
    public Contato(string nome, string telefone, string email)
    {
        Nome = nome;
        Telefone = telefone;
        Email = email;
        
        ValidateEntity();
    }

    public Contato()
    {
            
    }

    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Email { get; set; }
    public int RegiaoId { get; set; }
    //public virtual Regiao Regiao { get; set; }
    
    public void ValidateEntity()
    {
        AssertionConcern.AssertArgumentNotEmpty(Nome, "Nome cannot be null or empty");
        AssertionConcern.AssertArgumentNotEmpty(Telefone, "Telefone cannot be null or empty");
        AssertionConcern.AssertTelefoneIsValid(Telefone, "Telefone is invalid");
        AssertionConcern.AssertArgumentNotEmpty(Email, "Email cannot be null or empty");
        AssertionConcern.AssertEmailIsValid(Email, "Email is invalid");
    }
}