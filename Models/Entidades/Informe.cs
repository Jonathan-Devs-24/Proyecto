using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SaludTotalAPI.Models.Entidades
{
    [Table ("Informe")]
    public class Informe
    {
        [Key]
        [Column ("id_informe")]
        public int Id_Informe { get; set; }

        [Required]
        [Column ("id_turno")]
        public int Id_Turno { get; set; }

        [Required]
        [Column ("periodo")]
        public string Periodo { get; set; }

        [Required]
        [Column ("tipo_informe")]
        public string Tipo_Informe { get; set; }

        public Turno Turno { get; set; }

    }
}
