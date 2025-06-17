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
        [ForeignKey("Especialidad")]
        [Column("id_especialidad")]
        public int Id_Especialidad { get; set; }

        [Required]
        [ForeignKey("Profesional")]
        [Column("id_profesional")]
        public int Id_Profesional { get; set; }

        [Required]
        [Column ("fecha_turno")]
        public DateTime Fecha_Turno { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Required]
        [Column("estado_turno")]
        public string Estado_Turno { get; set; }

        public Paciente? Paciente { get; set; }
        public Especialidad? Especialidad { get; set; }
        public Profesional? Profesional { get; set; }
    
    }
}
