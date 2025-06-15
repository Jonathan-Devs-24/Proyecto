using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table("disponibilidad")]
    public class Disponibilidad
    {
        [Key]
        [Column("id_disponibilidad")]
        public int Id_Disponibilidad { get; set; }

        [Required]
        [ForeignKey("Profesional")]
        [Column("id_profesional")]
        public int Id_Profesional { get; set; }

        [Required]
        [Column ("fecha_hora_inicio")]
        public DateTime Fecha_Hora_Inicio { get; set; }

        [Required]
        [Column ("fecha_hora_fin")]
        public DateTime Fecha_Hora_Fin { get; set;}

        [Required]
        [Column ("duracion_turno_minutos")]
        public int Duracion_Turno_Minutos { get; set; }

        [Required]
        [Column ("cantidad_maxima_turno")]
        public int Cantidad_Maximo_Turno { get; set; }

        [Required]
        [Column("estado_disponibilidad")]
        public string Estado_Disponibilidad { get; set; } = "Disponible";


        public Profesional? Profesional { get; set; }
    }
}
