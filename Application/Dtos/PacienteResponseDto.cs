namespace ApiMedicalOffice.Application.Dtos;

public class PacienteResponseDto
{
    public long Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? NomePai { get; set; }
    public string? NomeMae { get; set; }
    public DateOnly DataNascimento { get; set; }
    public string Celular { get; set; } = string.Empty;
    public string? Email { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
}
