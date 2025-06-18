using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace SaludTotalAPI.Models.View
{
    [ViewComponent(Name = "VistaTurnoDetallado")]
    public class VistaTurnoDetallado
    {
        
        public int IdTurno { get; set; }
        public string NombrePaciente { get; set; }
        public string NombreProfesional { get; set; }
        public string Especialidad { get; set; }
        public string Estado { get; set; }
        public DateOnly FechaTurno { get; set; }

    }
}
