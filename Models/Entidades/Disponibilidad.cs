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
        [Column("max_turnos")]
        public int Max_Turnos { get; set; }

        [Required]
        [Column("dia_semana")]
        public int Dia_Semana { get; set; }

        public Profesional? Profesional { get; set; }
    }
}
