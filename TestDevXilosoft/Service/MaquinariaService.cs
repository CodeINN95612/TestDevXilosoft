using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public class MaquinariaService : IMaquinariaService
{
    private TestDevXilosoftDatabase _db;

    public MaquinariaService(TestDevXilosoftDatabase db)
    {
        _db = db;
    }

    public async Task<List<MaquinariaDTO>> GetAll()
    {
        return await _db.Maquinarias.Select(m => new MaquinariaDTO
        {
            Id = m.Id,
            Descripcion = m.Descripcion,
            Estado = m.Estado,
            Serie = m.Serie,
        }).ToListAsync();
    }

    public async Task<MaquinariaDTO> GetById(int id)
    {
        var maquinariaEncontrada = await _db.Maquinarias.FirstAsync(m => m.Id == id);
        return new()
        {
            Id = maquinariaEncontrada.Id,
            Serie = maquinariaEncontrada.Serie,
            Descripcion = maquinariaEncontrada.Descripcion,
            Estado = maquinariaEncontrada.Estado,
        };
    }

    public async Task SaveOrUpdate(MaquinariaDTO maquinaria)
    {
        var maquinariaEncontrada = await _db.Maquinarias.FirstOrDefaultAsync(m => m.Id == maquinaria.Id);
        if (maquinariaEncontrada is null)
        {
            await _db.Maquinarias.AddAsync(new()
            {
                Id = maquinaria.Id,
                Serie = maquinaria.Serie,
                Descripcion = maquinaria.Descripcion,
                Estado = false
            });
        }
        else
        {
            maquinariaEncontrada.Descripcion = maquinaria.Descripcion;
            maquinariaEncontrada.Serie = maquinaria.Serie;
        }
        await _db.SaveChangesAsync();
    }
}