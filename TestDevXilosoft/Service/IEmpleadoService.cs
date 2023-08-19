using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public interface IEmpleadoService
{
    public Task<List<EmpleadoDTO>> GetAll();
    public Task<EmpleadoDTO> GetById(int id);
    public Task SaveOrUpdate(EmpleadoDTO empleado);
}
