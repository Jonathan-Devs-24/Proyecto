using Microsoft.AspNetCore.Mvc;

namespace SaludTotalAPI.Models.View
{
    [ViewComponent(Name = "TurnoOnlineRecibido")]
    public class TurnoOnlineRecibido
    {
        public int id_TurnoOnline { get; set; }
        public string nombre { get; set; } = "";
        public string apellido { get; set; } = "";
        public int dni { get; set; }
        public string correo { get; set; } = "";
        public string especialidad { get; set; } = "";
        public string estado { get; set; } = "";
    }

}
