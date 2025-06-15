using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table ("Profesional_Especialidad")]
    public class Profesional_Especialidad
    {
        [Required]
        [ForeignKey("Profesional")]
        [Column ("id_profesional")]
        public int Id_Profesional { get; set; }

        [Required]
        [ForeignKey("Especialidad")]
        [Column ("id_especialidad")]
        public int Id_Especialidad { get; set; }

        public Profesional? Profesional { get; set; }

        public Especialidad? Especialidad { get; set; }


    }
}
