using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.View;

namespace SaludTotalAPI.Controllers.ViewController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoOnlineRecibidoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public TurnoOnlineRecibidoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("recibidos")]
        public async Task<ActionResult<IEnumerable<TurnoOnlineRecibido>>> GetTurnosOnlineRecibidos()
        {
            var turnos = await _context.Set<TurnoOnlineRecibido>().ToListAsync();

            // Ordenar manualmente por estado: En Proceso → Aceptado → Cancelado
            var ordenados = turnos.OrderBy(t => t.estado == "En Proceso" ? 0 : t.estado == "Aceptado" ? 1 : 2)
                                  .ToList();
            return ordenados;
        }

    }
}
