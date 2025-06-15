using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table("Paciente")]
    public class Paciente
    {
        [Key]
        [Column("id_paciente")]
        public int Id { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = null!;

        [Required]
        [Column("apellido")]
        public string Apellido { get; set; } = null!;

        [Required]
        [Column("DNI")]
        public int DNI { get; set; }

        [Column("fecha_nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Required]
        [Column("correo")]
        public string Correo { get; set; } = null!;

        [Column("nro_telf_movil")]
        public long? Telefono { get; set; }
    }
}

