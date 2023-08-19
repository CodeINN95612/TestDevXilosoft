using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data;
using TestDevXilosoft.Data.Models;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public interface IAsignacionService
{
    public Task<List<AsignacionDTO>> GetAll();
    public Task<List<AsignacionDTO>> GetByIdEmpleado(int idEmpleado);
    public Task SaveOrUpdate(AsignacionDTO asignacion);
}

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
        return await _db.Asignaciones.Where(a => a.IdEmpleado == idEmpleado)
            .Select(a => new AsignacionDTO
            {
                Id = a.Id,
                IdEmpleado = a.IdEmpleado,
                IdMaquinaria = a.IdMaquinaria,
                Activo = a.Activo,
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
                Activo = asignacion.Activo,
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
