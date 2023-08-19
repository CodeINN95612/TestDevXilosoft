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
            .Where(m => !m.Estado || ( (m.Estado && m.Asignacion != null) && m.Asignacion.IdEmpleado == idEmpleado))
            .Select(m => new AsignacionDTO
            {
                IdEmpleado = m.Asignacion != null ? m.Asignacion.IdEmpleado : idEmpleado,
                IdMaquinaria = m.Id,
                Maquinaria = m.Descripcion,
                SerieMaquinaria = m.Serie,
                Activo = m.Estado
            }).ToListAsync();
    }

    public async Task SaveOrUpdate(int idEmpleado, List<AsignacionDTO> asignaciones)
    {
        foreach (var asignacion in asignaciones)
        {
            var asignacionEncontrada = await _db.Asignaciones
                .Where(a => a.IdEmpleado == asignacion.IdEmpleado)
                .Where(a => a.IdMaquinaria == asignacion.IdMaquinaria)
                .FirstOrDefaultAsync();
            var maquinaria = _db.Maquinarias.First(m => m.Id == asignacion.IdMaquinaria);
            if (asignacionEncontrada is null)
            {
                await _db.Asignaciones.AddAsync(new()
                {
                    Id = asignacion.Id,
                    IdEmpleado = idEmpleado,
                    IdMaquinaria = maquinaria.Id,
                    Maquinaria = maquinaria,
                    Activo = asignacion.Activo,
                });

                maquinaria.Estado = asignacion.Activo; // Asignado
            }
            else
            {
                asignacionEncontrada.Maquinaria.Estado = false; //Disponible
                maquinaria.Estado = asignacion.Activo; // Asignado
                asignacionEncontrada.Maquinaria = maquinaria;
                asignacionEncontrada.IdMaquinaria = maquinaria.Id;
                asignacionEncontrada.Activo = asignacion.Activo;
                asignacionEncontrada.IdEmpleado = idEmpleado;
            }
        }
        await _db.SaveChangesAsync();
    }
}
