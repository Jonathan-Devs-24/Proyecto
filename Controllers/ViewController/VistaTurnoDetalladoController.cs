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
}
