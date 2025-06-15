using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table ("Usuario")]
    public class Usuario
    {
        [Key]
        [Column ("id_usuario")]
        public int Id_Usuario { get; set; }

        [Required]
        [Column ("nombre_usuario")]
        public string Nombre_Usuario { get; set; } = null!;

        [Required]
        [Column ("contrasenia")]
        public string Contrasenia { get; set; } = null!;
    }
}
