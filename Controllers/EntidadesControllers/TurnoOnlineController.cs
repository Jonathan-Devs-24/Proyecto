using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoOnlineController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TurnoOnlineController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/TurnoOnline
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoOnline>>> GetTurnosOnline()
        {
            return await _context.TurnosOnline.ToListAsync();
        }

        // GET: api/TurnoOnline/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TurnoOnline>> GetTurnoOnline(int id)
        {
            var turnoOnline = await _context.TurnosOnline.FirstOrDefaultAsync(t => t.Id_TurnoOnline == id);
            if (turnoOnline == null)
                return NotFound();
            return turnoOnline;
        }

        //Verificar estado por DNI y correo
        [HttpGet("verificar")]
        public async Task<ActionResult<TurnoOnline>> VerificarTurno([FromQuery] int dni)
        {
            var turno = await _context.TurnosOnline
                .FirstOrDefaultAsync(t => t.DNI_pacienteOnline == dni);

            if (turno == null)
                return NotFound();

            return Ok(turno);
        }

        // POST: api/TurnoOnline
        [HttpPost]
        public async Task<ActionResult<TurnoOnline>> PostTurnoOnline(TurnoOnline turno)
        {
            // Estado por defecto al crearlo
            turno.Estado_TurnoOnline = "En Proceso";

            _context.TurnosOnline.Add(turno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTurnosOnline), new { id = turno.Id_TurnoOnline }, turno);
        }


        // PUT: api/TurnoOnline/{id}
        public class EstadoRequest
        {
            public string Estado { get; set; }
        }


        [HttpPut("actualizar-estado/{id}")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] EstadoRequest request)
        {
            var turno = await _context.TurnosOnline.FindAsync(id);
            if (turno == null)
                return NotFound();

            turno.Estado_TurnoOnline = request.Estado;
            await _context.SaveChangesAsync();
            return Ok(turno);
        }



    }
}
