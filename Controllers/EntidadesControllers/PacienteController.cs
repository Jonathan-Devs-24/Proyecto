using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.Entidades;

namespace SaludTotalAPI.Controllers.EntidadesControllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PacienteController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PacienteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Aca se encuentra el método GetPacientes que obtiene todos los pacientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            return await _context.Pacientes.ToListAsync();
        } 

        // Aca se encuentra el método GetPaciente que obtiene un paciente por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Paciente>> GetPaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();
            return paciente;
        }

        // Aca se encuentra el método CreatePaciente que crea un nuevo paciente
        [HttpPost]
        public async Task<ActionResult<Paciente>> CreatePaciente(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPaciente), new { id = paciente.Id }, paciente);
        }


        // Aca se encuentra el método UpdatePaciente que actualiza un paciente existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest();

            _context.Entry(paciente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Aca se encuentra el método DeletePaciente que elimina un paciente por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaciente(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente == null)
                return NotFound();

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

