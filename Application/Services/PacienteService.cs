using ApiMedicalOffice.Application.Dtos;
using ApiMedicalOffice.Application.Interfaces;
using ApiMedicalOffice.Domain.Entities;
using ApiMedicalOffice.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TimeZoneConverter;

namespace ApiMedicalOffice.Application.Services;

public class PacienteService : IPacienteService
{
    private readonly AppDbContext _context;
    private const int DefaultPageNumber = 1;
    private const int DefaultPageSize = 10;

    public PacienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedResponse<PacienteResponseDto>> ListarAsync(
        int pageNumber = DefaultPageNumber,
        int pageSize = DefaultPageSize)
    {
        var query = _context.Pacientes
            .OrderBy(p => p.Id)
            .Select(p => new PacienteResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                NomePai = p.NomePai,
                NomeMae = p.NomeMae,
                DataNascimento = p.DataNascimento,
                Celular = p.Celular,
                Email = p.Email,
                DataCriacao = p.DataCriacao,
                DataAtualizacao = p.DataAtualizacao
            });

        var totalItems = await query.CountAsync();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<PacienteResponseDto>
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalItems = totalItems
        };
    }

    public async Task<PaginatedResponse<PacienteResponseDto>?> ObterPorIdAsync(int id)
    {
        var p = await _context.Pacientes.FindAsync(id);
        if (p == null) return null;

        var dto = new PacienteResponseDto
        {
            Id = p.Id,
            Nome = p.Nome,
            NomePai = p.NomePai,
            NomeMae = p.NomeMae,
            DataNascimento = p.DataNascimento,
            Celular = p.Celular,
            Email = p.Email,
            DataCriacao = p.DataCriacao,
            DataAtualizacao = p.DataAtualizacao
        };

        return new PaginatedResponse<PacienteResponseDto>
        {
            Data = new[] { dto },
            PageNumber = 1,
            PageSize = 1,
            TotalItems = 1
        };
    }

    public async Task<PaginatedResponse<PacienteResponseDto>> CriarAsync(PacienteCreateDto dto)
    {
        var brTz = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");
        var dataBR = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brTz);

        var novo = new Paciente
        {
            Nome = dto.Nome,
            NomePai = dto.NomePai,
            NomeMae = dto.NomeMae,
            DataNascimento = dto.DataNascimento,
            Celular = dto.Celular,
            Email = dto.Email,
            DataCriacao = dataBR
        };
        _context.Pacientes.Add(novo);
        await _context.SaveChangesAsync();

        var responseDto = new PacienteResponseDto
        {
            Id = novo.Id,
            Nome = novo.Nome,
            NomePai = novo.NomePai,
            NomeMae = novo.NomeMae,
            DataNascimento = novo.DataNascimento,
            Celular = novo.Celular,
            Email = novo.Email,
            DataCriacao = novo.DataCriacao,
            DataAtualizacao = novo.DataAtualizacao
        };

        return new PaginatedResponse<PacienteResponseDto>
        {
            Data = new[] { responseDto },
            PageNumber = DefaultPageNumber,
            PageSize = DefaultPageSize,
            TotalItems = 1
        };
    }

    public async Task<PaginatedResponse<PacienteResponseDto>?> AtualizarAsync(int id, PacienteUpdateDto dto)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return null;

        paciente.Nome = dto.Nome;
        paciente.NomePai = dto.NomePai;
        paciente.NomeMae = dto.NomeMae;
        paciente.DataNascimento = dto.DataNascimento;
        paciente.Celular = dto.Celular;
        paciente.Email = dto.Email;
        paciente.DataAtualizacao = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TZConvert.GetTimeZoneInfo("America/Sao_Paulo"));

        await _context.SaveChangesAsync();

        var updatedDto = new PacienteResponseDto
        {
            Id = paciente.Id,
            Nome = paciente.Nome,
            NomePai = paciente.NomePai,
            NomeMae = paciente.NomeMae,
            DataNascimento = paciente.DataNascimento,
            Celular = paciente.Celular,
            Email = paciente.Email,
            DataCriacao = paciente.DataCriacao,
            DataAtualizacao = paciente.DataAtualizacao
        };

        return new PaginatedResponse<PacienteResponseDto>
        {
            Data = new[] { updatedDto },
            PageNumber = DefaultPageNumber,
            PageSize = DefaultPageSize,
            TotalItems = 1
        };
    }

    public async Task<bool> RemoverAsync(int id)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return false;
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
        return true;
    }
}
