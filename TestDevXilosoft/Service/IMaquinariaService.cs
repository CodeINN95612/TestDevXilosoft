using TestDevXilosoft.Data.Models;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public interface IMaquinariaService
{
    public Task<List<MaquinariaDTO>> GetAll();
    public Task<MaquinariaDTO> GetById(int id);
    public Task SaveOrUpdate(MaquinariaDTO maquinaria);
}
