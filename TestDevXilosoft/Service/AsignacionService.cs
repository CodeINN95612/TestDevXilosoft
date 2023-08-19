using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public class AsignacionService : IAsignacionService
{
    private TestDevXilosoftDatabase _db;

    public AsignacionService(TestDevXilosoftDatabase db)
    {
        _db = db;
    }

    public async Task<List<AsignacionDTO>> GetAll()
    {
        return await _db.Asignaciones.Select(e => new AsignacionDTO
        {
            Id = e.Id,
            IdEmpleado = e.IdEmpleado,
            IdMaquinaria = e.IdMaquinaria,
            Activo = e.Activo,
        }).ToListAsync();
    }

    public async Task<List<AsignacionDTO>> GetByIdEmpleado(int idEmpleado)
    {
        return await _db.Maquinarias
            .Where(m => !m.Estado || (m.Asignacion != null && m.Asignacion.IdEmpleado == idEmpleado))
            .Select(m => new AsignacionDTO
            {
                IdEmpleado = m.Asignacion != null ? m.Asignacion.IdEmpleado : idEmpleado,
                IdMaquinaria = m.Id,
                Maquinaria = m.Descripcion,
                SerieMaquinaria = m.Serie,
                Activo = m.Estado
            }).ToListAsync();
    }

    public async Task SaveOrUpdate(AsignacionDTO asignacion)
    {
        var asignacionEncontrada = await _db.Asignaciones
            .Where(a => a.IdEmpleado == asignacion.IdEmpleado)
            .Where(a => a.IdMaquinaria == asignacion.IdMaquinaria)
            .FirstOrDefaultAsync();
        var maquinaria = _db.Maquinarias.First(m => m.Id == asignacion.IdMaquinaria && !m.Estado);
        if (asignacionEncontrada is null)
        {
            await _db.Asignaciones.AddAsync(new()
            {
                Id = asignacion.Id,
                IdEmpleado = asignacion.IdEmpleado,
                IdMaquinaria = maquinaria.Id,
                Maquinaria = maquinaria,
                Activo = true,
            });

            maquinaria.Estado = true; // Asignado
        }
        else
        {
            asignacionEncontrada.Maquinaria.Estado = false; //Disponible
            asignacionEncontrada.Maquinaria = maquinaria;
            asignacionEncontrada.IdMaquinaria = maquinaria.Id;
        }
        await _db.SaveChangesAsync();
    }
}
