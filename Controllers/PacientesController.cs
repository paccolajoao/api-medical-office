using Microsoft.AspNetCore.Mvc;
using ApiMedicalOffice.Application.Dtos;
using ApiMedicalOffice.Application.Interfaces;

namespace ApiMedicalOffice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _service;

    public PacientesController(IPacienteService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteResponseDto>>> Listar()
    {
        var lista = await _service.ListarAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PacienteResponseDto>> ObterPorId(int id)
    {
        var paciente = await _service.ObterPorIdAsync(id);
        if (paciente == null) return NotFound();
        return Ok(paciente);
    }

    [HttpPost]
    public async Task<ActionResult<PacienteResponseDto>> Criar(PacienteCreateDto dto)
    {
        var criado = await _service.CriarAsync(dto);
        return CreatedAtAction(nameof(ObterPorId), new { id = criado.Id }, criado);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PacienteResponseDto>> Atualizar(int id, PacienteUpdateDto dto)
    {
        var atualizado = await _service.AtualizarAsync(id, dto);
        if (atualizado == null)
            return NotFound();

        return Ok(atualizado);
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var removido = await _service.RemoverAsync(id);
        if (!removido) return NotFound();
        return NoContent();
    }
}
