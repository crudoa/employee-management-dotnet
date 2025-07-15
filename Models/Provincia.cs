using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyperPrueba.Models
{
    [Table("Provincia")]
    public class Provincia
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(500)]
        [Column("NombreProvincia")]
        [Display(Name = "Provincia")]
        public string? NombreProvincia { get; set; }

        [Column("IdDepartamento")]
        [Display(Name = "Departamento")]
        public int? IdDepartamento { get; set; }

        
        [ForeignKey("IdDepartamento")]
        public virtual Departamento? Departamento { get; set; }

        
        public virtual ICollection<Distrito>? Distritos { get; set; }
    }
}
