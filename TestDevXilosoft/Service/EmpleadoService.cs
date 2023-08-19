using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data;
using TestDevXilosoft.Data.Models;
using TestDevXilosoft.DTO;

namespace TestDevXilosoft.Service;

public class EmpleadoService : IEmpleadoService
{
    private TestDevXilosoftDatabase _db;

    public EmpleadoService(TestDevXilosoftDatabase db)
    {
        _db = db;
    }

    public async Task<List<EmpleadoDTO>> GetAll()
    {
        return await _db.Empleados.Select(e => new EmpleadoDTO
        {
            Id = e.Id,
            Area = e.Area,
            Cargo = e.Cargo,
            Cedula = e.Cedula,
            Nombre = e.Nombre,
        }).ToListAsync();
    }

    public async Task<EmpleadoDTO> GetById(int id)
    {

        var empleadoEncontrado = await _db.Empleados.FirstAsync(e => e.Id == id);
        return new()
        {
            Id = empleadoEncontrado.Id,
            Nombre = empleadoEncontrado.Nombre,
            Area = empleadoEncontrado.Area,
            Cargo = empleadoEncontrado.Cargo,
            Cedula = empleadoEncontrado.Cedula
        };
    }

    public async Task SaveOrUpdate(EmpleadoDTO empleado)
    {
        var empleadoEncontrado = await _db.Empleados.FirstOrDefaultAsync(e => e.Id == empleado.Id || e.Cedula == empleado.Cedula);
        if (empleadoEncontrado is null)
        {
            Empleado nuevo = new()
            {
                Id = empleado.Id,
                Area = empleado.Area,
                Cargo = empleado.Cargo,
                Cedula = empleado.Cedula,
                Nombre = empleado.Nombre
            };
            _db.Empleados.Add(nuevo);
        }
        else
        {
            empleadoEncontrado.Nombre = empleado.Nombre;
            empleadoEncontrado.Area = empleado.Area;
            empleadoEncontrado.Cargo = empleado.Cargo;
        }
        await _db.SaveChangesAsync();
    }
}