namespace Fase4.Contato.Core.DTO;

public class ContatoPost
{
    public required string Nome { get; set; }
    public required string Telefone { get; set; }
    public required string Email { get; set; }
    public int RegiaoId { get; set; }
}