using ApiMedicalOffice.Application.Dtos;

namespace ApiMedicalOffice.Application.Interfaces;

public interface IPacienteService
{
    Task<IEnumerable<PacienteResponseDto>> ListarAsync();
    Task<PacienteResponseDto?> ObterPorIdAsync(int id);
    Task<PacienteResponseDto> CriarAsync(PacienteCreateDto dto);
    Task<PacienteResponseDto?> AtualizarAsync(int id, PacienteUpdateDto dto);
    Task<bool> RemoverAsync(int id);
}
