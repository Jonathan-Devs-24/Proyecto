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
    public class InformeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public InformeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Obtiene todos los informes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Informe>>> GetInformes()
        {
            return await _context.Informes.ToListAsync();
        }

        // Obtiene un informe por su ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Informe>> GetInforme(int id)
        {
            var informe = await _context.Informes.FindAsync(id);
            if (informe == null)
                return NotFound();

            return informe;
        }

        // Crea un nuevo informe
        [HttpPost]
        public async Task<ActionResult<Informe>> CreateInforme(Informe informe)
        {
            _context.Informes.Add(informe);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetInforme), new { id = informe.Id_Informe }, informe);
        }

        // Actualiza un informe existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInforme(int id, Informe informe)
        {
            if (id != informe.Id_Informe)
                return BadRequest();

            _context.Entry(informe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InformeExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // Elimina un informe por su ID
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInforme(int id)
        {
            var informe = await _context.Informes.FindAsync(id);
            if (informe == null)
                return NotFound();

            _context.Informes.Remove(informe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Método auxiliar para verificar si un informe existe
        private bool InformeExists(int id)
        {
            return _context.Informes.Any(e => e.Id_Informe == id);
        }
    }
}
