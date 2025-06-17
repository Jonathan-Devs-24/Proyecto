using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfesionalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Profesional
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesionales()
        {
            return await _context.Profesionales.ToListAsync();
        }

        // GET: api/Profesional/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesional>> GetProfesional(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
                return NotFound();

            return profesional;
        }

        // GET: api/Profesional/nombre/{nombre}
        [HttpGet("disponibilidad/{idProfesional}/{fecha}")]
        public async Task<IActionResult> VerificarDisponibilidad(int idProfesional, DateTime fecha)
        {
            // Obtener los turnos programados para ese profesional en esa fecha
            var turnosTomados = await _context.Turnos
                .Where(t => t.Id_Profesional == idProfesional && t.Fecha_Turno.Date == fecha.Date)
                .CountAsync();

            // Restricción: máximo 4 turnos por día
            bool disponible = turnosTomados < 4;

            return Ok(new { disponible });
        }

        // GET: api/Profesional/especialidad/5
        [HttpGet("especialidad/{idEspecialidad}")]
        public async Task<ActionResult<IEnumerable<Profesional>>> GetProfesionalesPorEspecialidad(int idEspecialidad)
        {
            var profesionales = await _context.Profesional_Especialidades
                .Where(pe => pe.Id_Especialidad == idEspecialidad)
                .Include(pe => pe.Profesional)
                .Select(pe => pe.Profesional)
                .Distinct()
                .ToListAsync();

            return profesionales;
        }

        // POST: api/Profesional
        [HttpPost]
        public async Task<ActionResult<Profesional>> CreateProfesional(Profesional profesional)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Profesionales.Add(profesional);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesional), new { id = profesional.Id_Profesional }, profesional);
        }

        // PUT: api/Profesional/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProfesional(int id, Profesional profesional)
        {
            if (id != profesional.Id_Profesional)
                return BadRequest();

            _context.Entry(profesional).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Profesional/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesional(int id)
        {
            var profesional = await _context.Profesionales.FindAsync(id);
            if (profesional == null)
                return NotFound();

            _context.Profesionales.Remove(profesional);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        

    }
}
