using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyperPrueba.Models
{
    [Table("Distrito")]
    public class Distrito
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("IdProvincia")]
        public int? IdProvincia { get; set; }

        [StringLength(500)]
        [Column("NombreDistrito")]
        public string? NombreDistrito { get; set; }

        
        [ForeignKey("IdProvincia")]
        public virtual Provincia? Provincia { get; set; }
    }
}
