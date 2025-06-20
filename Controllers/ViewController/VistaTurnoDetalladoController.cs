using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.View;

[ApiController]
[Route("api/[controller]")]
public class VistaTurnoDetalladoController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public VistaTurnoDetalladoController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/VistaTurnoDetallado
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VistaTurnoDetallado>>> Get()
    {
        var turnos = await _context.VistaTurnosDetallada.ToListAsync();
        return Ok(turnos);
    }

    // GET: api/VistaTurnoDetallado/paciente/{idPaciente}
    [HttpGet("paciente/{idPaciente}")]
    public async Task<ActionResult<IEnumerable<VistaTurnoDetallado>>> GetPorPaciente(int idPaciente)
    {
        var turnos = await _context.VistaTurnosDetallada
            .Where(t => t.IdTurno > 0 && t.NombrePaciente != null) // opcional: protección
            .Where(t => _context.Pacientes
                .Any(p => p.Id == idPaciente && (p.Nombre + " " + p.Apellido) == t.NombrePaciente))
            .ToListAsync();

        return Ok(turnos);
    }

}
