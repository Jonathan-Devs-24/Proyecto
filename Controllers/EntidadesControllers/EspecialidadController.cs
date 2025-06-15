using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EspecialidadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EspecialidadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Especialidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Especialidad>>> GetEspecialidades()
        {
            return await _context.Especialidades.ToListAsync();
        }

        // GET: api/Especialidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Especialidad>> GetEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
                return NotFound();

            return especialidad;
        }

        // POST: api/Especialidad
        [HttpPost]
        public async Task<ActionResult<Especialidad>> CreateEspecialidad(Especialidad especialidad)
        {
            _context.Especialidades.Add(especialidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEspecialidad), new { id = especialidad.Id_Especialidad }, especialidad);
        }

        // PUT: api/Especialidad/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEspecialidad(int id, Especialidad especialidad)
        {
            if (id != especialidad.Id_Especialidad)
                return BadRequest();

            _context.Entry(especialidad).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Especialidad/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEspecialidad(int id)
        {
            var especialidad = await _context.Especialidades.FindAsync(id);
            if (especialidad == null)
                return NotFound();

            _context.Especialidades.Remove(especialidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

