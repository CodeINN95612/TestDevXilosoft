using Microsoft.EntityFrameworkCore;
using TestDevXilosoft.Data.Models;

namespace TestDevXilosoft.Data;

public class TestDevXilosoftDatabase : DbContext
{
    public TestDevXilosoftDatabase(DbContextOptions<TestDevXilosoftDatabase> options) :
        base(options)
    {
        
    }

    public virtual DbSet<Empleado> Empleados { get; set; }
    public virtual DbSet<Maquinaria> Maquinarias { get; set; }
    public virtual DbSet<Asignacion> Asignaciones { get; set; }
}
