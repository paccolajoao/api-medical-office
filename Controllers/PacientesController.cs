using ApiMedicalOffice.Application.Dtos;
using ApiMedicalOffice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly IPacienteService _service;
    public PacientesController(IPacienteService service) => _service = service;

    [HttpGet]
    public async Task<ActionResult<PaginatedResponse<PacienteResponseDto>>> Listar(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        var paginado = await _service.ListarAsync(pageNumber, pageSize);
        return Ok(paginado);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaginatedResponse<PacienteResponseDto>>> ObterPorId(int id)
    {
        var paged = await _service.ObterPorIdAsync(id);
        if (paged == null || !paged.Data.Any())
            return NotFound();
        return Ok(paged);
    }

    [HttpPost]
    public async Task<ActionResult<PaginatedResponse<PacienteResponseDto>>> Criar(PacienteCreateDto dto)
    {
        var paged = await _service.CriarAsync(dto);

        // Extrai o Id do primeiro item em Data
        var newId = paged.Data.First().Id;

        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = newId },
            paged
        );
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PaginatedResponse<PacienteResponseDto>>> Atualizar(int id, PacienteUpdateDto dto)
    {
        var paged = await _service.AtualizarAsync(id, dto);
        if (paged == null || !paged.Data.Any())
            return NotFound();

        var updatedId = paged.Data.First().Id;
        return CreatedAtAction(
            nameof(ObterPorId),
            new { id = updatedId },
            paged
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(int id)
    {
        var ok = await _service.RemoverAsync(id);
        return ok ? NoContent() : NotFound();
    }
}