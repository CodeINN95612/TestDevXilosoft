using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public interface IResumenService
{
    public Task<List<ResumenDTO>> Get();
}

public class ResumenService : IResumenService
{
    private TestDevXilosoftDatabase _db;

    public ResumenService(TestDevXilosoftDatabase db)
    {
        _db = db;
    }

    public Task<List<ResumenDTO>> Get()
    {
        return _db.Asignaciones
            .Where(a => a.Activo)
            .Select(a => new ResumenDTO
            {
                Empleado = a.Empleado.Nombre,
                Maquinaria = a.Maquinaria.Serie
            }).ToListAsync();
    }
}