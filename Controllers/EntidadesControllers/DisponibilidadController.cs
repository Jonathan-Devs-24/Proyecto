using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisponibilidadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DisponibilidadController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Entidades.Disponibilidad>>> GetDisponibilidades()
        {
            return await _context.Disponibilidades.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Entidades.Disponibilidad>> GetDisponibilidad(int id)
        {
            var disponibilidad = await _context.Disponibilidades.FindAsync(id);
            if (disponibilidad == null)
                return NotFound();
            return disponibilidad;
        }

        [HttpGet("verificar/{idProfesional}/{fecha}")]
        public async Task<IActionResult> VerificarDisponibilidad(int idProfesional, DateTime fecha)
        {
            // Obtener el día de la semana (1 = lunes, 7 = domingo)
            int diaSemana = (int)fecha.DayOfWeek;
            if (diaSemana == 0) diaSemana = 7;

            // Buscar la disponibilidad para ese profesional y ese día de la semana
            var disponibilidad = await _context.Disponibilidades
                .FirstOrDefaultAsync(d => d.Id_Profesional == idProfesional && d.Dia_Semana == diaSemana);

            if (disponibilidad == null)
            {
                return NotFound(new { mensaje = "El profesional no tiene disponibilidad para ese día." });
            }

            // Contar la cantidad de turnos ya reservados para ese profesional y esa fecha (excepto cancelados)
            int turnosReservados = await _context.Turnos
                .CountAsync(t => t.Id_Profesional == idProfesional &&
                                 t.Fecha_Turno.Date == fecha.Date &&
                                 t.Estado_Turno != "Cancelado");

            // Verificar si quedan cupos disponibles
            int cuposDisponibles = disponibilidad.Max_Turnos - turnosReservados;

            return Ok(new
            {
                fecha = fecha.Date.ToString("yyyy-MM-dd"),
                profesional = idProfesional,
                cupos_disponibles = cuposDisponibles,
                max_turnos = disponibilidad.Max_Turnos,
                disponible = cuposDisponibles > 0
            });
        }





        [HttpPost]
        public async Task<ActionResult<Models.Entidades.Disponibilidad>> CreateDisponibilidad(Models.Entidades.Disponibilidad disponibilidad)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Disponibilidades.Add(disponibilidad);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDisponibilidad), new { id = disponibilidad.Id_Disponibilidad }, disponibilidad);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDisponibilidad(int id, Models.Entidades.Disponibilidad disponibilidad)
        {
            if (id != disponibilidad.Id_Disponibilidad)
                return BadRequest();
            _context.Entry(disponibilidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisponibilidad(int id)
        {
            var disponibilidad = await _context.Disponibilidades.FindAsync(id);
            if (disponibilidad == null)
                return NotFound();
            _context.Disponibilidades.Remove(disponibilidad);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
