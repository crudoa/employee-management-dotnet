using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyperPrueba.Models
{
    [Table("Trabajadores")]
    public class Trabajador
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [StringLength(3)]
        [Column("TipoDocumento")]
        [Display(Name = "Tipo Doc.")]
        public string? TipoDocumento { get; set; }

        [StringLength(50)]
        [Column("NumeroDocumento")]
        [Display(Name = "Número Doc.")]
        public string? NumeroDocumento { get; set; }

        [StringLength(500)]
        [Column("Nombres")]
        public string? Nombres { get; set; }

        [StringLength(1)]
        [Column("Sexo")]
        public string? Sexo { get; set; }

        [Column("IdDepartamento")]
        [Display(Name = "Departamento")]
        public int? IdDepartamento { get; set; }

        [Column("IdProvincia")]
        [Display(Name = "Provincia")]
        public int? IdProvincia { get; set; }

        [Column("IdDistrito")]
        [Display(Name = "Distrito")]
        public int? IdDistrito { get; set; }

        // Propiedades de navegación
        [ForeignKey("IdDepartamento")]
        public virtual Departamento? Departamento { get; set; }

        [ForeignKey("IdProvincia")]
        public virtual Provincia? Provincia { get; set; }

        [ForeignKey("IdDistrito")]
        public virtual Distrito? Distrito { get; set; }
    }
}
