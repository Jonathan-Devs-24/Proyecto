using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace SaludTotalAPI.Models.Entidades
{
    [Table ("Especialidad")]
    public class Especialidad
    {
        [Key]
        [Column ("id_especialidad")]
        public int Id_Especialidad { get; set; }

        [Required]
        [Column("nombre_especialidad")]
        public string Nombre_Especialidad { get; set; }
    }
}
