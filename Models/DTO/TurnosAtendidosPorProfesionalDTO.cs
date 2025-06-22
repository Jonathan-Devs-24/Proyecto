namespace SaludTotalAPI.Models.DTO
{
    public class TurnosAtendidosPorProfesionalDTO
    {
        public string NombreProfesional { get; set; }
        public DateOnly FechaInicio { get; set; }
        public DateOnly FechaFin { get; set; }
        public int CantidadTurnosAtendidos { get; set; }
    }

}
