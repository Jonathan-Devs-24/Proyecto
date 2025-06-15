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

        [HttpPost]
        public async Task<ActionResult<Models.Entidades.Disponibilidad>> CreateDisponibilidad(Models.Entidades.Disponibilidad disponibilidad)
        {
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
