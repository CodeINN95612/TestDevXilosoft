using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestDevXilosoft.Data.Models;

public class Asignacion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int IdEmpleado { get; set; }

    [Required]
    public int IdMaquinaria { get; set; }

    [Required]
    public bool Activo { get; set; }

    [ForeignKey(nameof(IdEmpleado))]
    public virtual Empleado Empleado { get; set; } = default!;

    [ForeignKey(nameof(IdMaquinaria))]
    public virtual Maquinaria Maquinaria { get; set; } = default!;
}
