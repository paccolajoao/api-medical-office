using ApiMedicalOffice.Application.Dtos;

namespace ApiMedicalOffice.Application.Interfaces;

public interface IPacienteService
{
    Task<PaginatedResponse<PacienteResponseDto>> ListarAsync(int pageNumber, int pageSize);
    Task<PaginatedResponse<PacienteResponseDto>?> ObterPorIdAsync(int id);
    Task<PaginatedResponse<PacienteResponseDto>> CriarAsync(PacienteCreateDto dto);
    Task<PaginatedResponse<PacienteResponseDto>?> AtualizarAsync(int id, PacienteUpdateDto dto);
    Task<bool> RemoverAsync(int id);
}