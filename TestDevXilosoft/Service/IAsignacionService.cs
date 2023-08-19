using TestDevXilosoft.Data.Models;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public interface IAsignacionService
{
    public Task<List<AsignacionDTO>> GetAll();
    public Task<List<AsignacionDTO>> GetByIdEmpleado(int idEmpleado);
    public Task SaveOrUpdate(int idEmpleado, List<AsignacionDTO> asignaciones);
}
