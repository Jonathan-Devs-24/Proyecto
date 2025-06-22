using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SaludTotalAPI.Data;
using SaludTotalAPI.Models.DTO;

namespace SaludTotalAPI.Controllers.DTOControllers
{
    [Route ("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReportesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("turnos-atendidos")]
        public async Task<ActionResult<List<TurnosAtendidosPorProfesionalDTO>>> GetTurnosAtendidos(
        [FromQuery] string fechaInicio,
        [FromQuery] string fechaFin)
        {
            if (!DateOnly.TryParse(fechaInicio, out var desde) ||
                !DateOnly.TryParse(fechaFin, out var hasta))
            {
                return BadRequest("Fechas inválidas");
            }

            // El resto del código sigue igual
            var resultado = await _context.Turnos
                .Where(t => t.Fecha_Turno >= desde.ToDateTime(TimeOnly.MinValue)
                         && t.Fecha_Turno <= hasta.ToDateTime(TimeOnly.MaxValue)
                         && t.Estado_Turno == "Completo")
                .GroupBy(t => new { t.Profesional.Nombre_Profesional, t.Profesional.Apellido_Profesional })
                .Select(g => new TurnosAtendidosPorProfesionalDTO
                {
                    NombreProfesional = $"{g.Key.Nombre_Profesional} {g.Key.Apellido_Profesional}",
                    FechaInicio = desde,
                    FechaFin = hasta,
                    CantidadTurnosAtendidos = g.Count()
                })
                .ToListAsync();

            return Ok(resultado);
        }



        [HttpGet("turnos-cancelados-reprogramados")]
        public async Task<ActionResult<List<TurnosCanceladosYReprogramadosDTO>>> GetTurnosCanceladosYReprogramados(
            [FromQuery] string fechaInicio,
            [FromQuery] string fechaFin)
        {
            if (!DateOnly.TryParse(fechaInicio, out var desde) || !DateOnly.TryParse(fechaFin, out var hasta))
                return BadRequest("Fechas inválidas");

            var datos = await _context.Turnos
                .Where(t => t.Fecha_Turno >= desde.ToDateTime(TimeOnly.MinValue)
                         && t.Fecha_Turno <= hasta.ToDateTime(TimeOnly.MaxValue)
                         && (t.Estado_Turno == "Cancelado" || t.Estado_Turno == "Reservado (Reprogramado)"))
                .GroupBy(t => new { t.Profesional.Nombre_Profesional, t.Profesional.Apellido_Profesional })
                .Select(g => new TurnosCanceladosYReprogramadosDTO
                {
                    NombreProfesional = $"{g.Key.Nombre_Profesional} {g.Key.Apellido_Profesional}",
                    FechaInicio = desde.ToString("yyyy-MM-dd"),
                    FechaFin = hasta.ToString("yyyy-MM-dd"),
                    CantidadCancelados = g.Count(t => t.Estado_Turno == "Cancelado"),
                    CantidadReprogramados = g.Count(t => t.Estado_Turno == "Reservado (Reprogramado)")
                })
                .ToListAsync();

            return Ok(datos);
        }

    }
}
