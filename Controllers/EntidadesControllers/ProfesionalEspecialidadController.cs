using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesionalEspecialidadController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProfesionalEspecialidadController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todas las relaciones entre profesionales y especialidades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesional_Especialidad>>> GetProfesionalEspecialidades()
        {
            return await _context.Profesional_Especialidades.ToListAsync();
        }

        // Obtiene una relación específica entre un profesional y una especialidad
        [HttpGet("{profesionalId}/{especialidadId}")]
        public async Task<ActionResult<Profesional_Especialidad>> GetProfesionalEspecialidad(int profesionalId, int especialidadId)
        {
            var profesionalEspecialidad = await _context.Profesional_Especialidades
                .FindAsync(profesionalId, especialidadId);

            if (profesionalEspecialidad == null)
                return NotFound();

            return profesionalEspecialidad;
        }

        // Asigna una especialidad a un profesional
        [HttpPost]
        public async Task<ActionResult<Profesional_Especialidad>> CreateProfesionalEspecialidad(Profesional_Especialidad profesionalEspecialidad)
        {
            _context.Profesional_Especialidades.Add(profesionalEspecialidad);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProfesionalEspecialidad), new
            {
                profesionalId = profesionalEspecialidad.Id_Profesional,
                especialidadId = profesionalEspecialidad.Id_Especialidad
            }, profesionalEspecialidad);
        }

        // Elimina una relación entre profesional y especialidad
        [HttpDelete("{profesionalId}/{especialidadId}")]
        public async Task<IActionResult> DeleteProfesionalEspecialidad(int profesionalId, int especialidadId)
        {
            var profesionalEspecialidad = await _context.Profesional_Especialidades
                .FindAsync(profesionalId, especialidadId);

            if (profesionalEspecialidad == null)
                return NotFound();

            _context.Profesional_Especialidades.Remove(profesionalEspecialidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
