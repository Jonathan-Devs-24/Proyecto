using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table("Turno")]
    public class Turno
    {
        [Key]
        [Column("id_turno")]
        public int Id_Turno { get; set; }

        [Required]
        [ForeignKey("Paciente")]
        [Column("id_paciente")]
        public int Id_Paciente { get; set; }

        [Required]
        [ForeignKey("Profesional")]
        [Column("id_profesional")]
        public int Id_Profesional { get; set; }

        [Required]
        [Column("estado_turno")]
        public string Estado_Turno { get; set; }

        public Paciente? Paciente { get; set; }
        public Profesional? Profesional { get; set; }
    }
}
