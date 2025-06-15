using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace SaludTotalAPI.Models.Entidades
{
    [Table ("Profesional")]
    public class Profesional
    {
        [Key]
        [Column ("id_profesional")]
        public int Id_Profesional { get; set; }

        [Required]
        [Column ("nombre_prof")]
        public string Nombre_Profesional { get; set; }

        [Required]
        [Column ("apellido_prof")]
        public string Apellido_Profesional { get; set; }

        [Required]
        [Column("correo_prof")]
        public string Correo_Profesional { get; set; }

        [Required]
        [Column ("nro_telf_prof")]
        public long? Telefono_Profesional { get; set; }

    }
}
