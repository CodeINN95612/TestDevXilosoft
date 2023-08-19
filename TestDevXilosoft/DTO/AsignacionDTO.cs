namespace TestDevXilosoft.DTO;

public class AsignacionDTO
{
    public int Id { get; set; }
    public int IdEmpleado { get; set; }
    public int IdMaquinaria { get; set; }
    public string Maquinaria { get; set; } = string.Empty;
    public string SerieMaquinaria { get; set; } = string.Empty;
    public bool Activo { get; set; }

}
