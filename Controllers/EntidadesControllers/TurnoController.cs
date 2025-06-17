using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TurnoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TurnoController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Turno>>> GetTurnos()
        {
            return await _context.Turnos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Turno>> GetTurno(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
                return NotFound();
            return turno;
        }

        [HttpPost]
        public async Task<ActionResult<Turno>> CrearTurno(Turno turno)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Obtener el día de la semana para la fecha del turno (1=Lunes,...7=Domingo)
            int diaSemanaTurno = ((int)turno.Fecha_Turno.DayOfWeek == 0) ? 7 : (int)turno.Fecha_Turno.DayOfWeek;

            // Obtener la disponibilidad para ese profesional y día de la semana
            var disponibilidad = await _context.Disponibilidades.FirstOrDefaultAsync(d =>
                d.Id_Profesional == turno.Id_Profesional &&
                d.Dia_Semana == diaSemanaTurno);

            if (disponibilidad == null)
            {
                return BadRequest(new { mensaje = "El profesional no tiene disponibilidad para ese día." });
            }

            // Contar cuantos turnos hay ya reservados para ese profesional y fecha, excluyendo cancelados
            int turnosReservados = await _context.Turnos.CountAsync(t =>
                t.Id_Profesional == turno.Id_Profesional &&
                t.Fecha_Turno == turno.Fecha_Turno &&
                t.Estado_Turno != "Cancelado");

            if (turnosReservados >= disponibilidad.Max_Turnos)
            {
                return Conflict(new { mensaje = "Ya se alcanzó el límite de turnos para ese día." });
            }

            _context.Turnos.Add(turno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTurno), new { id = turno.Id_Turno }, turno);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarTurno(int id, Turno turno)
        {
            if (id != turno.Id_Turno)
                return BadRequest();

            _context.Entry(turno).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarTurno(int id)
        {
            var turno = await _context.Turnos.FindAsync(id);
            if (turno == null)
                return NotFound();

            _context.Turnos.Remove(turno);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        // Método para contar turnos por profesional y fecha
        [HttpGet("contador")]
        public async Task<ActionResult<int>> ContarTurnosPorDia(int idProfesional, DateTime fecha)
        {
            var cantidad = await _context.Turnos
                .Where(t => t.Id_Profesional == idProfesional
                         && t.Fecha_Turno == fecha
                         && t.Estado_Turno != "Cancelado") // solo cuenta los turnos activos
                .CountAsync();

            return Ok(cantidad);
        }
    }
}

