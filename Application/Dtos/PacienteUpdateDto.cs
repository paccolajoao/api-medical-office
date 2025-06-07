using System.ComponentModel.DataAnnotations;

namespace ApiMedicalOffice.Application.Dtos;

public class PacienteUpdateDto
{
    [Required]
    [MaxLength(100)]
    public string Nome { get; set; } = string.Empty;

    [MaxLength(100)]
    public string? NomePai { get; set; }

    [MaxLength(100)]
    public string? NomeMae { get; set; }

    [Required]
    public DateOnly DataNascimento { get; set; }

    [Required]
    [MaxLength(20)]
    public string Celular { get; set; } = string.Empty;

    [MaxLength(120)]
    public string? Email { get; set; }
}