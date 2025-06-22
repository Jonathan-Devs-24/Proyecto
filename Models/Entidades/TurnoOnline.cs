using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table ("TurnoOnline")]
    public class TurnoOnline
    {
        [Key]
        [Column ("id_turnoONLINE")]
        public int Id_TurnoOnline { get; set; } 

        [Required]
        [Column ("nombre_pacienteOnline")]
        public string nombre_pacienteOnline { get; set; }

        [Required]
        [Column ("apellido_pacienteOnline")]
        public string apellido_pacienteOnline { get; set; }

        [Required]
        [Column ("DNI_pacienteOnline")]
        public int DNI_pacienteOnline { get; set; }

        [Required]
        [Column ("fechaNacimiento_pacienteOnline")]
        public string FechaNacimiento_pacienteOnline { get; set; }

        [Required]
        [Column ("correo_pacienteOnline")]
        public string correo_pacienteOnline { get; set; }

        [Required]
        [Column ("telefono_pacienteOnline")]
        public long telefono_pacienteOnline { get; set; }

        [Required]
        [Column ("especialidad_seleccionada")]
        public string Especialidad_Seleccionada { get; set; }

        [Required]
        [Column ("estadoTurno_Online")]
        public string Estado_TurnoOnline { get; set; }

    }
}
