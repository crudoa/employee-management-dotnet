using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyperPrueba.Models
{
    [Table("Departamento")]
    public class Departamento
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(500)]
        [Column("NombreDepartamento")]
        [Display(Name = "Departamento")]
        public string? NombreDepartamento { get; set; }

        
        public virtual ICollection<Provincia>? Provincias { get; set; }
    }
}
