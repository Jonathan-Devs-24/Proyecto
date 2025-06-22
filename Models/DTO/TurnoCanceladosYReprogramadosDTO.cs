namespace SaludTotalAPI.Models.DTO
{
    public class TurnosCanceladosYReprogramadosDTO
    {
        public string NombreProfesional { get; set; }
        public string FechaInicio { get; set; }
        public string FechaFin { get; set; }
        public int CantidadCancelados { get; set; }
        public int CantidadReprogramados { get; set; }
    }

}
