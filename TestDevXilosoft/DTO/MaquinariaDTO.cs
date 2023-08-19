namespace TestDevXilosoft.DTO;

public class MaquinariaDTO
{
    public int Id { get; set; }
    public string Serie { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public bool Estado { get; set; }
    public string EstadoStr => Estado ? "Asignado" : "Disponible";
}
