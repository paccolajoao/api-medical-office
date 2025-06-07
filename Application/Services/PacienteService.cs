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

    public PacienteService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<PacienteResponseDto>> ListarAsync()
    {
        return await _context.Pacientes
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
            })
            .ToListAsync();
    }

    public async Task<PacienteResponseDto?> ObterPorIdAsync(int id)
    {
        var p = await _context.Pacientes.FindAsync(id);
        if (p == null) return null;

        return new PacienteResponseDto
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
    }

    public async Task<PacienteResponseDto> CriarAsync(PacienteCreateDto dto)
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

        return new PacienteResponseDto
        {
            Id = novo.Id,
            Nome = novo.Nome,
            NomePai = novo.NomePai,
            NomeMae = novo.NomeMae,
            DataNascimento = novo.DataNascimento,
            Celular = novo.Celular,
            Email = novo.Email,
            DataCriacao = novo.DataCriacao
        };
    }

    public async Task<PacienteResponseDto?> AtualizarAsync(int id, PacienteUpdateDto dto)
    {
        var paciente = await _context.Pacientes.FindAsync(id);
        if (paciente == null) return null;

        paciente.Nome = dto.Nome;
        paciente.NomePai = dto.NomePai;
        paciente.NomeMae = dto.NomeMae;
        paciente.DataNascimento = dto.DataNascimento;
        paciente.Celular = dto.Celular;
        paciente.Email = dto.Email;

        var brTz = TZConvert.GetTimeZoneInfo("America/Sao_Paulo");
        paciente.DataAtualizacao = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, brTz);

        await _context.SaveChangesAsync();

        return new PacienteResponseDto
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
